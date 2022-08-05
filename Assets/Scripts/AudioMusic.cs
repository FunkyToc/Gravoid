using System.Collections;
using UnityEngine;

public class AudioMusic : MonoBehaviour
{
    [SerializeField] private AudioSource _musicIntro;
    [SerializeField] private AudioSource _musicLoop;
    [SerializeField] private AudioSource _musicOutro;
    [Range(0f, 1f)]
    [SerializeField] private float _fadeToVolume = 1.0f;
    [Range(0f, 1f)]
    [SerializeField] private float _fadePerSecond = 0.1f;

    private float _playtime;
    private float _playtimeOverlap = 0.5f;

    void Start()
    {
        
    }

    public void StartButton()
    {
        _musicLoop.loop = true;

        if (!_musicLoop.isPlaying)
        {
            _playtime = Time.time + _musicIntro.clip.length;
            _musicIntro.volume = 0f;
            _musicIntro.Play();
            StartCoroutine(fadeInMusicIntro(_musicIntro, _fadeToVolume, _fadePerSecond));
            _musicLoop.PlayDelayed(_musicIntro.clip.length);
        }
    }

    public void EndGame()
    {
        _musicLoop.loop = false;
        _musicOutro.PlayDelayed((_musicLoop.clip.length - _playtimeOverlap) - ((Time.time - _playtime) % _musicLoop.clip.length));
    }

    IEnumerator fadeInMusicIntro(AudioSource source, float fadeToVolume, float fadePerSeconds)
    {
        while (source.volume < fadeToVolume)
        {
            source.volume += fadePerSeconds * Time.deltaTime;
            source.volume = source.volume > fadeToVolume ? fadeToVolume : source.volume;

            yield return null;
        }
    }
}