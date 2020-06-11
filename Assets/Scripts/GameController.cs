using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Player {
    public Image panel;
    public Text text;
    public Button button;
}

[System.Serializable]
public class PlayerColor {
    public Color panelColor;
    public Color textColor;
}

public class GameController : MonoBehaviour {
    public Text[] buttonList;
    public GameObject gameOverPanel;
    public Text gameOverText;
    public GameObject resetButton;
    public Player playerX;
    public Player playerO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;

    private string playerSide;
    private WinningOptions winOpts;
    private int moveCount;

    private void Awake() {
        winOpts = new WinningOptions();
        SetGameControllerReferencesOnButtons();
        gameOverPanel.SetActive(false);
        gameOverText.text = "Game Over";
        moveCount = 0;
        resetButton.SetActive(false);
    }

    private void SetGameControllerReferencesOnButtons() {
        foreach (Text button in buttonList)
        {
            button.GetComponentInParent<GridSpace>().SetGameControllerRef(this);
        }
    }

    void GameOver(bool gameIsADraw) {
        SetBoardInteractable(false);

        if (gameIsADraw)
            gameOverText.text = "It's a draw!";
        else
            gameOverText.text = playerSide + " wins!";

        gameOverPanel.SetActive(true);
        resetButton.SetActive(true);
        SetPlayerColorsInactive();
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

    public string GetPlayerSide() {
        return playerSide;
    }

    public void EndTurn(int buttonPressed) {
        bool gameWon = CheckForWin(buttonPressed);
        bool gameIsADraw = ++moveCount >= 9 && !gameWon;

        if ( gameWon || gameIsADraw) {
            GameOver(gameIsADraw);
        } else {
            ChangeSides();
        }
    }

    public bool CheckForWin(int blockIndex) {
        int [][] currentWinningOptions = winOpts.GetByPosition(blockIndex);
        bool winFound = false;
        
        for (int i = 0; i < currentWinningOptions.Length; i++)
        {
            int winTicked = 0;
            for (int j = 0; j < currentWinningOptions[i].Length; j++)
            {
                if (buttonList[currentWinningOptions[i][j]].text == playerSide) {
                    winTicked++;
                }
            }
            if (winTicked == 2) {
                winFound = true;
            }
        }

        return winFound;
    }

    void SetBoardInteractable(bool active) {
        foreach (Text button in buttonList)
        {
            button.GetComponentInParent<Button>().interactable = active;
            if (active) button.text = "";
        }
    }

    public void RestartGame() {
        moveCount = 0;
        gameOverPanel.SetActive(false);

        resetButton.SetActive(false);
        SetPlayerButtons(true);
        SetPlayerColorsInactive();
    }

    void SetPlayerColors(Player newPlayer, Player oldPlayer) {
        newPlayer.panel.color = activePlayerColor.panelColor;
        newPlayer.text.color = activePlayerColor.textColor;

        oldPlayer.panel.color = inactivePlayerColor.panelColor;
        oldPlayer.text.color = inactivePlayerColor.textColor;
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

    void StartGame() {
        SetBoardInteractable(true);
        SetPlayerButtons(false);
    }

    void SetPlayerButtons(bool toggle) {
        playerX.button.interactable = toggle;
        playerO.button.interactable = toggle;
    }

    void SetPlayerColorsInactive() {
        playerX.panel.color = inactivePlayerColor.panelColor;
        playerX.text.color = inactivePlayerColor.textColor;
        playerO.panel.color = inactivePlayerColor.panelColor;
        playerO.text.color = inactivePlayerColor.textColor;
    }
}
