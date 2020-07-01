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
    private GlobalController globalController;
    
    public void SetGameControllerRef(GameController controller, GlobalController global) {
        gameController = controller;
        globalController = global;
    }

    public void SetSpace ()
    {
        buttonText.text = globalController.GetPlayerSide();
        button.interactable = false;
        gameController.EndTurn(position);
    }
}
