using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMove : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    private float _slowingSpeedDelay = 2.0f;
    private float _slowingSpeedAmount = 1.0f;
    private float _lifetime = 10.0f;
    private float _ctime;

    void Start()
    {
        _ctime = Time.time;
    }

    void Update()
    {
        // absolute lifetime
        _lifetime -= Time.deltaTime;
        if (_lifetime <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }

        // speed decay
        if (Time.time > _ctime + _slowingSpeedDelay)
        {
            _rb.velocity -= _rb.velocity * _slowingSpeedAmount * Time.deltaTime;
        }
    }

    public void setLifetime(float lifetime)
    {
        _lifetime = lifetime;
    }

    public void setCtimeReset(float time)
    {
        _ctime = time == 0f ? time : Time.time;
    }

    public void setSlowingSpeedDelay(float delay)
    {
        _slowingSpeedDelay = delay;
    }

    public void setSlowingSpeedAmount(float force)
    {
        _slowingSpeedAmount = force;
    }
}
