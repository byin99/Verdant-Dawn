using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New QuestData", menuName = "Scriptable Object/Quest Data/Dungeon", order = 1)]
public class QuestData_Dungeon : QuestData
{
    [Header("퀘스트 목표")]
    public DungeonType dungeonType = DungeonType.DevilDungeon;
}
