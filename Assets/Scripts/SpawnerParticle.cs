using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class SpawnerParticle : MonoBehaviour
{
    [Header("Spawner")]
    [SerializeField] GameObject _particle;
    [Range(0.001f,1f)]
    [SerializeField] float _spawnDelay = 0.01f;
    [Range(0.01f,10f)]
    [SerializeField] float _spawnRadius = 1.0f;

    [Header("Particules")]
    [Range(1f,50f)]
    [SerializeField] float _bulletSpeed = 10.0f;
    [Range(1f,50f)]
    [SerializeField] float _bulletLifetime = 6.0f;
    [Range(0f,10f)]
    [SerializeField] float _slowingSpeedDelay = 0.6f;
    [Range(0f,10f)]
    [SerializeField] float _slowingSpeedAmount = 1.0f;
    
    private float _timer;

    void Start()
    {
        _timer = Time.time;

        // inactive arrow sprite
        Transform texture = transform.Find("vectorTexture");
        if (texture)
        {
            texture.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Time.time > (_timer + _spawnDelay))
        {
            // instanciate
            Vector2 spread = transform.position + (Random.insideUnitSphere * _spawnRadius * Utils.GaussianRandom(0f, 0.5f));
            GameObject obj = GameObject.Instantiate(_particle, spread, transform.rotation);

            // speed
            if (obj.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            {
                rb.velocity = obj.transform.right * _bulletSpeed;
            }

            // lifetime & speed decay
            if (obj.TryGetComponent<ParticleMove>(out ParticleMove pm))
            {
                pm.setLifetime(_bulletLifetime);
                pm.setSlowingSpeedDelay(_slowingSpeedDelay);
                pm.setSlowingSpeedAmount(_slowingSpeedAmount);
            }

            // update time
            _timer = Time.time;
        }
    }
}
