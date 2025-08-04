using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardShuffleListener : MonoBehaviour {

    //���̃C�x���g�ɂ���ē���������������SerializedField�ɓo�^����
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

    //�C�x���g�����΂������ɁA���̒��ɂ��鋓�����J�n�����
    void HandleDrawnCard() {

        showTable.ShowTableCard();
    }
}
