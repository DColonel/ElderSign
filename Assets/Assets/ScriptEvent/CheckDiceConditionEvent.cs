using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*=========ダイスの出目が全て揃った後、カードの条件を満たすかを確認するフェーズに以降するためのEvent=========*/
public class CheckDiceConditionEvent : MonoBehaviour {

      public static CheckDiceConditionEvent Instance;

    private void Awake() {

        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    /*==========イベント定義===========*/
    public event Action CheckDice;

    /*==========イベント発火===========*/
    public void TriggerCheckDiced() {

        CheckDice?.Invoke();
    }
}