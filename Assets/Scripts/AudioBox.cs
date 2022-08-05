using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBox : MonoBehaviour
{
    [Range(0f,1f)]
    [SerializeField] private float _fadeToVolume = 1.0f;
    [Range(0f,1f)]
    [SerializeField] private float _fadePerSecond = 0.1f;
    
    private AudioSource[] _musicBoxes;

    void Start()
    {
        _musicBoxes = GameObject.FindObjectsOfType<AudioSource>();
        foreach (AudioSource box in _musicBoxes)
        {
            if (box.gameObject.layer == 8)
            {
                box.Play();
                StartCoroutine(fadeInBgSound(box, _fadeToVolume, _fadePerSecond));
            }
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