using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class QuestsManager : MonoBehaviour
{
    [Inject] private Player player;
    [SerializeField] private QuestsSO quests;

    public QuestsSO Quests => quests;

    public UnityAction<Quest, int> OnQuestUpdated;
    public UnityAction<Quest> OnQuestCompleted;

    private void Start() => StartCoroutine(Timer());

    private void OnEnable()
    {
        player.OnKill += v => OnQuestProgress(QuestType.Kill, v);
        player.OnLevelUp += v => OnQuestProgress(QuestType.LevelUp, v);
    }

    private void OnDisable()
    {
        player.OnLevelUp -= v => OnQuestProgress(QuestType.LevelUp, v);
        player.OnKill -= v => OnQuestProgress(QuestType.Kill, v);
    }

    private IEnumerator Timer()
    {
        foreach (var quest in quests.quests)
        {
            if (quest.type != QuestType.SurviveForSeconds)
                continue;

            var remainingTime = quest.times;

            while (remainingTime > 0)
            {
                OnQuestUpdated?.Invoke(quest, quest.times - remainingTime);

                yield return new WaitForSeconds(1f);
                remainingTime -= 1;
            }

            OnQuestCompleted?.Invoke(quest);
        }
    }

    private void OnQuestProgress(QuestType questType, int amount)
    {
        foreach (var quest in quests.quests)
        {
            if (quest.type != questType)
                continue;

            if (quest.times <= amount)
            {
                OnQuestCompleted?.Invoke(quest);
                return;
            }

            OnQuestUpdated?.Invoke(quest, amount);
        }
    }

}