using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*=========プレイするカードの確認時、はいを選んだ時にテキストを消してダイスの用意をするためのEvent=========*/
public class YesConfirmEvent : MonoBehaviour
{
    public static YesConfirmEvent Instance;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    /*==========イベント定義===========*/
    public event Action YesConfirmed;

    /*==========イベント発火===========*/
    public void TriggerYesConfirmed() {

        YesConfirmed?.Invoke();
    }
}
