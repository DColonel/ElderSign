using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*=========カードをPopUpする際に、手札一覧を画面下に移動させるためのScript=========*/
public class HandCardsHideCardController : MonoBehaviour {

    [SerializeField] RectTransform cardGroup;

    float duration = 0.3f;
    float elapsed = 0f;
    Vector2 startPos;
    Vector2 endPos;

    private void Start() {

        startPos = cardGroup.anchoredPosition;
        endPos = startPos + new Vector2(0, -2000f);
    }

    public void StartMyProcess() {

        StartCoroutine(CardGroupMove());
    }

    private IEnumerator CardGroupMove() {

        while (elapsed < duration) {
            float t = elapsed / duration;
            cardGroup.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        cardGroup.anchoredPosition = endPos;
    }
}