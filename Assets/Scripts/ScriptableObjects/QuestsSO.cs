using UnityEngine;

[CreateAssetMenu(fileName = "New Quests", menuName = "SO/Quests")]
public class QuestsSO : ScriptableObject
{
    public Quest[] quests;
}

[System.Serializable]
public struct Quest
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