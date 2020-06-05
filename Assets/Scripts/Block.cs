using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public TTTBoard parentBoard;
    public bool IsClicked { get { return clicked; }}
    public int Index;

    Renderer renderer;
    bool clicked = false;
    
    private void Start() {
        renderer = GetComponent<Renderer>();
        parentBoard = transform.parent.gameObject.GetComponent<TTTBoard>();
    }

    private void OnMouseDown() {
        clicked = !clicked;

        if (clicked) 
            renderer.material.color = Color.red;
        else 
            renderer.material.color = Color.blue;

        parentBoard.CheckForWin(Index);
    }
}
