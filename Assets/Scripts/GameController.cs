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
    public int gamePosition;

    private WinningOptions winOpts;
    private int moveCount;

    void Awake() {
        SetGameControllerReferencesOnButtons();
        gameOverPanel.SetActive(false);
        gameOverText.text = "Game Over";
        moveCount = 0;
    }

    public void SetGlobalControllerRef(GlobalController global, WinningOptions opts) {
        globalController = global;
        winOpts = opts;
    }

    void SetGameControllerReferencesOnButtons() {
        foreach (Text button in buttonList)
        {
            button.GetComponentInParent<GridSpace>().SetGameControllerRef(this, globalController);
        }
    }

    void SectionOver(bool gameIsADraw) {
        SetBoardInteractable(false);

        if (gameIsADraw)
            gameOverText.text = "-";
        else
            gameOverText.text = globalController.GetPlayerSide();

        gameOverPanel.SetActive(true);
        globalController.CheckForWin(gamePosition);
    }

    public void SetBoardInteractable(bool active) {
        foreach (Text button in buttonList)
        {
            button.GetComponentInParent<Button>().interactable = active;
            if (active) button.text = "";
        }
    }

    public void StartGame() {
        moveCount = 0;
        gameOverPanel.SetActive(false);
        gameOverText.text = "";
        SetBoardInteractable(true);
    }

    public void EndTurn(int buttonPressed) {
        bool sectionWon = CheckForWin(buttonPressed);
        bool sectionIsADraw = ++moveCount >= 9 && !sectionWon;

        if ( sectionWon || sectionIsADraw ) {
            SectionOver(sectionIsADraw);
        } else {
            globalController.EndTurn();
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
}
