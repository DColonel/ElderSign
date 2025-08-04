using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPopupDisplayListener : MonoBehaviour {

    //このイベントによって動かしたい挙動をSerializedFieldに登録する
    [SerializeField] CardPopupDisplayController cardPopupDisplayController;

    void OnEnable() {
        CardPopupDisplayEvent.Instance.CardPopupDisplay += HandlePopupCard;
    }

    void OnDisable() {
        CardPopupDisplayEvent.Instance.CardPopupDisplay -= HandlePopupCard;
    }

    //イベントが発火した時に、この中にある挙動が開始される
    void HandlePopupCard() {

        cardPopupDisplayController.CardPopupDisplay();
    }
}
