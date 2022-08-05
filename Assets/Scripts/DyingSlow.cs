using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DyingSlow : MonoBehaviour
{
    [SerializeField] private VisualEffect _vfx;
    [Range(0.01f,10f)]
    [SerializeField] private float _sizeMultiplier = 0.5f;
    [SerializeField] private float _maxSize = 0.5f;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float size = _sizeMultiplier * (_rb.velocity.magnitude / 20);
        size = size > _maxSize ? _maxSize : size;
        _vfx.SetFloat("Size", size);
    }
}
