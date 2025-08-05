using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotConfirmListener : MonoBehaviour
{
    //このイベントによって動かしたい挙動をSerializedFieldに登録する
    [SerializeField] CardHideDisplayController cardHideDisplayController;
    [SerializeField] HandCardsPopUpCardController handCardsPopUpCardController;

    IEnumerator RegisterListener() {
        while (NotConfirmEvent.Instance == null) {
            yield return null;
        }
        NotConfirmEvent.Instance.NotConfirmed += Handle;
    }

    void OnEnable() {
        StartCoroutine(RegisterListener());
    }

    void OnDisable() {
        NotConfirmEvent.Instance.NotConfirmed -= Handle;
    }

    //イベントが発火した時に、この中にある挙動が開始される
    void Handle() {

        cardHideDisplayController.CardHideDisplay();
        handCardsPopUpCardController.StartMyProcess();
    }
}
