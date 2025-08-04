using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Start() {

        rect = selectedPlayCard.GetComponent<RectTransform>();
        endPos = new Vector2(-300f, -100f);
        startPos = endPos + new Vector2(0, -2000f);

        rect.anchoredPosition = startPos;

        confirmCardPlay.SetActive(false);
    }

    /*========CardPopupDisplayEvent‚ª”­¶‚µ‚½‚ç‹N“®‚·‚é==========*/
    public void CardPopupDisplay() {

        //selectedPlayCard‚ÉCardData‚ÆImage‚ğ•Û‘¶‚·‚é
        selectedPlayCard.sprite = mouseOnCardController.card.CardImage;
        selectedPlayCard.GetComponent<CardAttachedCardData>().cardData = mouseOnCardController.card;

        //selectedPlayCard‚ğstartPos(‰æ–Ê«)‚©‚çendPos(‰æ–Ê’†‰›)‚ÉˆÚ“®‚³‚¹‚é
        elapsed = 0f;
        StartCoroutine(CardGroupMove());

        //mouseOnCardController“à‚ÌŒp³‚Ì‚½‚ß‚ÌCardData‚ğíœ‚·‚é
        mouseOnCardController.CardDataClear();

        confirmCardPlay.SetActive(true);
    }

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
