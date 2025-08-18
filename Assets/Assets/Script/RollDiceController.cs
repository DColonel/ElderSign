
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*==============マウスがPlayTextの上にある時ダイスをその場で回転させる処理、クリックした時に投げる処理=================*/
public class RollDiceController : MonoBehaviour {

    /*=============Core============*/
    [SerializeField] GameObject playBotton;
    [SerializeField] DiceManager diceManager;
    [SerializeField] Transform dicePoint;

    [SerializeField] private GraphicRaycaster raycaster;
    [SerializeField] private EventSystem eventSystem;

    //Diceのprefab
    [SerializeField] GameObject D6;

    private PointerEventData pointerData;
    private List<RaycastResult> results = new List<RaycastResult>();
    float range = 2.0f;

    public bool createDice;
    bool mouseOnBotton = false;

    //DiceTopFaceCheckerにダイスの上面を記録許可を出すためのbool
    public bool diceRollAllowedEvent = false;

    void Update(){

        //RayCast作成
        if (pointerData == null)
            pointerData = new PointerEventData(eventSystem);

        //毎フレーム更新
        results.Clear();
        pointerData.position = Input.mousePosition;
        raycaster.Raycast(pointerData, results);

        //boolの中身の作成
        mouseOnBotton = results.Count > 0 && results[0].gameObject == playBotton;

        if (mouseOnBotton) {

            if (!createDice) {

                createDice = true;
                SpawnDice();

                for (int i = 0; i < dicePoint.childCount; i++) {
                    ResetChecker(dicePoint.transform.GetChild(i).GetComponent<DiceTopFaceChecker>());
                }
            }

            if (Input.GetMouseButton(0)) {

                for (int i = dicePoint.childCount - 1; i >= 0; i--) {
                    RollDice(dicePoint.GetChild(i));
                    
                }
                diceRollAllowedEvent = true;
            }

        } else if (!mouseOnBotton && !diceRollAllowedEvent) {

            createDice = false;
            DestroyDice();
        }
        else {
        }
    }

    /*============DicePointの子としてダイスを複製する===========*/
    public void SpawnDice() {

        for (int i = 0; i < diceManager.diceBlue; i++) {
            Vector3 randomPos = GetRandomPositionInRange();
            Quaternion randomRot = GetRandomRotation();
            Instantiate(D6, randomPos, randomRot, dicePoint);
        }
    }

    public void DestroyDice() {

        for (int i = dicePoint.childCount - 1; i >= 0; i--) {
            Destroy(dicePoint.GetChild(i).gameObject);
        }
    }

    /*===========ダイスの出現場所の決定===========*/
    private Vector3 GetRandomPositionInRange() {
        float x = Random.Range(-range, range);
        float y = Random.Range(-range, range);
        Vector3 pos = dicePoint.position + new Vector3(x, y, -3);
        return pos;
    }

    /*=========出現時のダイスの角度をランダム算出==========*/
    private Quaternion GetRandomRotation() {
        float x = Random.Range(0f, 360f);
        float y = Random.Range(0f, 360f);
        float z = Random.Range(0f, 360f);
        return Quaternion.Euler(x, y, z);
    }

    /*=========ダイスを回転させる=========*/
    public void RollDice(Transform dice) {

        Rigidbody rb = dice.GetComponent<Rigidbody>();
        if (rb == null) return;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        //ダイスが動く方向
        Vector3 randomForce = new Vector3(
            Random.Range(-3f, 3f),
            Random.Range(-3f, 3f),
            Random.Range(-3f, -2f)
            ) * 1.1f;//都度変更

        rb.AddForce(randomForce, ForceMode.Impulse);

        //ダイスが回転する角度
        Vector3 randomTorque = new Vector3(
            Random.Range(-2f, 2f),
            Random.Range(-2f, 2f),
            Random.Range(-2f, 2f)
        ) * 80f;//都度変更

        rb.AddTorque(randomTorque);
    }

    public void ResetChecker(DiceTopFaceChecker dice) {
        dice.hasStopped = false;
        dice.stopTimer = 0f;
        dice.velocityHistory.Clear();
        dice.angularVelocityHistory.Clear();
        dice.topFaceName = null;
    }
}
