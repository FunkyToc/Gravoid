using System.Collections;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class LevelManager : MonoBehaviour
{
    public static LevelManager __LevelManager { get; private set; }

    [Header("Scene Target")]
    [Tooltip("Start Scene path")]
    [SerializeField] string _startScenePath;
    [Tooltip("First level, when press SPACE on Start or Retry")]
    [SerializeField] string _firstPlaySceneName;
    [Tooltip("Last level that will load the end menu when completed")]
    [SerializeField] string _lastPlaySceneName;
    [Tooltip("Last Scene path")]
    [SerializeField] string _endScenePath;

    [Header("Scene Transition")]
    [SerializeField] Image _bgFade;
    [Range(0f,3f)]
    [SerializeField] float _fadeSpeed;

    private Coroutine _ccoroutine;
    private int _currentSceneId;
    private GameObject[] _musicBoxes;
    private int[] _playSceneMinMaxBuildIndex = {3, 99};

    private void Start()
    {
        if (__LevelManager != null)
        {
            Destroy(gameObject);
        }
        __LevelManager = this;

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
        if (_ccoroutine == null)
        {
            _ccoroutine = StartCoroutine(LoadLevelTransition(levelname));
        }
    }

    IEnumerator LoadLevelTransition(string levelname)
    {
        float alpha;

        // fade in
        while (_bgFade.color.a < 1)
        {
            alpha = _bgFade.color.a + (_fadeSpeed * Time.deltaTime);
            _bgFade.color = new Color(_bgFade.color.r, _bgFade.color.g, _bgFade.color.b, alpha);
            yield return null;
        }

        // load
        SceneManager.LoadScene(levelname);
        yield return new WaitForSeconds(0.2f);

        // fade out
        while (_bgFade.color.a > 0)
        {
            alpha = _bgFade.color.a - (_fadeSpeed * Time.deltaTime);
            _bgFade.color = new Color(_bgFade.color.r, _bgFade.color.g, _bgFade.color.b, alpha);
            yield return null;
        }

        _ccoroutine = null;
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
