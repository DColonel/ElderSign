using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class HandCardsPopUpCardController : MonoBehaviour {

    [SerializeField] RectTransform cardGroup;

    float duration = 0.6f;
    float elapsed = 0f;
    Vector2 startPos;
    Vector2 endPos;

    private void Start() {

        startPos = cardGroup.anchoredPosition;
        endPos = new Vector2(0, 0);
    }

    public void StartMyProcess() {

        elapsed = 0f;
        startPos = cardGroup.anchoredPosition;
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