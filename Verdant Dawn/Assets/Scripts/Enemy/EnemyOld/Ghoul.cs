using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class Ghoul : RecycleObject
{
    /// <summary>
    /// 공격 쿨타임
    /// </summary>
    public float attackCoolTime = 3.0f;

    /// <summary>
    /// 순찰중 목표지점에 도달했을 때 머무는 시간
    /// </summary>
    public float patrolBreakTime = 5.0f;

    /// <summary>
    /// 순찰할 때의 속도
    /// </summary>
    public float walkSpeed = 2.0f;

    /// <summary>
    /// 추적할 때의 속도
    /// </summary>
    public float runSpeed = 5.0f;

    /// <summary>
    /// 현재상태를 공격상태로 변경하는 거리(제곱)
    /// </summary>
    public float attackDistance = 4.0f;

    /// <summary>
    /// 플레이어를 감지하는 거리(제곱)
    /// </summary>
    public float detectDistance = 25.0f;

    /// <summary>
    /// 순찰지역으로 다시 돌아가는 거리(제곱)
    /// </summary>
    public float returnDistance = 400.0f;

    /// <summary>
    /// Dissolve 진행 시간(사망 연출 시간)
    /// </summary>
    public float dissolveDuration = 1.0f;

    /// <summary>
    /// 플레이어와의 거리 계산 속도
    /// </summary>
    float calculationTime = 0.2f;

    /// <summary>
    /// patrolBreakTime중 플레이어를 감지하면 상태를 전환하기 위해 추가하는 변수
    /// </summary>
    float patrolElaspedTime = 0.0f;

    /// <summary>
    /// 순찰 위치 저장용
    /// </summary>
    int index = 0;

    /// <summary>
    /// 구울의 생존여부(true면 죽음, false면 살아있음)
    /// </summary>
    bool isDead = false;

    /// <summary>
    /// 적의 현재 상태
    /// </summary>
    EnemyState currentState = EnemyState.Idle;

    /// <summary>
    /// 구울 순찰 위치 저장용
    /// </summary>
    Vector3[] patrolPosition;

    /// <summary>
    /// 목표 지점
    /// </summary>
    Vector3 target;

    /// <summary>
    /// 플레이어
    /// </summary>
    Player player;

    // 컴포넌트들
    Material[] materials;
    NavMeshAgent agent;
    Animator animator;
    ParticleSystem particle;


    // 해쉬 값들
    readonly int Move_Hash = Animator.StringToHash("Move");
    readonly int Chase_Hash = Animator.StringToHash("Chase");
    readonly int Attack_Hash = Animator.StringToHash("Attack");
    readonly int Death_Hash = Animator.StringToHash("Death");

    // 쉐이터 프로퍼티용 ID들
    readonly int Fade_ID = Shader.PropertyToID("_Fade");

    private void Awake()
    {
        Transform child = transform.GetChild(4);
        particle = child.GetComponent<ParticleSystem>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        patrolPosition = new Vector3[2];
        player = GameManager.Instance.Player;

        // 각각의 material 직접 찾기(GhoulDissolve)
        SkinnedMeshRenderer[] skinnedMeshRenderers = new SkinnedMeshRenderer[3];
        skinnedMeshRenderers[0] = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderers[1] = transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderers[2] = transform.GetChild(3).GetComponent<SkinnedMeshRenderer>();
        materials = new Material[3];
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i] = skinnedMeshRenderers[i].material;
        }
    }

    /// <summary>
    /// RecycleObject용 초기화 함수
    /// </summary>
    protected override void OnReset()
    {
        // 머티리얼 초기상태로 돌려놓기
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetFloat(Fade_ID, 1);
        }

        // 순찰 장소 저장
        Vector3 patrolPos1 = RandomPatrolPlace();
        Vector3 patrolPos2 = RandomPatrolPlace();
        while ((patrolPos1 - patrolPos2).sqrMagnitude < 0.1)
        {
            patrolPos2 = RandomPatrolPlace();
        }
        patrolPosition[0] = transform.position + (5 * patrolPos1);  
        patrolPosition[1] = transform.position + (5 * patrolPos2);

        // 저장한 장소로 목표 설정
        target = patrolPosition[index];

        // AI 시작
        StartCoroutine(CheckState());
        StartCoroutine(CheckStateForAction());
    }

    /// <summary>
    /// 상태를 업데이트 해주는 코루틴 함수
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckState()
    {
        while (!isDead)
        {
            float playerDistance = (player.transform.position - transform.position).sqrMagnitude;   // 플레이어와의 거리 계산
            float targetDistance = (target - transform.position).sqrMagnitude;                      // 목표와의 거리 계산

            yield return new WaitForSeconds(calculationTime);   // 프레임마다 계산하지않고 0.2초마다 계산

            if (targetDistance > returnDistance)                                            // 복귀가 1순위(순찰 장소와의 거리가 일정이상 떨어지면)
            {
                currentState = EnemyState.Return;   // 복귀
            }
            else if (currentState == EnemyState.Trace && playerDistance < attackDistance)  // 공격이 2순위(공격이 닿을 거리에 플레이어가 있으면)
            {
                currentState = EnemyState.Attack;   // 공격
            }
            else if (currentState != EnemyState.Return && playerDistance < detectDistance)  // 추적이 3순위(플레이어를 감지하면)
            {
                currentState = EnemyState.Trace;    // 추적
            }
            else if (currentState == EnemyState.Return && targetDistance < 0.05f)           // 순찰이 4순위(복귀를 마치면)
            {
                currentState = EnemyState.Idle;     // 순찰
            }
        }
    }

    /// <summary>
    /// 상태에 따른 행동을 실행하는 코루틴 함수
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckStateForAction()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 5.0f));  // 각각 다르게 순찰하기 위해서 랜덤으로 기다렸다가 출발하기
        while (!isDead)
        {
            switch (currentState)
            {
                case EnemyState.Idle:
                    // 걷는 애니메이션 켜기
                    animator.SetBool(Chase_Hash, false);
                    animator.SetBool(Move_Hash, true);

                    // 순찰 시작
                    agent.isStopped = false;
                    agent.SetDestination(target);

                    if (agent.remainingDistance < 0.2f)      // 목표 장소에 도착하면
                    {
                        patrolElaspedTime += Time.deltaTime;

                        // 걷는 애니메이션 끄기
                        animator.SetBool(Move_Hash, false);

                        if (patrolElaspedTime > patrolBreakTime)
                        {
                            // 다시 걷는 애니메이션 켜기
                            animator.SetBool(Move_Hash, true);
                            SetNextDestination();   // 다음 장소로 이동
                            patrolElaspedTime = 0.0f;
                        }
                    }
                    break;

                case EnemyState.Trace:
                    // 플레이어 추적
                    TracePlayer();
                    break;

                case EnemyState.Return:
                    // 순찰 지역으로 복귀
                    ReturnPatrol();
                    break;

                case EnemyState.Attack:
                    // 이동 중단
                    agent.isStopped = true;
                    animator.SetBool(Chase_Hash, false);

                    // 공격
                    animator.SetTrigger(Attack_Hash);

                    // 공격 쿨타임 주기
                    yield return new WaitForSeconds(attackCoolTime);
                    break;

                case EnemyState.Die:
                    Die();
                    break;
            }

            yield return null;
        }
    }

    /// <summary>
    /// 플레이어를 쫓는 함수
    /// </summary>
    void TracePlayer()
    {
        // 애니메이션 전환
        animator.SetBool(Move_Hash, false);
        animator.SetBool(Chase_Hash, true);

        // 플레이어를 뛰는 속도로 추적
        agent.speed = runSpeed;
        agent.isStopped = false;
        agent.SetDestination(player.transform.position);
    }

    /// <summary>
    /// 다음 장소로 목표 재설정하고 이동하는 함수
    /// </summary>
    void SetNextDestination()
    {   
        agent.speed = walkSpeed;    // 걷는 속도로 변경

        // 다음 지역으로 이동
        index = (index + 1) % 2;   
        agent.isStopped = false;
        agent.SetDestination(patrolPosition[index]);
        target = patrolPosition[index];
    }

    /// <summary>
    /// 순찰 지역으로 복귀하는 함수
    /// </summary>
    void ReturnPatrol()
    {
        // 빠르게 복귀
        agent.speed = 20.0f;            
        agent.SetDestination(target);
    }

    /// <summary>
    /// 이 구울이 죽을 때 실행되는 함수
    /// </summary>
    public void Die()
    {
        isDead = true;                      // 코루틴 종료
        agent.isStopped = true;             // 이동 중지
        animator.SetTrigger(Death_Hash);    // 죽는 애니메이션 실행
        particle.Stop();
        StartCoroutine(Dissolve());
    }

    /// <summary>
    /// 순찰 구역을 랜덤으로 설정하기 위한 함수
    /// </summary>
    /// <returns>순찰 위치</returns>
    Vector3 RandomPatrolPlace()
    {
        Vector3 patrolPos = Vector3.zero;

        // x값 뽑기
        int n = Random.Range(0, 3);
        if (n == 0)
        {
            patrolPos += Vector3.right;
        }
        else if (n == 2)
        {
            patrolPos += Vector3.left;
        }

        // z값 뽑기
        n = Random.Range(0, 2);
        if (n == 0)
        {
            patrolPos += Vector3.forward;
        }
        else if (n == 2)
        {
            patrolPos += Vector3.back;
        }

        return patrolPos;
    }

    /// <summary>
    /// Dissolve 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator Dissolve()
    {
        agent.isStopped = true;                             // 이동 중지
        animator.SetTrigger(Death_Hash);                    // 죽는 애니메이션 실행

        float fadeNormalize = 1 / dissolveDuration;         // 나누기 연산을 줄이기 위해 미리 계산
        float timeElapsed = 0.0f;                           // 시간 누적용

        yield return new WaitForSeconds(4.0f);              // 사망 후 2초만 기다리기

        while (timeElapsed < dissolveDuration)              // 시간 될때까지 반복
        {
            timeElapsed += Time.deltaTime;                  // 시간 누적

            for (int i = 0; i < materials.Length; i++)
            {
                materials[i].SetFloat(Fade_ID, 1 - (timeElapsed * fadeNormalize));  // fade값을 1 -> 0으로 점점 감소시키기
            }

            yield return null;
        }

        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetFloat(Fade_ID, 0);
        }

        particle.Stop();
        gameObject.SetActive(false);                // 재활용 하기 위해서 SetActive false만 하기 
    }
}
