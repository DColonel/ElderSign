using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardShuffleListener : MonoBehaviour {

    //このイベントによって動かしたい挙動をSerializedFieldに登録する
    [SerializeField] private CardShowTableController showTable;

    IEnumerator RegisterListener() {
        while (CardShuffleEvent.Instance == null) {
            yield return null;
        }
        CardShuffleEvent.Instance.OnDeckShuffled += HandleDrawnCard;
    }

    void OnEnable() {
        StartCoroutine(RegisterListener());
    }

    void OnDisable() {
        CardShuffleEvent.Instance.OnDeckShuffled -= HandleDrawnCard;
    }

    //イベントが発火した時に、この中にある挙動が開始される
    void HandleDrawnCard() {

        showTable.ShowTableCard();
    }
}
