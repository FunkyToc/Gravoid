using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] GameObject _toggleGameObject;

    public bool Actived
    {
        get { return gameObject.activeSelf; }
        private set { }
    }

    public void toggle()
    {
        if (_toggleGameObject != null)
        {
            _toggleGameObject.SetActive(!_toggleGameObject.activeSelf);
            return;
        }
        
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
