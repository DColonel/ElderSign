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

    /*==========�C�x���g��`===========*/
    public event Action OnDeckShuffled;

    /*==========�C�x���g����===========*/
    public void TriggerDeckShuffled() {

        OnDeckShuffled.Invoke();
    }
}
