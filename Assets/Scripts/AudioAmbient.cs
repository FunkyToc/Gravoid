using System.Collections;
using UnityEngine;

public class AudioAmbient : MonoBehaviour
{
    [SerializeField] private AudioSource _bgWhoosh;
    [SerializeField] private AudioClip[] _clipWhoosh;
    [Range(0f, 10f)]
    [SerializeField] private float _delayWhoosh = 1.5f;
    private float _timerWhoosh = 0;

    [SerializeField] private AudioSource _bgSound;
    [Range(0f, 1f)]
    [SerializeField] private float _fadeToVolume = 1.0f;
    [Range(0f, 1f)]
    [SerializeField] private float _fadePerSecond = 0.1f;

    void Start()
    {
        _bgSound.volume = 0f;
        _bgSound.Play();
        StartCoroutine(fadeInBgSound(_bgSound, _fadeToVolume, _fadePerSecond));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (Time.time > (_timerWhoosh + _delayWhoosh + Random.Range(-1f, 1f)) && col.gameObject.layer == 6)
        {
            AudioClip rngClip = _clipWhoosh[Random.Range(0, _clipWhoosh.Length - 1)];
            _bgWhoosh.PlayOneShot(rngClip);
            _timerWhoosh = Time.time;
        }
    }

    IEnumerator fadeInBgSound(AudioSource source, float fadeToVolume, float fadePerSeconds)
    {
        while (source.volume < fadeToVolume)
        {
            source.volume += fadePerSeconds * Time.deltaTime;
            source.volume = source.volume > fadeToVolume ? fadeToVolume : source.volume;

            yield return null;
        }
    }
}