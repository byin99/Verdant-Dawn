using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SubmapManager : MonoBehaviour, IInitializable
{
    /// <summary>
    /// 서브맵의 X축 개수
    /// </summary>
    const int XCount = 1;

    /// <summary>
    /// 서브맵의 Z축 개수
    /// </summary>
    const int ZCount = 5;

    /// <summary>
    /// 서브맵의 X축 길이
    /// </summary>
    const float submapXSize = 50.0f;

    /// <summary>
    /// 서브맵의 Z축 길이
    /// </summary>
    const float submapZSize = 50.0f;

    /// <summary>
    /// 월드(모든 서브맵의 합)의 원점(월드의 왼쪽 아래의 끝)
    /// </summary>
    readonly Vector3 worldOrigine = new Vector3(-submapXSize * XCount * 0.5f, 0, -submapZSize * ZCount * 0.5f);

    /// <summary>
    /// 씬 이름 조합용 기본 문자열
    /// </summary>
    const string SceneNameBase = "Seemless";

    /// <summary>
    /// 모든 씬의 이름을 저장해 놓은 배열
    /// </summary>
    string[] sceneNames;

    /// <summary>
    /// 씬의 로딩 상태를 저장하는 열거형
    /// </summary>
    enum SceneLoadState : byte
    {
        Unload = 0,     // 로딩 해제완료된 상태(로딩이 안되어 있는 상태)
        PendingUnload,  // 로딩 해제 진행 중인 상태
        PendingLoad,    // 로딩 진행 중인 상태
        Loaded          // 로딩 완료된 상태
    }

    /// <summary>
    /// 모든 씬의 로딩 진행 상태를 저장해 놓은 배열
    /// </summary>
    SceneLoadState[] sceneLoadStates;

    /// <summary>
    /// 로딩 요청이 들어온 씬의 목록
    /// </summary>
    List<int> loadWork = new List<int>();

    /// <summary>
    /// 로딩이 완료된 씬의 목록
    /// </summary>
    List<int> loadWorkComplete = new List<int>();

    /// <summary>
    /// 로딩 해제 요청이 들어온 씬의 목록
    /// </summary>
    List<int> unloadWork = new List<int>();

    /// <summary>
    /// 로딩 해제가 완료된 씬의 목록
    /// </summary>
    List<int> unloadWorkComplete = new List<int>();

    /// <summary>
    /// 모든 씬이 언로드 되었음을 확인해주는 프로퍼티(모든 씬이 Unload 상태면 true, 아니면 false)
    /// </summary>
    public bool IsUnloadAll
    {
        get
        {
            bool result = true;
            foreach (SceneLoadState state in sceneLoadStates)
            {
                if (state != SceneLoadState.Unload)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
    }

    /// <summary>
    /// 플레이어가 있는 서브맵
    /// </summary>
    Vector3Int playerSubmap = Vector3Int.zero;

    /// <summary>
    /// 처음 생성되었을 때 한번만 실행되는 함수
    /// </summary>
    public void PreInitialize()
    {
        int mapCount = ZCount * XCount;
        sceneNames = new string[mapCount];
        sceneLoadStates = new SceneLoadState[mapCount];

        for (int z = 0; z < ZCount; z++)
        {
            for (int x = 0; x < XCount; x++)
            {
                int index = GetIndex(x, z);
                sceneNames[index] = $"{SceneNameBase}_{x}_{z}";
                sceneLoadStates[index] = SceneLoadState.Unload;
            }
        }
    }

    /// <summary>
    /// 씬이 Single모드로 로드될 떄마다 호출될 초기화 함수(플레이어 필요)
    /// </summary>
    public void Initialize()
    {
        for (int i = 0; i < sceneLoadStates.Length; i++)
        {
            sceneLoadStates[i] = SceneLoadState.Unload;     // 맵 로드 상태 전부 unload로 초기화
        }

        // 플레이어 관련 초기화
        PlayerMovement player = GameManager.Instance.PlayerMovement;
        if (player != null)
        {
            // 플레이어 주변 맵 로딩 요청
            playerSubmap = WorldToGrid(player.transform.position);      // 플레이어 서브맵 그리드 위치 구하고
            RequestAsyncSceneLoad(playerSubmap.x, playerSubmap.y);      // 플레이어가 있는 서브맵을 최우선으로 요청
            RefreshScenes(playerSubmap.x, playerSubmap.y);              // 주변맵 포함해서 전부 요청

            // 플레이어가 이동 할 때의 처리
            player.onMove += (world) =>
            {
                Vector3Int grid = WorldToGrid(world);
                if (grid != playerSubmap)             // 이동 결과 그리드가 변경되었으면
                {
                    RefreshScenes(grid.x, grid.y);  // 씬 갱신
                    playerSubmap = grid;
                }
            };
        }
    }

    /// <summary>
    /// 특정 서브맵의 비동기 로딩을 요청하는 함수
    /// </summary>
    /// <param name="x">서브맵의 x위치</param>
    /// <param name="z">서브맵의 z위치</param>
    void RequestAsyncSceneLoad(int x, int z)
    {
        int index = GetIndex(x, z);
        if (sceneLoadStates[index] == SceneLoadState.Unload)    // 해당 서브맵이 Unload 상태일때만 작업 리스트에 추가
        {
            loadWork.Add(index);
        }
    }

    /// <summary>
    /// 특정 서브맵의 비동기 로딩해제를 요청하는 함수
    /// </summary>
    /// <param name="x">서브맵의 x위치</param>
    /// <param name="z">서브맵의 z위치</param>
    void RequestAsyncSceneUnload(int x, int z)
    {
        int index = GetIndex(x, z);
        if (sceneLoadStates[index] == SceneLoadState.Loaded)    // 해당 서브맵이 Loaded 상태일때만 작업 리스트에 추가
        {
            unloadWork.Add(index);
        }
    }

    /// <summary>
    /// 로딩 요청이 들어온 서브맵을 비동기 로딩하는 함수
    /// </summary>
    /// <param name="index">배열용 인덱스 값</param>
    void AsyncSceneLoad(int index)
    {
        if (sceneLoadStates[index] == SceneLoadState.Unload)        // 이미 로딩이 시작되었거나 완료된 것은 처리 안하기 위해
        {
            sceneLoadStates[index] = SceneLoadState.PendingLoad;    // 진행 중이라고 표시

            // 씬 로딩
            AsyncOperation async = SceneManager.LoadSceneAsync(sceneNames[index], LoadSceneMode.Additive);  // 비동기 로딩 시작
            async.completed += (_) =>   // 비동기 작업이 완료되면 실행되는 델리게이트에 람다 함수 추가
            {
                sceneLoadStates[index] = SceneLoadState.Loaded; // 완료되었으니 Loaded 상태로 변경
                loadWorkComplete.Add(index);                    // 완료 리스트에 인덱스 추가
            };
        }
    }

    /// <summary>
    /// 특정 서브맵의 비동기 로딩 해제를 요청하는 함수
    /// </summary>
    /// <param name="index">배열용 인덱스 값</param>
    void AsyncSceneUnload(int index)
    {
        if (sceneLoadStates[index] == SceneLoadState.Loaded)        // 이미 로딩해제가 시작되었거나 완료된 것은 처리 안하기 위해
        {
            sceneLoadStates[index] = SceneLoadState.PendingUnload;  // 진행 중이라고 표시

            // 해제할 씬에 있는 적을 풀로 되돌리기(씬 언로드로 삭제되는 것 방지)
            Scene scene = SceneManager.GetSceneByName(sceneNames[index]);
            if (scene.isLoaded)
            {
                GameObject[] sceneRoots = scene.GetRootGameObjects();   // 루트 오브젝트 모두 찾기(부모가 없는 오브젝트 모두 찾기)
                if (sceneRoots != null && sceneRoots.Length > 0)
                {
                    EnemyController[] enemies = sceneRoots[1].GetComponentsInChildren<EnemyController>();    // 루트 오브젝트의 자손으로 있는 모드 적 찾기
                    foreach (EnemyController enemy in enemies)
                    {
                        enemy.ReturnToPool();    // 적을 풀로 되돌리기
                        enemy.gameObject.SetActive(false); // 적 비활성화
                    }
                }
            }

            // 씬 로딩 해제
            AsyncOperation async = SceneManager.UnloadSceneAsync(sceneNames[index]);  // 비동기 로딩해제 시작
            async.completed += (_) =>   // 비동기 작업이 완료되면 실행되는 델리게이트에 람다 함수 추가
            {
                sceneLoadStates[index] = SceneLoadState.Unload; // 완료되었으니 Unload 상태로 변경
                unloadWorkComplete.Add(index);                  // 완료 리스트에 인덱스 추가
            };
        }
    }

    private void Update()
    {
        // 완료된 작업은 작업리스트에서 제거
        foreach (int index in loadWorkComplete)
        {
            loadWork.RemoveAll((x) => x == index);  // loadWork에서 값이 index인 항목은 모두 제거
        }
        loadWorkComplete.Clear();       // 다 제거했으니 리스트 비우기

        // 들어온 요청 처리
        foreach (int index in loadWork)
        {
            AsyncSceneLoad(index);      // 비동기 로딩 시작x
        }

        // 완료된 작업은 작업리스트에서 제거
        foreach (int index in unloadWorkComplete)
        {
            unloadWork.RemoveAll((x) => x == index);  // unloadWork에서 값이 index인 항목은 모두 제거
        }
        unloadWorkComplete.Clear();     // 다 제거했으니 리스트 비우기

        // 들어온 요청 처리
        foreach (int index in unloadWork)
        {
            AsyncSceneUnload(index);    // 비동기 로딩 해제 시작
        }
    }

    /// <summary>
    /// 지정된 위치 주변 맵은 로딩 요청하고, 그 외의 맵은 로딩해제를 요청하는 함수
    /// </summary>
    /// <param name="subX">서브맵의 X위치</param>
    /// <param name="subZ">서브맵의 Z위치</param>
    void RefreshScenes(int subX, int subZ)
    {
        // (0, 0) ~ (XCount, ZCount) 사이만 범위로 설정
        int startX = Mathf.Max(0, subX - 1);
        int endX = Mathf.Min(XCount, subX + 2);
        int startZ = Mathf.Max(0, subZ - 1);
        int endZ = Mathf.Min(ZCount, subZ + 2);

        List<Vector3Int> opens = new List<Vector3Int>(5);
        for (int z = startZ; z < endZ; z++)
        {
            for (int x = startX; x < endX; x++)
            {
                RequestAsyncSceneLoad(x, z);        // start ~ end 안에 있는 것은 모두 로딩 요청
                opens.Add(new Vector3Int(x, z));
            }
        }

        for (int z = 0; z < ZCount; z++)
        {
            for (int x = 0; x < XCount; x++)
            {
                if (!opens.Contains(new Vector3Int(x, z)))  // 로딩 요청한 것이 아니면 모두 로딩 해제
                {
                    RequestAsyncSceneUnload(x, z);
                }
            }
        }
    }

    /// <summary>
    /// 맵의 그리드 좌표를 인덱스로 변경해주는 함수
    /// </summary>
    /// <param name="x">x 좌표</param>
    /// <param name="z">z 좌표</param>
    /// <returns>배열용 인덱스 값</returns>
    int GetIndex(int x, int z)
    {
        return x + z * XCount;
    }

    /// <summary>
    /// 월드 좌표가 어떤 서브맵에 속하는지 계산하는 함수
    /// </summary>
    /// <param name="world">확인할 월드 좌표</param>
    public Vector3Int WorldToGrid(Vector3 world)
    {
        Vector3 offset = world - worldOrigine;
        return new Vector3Int((int)(offset.x / submapXSize), (int)(offset.z / submapZSize));
    }
}
