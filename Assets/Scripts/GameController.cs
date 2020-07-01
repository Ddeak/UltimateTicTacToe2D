using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerColor {
    public Color panelColor;
    public Color textColor;
}

public class GameController : MonoBehaviour {
    public GlobalController globalController;
    public GameObject gameOverPanel;

    public Text[] buttonList;
    public Text gameOverText;

    private WinningOptions winOpts;
    private int moveCount;

    void Awake() {
        winOpts = new WinningOptions();
        SetGameControllerReferencesOnButtons();
        gameOverPanel.SetActive(false);
        gameOverText.text = "Game Over";
        moveCount = 0;
    }

    public void SetGlobalControllerRef(GlobalController global) {
        globalController = global;
    }

    void SetGameControllerReferencesOnButtons() {
        foreach (Text button in buttonList)
        {
            button.GetComponentInParent<GridSpace>().SetGameControllerRef(this, globalController);
        }
    }

    void GameOver(bool gameIsADraw) {
        SetBoardInteractable(false);

        if (gameIsADraw)
            gameOverText.text = "-";
        else
            gameOverText.text = globalController.GetPlayerSide();

        gameOverPanel.SetActive(true);
    }

    void SetBoardInteractable(bool active) {
        foreach (Text button in buttonList)
        {
            button.GetComponentInParent<Button>().interactable = active;
            if (active) button.text = "";
        }
    }

    public void StartGame() {
        SetBoardInteractable(true);
    }

    public void EndTurn(int buttonPressed) {
        bool gameWon = CheckForWin(buttonPressed);
        bool gameIsADraw = ++moveCount >= 9 && !gameWon;

        if ( gameWon || gameIsADraw) {
            GameOver(gameIsADraw);
        } 

        globalController.EndTurn();
    }

    public bool CheckForWin(int blockIndex) {
        int [][] currentWinningOptions = winOpts.GetByPosition(blockIndex);
        bool winFound = false;
        
        for (int i = 0; i < currentWinningOptions.Length; i++)
        {
            int winTicked = 0;
            for (int j = 0; j < currentWinningOptions[i].Length; j++)
            {
                if (buttonList[currentWinningOptions[i][j]].text == globalController.GetPlayerSide()) {
                    winTicked++;
                }
            }
            if (winTicked == 2) {
                winFound = true;
            }
        }
        return winFound;
    }

    public void RestartGame() {
        moveCount = 0;
        gameOverPanel.SetActive(false);
    }
}
