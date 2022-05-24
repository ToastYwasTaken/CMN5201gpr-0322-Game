using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioSource _musicSource;
    [SerializeField]
    AudioSource _soundSource;

    private static AudioManager s_instance;
    // Start is called before the first frame update
    void Start()
    {
        if (s_instance != null && s_instance != this)
        {
            Destroy(this);
        }
        else
        {
            s_instance = this;
        }
        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// Play game melody
    /// </summary>
    /// <param name="musicClip"></param>
    public void PlayMelody(AudioClip musicClip)
    {
        _musicSource.clip = musicClip;
        _musicSource.Play();
    }

    /// <summary>
    /// Play sounds
    /// </summary>
    /// <param name="soundClip"></param>
    public void PlaySound(AudioClip soundClip)
    {
        _soundSource.clip = soundClip;
        _soundSource.Play();
    }
}
