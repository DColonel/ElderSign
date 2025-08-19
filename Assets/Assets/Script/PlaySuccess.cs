using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [SerializeField] private DicePlacementController dicePlacementController;

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

                    if (clickedImage == slot1Image) {
                        dicePlacementController.PlaceDiceOnSlot(1);
                    }
                    else if (clickedImage == slot2Image) {
                        dicePlacementController.PlaceDiceOnSlot(2);
                    }
                    else if (clickedImage == slot3Image) {
                        dicePlacementController.PlaceDiceOnSlot(3);
                    }

                    StopAllCoroutines();
                    break;
                }
            }
        }
    }

    public void DicePlacement() {

        StopAllCoroutines();
        activeHighlightedImages.Clear();

        if (diceCondition.slot1Met) {
            activeHighlightedImages.Add(slot1Image);
            slot1Coroutine = StartCoroutine(BlinkImageAlpha(slot1Image));
        }

        if (diceCondition.slot2Met) {
            activeHighlightedImages.Add(slot2Image);
            slot2Coroutine = StartCoroutine(BlinkImageAlpha(slot2Image));
        }

        if (diceCondition.slot3Met) {
            activeHighlightedImages.Add(slot3Image);
            slot3Coroutine = StartCoroutine(BlinkImageAlpha(slot3Image));
        }

        waitingForClick = activeHighlightedImages.Count > 0;
    }

    private IEnumerator BlinkImageAlpha(Image image, float minAlpha = 0f, float maxAlpha = 0.4f, float interval = 1f) {

        if (image == null) yield break;

        while (true) {
            // 現在の色を取って編集する
            var c = image.color;
            c.a = maxAlpha;
            image.color = c;
            yield return new WaitForSeconds(interval);

            c = image.color;
            c.a = minAlpha;
            image.color = c;
            yield return new WaitForSeconds(interval);
        }
    }
}