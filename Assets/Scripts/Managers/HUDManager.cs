using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class HUDManager : MonoBehaviour
{
    [Inject] private AudioManager audioManager;
    [Inject] private Joystick joystick;
    [Inject] private Player player;
    [Inject] private QuestsManager questsManager;

    [Header("HUD Elements")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider experienceSlider;
    [SerializeField] private Text killCountText;
    [SerializeField] private Text levelText;

    [Header("Quests")]
    [SerializeField] private Transform questContainer;
    [SerializeField] private Text questText;

    [Header("Death Screen")]
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private Text totalKillCountText;
    [SerializeField] private Text totalLevelText;
    [SerializeField] private Button restartButton;

    private Dictionary<Quest, Text> questTexts = new();
    private const string levelPrefix = "Lv.";
    private const string KillQuestTemplate = "Kill {0} enemies. {1}";
    private const string SurviveQuestTemplate = "Survive for {0} seconds. {1}";
    private const string LevelUpQuestTemplate = "Gain {0} levels. {1}";

    private void OnEnable()
    {
        foreach (var quest in questsManager.Quests.quests)
        {
            var text = Instantiate(questText, questContainer);
            questTexts.Add(quest, text);
            UpdateQuestProgress(quest, 0);
        }

        player.OnDied += DeathScreen;
        player.OnKill += v => SetText(killCountText, v.ToString());
        player.OnLevelUp += v => SetText(levelText, levelPrefix + v.ToString());
        player.OnHealthChanged += v => SetValue(healthSlider, v);
        player.OnExperienceChanged += v => SetValue(experienceSlider, v);

        questsManager.OnQuestUpdated += UpdateQuestProgress;
        questsManager.OnQuestCompleted += FinishQuest;

        restartButton.onClick.AddListener(Reload);
    }

    private void OnDisable()
    {
        restartButton.onClick.RemoveListener(Reload);

        questsManager.OnQuestCompleted -= FinishQuest;
        questsManager.OnQuestUpdated -= UpdateQuestProgress;

        player.OnExperienceChanged -= v => SetValue(experienceSlider, v);
        player.OnHealthChanged -= v => SetValue(healthSlider, v);
        player.OnLevelUp -= v => SetText(levelText, levelPrefix + v.ToString());
        player.OnKill -= v => SetText(killCountText, v.ToString());
        player.OnDied -= DeathScreen;

        foreach (var questText in questTexts.Values)
            Destroy(questText.gameObject);

        questTexts.Clear();
    }

    private void DeathScreen()
    {
        questsManager.OnQuestCompleted = null;
        questsManager.OnQuestUpdated = null;

        player.OnExperienceChanged = null;
        player.OnHealthChanged = null;
        player.OnLevelUp = null;
        player.OnKill = null;
        player.OnDied = null;

        audioManager.StopBGM();
        audioManager.Play("Lose");

        Time.timeScale = 0;
        
        joystick.gameObject.SetActive(false);
        healthSlider.gameObject.SetActive(false);
        experienceSlider.gameObject.SetActive(false);
        killCountText.gameObject.SetActive(false);
        levelText.gameObject.SetActive(false);

        totalKillCountText.text = killCountText.text;
        totalLevelText.text = levelText.text;

        deathScreen.SetActive(true);
    }
    
    private void Reload()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateQuestProgress(Quest quest, int amount)
    {
        if (questTexts == null || !questTexts.TryGetValue(quest, out var questText))
            return;

        questText.text = string.Format(GetTemplate(quest.type), quest.times, $"{amount}/{quest.times}");
    }

    private void FinishQuest(Quest quest)
    {
        if (questTexts == null || !questTexts.TryGetValue(quest, out var questText))
            return;
        
        questText.text = string.Format(GetTemplate(quest.type), quest.times, "Done");
    }

    private string GetTemplate(QuestType type) => type switch
    {
        QuestType.Kill => KillQuestTemplate,
        QuestType.SurviveForSeconds => SurviveQuestTemplate,
        QuestType.LevelUp => LevelUpQuestTemplate,
        _ => null
    };

    private void SetText(Text text, string value) => text.text = value;
    private void SetValue(Slider slider, float value) => slider.value = value;

}