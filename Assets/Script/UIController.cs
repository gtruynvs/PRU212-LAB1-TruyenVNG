using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    [SerializeField] private Slider energySlider;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Slider scoreSlider;
    [SerializeField] private TMP_Text scoreText;


    public GameObject pausePanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateEnergySlider(float current, float max)
    {
        energySlider.maxValue = max;
        energySlider.value = Mathf.RoundToInt(current);
        int roundedEnergy = Mathf.RoundToInt(current);
        int roundedMax = Mathf.RoundToInt(max);
        energyText.text = $"{roundedEnergy}/{roundedMax}";
    }
    public void UpdateHealthSlider(float current, float max)
    {
        healthSlider.maxValue = max;
        healthSlider.value = Mathf.RoundToInt(current);
        int roundedHealth = Mathf.RoundToInt(current);
        int roundedMax = Mathf.RoundToInt(max);
        healthText.text = $"{roundedHealth}/{roundedMax}";
    }
    public void UpdateScoreText(int current, int max)
    {
        scoreSlider.maxValue = max;
        scoreSlider.value = current;

        scoreText.text = "SCORE: " + current + " / " + max;
    }



}
