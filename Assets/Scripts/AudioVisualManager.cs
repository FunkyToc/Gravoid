using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioVisualManager : MonoBehaviour
{
    [Header("Display")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] Renderer[] _visualVolume;
    [SerializeField] Material _materialVolumeOn;
    [SerializeField] Material _materialVolumeOff;

    [Header("Vars")]
    [Range(0.0f, 1.0f)]
    [SerializeField] float _minVolume = 0.01f;
    [Range(0.0f, 1.0f)]
    [SerializeField] float _volumePerBullet = 0.01f;
    [Range(0.0f, 3.0f)]
    [SerializeField] float _volumeDownDelay = 0.5f;
    [Range(0.0f, 1.0f)]
    [SerializeField] float _volumeDownFrequency = 0.03f;
    [Range(0.0f, 1.0f)]
    [SerializeField] float _volumeDownAmount = 0.03f;

    private float _renderPerBar;
    private float _volDownDelay;
    private float _volDownFreq;

    void Start()
    {
        _renderPerBar = 1f / _visualVolume.Length;
        _volDownDelay = Time.time;
        _volDownFreq = Time.time;
    }

    void Update()
    {
        // update volume bars
        for (int i = 0; i < _visualVolume.Length; i++)
        {
            _visualVolume[i].material = (_audioSource.volume - _minVolume) > (_renderPerBar * i) ? _materialVolumeOn : _materialVolumeOff;
        }

        // up/down volume
        if (Time.time > (_volDownDelay + _volumeDownDelay))
        {
            if (Time.time > (_volDownFreq + _volumeDownFrequency))
            {
                _audioSource.volume -= _volumeDownAmount;
                _audioSource.volume = _audioSource.volume > _minVolume ? _audioSource.volume : _minVolume;
                _volDownFreq = Time.time;
            }
        }
    }

     void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 6)
        {
            _audioSource.volume += _volumePerBullet;
            _volDownDelay = Time.time;
        }
    }
}
