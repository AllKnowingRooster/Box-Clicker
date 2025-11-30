using UnityEngine;

[System.Serializable]
public class AudioData
{
    public float volume;
    public AudioClip clip;
    public float cooldown;
    [HideInInspector] public float lastPlayed;

    public AudioData(float volume, AudioClip clip, float cooldown)
    {
        this.volume = volume;
        this.clip = clip;
        this.cooldown = cooldown;
        this.lastPlayed = 0.0f;
    }

    public void UpdateLastPlayed()
    {
        lastPlayed = Time.time;
    }

}
