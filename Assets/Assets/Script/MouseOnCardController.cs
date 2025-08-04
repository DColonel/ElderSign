using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseOnCardController : MonoBehaviour {

    [SerializeField] List<RectTransform> Card;
    [SerializeField] CardSelectArrowMoved arrowMoved;
    [SerializeField] ClickedMoveCardController clickedMoveCardController;

    [SerializeField] private GraphicRaycaster raycaster;
    [SerializeField] private EventSystem eventSystem;

    bool mouseOnCard = false;
    RectTransform targetPos;
    public Card card;

    void Update() {

        //CardSelectArrowMoved���N������
        if (MouseOnAnyCard()) {
            arrowMoved.StartMyProcess(targetPos);
            mouseOnCard = true;

            //CardSelectArrowMoved���~����
        } else {
            arrowMoved.StopMyProcess();
            mouseOnCard = false;
        }

        //�J�[�h�����Ɉړ�������controller���N�����A�N���b�N�����J�[�h����ۑ�����
        if (mouseOnCard && Input.GetMouseButtonDown(0)) {
            clickedMoveCardController.StartMyProcess();

            //PointerEventData���쐬
            PointerEventData pointerData = new PointerEventData(eventSystem) {
                position = Input.mousePosition
            };

            //UI��̃q�b�g���ʂ��i�[
            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(pointerData, results);

            for (int i = 0; i < results.Count; i++) {

                GameObject hitObject = results[i].gameObject;

                //�q�b�g�����I�u�W�F�N�g�̒���CardAttachedCardData������ΏE��
                CardAttachedCardData attachedObject = hitObject.GetComponent<CardAttachedCardData>();
                if (attachedObject != null && attachedObject.cardData != null) {
                    card = attachedObject.cardData;
                    break;
                }
            }

            //�J�[�h�f�[�^�擾��ɃC�x���g����
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

    /*============�����I����CardData����������p=============*/
    public void CardDataClear() {

        card = null;
    }
}
