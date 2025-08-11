using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*==============3Dオブジェクトに働く重力の方向と強さを設定するための処理=============*/
public class DiceGravityManager : MonoBehaviour {

    [SerializeField] Transform dicePoint;
    [SerializeField] Vector3 customGravity = new Vector3(0, 0, 9.81f);

    void FixedUpdate() {

        if (dicePoint.childCount == 0) return;

        for (int i = 0; i < dicePoint.childCount; i++) {
            Transform child = dicePoint.GetChild(i);
            Rigidbody rb = child.GetComponent<Rigidbody>();

            if (rb != null) {
                rb.useGravity = false; // Unityの重力は無効化
                rb.AddForce(customGravity, ForceMode.Acceleration); // 自作重力を適用
            }
        }
    }
}
