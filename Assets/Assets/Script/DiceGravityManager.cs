using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*===========================*/
public class DiceGravityManager : MonoBehaviour {

    [SerializeField] Transform dicePoint;
    [SerializeField] Vector3 customGravity = new Vector3(0, 0, 9.81f);

    void FixedUpdate() {

        if (dicePoint.childCount == 0) return;

        for (int i = 0; i < dicePoint.childCount; i++) {
            Transform child = dicePoint.GetChild(i);
            Rigidbody rb = child.GetComponent<Rigidbody>();

            if (rb != null) {
                rb.useGravity = false; // Unity�̏d�͖͂�����
                rb.AddForce(customGravity, ForceMode.Acceleration); // ����d�͂�K�p
            }
        }
    }
}
