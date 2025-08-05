using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardShuffleEvent : MonoBehaviour {

    public static CardShuffleEvent Instance;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    /*==========イベント定義===========*/
    public event Action OnDeckShuffled;

    /*==========イベント発火===========*/
    public void TriggerDeckShuffled() {

        OnDeckShuffled.Invoke();
    }
}
