using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSlotMonitor : MonoBehaviour {

    [SerializeField] private CheckDiceCondition diceCondition;
    [SerializeField] private DiceResultCollector diceResults;
    [SerializeField] GameObject selectedPlayCard;

    bool slot1Satisfied = false;
    bool slot2Satisfied = false;
    bool slot3Satisfied = false;

    Card card;

    //全ての条件付きスロットが埋まっているかどうかを返す
    public bool AreAllSlotsSatisfied() {

        card = selectedPlayCard.GetComponent<CardAttachedCardData>().cardData;

        // 各スロットの達成状態をチェック
        if (!slot1Satisfied && card.slot1.Count > 0) {
            return false;  // 1つでも達成していない場合は即座にfalseを返す
        }

        if (!slot2Satisfied && card.slot2.Count > 0) {
            return false;  // 2つ目もチェックして、達成していなければfalse
        }

        if (!slot3Satisfied && card.slot3.Count > 0) {
            return false;  // 3つ目も同様
        }

        // すべて達成していればtrueを返す
        return true;
    }

    //slotIndexに応じて達成フラグを立てる
    public void MarkSlotAsCompleted(int slotIndex) {

        switch (slotIndex) {
            case 1:
                slot1Satisfied = true;
                break;
            case 2:
                slot2Satisfied = true;
                break;
            case 3:
                slot3Satisfied = true;
                break;
            default:
                Debug.LogWarning($"Invalid slotIndex {slotIndex} passed to MarkSlotAsCompleted.");
                break;
        }
    }

    public void Reset() {
        
        slot1Satisfied = false;
        slot2Satisfied = false;
        slot3Satisfied = false;
    }
}