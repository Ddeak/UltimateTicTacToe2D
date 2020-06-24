using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour
{
    public GameObject resetButton;
    public GameObject startInfo;

    public Player playerX;
    public Player playerO;
    private string playerSide;

    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;

    public GameController[] sections;

    public string GetPlayerSide() {
        return playerSide;
    }

    void Awake() {
        resetButton.SetActive(false);
        SetGlobalControllerReferencesOnSections();
    }

    void SetGlobalControllerReferencesOnSections() {
        foreach (GameController section in sections)
        {
            section.SetGlobalControllerRef(this);
        }
    }

    void StartGame() {
        foreach (GameController section in sections)
        {
            section.StartGame();
        }
        
        startInfo.SetActive(false);
    }

    void SetPlayerButtons(bool toggle) {
        playerX.button.interactable = toggle;
        playerO.button.interactable = toggle;
    }

    void ChangeSides ()
    {
        if (playerSide == "X") {
            playerSide = "O";
            SetPlayerColors(playerO, playerX);
        } else {
            playerSide = "X";
            SetPlayerColors(playerX, playerO);
        }
    }

    void SetPlayerColors(Player newPlayer, Player oldPlayer) {
        newPlayer.panel.color = activePlayerColor.panelColor;
        newPlayer.text.color = activePlayerColor.textColor;

        oldPlayer.panel.color = inactivePlayerColor.panelColor;
        oldPlayer.text.color = inactivePlayerColor.textColor;
    }

    void SetPlayerColorsInactive() {
        playerX.panel.color = inactivePlayerColor.panelColor;
        playerX.text.color = inactivePlayerColor.textColor;
        playerO.panel.color = inactivePlayerColor.panelColor;
        playerO.text.color = inactivePlayerColor.textColor;
    }

    public void RestartGame() {
        foreach (GameController section in sections)
        {
            section.RestartGame();
        }
        resetButton.SetActive(false);
        SetPlayerButtons(true);
        SetPlayerColorsInactive();
    }

    public void EndTurn() {
        ChangeSides();
    }

    public void SetStartingSide(string startingSide) {
        playerSide = startingSide;
        
        if (playerSide == "X") {
            SetPlayerColors(playerX, playerO);
        } else {
            SetPlayerColors(playerO, playerX);
        }

        StartGame();
    }
}
