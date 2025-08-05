using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*==============クリックしたカードをPopUpするためのcontroller===============*/
public class CardPopupDisplayController : MonoBehaviour {

    /*===========Core===========*/
    [SerializeField] MouseOnCardController mouseOnCardController;
    [SerializeField] Image selectedPlayCard;
    [SerializeField] GameObject confirmCardPlay;

    RectTransform rect;
    float duration = 0.5f;
    float elapsed = 0f;
    Vector2 startPos;
    Vector2 endPos;

    /*========初期設定=========*/
    private void Start() {

        rect = selectedPlayCard.GetComponent<RectTransform>();
        endPos = new Vector2(-300f, -100f);
        startPos = endPos + new Vector2(0, -2000f);

        rect.anchoredPosition = startPos;

        confirmCardPlay.SetActive(false);
    }

    /*========CardPopupDisplayEventが発生したら起動する==========*/
    public void CardPopupDisplay() {

        //selectedPlayCardにCardDataとImageを保存する
        selectedPlayCard.sprite = mouseOnCardController.card.CardImage;
        selectedPlayCard.GetComponent<CardAttachedCardData>().cardData = mouseOnCardController.card;

        //selectedPlayCardをstartPos(画面↓)からendPos(画面中央)に移動させる
        elapsed = 0f;
        StartCoroutine(CardGroupMove());

        confirmCardPlay.SetActive(true);
    }

    /*=========所定の位置に来るまで動かし続ける=========*/
    private IEnumerator CardGroupMove() {

        while (elapsed < duration) {
            float t = elapsed / duration;
            rect.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        rect.anchoredPosition = endPos;
    }
}
