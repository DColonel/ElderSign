using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*=========プレイするカードの確認時、いいえを選んだ時に手札の表示やPopUpの削除をするためのEvent=========*/
public class NotConfirmEvent : MonoBehaviour
{
    public static NotConfirmEvent Instance;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    /*==========イベント定義===========*/
    public event Action NotConfirmed;

    /*==========イベント発火===========*/
    public void TriggerNotConfirmed() {

        NotConfirmed?.Invoke();
    }
}
