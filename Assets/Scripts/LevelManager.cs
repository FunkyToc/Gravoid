using System.Collections;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using System.IO;

public class LevelManager : MonoBehaviour
{
    [SerializeField] string _startScenePath;
    [SerializeField] string _firstPlaySceneName;
    [SerializeField] string _lastPlaySceneName;
    [SerializeField] string _endScenePath;

    private int _currentSceneId;
    private GameObject[] _musicBoxes;
    private int[] _playSceneMinMaxBuildIndex = {3, 99};

    private void Start()
    {
        // fade out black screen

        StartCoroutine(GetMusicBoxes());
    }


    void Update()
    {
        _currentSceneId = SceneManager.GetActiveScene().buildIndex;

        // Win logic for playing levels
        if (SceneManager.GetActiveScene().path.Contains("levels") && SceneManager.GetActiveScene().name.Contains("lvl"))
        {
            // check score
            if (_musicBoxes != null && wonLevel())
            {
                // reset for next level
                _musicBoxes = null;

                // last scene
                if (SceneManager.GetActiveScene().name == _lastPlaySceneName)
                {
                    LoadLevel(_endScenePath);
                    return;
                }

                // next level
                LoadLevel(SceneUtility.GetScenePathByBuildIndex(_currentSceneId + 1));
                return;
            }
        }
    }

    IEnumerator GetMusicBoxes()
    {
        // update slowly musicbox 
        while (true)
        {
            _musicBoxes = GameObject.FindGameObjectsWithTag("MusicBox");
            yield return new WaitForSeconds(1f);
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
        // transition
        

        // load
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
        else if (levelname == _startScenePath)
        {
            PressRetryButton();
            return;
        }

        LoadLevel(levelname);
    }

    private void PressStartButton()
    {
        GameManager.Singleton.GetComponentInChildren<AudioMusic>().StartButton();
        LoadLevel(_firstPlaySceneName);
    }
    
    private void PressRetryButton()
    {
        GameManager.Singleton.GetComponentInChildren<AudioMusic>().StartButton();
        LoadLevel(_startScenePath);
    }
}
