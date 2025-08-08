using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour {

    public int diceBlue;
    public int diceYellow;
    public int diceRed;
    public int diceBlack;
    public int diceWhite;

    private void Start() {

        diceBlue = 6;
        diceYellow = 0;
        diceRed = 0;
        diceBlack = 0;
        diceWhite = 0;
    }

    public void ResetTurnState() {

        diceBlue = 6;
        diceYellow = 0;
        diceRed = 0;
        diceBlack = 0;
        diceWhite = 0;
    }
}
