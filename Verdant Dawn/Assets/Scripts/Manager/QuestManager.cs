using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    /// <summary>
    /// ItemData들
    /// </summary>
    public QuestData[] questDatas;

    /// <summary>
    /// 퀘스트 번호
    /// </summary>
    [HideInInspector]
    public uint currentQuestCount = 0;
}
