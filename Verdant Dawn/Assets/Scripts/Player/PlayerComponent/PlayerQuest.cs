using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerQuest : MonoBehaviour, IQuestReceiver
{
    /// <summary>
    /// 퀘스트를 바꾸는 델리게이트
    /// </summary>
    public event Action<QuestData> onChangeQuest;

    /// <summary>
    /// 퀘스트를 클리어하면 실행되는 델리게이트
    /// </summary>
    public event Action onQuestClear;

    /// <summary>
    /// 적을 죽였을 때, 실행되는 델리게이트
    /// </summary>
    public Action<EnemyType> onEnemyKill;

    /// <summary>
    /// 퀘스트를 변동시키면 실행되는 델리게이트
    /// </summary>
    public Action onRefreshQuest;

    /// <summary>
    /// 현재 퀘스트
    /// </summary>
    QuestData currentQuest;

    /// <summary>
    /// 아이템을 줏을 수 있는 거리
    /// </summary>
    [SerializeField]
    float interactionRange = 3.0f;

    /// <summary>
    /// currentQuest를 반환하는 프로퍼티(읽기 전용)
    /// </summary>
    public QuestData CurrentQuest => currentQuest;

    // 컴포넌트들
    Player player;
    NPC npc;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Start()
    {
        onEnemyKill += EnemyKill;
    }

    /// <summary>
    /// 퀘스트를 받는 함수
    /// </summary>
    /// <param name="quest">받을 퀘스트</param>
    public void ReceiveQuest(QuestData quest)
    {
        currentQuest = quest;
        onChangeQuest?.Invoke(quest);
    }

    /// <summary>
    /// 퀘스트 보상 받는 함수
    /// </summary>
    /// <param name="exp">보상 exp</param>
    /// <param name="itemCode">보상 아이템 종류</param>
    /// <param name="itemCount">보상 아이템 개수</param>
    public void GetQuestReward(float exp, ItemCode itemCode, uint itemCount)
    {
        player.GetReward(exp, itemCode, itemCount);
        currentQuest = null;
        onQuestClear?.Invoke();
    }

    /// <summary>
    /// NPC와 상호 작용하는 함수
    /// </summary>
    public void InteractionToNPC()
    {
        // 거리내에 NPC들 찾기
        Collider[] npcColliders = Physics.OverlapSphere(transform.position, interactionRange, LayerMask.GetMask("NPC"));

        if (npcColliders.Length > 0)
        {
            // NPC중에서 가장 가까운 NPC찾기
            npcColliders = npcColliders.OrderBy(c => Vector3.Distance(transform.position, c.transform.position)).ToArray();
            npc = npcColliders[0].GetComponent<NPC>();

            if (npc != null)
            {
                npc.InteractionToPlayer();
            }
        }
    }

    /// <summary>
    /// 적을 죽였을 때, 실행하는 함수
    /// </summary>
    /// <param name="enemyType">적 종류</param>
    void EnemyKill(EnemyType enemyType)
    {
        if (currentQuest is QuestData_Suppression quest)
        {
            if (quest.enemyType == enemyType)
            {
                quest.currentEnemyCount++;
                quest.currentEnemyCount = Mathf.Clamp(quest.currentEnemyCount, 0, quest.enemyCount);
                onRefreshQuest.Invoke();
                if (quest.currentEnemyCount >= quest.enemyCount)
                {
                    npc.QuestCanCompleted();
                }
            }
        }
    }
}
