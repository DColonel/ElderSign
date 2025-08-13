using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*==========プレイの結果失敗した場合===========*/
/*==========カードの達成に失敗した場合===========*/
public class PlayFailure : MonoBehaviour {

    [SerializeField] DiceManager diceManager;
    [SerializeField] CardAttachedCardData selectedPlayCard;

    [SerializeField] GameObject failureImage;
    [SerializeField] DiceResultCollector diceResultCollector;

    public void OnPlayFailure() {

        //ブルーダイスの数をキャッチして一つ減らす
        int blueDiceNum = diceManager.diceBlue;
        blueDiceNum--;

        //現在のダイスの数が、プレイカードの要求量を下回ってないかチェック
        Card cardData = selectedPlayCard.cardData;
        int slotCountNum = cardData.slot1.Count + cardData.slot2.Count + cardData.slot3.Count;

        //もし上回っていたら
        if (slotCountNum > blueDiceNum) {

            failureImage.SetActive(true);
            StartCoroutine(AnimateFailureImage());
        }else {

            diceManager.diceBlue = blueDiceNum;
            YesConfirmEvent.Instance.TriggerYesConfirmed();
            diceResultCollector.topFaceNames.Clear();
        }
    }

     private IEnumerator AnimateFailureImage() {

        yield return MoveToY(-2000f);              // 初期位置セット
        yield return MoveToY(0f, 3f);              // 表示へ3秒
        yield return new WaitForSeconds(2f);       // 待機2秒
        yield return MoveToY(2000f, 3f);           // 退場へ3秒
    }

    private IEnumerator MoveToY(float targetY, float duration = 0f) {

        RectTransform failureTransform = failureImage.GetComponent<RectTransform>();

        if (duration == 0f) {
            failureTransform.anchoredPosition = new Vector2(0, targetY);
            yield break;
        }

        float elapsed = 0f;
        Vector2 startPos = failureTransform.anchoredPosition;
        Vector2 endPos = new Vector2(0, targetY);

        while (elapsed < duration) {

            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            failureTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        failureTransform.anchoredPosition = endPos;
    }
}
