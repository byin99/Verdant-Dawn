using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New QuestData", menuName = "Scriptable Object/Quest Data/Suppression", order = 0)]
public class QuestData_Suppression : QuestData
{
    [Header("퀘스트 목표")]
    public EnemyType enemyType = EnemyType.Ghoul;
    public int enemyCount = 40;
    public int currentEnemyCount = 0;
}
