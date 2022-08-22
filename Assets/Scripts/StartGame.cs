using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    LevelManager _levelManager;
    [SerializeField] string _targetSceneName;

    void Start()
    {
        _levelManager = LevelManager.__LevelManager;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _levelManager.PressButton(_targetSceneName);
        }
    }

    public void LoadScene(string sceneName)
    {
        _levelManager.PressButton(sceneName);
    }
}
