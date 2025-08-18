using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DicePlacementController : MonoBehaviour {

    [SerializeField] Image slot1Image;
    [SerializeField] Image slot2Image;
    [SerializeField] Image slot3Image;

    [SerializeField] GameObject dicePoint; // �_�C�X�̐e
    [SerializeField] CheckDiceCondition diceCondition;
    [SerializeField] DiceSlotMonitor diceSlotMonitor;
    [SerializeField] DiceManager diceManager;

    [SerializeField] DiceResultCollector diceResultCollector;
    [SerializeField] RollDiceController rollDiceController;
    [SerializeField] ElderSignEffect elderSignEffect;
    [SerializeField] DiceManager DiceManager;

    int countNum = 0; // �폜�����_�C�X�̐����J�E���g
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

        // slot�̐F�ύX
        targetImage.color = highlightColor;

        // DicePoint���̃_�C�X�����X�g��
        List<Transform> diceChildren = new List<Transform>();
        for (int i = 0; i < dicePoint.transform.childCount; i++) {
            diceChildren.Add(dicePoint.transform.GetChild(i));
        }

        // �K�v�Ȑ������폜���Ă���
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
                    Destroy(dice.gameObject); // DicePoint����폜
                    diceChildren.RemoveAt(d); // ���X�g������폜
                    countNum++; // �J�E���g��1���₷
                    found = true;
                    break; // ����req��
                }
            }

            if (!found) {
                Debug.LogWarning($"Dice for required '{reqRaw}' not found.");
            }
        }

        // �����ŊY���X���b�g���u�B���ς݁v�ɂ���
        diceSlotMonitor.MarkSlotAsCompleted(slotIndex);

        //�B�����̏���
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

            //���B�����̏���
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
