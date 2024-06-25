using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class QuestsManager : MonoBehaviour
{
    [Inject] private Player player;
    [SerializeField] private QuestsSO quests;

    public List<Quest> Quests => quests.quests;

    public UnityAction<Quest, int> OnQuestUpdated;
    public UnityAction<Quest> OnQuestCompleted;

    private void OnEnable()
    {
        StartCoroutine(Timer());
        
        player.OnKill += v => OnQuestProgress(QuestType.Kill, v);
        player.OnLevelUp += v => OnQuestProgress(QuestType.LevelUp, v);
    }

    private void OnDisable()
    {
        player.OnLevelUp -= v => OnQuestProgress(QuestType.LevelUp, v);
        player.OnKill -= v => OnQuestProgress(QuestType.Kill, v);

        StopAllCoroutines();
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
        if (quests == null || quests.quests == null) return;

        foreach (var quest in quests.quests)
        {
            if (quest.type != questType)
                continue;

            if (quest.times <= amount)
            {
                OnQuestCompleted?.Invoke(quest);
                continue;
            }

            OnQuestUpdated?.Invoke(quest, amount);
        }
    }

}