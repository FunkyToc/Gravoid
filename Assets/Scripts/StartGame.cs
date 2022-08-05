using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] LevelManager _levelManager;
    [SerializeField] string _targetSceneName;
    
    private Image _button;
    [Range(0f,1f)]
    [SerializeField] float _fadeAmplitude = 1.0f;
    [Range(1f,10f)]
    [SerializeField] float _fadeFrequency = 5.0f;

    void Start()
    {
        _button = GetComponent<Image>();
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            _levelManager.PressButton(_targetSceneName);
        }

        // Fade loop animation
        float alpha = 1 - (Mathf.Cos(Time.time * _fadeFrequency) * _fadeAmplitude);
        _button.color = new Color(_button.color.r, _button.color.g, _button.color.b, alpha);
    }

    public void LoadScene(string sceneName)
    {
        _levelManager.PressButton(sceneName);
    }
}
