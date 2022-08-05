using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHover : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] Texture2D _cursorBase;
    [SerializeField] Texture2D _cursorMove;
    [SerializeField] Texture2D _cursorResize;


    void Start()
    {
        Cursor.SetCursor(_cursorBase, Vector2.zero, CursorMode.Auto);
    }

    void Update()
    {
        RaycastHit2D hitMove = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0.01f, 1<<9);
        RaycastHit2D hitResize = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0.01f, 1<<10);
        
        if (hitMove.collider != null)
        {
            Cursor.SetCursor(_cursorMove, new Vector2(16f, 16f), CursorMode.Auto);
        }
        else if (hitResize.collider != null)
        {
            Cursor.SetCursor(_cursorResize, new Vector2(16f, 16f), CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(_cursorBase, Vector2.zero, CursorMode.Auto);
        }
    }
}
