using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

/*============プレイしているカードの条件をダイスの出目が満たしているかの確認=============*/
public class CheckDiceCondition : MonoBehaviour {

    /*=========Core==========*/
    [SerializeField] DiceResultCollector DiceResultCollector;
    [SerializeField] CardAttachedCardData selectedPlayCard;
    [SerializeField] PlayFailure playFailure;
    [SerializeField] PlaySuccess playSuccess;

    List<string> diceResults;
    public bool slot1Met;
    public bool slot2Met;
    public bool slot3Met;

    public List<string> slot1Required;
    public List<string> slot2Required;
    public List<string> slot3Required;

    private void Start() {

        diceResults = DiceResultCollector.topFaceNames;
    }

    /*========確認開始========*/
    public void checkDice() {

        diceResults = new List<string>(DiceResultCollector.topFaceNames);

        slot1Met = false;
        slot2Met = false;
        slot3Met = false;

        var cardData = selectedPlayCard.GetComponent<CardAttachedCardData>().cardData;

        slot1Required = cardData.slot1.Select(s => s.requiredDice.ToString()).ToList();
        slot2Required = cardData.slot2.Select(s => s.requiredDice.ToString()).ToList();
        slot3Required = cardData.slot3.Select(s => s.requiredDice.ToString()).ToList();

        slot1Met = slot1Required != null && slot1Required.Count > 0 && CheckListSatisfied(diceResults, slot1Required);
        slot2Met = slot2Required != null && slot2Required.Count > 0 && CheckListSatisfied(diceResults, slot2Required);
        slot3Met = slot3Required != null && slot3Required.Count > 0 && CheckListSatisfied(diceResults, slot3Required);

        // ログ出力
        if (slot1Met || slot2Met || slot3Met) {
            Debug.Log($"結果: " +
                $"{(slot1Met ? "スロット1〇" : "スロット1×")} " +
                $"{(slot2Met ? "スロット2〇" : "スロット2×")} " +
                $"{(slot3Met ? "スロット3〇" : "スロット3×")}");
            playSuccess.DicePlacement();

        } else {
            Debug.Log("全部条件不一致×");
            playFailure.OnPlayFailure();
        }
    }

    /*========カードのlistと出目のlistの擦り合わせ========*/
    bool CheckListSatisfied(List<string> diceResults, List<string> required, bool debug = false) {
        // 正規化してコピー（小文字化・トリム）
        var temp = diceResults
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(s => s.Trim().ToLowerInvariant())
            .ToList();

        if (debug) Debug.Log($"[Check] starting. dice={string.Join(", ", temp)} req={string.Join(", ", required)}");

        foreach (var reqRaw in required) {
            string req = (reqRaw ?? "").Trim().ToLowerInvariant();
            bool matched = false;

            if (req.StartsWith("attack")) {
                // req の数値部分を取る (attack1 -> 1)
                string numPart = req.Substring("attack".Length);
                if (!int.TryParse(numPart, out int reqLevel)) {
                    if (debug) Debug.LogWarning($"[Check] required '{reqRaw}' parse failed");
                    return false;
                }

                // temp から attack系の要素を探して、レベル >= reqLevel のものを消費する
                for (int i = 0; i < temp.Count; i++) {
                    var d = temp[i];
                    if (!d.StartsWith("attack")) continue;
                    string dNum = d.Substring("attack".Length);
                    if (int.TryParse(dNum, out int diceLevel) && diceLevel >= reqLevel) {
                        // 見つけた -> 消費して次の req へ
                        temp.RemoveAt(i);
                        matched = true;
                        if (debug) Debug.Log($"[Check] matched req '{reqRaw}' with dice '{d}'");
                        break;
                    }
                }
            } else {
                // 完全一致（正規化済み）
                int idx = temp.FindIndex(x => x == req);
                if (idx >= 0) {
                    temp.RemoveAt(idx);
                    matched = true;
                    if (debug) Debug.Log($"[Check] matched req '{reqRaw}' exactly");
                }
            }

            if (!matched) {
                if (debug) Debug.Log($"[Check] failed to match required '{reqRaw}'. remaining dice: {string.Join(", ", temp)}");
                return false;
            }
        }

        if (debug) Debug.Log("[Check] all matched!");
        return true;
    }
}
