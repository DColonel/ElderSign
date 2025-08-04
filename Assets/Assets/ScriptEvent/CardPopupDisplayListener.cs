using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPopupDisplayListener : MonoBehaviour {

    //���̃C�x���g�ɂ���ē���������������SerializedField�ɓo�^����
    [SerializeField] CardPopupDisplayController cardPopupDisplayController;

    void OnEnable() {
        CardPopupDisplayEvent.Instance.CardPopupDisplay += HandlePopupCard;
    }

    void OnDisable() {
        CardPopupDisplayEvent.Instance.CardPopupDisplay -= HandlePopupCard;
    }

    //�C�x���g�����΂������ɁA���̒��ɂ��鋓�����J�n�����
    void HandlePopupCard() {

        cardPopupDisplayController.CardPopupDisplay();
    }
}
