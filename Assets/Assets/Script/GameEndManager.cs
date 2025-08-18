using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameEndManager : MonoBehaviour {

    [SerializeField] TextMeshProUGUI numText;

    // Update is called once per frame
    void Update() {

        int.TryParse(numText.text, out int num);
        
        if (num == 5) {

            //I‚í‚é‚Ìˆ—‚È‚ñ‚©‘‚¢‚Ä‚Ë
        }
    }
}
