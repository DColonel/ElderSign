using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardPopupDisplayEvent : MonoBehaviour {

    public static CardPopupDisplayEvent Instance;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    /*==========イベント定義===========*/
    public event Action CardPopupDisplay;

    /*==========イベント発火===========*/
    public void TriggerCardPopupDisplay() {

        CardPopupDisplay?.Invoke();
    }
}