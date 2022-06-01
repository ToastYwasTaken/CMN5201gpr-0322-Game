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
    private AudioMixer _audioMixer;
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
    void Start()
    {
       _audioMixer = FindObjectOfType<AudioMixer>();
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
    ///Fades out old melody, fades in new melody
    ///unfortunately can't do simultaneously
    /// </summary>
    /// <param name="newMelodyClip"></param>
    public void ChangeMelody(AudioClip newMelodyClip)
    {
        float originalVolume = _musicSource.volume;
        float fadedVolume = originalVolume / 8;
        StartCoroutine(Fade(2, fadedVolume));
        _musicSource.Stop();
        _musicSource.clip = newMelodyClip;
        _musicSource.volume = fadedVolume;
        _musicSource.Play();
        StartCoroutine(Fade(2, originalVolume));
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

    private IEnumerator Fade(float duration, float targetVolume)
    {
        float currentTime = 0;
        float startVolume = _musicSource.volume;
        while(currentTime < duration)
        {
            currentTime += Time.deltaTime;
            Debug.Log("Volume: " + _musicSource.volume);
            _musicSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
        yield return null;
        }
        yield break;
    }
   


}
