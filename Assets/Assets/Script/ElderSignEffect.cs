using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElderSignEffect : MonoBehaviour {

    [SerializeField] HandCardsPopUpCardController cardController;

    [SerializeField] GameObject selectedPlayCard; // フェードアウトさせるカード
    [SerializeField] Image elderSignPrefab; // フェードインさせるElderSign
    [SerializeField] Image targetElderSign; // HierarchyにあるElderSignの最終目的地
    [SerializeField] TextMeshProUGUI numText; // ElderSignの子Object NumText
    [SerializeField] Image[] diceAncherList; 

    [SerializeField] float fadeDuration = 2f;
    [SerializeField] float moveDuration = 1f;

    public void PlayEffect() {
        StartCoroutine(EffectSequence());
    }

    private IEnumerator EffectSequence() {

        //ElderSignをフェードイン
        Image elderSignInstance = Instantiate(elderSignPrefab, selectedPlayCard.transform.parent);
        elderSignInstance.rectTransform.position = selectedPlayCard.GetComponent<RectTransform>().position;
        elderSignInstance.color = new Color(elderSignInstance.color.r, elderSignInstance.color.g, elderSignInstance.color.b, 0f);
        Image cardImage = selectedPlayCard.GetComponent<Image>();
        if (cardImage != null) {

            Coroutine fadeIn = StartCoroutine(FadeImage(elderSignInstance, 0f, 1f, fadeDuration));
            Coroutine fadeList1 = StartCoroutine(FadeImage(diceAncherList[0], 1f, 0f, fadeDuration));
            Coroutine fadeList2 = StartCoroutine(FadeImage(diceAncherList[1], 1f, 0f, fadeDuration));
            Coroutine fadeList3 = StartCoroutine(FadeImage(diceAncherList[2], 1f, 0f, fadeDuration));
            Coroutine fadeOut = StartCoroutine(FadeImage(cardImage, 1f, 0f, fadeDuration));

            yield return fadeIn;
            yield return fadeList1;
            yield return fadeList2;
            yield return fadeList3;
            yield return fadeOut;
        }

        //ElderSignをtargetElderSignの位置へ移動
        Vector3 startPos = elderSignInstance.rectTransform.position;
        Vector3 endPos = targetElderSign.rectTransform.position;
        float elapsed = 0f;
        while (elapsed < moveDuration) {
            elapsed += Time.deltaTime;
            elderSignInstance.rectTransform.position = Vector3.Lerp(startPos, endPos, elapsed / moveDuration);
            yield return null;
        }
        elderSignInstance.rectTransform.position = endPos;

        //位置到達後、移動させたElderSignを非表示にして元の位置に戻す
        elderSignInstance.gameObject.SetActive(false);
        elderSignInstance.rectTransform.position = startPos;

        //NumTextの数字を1上昇
        if (numText != null) {
            if (int.TryParse(numText.text, out int currentValue)) {
                numText.text = (currentValue + 1).ToString();
            }
        }

        //selectedPlayCardを非表示にしてアルファを戻す
        if (cardImage != null) {
            selectedPlayCard.SetActive(false);
            cardImage.color = new Color(cardImage.color.r, cardImage.color.g, cardImage.color.b, 1f);
        }

        cardController.StartMyProcess();
    }

    private IEnumerator FadeImage(Image image, float startAlpha, float endAlpha, float duration) {

        float elapsed = 0f;
        Color c = image.color;
        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            image.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }
        image.color = new Color(c.r, c.g, c.b, endAlpha);
    }
}
