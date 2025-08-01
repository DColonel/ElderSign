using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOnCardController : MonoBehaviour  {

    [SerializeField] List<RectTransform> Card;
    [SerializeField] CardSelectArrowMoved arrowMoved;
    [SerializeField] ClickedMoveCardController clickedMoveCardController;

    bool mouseOnCard = false;
    RectTransform targetPos;

    void Update() {

        if (MouseOnAnyCard()) {
            arrowMoved.StartMyProcess(targetPos);
            mouseOnCard = true;
        }else {
            arrowMoved.StopMyProcess();
            mouseOnCard = false;
        }

        if (mouseOnCard && Input.GetMouseButtonDown(0)) {
            clickedMoveCardController.StartMyProcess();
        }
    }

    private bool MouseOnAnyCard() {

        Vector2 mousePosition = Input.mousePosition;

        for(int i = 0; i < Card.Count; i++) {
            if (Card[i] != null && RectTransformUtility.RectangleContainsScreenPoint(Card[i], mousePosition)) {
                targetPos = Card[i];
                return true;
            }
        }

        return false;
    }
}
