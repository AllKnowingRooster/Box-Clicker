using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IObserver
{
    public static AudioManager instance { get; private set; }
    [SerializeField] private List<AudioData> listSfx;
    [SerializeField] private AudioData bgm;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;
    private Dictionary<UserAction, AudioData> sfxMap;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        sfxMap = new Dictionary<UserAction, AudioData>();
        UserAction[] userActions = (UserAction[])Enum.GetValues(typeof(UserAction));
        for (int i = 0; i < userActions.Length; i++)
        {
            sfxMap[userActions[i]] = listSfx[i];
        }
        bgmSource.clip = bgm.clip;
        bgmSource.volume = bgm.volume;
        bgmSource.Play();
        DontDestroyOnLoad(instance);
    }

    private void OnEnable()
    {
        GameManager.instance.AddObserver(this);
    }

    private void OnDisable()
    {
        GameManager.instance.RemoveObserver(this);
    }

    public void OnNotify(UserAction action)
    {
        if (!sfxMap.ContainsKey(action))
        {
            return;
        }

        AudioData data = sfxMap[action];
        if (Time.time - data.cooldown >= data.lastPlayed)
        {
            sfxSource.PlayOneShot(data.clip, data.volume);
            data.UpdateLastPlayed();
        }
    }
}
