using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*==========プレイの結果失敗した場合===========*/
/*==========カードの達成に失敗した場合===========*/
public class PlayFailure : MonoBehaviour {

    [SerializeField] DiceManager diceManager;
    [SerializeField] CardAttachedCardData selectedPlayCard;

    [SerializeField] GameObject failureImage;
    [SerializeField] GameObject lostDiceImage;
    [SerializeField] DiceResultCollector diceResultCollector;
    [SerializeField] GameObject dicePoint;
    [SerializeField] RollDiceController rollDiceController;
    [SerializeField] HandCardsPopUpCardController handCardsPopUpCardController;
    [SerializeField] CardHideDisplayController cardHideDisplayController;
    [SerializeField] DiceManager DiceManager;

    public void OnPlayFailure() {

        //ブルーダイスの数をキャッチして一つ減らす
        int blueDiceNum = diceManager.diceBlue;
        blueDiceNum--;

        //現在のダイスの数が、プレイカードの要求量を下回ってないかチェック
        Card cardData = selectedPlayCard.cardData;
        int slotCountNum = cardData.slot1.Count + cardData.slot2.Count + cardData.slot3.Count;

        Debug.Log(slotCountNum + "," + blueDiceNum);

        //要求個数以上のダイスがなければ
        if (slotCountNum > blueDiceNum) {

            //初期化
            Reset(blueDiceNum);

            for (int i = dicePoint.transform.childCount - 1; i >= 0; i--) {
                Destroy(dicePoint.transform.GetChild(i).gameObject);
            }

            StartCoroutine(AnimateFailureImage(failureImage));
            diceManager.ResetTurnState();
            cardHideDisplayController.CardHideDisplay();
            handCardsPopUpCardController.StartMyProcess();

        //要求個数を満たせる数のダイスがあれば
        } else {

            StartCoroutine(AnimateFailureImage(lostDiceImage));

            Reset(blueDiceNum);

            for (int i = dicePoint.transform.childCount - 1; i >= 0; i--) {
                Destroy(dicePoint.transform.GetChild(i).gameObject);
            }

            YesConfirmEvent.Instance.TriggerYesConfirmed();
        }
    }

     private IEnumerator AnimateFailureImage(GameObject gameObject) {

        gameObject.SetActive(true);
        yield return MoveToY(gameObject, - 2000f);              // 初期位置セット
        yield return MoveToY(gameObject, 0f, 1f);              // 表示へ3秒
        yield return new WaitForSeconds(3f);       // 待機2秒
        yield return MoveToY(gameObject, 2000f, 1f);           // 退場へ3秒
        gameObject.SetActive(false);
    }

    private IEnumerator MoveToY(GameObject gameObject, float targetY, float duration = 0f) {

        RectTransform objectTransform = gameObject.GetComponent<RectTransform>();

        if (duration == 0f) {
            objectTransform.anchoredPosition = new Vector2(0, targetY);
            yield break;
        }

        float elapsed = 0f;
        Vector2 startPos = objectTransform.anchoredPosition;
        Vector2 endPos = new Vector2(0, targetY);

        while (elapsed < duration) {

            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            objectTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        objectTransform.anchoredPosition = endPos;
    }

    private void Reset(int blueDiceNum) {

        diceManager.diceBlue = blueDiceNum;
        diceResultCollector.topFaceNames.Clear();
        diceResultCollector.CheckDice = false;
        rollDiceController.createDice = false;
        rollDiceController.diceRollAllowedEvent = false;
    }
}
