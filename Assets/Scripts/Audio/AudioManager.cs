using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using Dennis.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    PauseUI _pauseUI;
    [SerializeField]
    AudioSource _musicSource;
    [SerializeField]
    AudioSource _soundSource;
    [SerializeField]
    AudioClip _musicMainMenu;
    [SerializeField]
    AudioClip _musicLevel;
    [SerializeField]
    AudioClip _musicBossRoom;

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
    private void Update()
    {
        if (!_musicSource.isPlaying)
        {
            if (_pauseUI != null)
            {
                if (!_pauseUI.IsPaused)
                {
                    if (SceneManager.GetActiveScene().buildIndex == 1)
                    {
                        //in 'normal' game
                        if (true)
                        {
                            PlayMelody(_musicLevel);
                        }
                        //in boss room
                        //else if (true)
                        //{
                        //    PlayMelody(_musicBossRoom);
                        //}
                    }
                }
            }
            else
            {
                //In Main menu
                if (SceneManager.GetActiveScene().buildIndex == 0)
                {
                    PlayMelody(_musicMainMenu);
                }
            }
        }
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
