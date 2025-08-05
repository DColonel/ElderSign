using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*==========PopUpを画面下に戻すScript===========*/
public class CardHideDisplayController : MonoBehaviour {

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
        endPos =  new Vector2(-300, -2000f);
        startPos = rect.anchoredPosition;
    }

    /*========CardPopupDisplayEventが発生したら起動する==========*/
    public void CardHideDisplay() {

        //selectedPlayCardをstartPos(画面中央)からendPos(画面↓)に移動させる
        elapsed = 0f;
        StartCoroutine(CardMove());

        confirmCardPlay.SetActive(false);
    }

    /*=========所定の位置に来るまで動かし続ける=========*/
    private IEnumerator CardMove() {

        while (elapsed < duration) {
            float t = elapsed / duration;
            rect.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        rect.anchoredPosition = endPos;
    }
}
