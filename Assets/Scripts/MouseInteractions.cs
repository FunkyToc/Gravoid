using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteractions : MonoBehaviour
{
    [SerializeField] bool interactable = true;
    [Range(0.6f, 10f)]
    [SerializeField] private float _minRadius = 0.6f;
    [Range(0.6f, 10f)]
    [SerializeField] private float _maxRadius = 5.0f;

    private CircleShape _areaEffector;
    private bool _mouseLock;
    private int _mouseAction;

    void Start()
    {
        _mouseAction = gameObject.tag == "CursorMove" ? 1 : 2;
        _areaEffector = GetComponentInParent<Transform>().GetComponentInChildren<CircleShape>();
    }

    void Update()
    {
        if (!interactable) return;

        if (_mouseLock == true)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); mousePos.z = 0;

            if (_mouseAction == 1) // move
            {
                transform.parent.position = mousePos;
            }
            else if (_mouseAction == 2) // radius   
            {
                float radius = (transform.parent.position - mousePos).magnitude;
                radius = radius >= _minRadius ? radius : _minRadius;
                radius = radius <= _maxRadius ? radius : _maxRadius;
                _areaEffector.Radius = radius;
            }
        }
    }

    void OnMouseDown()
    {
        _mouseLock = true;
    }

    void OnMouseUp()
    {
        _mouseLock = false;
    }
}