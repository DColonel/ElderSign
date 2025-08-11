using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*=============それぞれのダイスの結果をListでまとめるための処理===============*/
public class DiceResultCollector : MonoBehaviour {

    [SerializeField] public GameObject dicePoint;
    public List<string> topFaceNames = new List<string>();

    /*============複数のダイスの出目をキャッチする===========*/
    public void CollectDiceResults() {
        topFaceNames.Clear();

        int diceCount = dicePoint.transform.childCount;

        for (int i = 0; i < diceCount; i++) {

            Transform dice = dicePoint.transform.GetChild(i);
            DiceTopFaceChecker checker = dice.GetComponent<DiceTopFaceChecker>();

            if (checker != null) {
                string faceName = checker.topFaceName;

                // 重複を避けたい場合
                if (!topFaceNames.Contains(faceName)) {
                    topFaceNames.Add(faceName);
                }
            } else {

                Debug.LogWarning($"Dice {i} に DiceTopFaceChecker がアタッチされていません");
            }
        }
    }
}
