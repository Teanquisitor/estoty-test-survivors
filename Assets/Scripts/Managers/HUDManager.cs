using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class HUDManager : MonoBehaviour
{
    [Inject] private AudioManager audioManager;
    [Inject] private Joystick joystick;

    [Header("HUD Elements")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider experienceSlider;
    [SerializeField] private Text killCountText;
    [SerializeField] private Text levelText;

    [Header("Death Screen")]
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private Text totalKillCountText;
    [SerializeField] private Text totalLevelText;
    [SerializeField] private Button restartButton;

    private string levelPrefix = "Lv.";

    private void Awake()
    {
        Player.OnDied = null;
        Player.OnKill = null;
        Player.OnLevelUp = null;
        Player.OnHealthChanged = null;
        Player.OnExperienceChanged = null;
    }

    private void OnEnable()
    {
        Player.OnDied += DeathScreen;
        Player.OnKill += v => SetText(killCountText, v.ToString());
        Player.OnLevelUp += v => SetText(levelText, levelPrefix + v.ToString());
        Player.OnHealthChanged += v => SetValue(healthSlider, v);
        Player.OnExperienceChanged += v => SetValue(experienceSlider, v);

        restartButton.onClick.AddListener(Reload);
    }

    private void OnDisable()
    {
        restartButton.onClick.RemoveListener(Reload);

        Player.OnExperienceChanged -= v => SetValue(experienceSlider, v);
        Player.OnHealthChanged -= v => SetValue(healthSlider, v);
        Player.OnLevelUp -= v => SetText(levelText, levelPrefix + v.ToString());
        Player.OnKill -= v => SetText(killCountText, v.ToString());
        Player.OnDied -= DeathScreen;
    }

    private void DeathScreen()
    {
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

    private void SetText(Text text, string value) => text.text = value;
    private void SetValue(Slider slider, float value) => slider.value = value;

}