using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiceCountNumMonitor : MonoBehaviour {

    [SerializeField] TextMeshProUGUI blueDiceNumText;
    [SerializeField] DiceManager diceManager;

    // Update is called once per frame
    void Update()
    {
        if (blueDiceNumText != null) {
            // diceBlue の値をテキストとして表示
            blueDiceNumText.text = diceManager.diceBlue.ToString();
        }
    }
}
