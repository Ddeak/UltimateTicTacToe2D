using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public Player CurrentPlayer;
    public bool gameOver = false;

    public void SwapCurrentPlayer(Player nextPlayer) {
        CurrentPlayer = nextPlayer;
    }

    public Player GetPlayer() {
        return CurrentPlayer;
    }
}
