using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenu : MonoBehaviour
{
    public void toggle()
    {
        gameObject.SetActive(!gameObject.active);
    }
}
