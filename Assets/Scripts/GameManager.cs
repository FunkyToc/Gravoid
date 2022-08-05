using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _singleton;

    private void Awake()
    {
        if(_singleton != null)
        {
            Destroy(gameObject);
            return;
        }
        _singleton = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 144;
    }
}
