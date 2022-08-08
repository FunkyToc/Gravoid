using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] GameObject _toggleGameObject;
    public void toggle()
    {
        if (_toggleGameObject != null)
        {
            _toggleGameObject.SetActive(!_toggleGameObject.active);
            return;
        }
        
        gameObject.SetActive(!gameObject.active);
    }
}
