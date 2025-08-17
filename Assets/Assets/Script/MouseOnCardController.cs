using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*==========カードの真上にマウスが存在するとき、クリックした時それぞれの処理============*/
public class MouseOnCardController : MonoBehaviour {

    /*=============Core============*/
    [SerializeField] List<RectTransform> Card;
    [SerializeField] CardSelectArrowMoved arrowMoved;
    [SerializeField] HandCardsHideCardController handCardsHideCardController;

    [SerializeField] private GraphicRaycaster raycaster;
    [SerializeField] private EventSystem eventSystem;

    bool mouseOnCard = false;
    RectTransform targetPos;
    public Card card;

    void Update() {

        //CardSelectArrowMovedを起動する
        if (MouseOnAnyCard()) {
            arrowMoved.StartMyProcess(targetPos);
            mouseOnCard = true;

            //CardSelectArrowMovedを停止する
        } else {
            arrowMoved.StopMyProcess();
            mouseOnCard = false;
        }

        //カードを下に移動動かすcontrollerを起動し、クリックしたカード情報を保存する
        if (mouseOnCard && Input.GetMouseButtonDown(0)) {
            handCardsHideCardController.StartMyProcess();

            //PointerEventDataを作成
            PointerEventData pointerData = new PointerEventData(eventSystem) {
                position = Input.mousePosition
            };

            //UI上のヒット結果を格納
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            foreach (var result in results) {
                Debug.Log($"Hit: {result.gameObject.name}");
            }

            for (int i = 0; i < results.Count; i++) {

                GameObject hitObject = results[i].gameObject;

                //ヒットしたオブジェクトの中にCardAttachedCardDataがあれば拾う
                Transform t = hitObject.transform;
                while (t != null) {
                    var attachedObject = t.GetComponent<CardAttachedCardData>();
                    if (attachedObject != null) {
                        card = attachedObject.cardData;
                        break;
                    }
                    t = t.parent;
                }
            }

            //カードデータ取得後にイベント発火
            CardPopupDisplayEvent.Instance.TriggerCardPopupDisplay();
        }
    }

    private bool MouseOnAnyCard() {

        Vector2 mousePosition = Input.mousePosition;

        for (int i = 0; i < Card.Count; i++) {
            if (Card[i] != null && RectTransformUtility.RectangleContainsScreenPoint(Card[i], mousePosition)) {
                targetPos = Card[i];
                return true;
            }
        }

        return false;
    }
}
