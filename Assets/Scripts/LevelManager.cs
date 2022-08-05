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
    private int[] _playSceneMinMax = {3, 17};

    Coroutine _currentLoading;

    void Update()
    {
        _currentSceneId = SceneManager.GetActiveScene().buildIndex;

        // auto NEXT level
        
        /*int i = 0;
        FileInfo[] fis = d.GetFiles();
        foreach (FileInfo fi in fis)
        {
            if (fi.Extension.Contains("mp3"))
            i++;
        }*/

        // Win logic for playing levels
        if (_currentSceneId >= _playSceneMinMax[0] && _currentSceneId <= _playSceneMinMax[1])
        {
            _musicBoxes = GameObject.FindGameObjectsWithTag("MusicBox");

            if (wonLevel() && _currentLoading == null)
            {
                if (_playSceneMinMax[1] == _currentSceneId)
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
