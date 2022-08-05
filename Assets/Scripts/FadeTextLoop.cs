using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeTextLoop : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] float _fadeAmplitude = 1.0f;
    [Range(1f, 10f)]
    [SerializeField] float _fadeFrequency = 5.0f;

    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float alpha = 1 - (Mathf.Cos(Time.time * _fadeFrequency) * _fadeAmplitude);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
    }
}