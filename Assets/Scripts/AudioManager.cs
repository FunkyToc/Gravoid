using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer _masterMixer;
    [Range(0f,1f)]
    [SerializeField] float _defaultMaster;
    [Range(0f,1f)]
    [SerializeField] float _defaultMusic;
    [Range(0f,1f)]
    [SerializeField] float _defaultSfx;
    
    [SerializeField] private Scrollbar _masterScrollbar;
    [SerializeField] private Scrollbar _musicScrollbar;
    [SerializeField] private Scrollbar _sfxScrollbar;

    void Start()
    {
        SetMasterVolume(_defaultMaster);
        SetMusicVolume(_defaultMusic);
        SetSfxVolume(_defaultSfx);
    }

    private float NormalizeToVolume(float volume)
    {
        if (volume <= 0f) return -80;

        return -40 + (volume * 40);
    }

    private void SetMasterVolume(float volume)
    {
        _masterMixer.SetFloat("master", NormalizeToVolume(volume));
    }

    private void SetMusicVolume(float volume)
    {
        _masterMixer.SetFloat("music", NormalizeToVolume(volume));
    }

    private void SetSfxVolume(float volume)
    {
        _masterMixer.SetFloat("sfx", NormalizeToVolume(volume));
    }

    public void SetMasterScrollbar()
    {
        SetMasterVolume(_masterScrollbar.value);
    }

    public void SetMusicScrollbar()
    {
        SetMusicVolume(_musicScrollbar.value);
    }

    public void SetSfxScrollbar()
    {
        SetSfxVolume(_sfxScrollbar.value);
    }
}
