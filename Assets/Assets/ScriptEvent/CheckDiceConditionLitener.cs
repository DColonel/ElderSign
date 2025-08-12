using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDiceConditionLitener : MonoBehaviour
{
    //このイベントによって動かしたい挙動をSerializedFieldに登録する
    [SerializeField] CheckDiceCondition checkDiceCondition;

    IEnumerator RegisterListener() {
        while (CheckDiceConditionEvent.Instance == null) {
            yield return null;
        }
        CheckDiceConditionEvent.Instance.CheckDice += Handle;
    }

    void OnEnable() {
        StartCoroutine(RegisterListener());
    }

    void OnDisable() {
        CheckDiceConditionEvent.Instance.CheckDice -= Handle;
    }

    //イベントが発火した時に、この中にある挙動が開始される
    void Handle() {

        checkDiceCondition.checkDice();
    }
}
