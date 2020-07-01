using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalController : MonoBehaviour
{
    public GameObject resetButton;
    public GameObject startInfo;

    public GameObject gameOverPanel;
    public Text gameOverText;

    public Player playerX;
    public Player playerO;
    private string playerSide;

    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;

    public GameController[] sections;
    private int sectionsFilled;

    private WinningOptions winOpts;

    public string GetPlayerSide() {
        return playerSide;
    }

    void Awake() {
        winOpts = new WinningOptions();
        resetButton.SetActive(false);
        SetGlobalControllerReferencesOnSections();
        gameOverPanel.SetActive(false);
        gameOverText.text = "";
        SetPlayerColorsActive();

    }

    void SetGlobalControllerReferencesOnSections() {
        foreach (GameController section in sections)
        {
            section.SetGlobalControllerRef(this, winOpts);
        }
    }

    void StartGame() {
        sectionsFilled = 0;
        SetPlayerButtons(false);
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

    void SetPlayerColorsActive() {
        playerX.panel.color = activePlayerColor.panelColor;
        playerX.text.color = activePlayerColor.textColor;
        playerO.panel.color = activePlayerColor.panelColor;
        playerO.text.color = activePlayerColor.textColor;
    }

    public void RestartGame() {
        resetButton.SetActive(false);
        SetPlayerButtons(true);
        SetPlayerColorsActive();

        gameOverPanel.SetActive(false);
        gameOverText.text = "";

        startInfo.SetActive(true);
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

    void GameOver(string winText) {
        gameOverPanel.SetActive(true);
        gameOverText.text = winText;

        foreach (GameController section in sections)
        {
            section.SetBoardInteractable(false);
        }

        resetButton.SetActive(true);
    }

    public void CheckForWin(int position) {
        int [][] currentWinningOptions = winOpts.GetByPosition(position);
        bool winFound = false;
        sectionsFilled++;
        
        for (int i = 0; i < currentWinningOptions.Length; i++)
        {
            int winTicked = 0;
            for (int j = 0; j < currentWinningOptions[i].Length; j++)
            {
                if (sections[currentWinningOptions[i][j]].gameOverText.text == GetPlayerSide()) {
                    winTicked++;
                }
            }
            if (winTicked == 2) {
                winFound = true;
            }
        }
        
        if (winFound) GameOver("Game Over!\n" + GetPlayerSide() + " wins!");
        if (sectionsFilled == 9) {
            GameOver("Game Over!\n It's a draw!");
        }
    }
}
