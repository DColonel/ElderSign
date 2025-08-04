using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSelectArrowMoved : MonoBehaviour {

    /*===========Core===========*/
    [SerializeField] RectTransform arrowGroup;
    [SerializeField] List<Image> arrowObject;
    [SerializeField] List<RectTransform> arrowPos;
    bool isProcess = false;
    float floatDuration = 1.5f;

    List<Vector2> originalPos = new List<Vector2>();

    /*==========�J�n���ɋN��==========*/
    private void Start() {

        for (int i = 0; i < arrowPos.Count; i++) {
            originalPos.Add(arrowPos[i].anchoredPosition);
        }
    }

    /*===========�N��===========*/
    public void StartMyProcess(RectTransform targetPos) {

        isProcess = true;
        arrowGroup.anchoredPosition = targetPos.anchoredPosition;

        //����for�ŋN�����ē����ɓ������Ă�
        for (int i = 0; i < arrowPos.Count; i++) {
            arrowObject[i].enabled = true;
            StartCoroutine(FloatLoop(arrowPos[i], originalPos[i]));
        }
    }

    /*===========��~============*/
    public void StopMyProcess() {

        //���𕡐�for�Œ�~����
        isProcess = false;
        for (int i = 0; i < arrowPos.Count; i++) {
            arrowObject[i].enabled = false;
        }
    }

    private IEnumerator FloatLoop(RectTransform arrow, Vector2 origin) {

        while (isProcess) {

            Vector2 targetUp = origin + GetPosX(arrow.anchoredPosition.x) + GetPosY(arrow.anchoredPosition.y);
            yield return StartCoroutine(MoveOverTime(arrow, origin, targetUp, floatDuration));

            Vector2 targetDown = origin;
            yield return StartCoroutine(MoveOverTime(arrow, targetUp, targetDown, floatDuration));
        }
    }

    private IEnumerator MoveOverTime(RectTransform arrow, Vector2 from, Vector2 to, float duration) {

        float elapsed = 0f;
        while (elapsed < duration) {
            float t = elapsed / duration;
            arrow.anchoredPosition = Vector2.Lerp(from, to, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        arrow.anchoredPosition = to;
    }

    private Vector2 GetPosX(float currentPos) {

        if (currentPos > 0) {
            return new Vector2(10f, 0);
        }
        else if (currentPos < 0) {
            return new Vector2(-10f, 0);
        }
        else {
            return Vector2.zero;
        }
    }

    private Vector2 GetPosY(float currentPos) {

        if (currentPos > 0) {
            return new Vector2(0, 10f);
        }
        else if (currentPos < 0) {
            return new Vector2(0, -10f);
        }
        else {
            return Vector2.zero;
        }
    }
}
