using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public Text[] buttonList;
    public GameObject gameOverPanel;
    public Text gameOverText;
    public GameObject resetButton;

    private string playerSide;
    private WinningOptions winOpts;
    private int moveCount;

    private void Awake() {
        winOpts = new WinningOptions();
        SetGameControllerReferencesOnButtons();
        playerSide = "X";
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
    }

    void ChangeSides ()
    {
        playerSide = (playerSide == "X") ? "O" : "X";
    }

    public string GetPlayerSide() {
        return playerSide;
    }

    public void EndTurn(int buttonPressed) {
        bool gameIsADraw = ++moveCount >= 9;

        if (CheckForWin(buttonPressed) == true || gameIsADraw) {
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
        playerSide = "X";
        moveCount = 0;
        gameOverPanel.SetActive(false);

        SetBoardInteractable(true);
        resetButton.SetActive(false);
    }
}
