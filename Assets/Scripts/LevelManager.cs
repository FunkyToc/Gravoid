using System.Collections;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using System.IO;

public class LevelManager : MonoBehaviour
{
    [SerializeField] string _startSceneName;
    [SerializeField] string _firstPlaySceneName;
    [SerializeField] string _endSceneName;

    private int _currentSceneId;
    private GameObject[] _musicBoxes;
    private int[] _playSceneMinMaxBuildIndex = {3, 99};

    Coroutine _currentLoading;

    private void Start()
    {
        // count scene
        _playSceneMinMaxBuildIndex[1] = 0;
        FileInfo[] fis = new DirectoryInfo("Assets/Scenes/levels/").GetFiles();
        foreach (FileInfo fi in fis)
        {
            if (fi.Extension.Contains("unity"))
                _playSceneMinMaxBuildIndex[1]++;
        }
        _playSceneMinMaxBuildIndex[1] += _playSceneMinMaxBuildIndex[0] - 1;
        Debug.Log(_playSceneMinMaxBuildIndex[1]);
    }

    void Update()
    {
        _currentSceneId = SceneManager.GetActiveScene().buildIndex;

        // Win logic for playing levels
        if (_currentSceneId >= _playSceneMinMaxBuildIndex[0] && _currentSceneId <= _playSceneMinMaxBuildIndex[1])
        {
            _musicBoxes = GameObject.FindGameObjectsWithTag("MusicBox");

            if (wonLevel() && _currentLoading == null)
            {
                if (_playSceneMinMaxBuildIndex[1] == _currentSceneId)
                {
                    LoadLevel(_endSceneName);
                    return;
                }

                SceneManager.LoadScene(_currentSceneId + 1);
            }
        }
    }

    private bool wonLevel()
    {
        bool won = true;
        if (_musicBoxes.Length > 0)
        {
            foreach(GameObject box in _musicBoxes)
            {
                float volume = box.GetComponentInChildren<AudioSource>().volume;
                won = (volume != 1f) ? false : won;
                if (won == false) break;
            }

            return won;
        }

        return false;
    }

    private void LoadLevel(string levelname)
    {
        SceneManager.LoadScene(levelname);
    }
    
    public void PressButton(string levelname = null)
    {
        levelname = levelname ?? _firstPlaySceneName;

        if (levelname == _firstPlaySceneName)
        {
            PressStartButton();
            return;
        }
        else if (levelname == _startSceneName)
        {
            PressRetryButton();
            return;
        }

        SceneManager.LoadScene(levelname);
    }

    private void PressStartButton()
    {
        GameManager.Singleton.GetComponentInChildren<AudioMusic>().StartButton();
        LoadLevel(_firstPlaySceneName);
    }
    
    private void PressRetryButton()
    {
        GameManager.Singleton.GetComponentInChildren<AudioMusic>().StartButton();
        LoadLevel(_startSceneName);
    }
}
