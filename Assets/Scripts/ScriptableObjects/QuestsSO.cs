using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quests", menuName = "SO/Quests")]
public class QuestsSO : ScriptableObject
{
    public List<Quest> quests;
}

[System.Serializable]
public class Quest
{
    public QuestType type;
    public int times;
}

[System.Serializable]
public enum QuestType
{
    Kill,
    SurviveForSeconds,
    LevelUp
}