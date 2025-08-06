using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*=============カードを選択した際の最終確認の選択肢を動かして、選択肢の結果移行するためのScript============*/
public class PlayCardSelectedController : MonoBehaviour
{

    /*============Core=============*/
    [SerializeField] GameObject textYesBG;
    [SerializeField] GameObject textNoBG;
    [SerializeField] GameObject selectedArrow;

    [SerializeField] private GraphicRaycaster raycaster;
    [SerializeField] private EventSystem eventSystem;

    PointerEventData pointerData;
    List<RaycastResult> results;

    /*==========初期化============*/
    private void Start() {

        selectedArrow.SetActive(false);
        pointerData = new PointerEventData(eventSystem);
        results = new List<RaycastResult>();
    }

    /*==========マウスがObjectに接触したら矢印を出す===========*/
    void Update(){

        pointerData.position = Input.mousePosition;
        results.Clear();

        raycaster.Raycast(pointerData, results);

        //RayCastの結果Objectが0以上ある場合
        if (results.Count > 0) {

            //はいの上にマウスがある
            if (results[0].gameObject == textYesBG) {

                selectedArrow.SetActive(true);
                selectedArrow.GetComponent<RectTransform>().anchoredPosition = new Vector2(260f, -75f);

                //もしこの時にマウスをクリックしたらイベントを発火させてカード攻略に進む
                if (Input.GetMouseButtonDown(0)) {
                    YesConfirmEvent.Instance.TriggerYesConfirmed();
                }

            }//いいえの上にマウスがある
            else if (results[0].gameObject == textNoBG) {

                selectedArrow.SetActive(true);
                selectedArrow.GetComponent<RectTransform>().anchoredPosition = new Vector2(260f, -205f);

                //もしこの時にマウスをクリックしたらイベントを発火させてリセットする
                if (Input.GetMouseButtonDown(0)) {
                    NotConfirmEvent.Instance.TriggerNotConfirmed();
                }

            }//どちらにも無い場合
            else if (results[0].gameObject != textYesBG && results[0].gameObject != textNoBG) {
                selectedArrow.SetActive(false);
            }
            else {
            }
        }
    }
}
