using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuCanvasManager : MonoBehaviour
{
    public static MainMenuCanvasManager instance { get; private set; }
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(() => { Play(); });
        exitButton.onClick.RemoveAllListeners();
        exitButton.onClick.AddListener(() => { Exit(); });
    }

    private void Exit()
    {
        Application.Quit();
    }

    private void Play()
    {
        SceneManager.LoadScene(1);
    }
}
