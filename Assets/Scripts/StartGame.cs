using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] LevelManager _levelManager;
    [SerializeField] string _targetSceneName;

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            _levelManager.PressButton(_targetSceneName);
        }
    }

    public void LoadScene(string sceneName)
    {
        _levelManager.PressButton(sceneName);
    }
}
