using UnityEngine.SceneManagement;

public class DungeonPortal : Portal
{
    /// <summary>
    /// 던전 씬의 이름
    /// </summary>
    public string dungeonSceneName;

    // 컴포넌트들
    PlayerPortal playerPortal;

    protected override void Awake()
    {
        base.Awake();
        playerPortal = GameManager.Instance.PlayerPortal;
    }

    protected void OnEnable()
    {
        playerPortal.onDungeonPortal += DungeonSceneLoad;
        playerPortal.offDungeonPortal += DungeonSceneUnload;
    }

    protected void OnDisable()
    {
        playerPortal.offDungeonPortal -= DungeonSceneUnload;
        playerPortal.onDungeonPortal -= DungeonSceneLoad;
    }

    /// <summary>
    /// 던전 씬을 로드하는 함수
    /// </summary>
    protected void DungeonSceneLoad(Portal _)
    {
        SceneManager.LoadSceneAsync(dungeonSceneName, LoadSceneMode.Additive);
    }

    /// <summary>
    /// 던전 씬을 언로드하는 함수
    /// </summary>
    protected void DungeonSceneUnload()
    {
        SceneManager.UnloadSceneAsync(dungeonSceneName);
    }
}
