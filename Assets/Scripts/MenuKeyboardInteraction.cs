using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuKeyboardInteraction : MonoBehaviour
{
    [SerializeField] OptionMenu menu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.toggle();
        }
    }
}
