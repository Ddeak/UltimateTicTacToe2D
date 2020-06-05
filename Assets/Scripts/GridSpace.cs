using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour
{
    public Button button;
    public Text buttonText;
    public int position;

    private GameController gameController;
    
    public void SetGameControllerRef(GameController controller) {
        gameController = controller;
    }

    public void SetSpace ()
    {
        buttonText.text = gameController.GetPlayerSide();
        button.interactable = false;
        gameController.EndTurn(position);
    }
}
