using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameCanvasManager : MonoBehaviour
{
    public static MainGameCanvasManager instance { get; private set; }

    [Header("Main")]
    [SerializeField] private CanvasGroup mainCanvasGroup;
    [SerializeField] private Button pauseButton;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject heartSpriteContainer;
    [SerializeField] private Color loseLiveColor;
    [SerializeField] private TextMeshProUGUI multiplierText;

    [Header("Pause")]
    [SerializeField] private CanvasGroup pauseCanvasGroup;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button pauseMainMenuButton;

    [Header("Result")]
    [SerializeField] private CanvasGroup resultCanvasGroup;
    [SerializeField] private TextMeshProUGUI resultScoreText;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button resultMainMenuButton;




    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        pauseButton.onClick.RemoveAllListeners();
        pauseButton.onClick.AddListener(() => { Pause(); });
        resumeButton.onClick.RemoveAllListeners();
        resumeButton.onClick.AddListener(() => { Pause(); });
        pauseMainMenuButton.onClick.RemoveAllListeners();
        pauseMainMenuButton.onClick.AddListener(() => { MainMenu(); });
        playAgainButton.onClick.RemoveAllListeners();
        playAgainButton.onClick.AddListener(() => { PlayAgain(); });
        resultMainMenuButton.onClick.RemoveAllListeners();
        resultMainMenuButton.onClick.AddListener(() => { MainMenu(); });
    }

    public void UpdateMultiplierText()
    {
        multiplierText.text = GameManager.instance.multiplier + "X";
    }

    private void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void Pause()
    {
        ToggleCanvasGroup(pauseCanvasGroup);
        ToggleCanvasGroup(mainCanvasGroup);
        Time.timeScale = Time.timeScale == 0.0f ? 1.0f : 0.0f;
    }

    public void Result()
    {
        ToggleCanvasGroup(resultCanvasGroup);
        ToggleCanvasGroup(mainCanvasGroup);
        Time.timeScale = 0;
    }

    private void ToggleCanvasGroup(CanvasGroup cg)
    {
        cg.blocksRaycasts = !cg.blocksRaycasts;
        cg.interactable = !cg.interactable;
        cg.alpha = cg.alpha == 0.0f ? 1.0f : 0.0f;
    }

    private void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdateScore(int score)
    {
        GameManager.instance.score += score * GameManager.instance.multiplier;
        resultScoreText.text = GameManager.instance.score.ToString().PadLeft(9, '0');
        scoreText.text = GameManager.instance.score.ToString().PadLeft(9, '0');
    }

    public void ChangeHeartSpriteColor(int index)
    {
        Transform selectedHeart = heartSpriteContainer.transform.GetChild(index);
        selectedHeart.GetComponent<Image>().color = loseLiveColor;
    }
}
