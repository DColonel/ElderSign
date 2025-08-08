using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesConfirmListener : MonoBehaviour
{
    //このイベントによって動かしたい挙動をSerializedFieldに登録する
    [SerializeField] YesConfirmedAfterController yesConfirmAfterController;

    IEnumerator RegisterListener() {
        while (YesConfirmEvent.Instance == null) {
            yield return null;
        }
        YesConfirmEvent.Instance.YesConfirmed += Handle;
    }

    void OnEnable() {
        StartCoroutine(RegisterListener());
    }

    void OnDisable() {
        YesConfirmEvent.Instance.YesConfirmed -= Handle;
    }

    //イベントが発火した時に、この中にある挙動が開始される
    void Handle() {
        yesConfirmAfterController.PlayDiceSet();
    }
}
