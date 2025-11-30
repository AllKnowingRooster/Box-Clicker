using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SpawnMethod
{
    Random,
    Weighted
}

public enum UserAction
{
    Hover,
    Click,
    BoxClicked,
    SlowCLicked,
    DoubleScoreClicked
}
public class GameManager : MonoBehaviour, ISubject
{
    public static GameManager instance { get; private set; }
    private List<IObserver> listObserver;
    public bool isGamePaused;
    public int score;
    public int lives;
    public int multiplier;
    public Coroutine doubleScoreCoroutine;
    public Coroutine slowCoroutine;
    public float timer;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        timer = 0.0f;
        listObserver = new List<IObserver>();
        isGamePaused = false;
        Time.timeScale = 1.0f;
        score = 0;
        DontDestroyOnLoad(instance);
        Physics.gravity = new Vector3(0, -0.5f, 0);
        lives = 3;
        multiplier = 1;
        SceneManager.sceneLoaded += SceneLoadLogic;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneLoadLogic;
    }

    private void SceneLoadLogic(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1.0f;

        if (scene.buildIndex == 1)
        {
            score = 0;
            timer = 0.0f;
            MainGameCanvasManager.instance.UpdateScore(0);
            Physics.gravity = new Vector3(0, -1.5f, 0);
            lives = 3;
            multiplier = 1;
            isGamePaused = false;
            StartCoroutine(GameLoop());
        }
        else if (scene.buildIndex == 0)
        {
            Physics.gravity = new Vector3(0, -0.5f, 0);
        }
    }
    public IEnumerator GameLoop()
    {
        yield return StartCoroutine(StartGame());
        yield return StartCoroutine(Playing());
        yield return StartCoroutine(EndGame());

    }

    public IEnumerator StartGame()
    {
        StartCoroutine(SpawnManager.instance.SpawnSpawnableObject());
        yield return null;
    }

    public IEnumerator Playing()
    {
        while (lives > 0)
        {
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator EndGame()
    {
        StopCoroutine(SpawnManager.instance.SpawnSpawnableObject());
        MainGameCanvasManager.instance.Result();
        yield return null;
    }

    public void AddObserver(IObserver observer)
    {
        listObserver.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        listObserver.Remove(observer);
    }

    public void NotifyObserver(UserAction action)
    {
        for (int i = 0; i < listObserver.Count; i++)
        {
            listObserver[i].OnNotify(action);
        }
    }
}
