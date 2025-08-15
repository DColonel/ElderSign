using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*============プレイの結果、成功した場合==========*/
public class PlaySuccess : MonoBehaviour {
    [SerializeField] private Image slot1Image;
    [SerializeField] private Image slot2Image;
    [SerializeField] private Image slot3Image;

    [SerializeField] private CheckDiceCondition diceCondition;
    [SerializeField] private DiceResultCollector diceResults;

    [SerializeField] private GraphicRaycaster raycaster;
    [SerializeField] private EventSystem eventSystem;

    [SerializeField] private DicePlacementcontroller dicePlacementController;

    private Coroutine slot1Coroutine;
    private Coroutine slot2Coroutine;
    private Coroutine slot3Coroutine;

    private bool waitingForClick = false;
    private List<Image> activeHighlightedImages = new List<Image>();

    void Update() {

        if (!waitingForClick) return;

        if (Input.GetMouseButtonDown(0)) {
            PointerEventData pointerData = new PointerEventData(eventSystem) {
                position = Input.mousePosition
            };

            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(pointerData, results);

            foreach (var result in results) {
                Image clickedImage = result.gameObject.GetComponent<Image>();
                if (clickedImage != null && activeHighlightedImages.Contains(clickedImage)) {
                    Debug.Log("Clicked on slot image: " + clickedImage.name);
                    waitingForClick = false;

                    /*if (clickedImage == slot1Image) {
                        dicePlacementController.PlaceDiceOnSlot(1);
                    }
                    else if (clickedImage == slot2Image) {
                        dicePlacementController.PlaceDiceOnSlot(2);
                    }
                    else if (clickedImage == slot3Image) {
                        dicePlacementController.PlaceDiceOnSlot(3);
                    }*/

                    StopAllCoroutines();

                    ResetImageAlpha(slot1Image);
                    ResetImageAlpha(slot2Image);
                    ResetImageAlpha(slot3Image);
                    break;
                }
            }
        }
    }

    public void DicePlacement() {

        StopAllCoroutines();
        activeHighlightedImages.Clear();

        if (diceCondition.slot1Met && IsSlotSatisfied(diceCondition.slot1Required)) {
            activeHighlightedImages.Add(slot1Image);
            slot1Coroutine = StartCoroutine(BlinkImageAlpha(slot1Image));
        }
        else {
            ResetImageAlpha(slot1Image);
        }

        if (diceCondition.slot2Met && IsSlotSatisfied(diceCondition.slot2Required)) {
            activeHighlightedImages.Add(slot2Image);
            slot2Coroutine = StartCoroutine(BlinkImageAlpha(slot2Image));
        }
        else {
            ResetImageAlpha(slot2Image);
        }

        if (diceCondition.slot3Met && IsSlotSatisfied(diceCondition.slot3Required)) {
            activeHighlightedImages.Add(slot3Image);
            slot3Coroutine = StartCoroutine(BlinkImageAlpha(slot3Image));
        }
        else {
            ResetImageAlpha(slot3Image);
        }

        waitingForClick = activeHighlightedImages.Count > 0;
    }

    private bool IsSlotSatisfied(List<string> requiredFaces) {

        if (requiredFaces.Count > diceResults.topFaceNames.Count) return false;

        int matchCount = 0;
        for (int i = 0; i < requiredFaces.Count; i++) {
            if (diceResults.topFaceNames.Contains(requiredFaces[i])) {
                matchCount++;
            }
        }
        return matchCount == requiredFaces.Count;
    }

    private void ResetImageAlpha(Image image) {

        if (image == null) return;

        Color c = image.color;
        c.a = 0f;
        image.color = c;

        if (image == slot1Image && slot1Coroutine != null) {
            StopCoroutine(slot1Coroutine);
            slot1Coroutine = null;
        }
        else if (image == slot2Image && slot2Coroutine != null) {
            StopCoroutine(slot2Coroutine);
            slot2Coroutine = null;
        }
        else if (image == slot3Image && slot3Coroutine != null) {
            StopCoroutine(slot3Coroutine);
            slot3Coroutine = null;
        }
    }

    private IEnumerator BlinkImageAlpha(Image image, float minAlpha = 0f, float maxAlpha = 0.4f, float interval = 1f) {

        if (image == null) yield break;

        Color baseColor = image.color;
        while (true) {
            // アルファをmaxAlphaにして待機
            baseColor.a = maxAlpha;
            image.color = baseColor;
            yield return new WaitForSeconds(interval);

            // アルファをminAlphaにして待機
            baseColor.a = minAlpha;
            image.color = baseColor;
            yield return new WaitForSeconds(interval);
        }
    }
}