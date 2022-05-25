using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioSource _musicSource;
    [SerializeField]
    AudioSource _soundSource;
    [SerializeField]
    public AudioClip MusicMainMenu;
    [SerializeField]
    public AudioClip MusicLevel;
    [SerializeField]
    public AudioClip MusicBossRoom;

    private static AudioManager s_instance;
    void Awake()
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

    public void PauseMelody()
    {
        _musicSource.Pause();
    }

    public void ContinueMelody()
    {
        _musicSource.UnPause();
    }

    public void ChangeMelody(AudioClip newMelodyClip)
    {
        _musicSource.Stop();
        _musicSource.clip = newMelodyClip;
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
