using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] LevelManager _levelManager;
    [SerializeField] AudioMusic _music;
    [SerializeField] string _targetSceneName;

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            _levelManager.LoadLevel(_targetSceneName);
            _music.StartButton();
        }
    }
}
