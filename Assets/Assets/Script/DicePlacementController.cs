using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DicePlacementController : MonoBehaviour {

    [SerializeField] Image slot1Image;
    [SerializeField] Image slot2Image;
    [SerializeField] Image slot3Image;

    [SerializeField] GameObject dicePoint; // ダイスの親
    [SerializeField] CheckDiceCondition diceCondition;
    [SerializeField] DiceSlotMonitor diceSlotMonitor;
    [SerializeField] DiceManager diceManager;

    [SerializeField] DiceResultCollector diceResultCollector;
    [SerializeField] RollDiceController rollDiceController;
    [SerializeField] ElderSignEffect elderSignEffect;
    [SerializeField] DiceManager DiceManager;

    int countNum = 0; // 削除したダイスの数をカウント
    private Color highlightColor = new Color(0f, 1f, 1f, 0.78f);

    public void PlaceDiceOnSlot(int slotIndex) {

        List<string> requiredFaces = null;
        Image targetImage = null;

        switch (slotIndex) {
            case 1:
                requiredFaces = diceCondition.slot1Required;
                targetImage = slot1Image;
                break;
            case 2:
                requiredFaces = diceCondition.slot2Required;
                targetImage = slot2Image;
                break;
            case 3:
                requiredFaces = diceCondition.slot3Required;
                targetImage = slot3Image;
                break;
        }

        if (requiredFaces == null || targetImage == null) return;

        // slotの色変更
        targetImage.color = highlightColor;

        // DicePoint内のダイスをリスト化
        List<Transform> diceChildren = new List<Transform>();
        for (int i = 0; i < dicePoint.transform.childCount; i++) {
            diceChildren.Add(dicePoint.transform.GetChild(i));
        }

        // 必要な数だけ削除していく
        foreach (string reqRaw in requiredFaces) {
            string req = (reqRaw ?? "").Trim().ToLowerInvariant();
            bool found = false;

            for (int d = 0; d < diceChildren.Count; d++) {
                Transform dice = diceChildren[d];
                DiceTopFaceChecker checker = dice.GetComponent<DiceTopFaceChecker>();
                if (checker == null) continue;

                string diceFace = (checker.topFaceName ?? "").Trim().ToLowerInvariant();
                if (string.IsNullOrEmpty(diceFace)) continue;

                bool match = false;

                if (req.StartsWith("attack") && diceFace.StartsWith("attack")) {
                    if (int.TryParse(req.Substring("attack".Length), out int reqLevel) &&
                        int.TryParse(diceFace.Substring("attack".Length), out int diceLevel)) {
                        if (diceLevel >= reqLevel) match = true;
                    }
                }
                else if (req == diceFace) {
                    match = true;
                }

                if (match) {
                    Destroy(dice.gameObject); // DicePointから削除
                    diceChildren.RemoveAt(d); // リストからも削除
                    countNum++; // カウントを1増やす
                    found = true;
                    break; // 次のreqへ
                }
            }

            if (!found) {
                Debug.LogWarning($"Dice for required '{reqRaw}' not found.");
            }
        }

        // ここで該当スロットを「達成済み」にする
        diceSlotMonitor.MarkSlotAsCompleted(slotIndex);

        //達成時の処理
        if (diceSlotMonitor.AreAllSlotsSatisfied()) {

            diceResultCollector.topFaceNames.Clear();
            diceResultCollector.CheckDice = false;
            rollDiceController.createDice = false;
            rollDiceController.diceRollAllowedEvent = false;

            for (int i = dicePoint.transform.childCount - 1; i >= 0; i--) {
                Destroy(dicePoint.transform.GetChild(i).gameObject);
            }

            elderSignEffect.PlayEffect();
            diceManager.ResetTurnState();

            //未達成時の処理
        }
        else {

            diceManager.diceBlue -= countNum;
            diceResultCollector.topFaceNames.Clear();
            diceResultCollector.CheckDice = false;
            rollDiceController.createDice = false;
            rollDiceController.diceRollAllowedEvent = false;

            for (int i = dicePoint.transform.childCount - 1; i >= 0; i--) {
                Destroy(dicePoint.transform.GetChild(i).gameObject);
            }

            YesConfirmEvent.Instance.TriggerYesConfirmed();
        }
    }
}
