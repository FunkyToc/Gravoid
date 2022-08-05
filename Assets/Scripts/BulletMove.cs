using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float _lifetime;

    void Update()
    {
        // lifetime
        _lifetime -= Time.deltaTime;
        if (_lifetime <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
