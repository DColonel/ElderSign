using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*==========ダイスobjectにアタッチすると、自身の上面をキャッチしてくれる===========*/
public class DiceTopFaceChecker : MonoBehaviour {

    [SerializeField] GameObject[] faces; // 6面のGameObjectをInspectorで登録
    [SerializeField] public string topFaceName;
    [SerializeField] Rigidbody rb;

    float stopThreshold = 0.05f;
    float stopTimeRequired = 0.5f;
    float stopTimer = 0f;

    List<float> velocityHistory = new List<float>();
    List<float> angularVelocityHistory = new List<float>();

    float checkDuration = 1f; // 1秒間履歴を取る想定
    int maxHistoryCount => Mathf.CeilToInt(checkDuration / Time.deltaTime);

    bool hasStopped = false;
    RollDiceController diceRollController;
    DiceResultCollector diceResultCollector;

    void Start() {

        // シーン上にあるGameManagerオブジェクトをFindしてDiceRollControllerを取得
        GameObject gameManager = GameObject.Find("GameManager");
        if (gameManager != null) {

            diceRollController = gameManager.GetComponent<RollDiceController>();
            diceResultCollector = gameManager.GetComponent<DiceResultCollector>();
        }
    }

    void Update() {

        if (faces.Length == 0 || rb == null || diceRollController == null) return;

        if (!hasStopped && diceRollController.diceRollAllowedEvent) {

            // 速度と角速度の履歴に追加
            velocityHistory.Add(rb.velocity.magnitude);
            angularVelocityHistory.Add(rb.angularVelocity.magnitude);

            // 履歴が多すぎたら古いものを削除
            if (velocityHistory.Count > maxHistoryCount) velocityHistory.RemoveAt(0);
            if (angularVelocityHistory.Count > maxHistoryCount) angularVelocityHistory.RemoveAt(0);

            // 平均を計算
            float avgVelocity = 0f;
            float avgAngularVelocity = 0f;
            if (velocityHistory.Count > 0) avgVelocity = velocityHistory.Average();
            if (angularVelocityHistory.Count > 0) avgAngularVelocity = angularVelocityHistory.Average();

            // Rigidbodyが完全に静止してるか、または平均速度・角速度が閾値以下かで判定
            if (rb.IsSleeping() || (avgVelocity < stopThreshold && avgAngularVelocity < stopThreshold)) {

                stopTimer += Time.deltaTime;

                if (stopTimer >= stopTimeRequired) {

                    StartCoroutine(WaitAndCheckTopFace());
                    hasStopped = true;
                }
            } else {
                stopTimer = 0f;
            }
        }
    }

    // 完全に停止したかを判定するための0.4秒待機
    IEnumerator WaitAndCheckTopFace() {

        yield return new WaitForSeconds(0.4f);

        // 再判定
        if (rb.velocity.magnitude >= stopThreshold || rb.angularVelocity.magnitude >= stopThreshold) {

            // 再判定できるように戻す
            hasStopped = false;
            yield break;
        }
        CheckTopFace();
    }

    void CheckTopFace() {

        if (faces == null || faces.Length == 0) return;

        Ray ray = new Ray(this.transform.position, Vector3.back);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2f)) { // 2fは届く距離の例。調整してね。

            // 当たったオブジェクトの名前を取得
            GameObject hitObject = hit.collider.gameObject;

            topFaceName = hitObject.name;
            diceResultCollector.topFaceNames.Add(topFaceName);
            Debug.Log($"停止判定: 上方向にある面は {topFaceName}");
        } else {
            Debug.LogWarning("停止判定: Raycastで面を検出できませんでした");
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, Vector3.back * 3f);
    }
}
