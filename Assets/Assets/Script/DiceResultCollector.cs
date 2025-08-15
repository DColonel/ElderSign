using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*=============それぞれのダイスの結果をListでまとめるための処理===============*/
public class DiceResultCollector : MonoBehaviour {

    [SerializeField] public GameObject dicePoint;
    [SerializeField] public GameObject diceGroup;
    [SerializeField] int diceCount;

    public List<string> topFaceNames = new List<string>();
    public bool CheckDice = false;

    public void Update() {

        diceCount = dicePoint.transform.childCount;

        //ダイスの出目が一個でも確定すればダイスを振るボタンを非表示にする
        if (topFaceNames.Count > 0) {

            diceGroup.SetActive(false);
        }
        
        //ダイスの出目のまとめlistの中身の数がダイスの総数と合えばイベント発火
        if (topFaceNames.Count == diceCount && diceCount !=  0 && !CheckDice) {

            CheckDice = true;
            CheckDiceConditionEvent.Instance.TriggerCheckDiced();
        }
    }

    /*============複数のダイスの出目をキャッチする===========*/
    public void CollectDiceResults() {

        topFaceNames.Clear();

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
