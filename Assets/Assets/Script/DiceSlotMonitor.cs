using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSlotMonitor : MonoBehaviour {

    [SerializeField] private CheckDiceCondition diceCondition;
    [SerializeField] private DiceResultCollector diceResults;

    //全ての条件付きスロットが埋まっているかどうかを返す
    public bool AreAllSlotsSatisfied() {
        if (!IsSlotSatisfied(diceCondition.slot1Required)) return false;
        if (!IsSlotSatisfied(diceCondition.slot2Required)) return false;
        if (!IsSlotSatisfied(diceCondition.slot3Required)) return false;

        return true;
    }

    //あるスロットが必要条件を満たしているかを判定
    //nullや空リストなら無視してtrueを返す
    private bool IsSlotSatisfied(List<string> requiredFaces) {
        if (requiredFaces == null || requiredFaces.Count == 0) return true;

        int matchCount = 0;
        foreach (var face in requiredFaces) {
            if (diceResults.topFaceNames.Contains(face)) {
                matchCount++;
            }
        }

        return matchCount == requiredFaces.Count;
    }

    //slotIndexに応じて達成フラグを立てる
    public void MarkSlotAsCompleted(int slotIndex) {
        switch (slotIndex) {
            case 1:
                diceCondition.slot1Met = true;
                break;
            case 2:
                diceCondition.slot2Met = true;
                break;
            case 3:
                diceCondition.slot3Met = true;
                break;
            default:
                Debug.LogWarning($"Invalid slotIndex {slotIndex} passed to MarkSlotAsCompleted.");
                break;
        }
    }
}