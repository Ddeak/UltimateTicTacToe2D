using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Player : MonoBehaviour {
    public string playerSide;
    public Image panel;
    public Text text;
    public Button button;

    public Player(string side, Image p, Text t, Button b) {
        playerSide = side;
        panel = p;
        text = t;
        button = b;
    }
}
