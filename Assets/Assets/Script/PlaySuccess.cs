using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySuccess : MonoBehaviour {

    [SerializeField] private List<GameObject> slot1Objects;
    [SerializeField] private List<GameObject> slot2Objects;
    [SerializeField] private List<GameObject> slot3Objects;

    [SerializeField] private CheckDiceCondition diceCondition;
    [SerializeField] private DiceResultCollector diceResults;

    private bool waitingForClick = false;
    private List<GameObject> activeHighlightedObjects = new List<GameObject>();

    public void DicePlacement() {

        activeHighlightedObjects.Clear();

        // Slot1
        if (diceCondition.slot1Met && IsSlotSatisfied(diceCondition.slot1Required)) {
            HighlightObjects(slot1Objects, true);
            AddToActive(slot1Objects);
        }

        // Slot2
        if (diceCondition.slot2Met && IsSlotSatisfied(diceCondition.slot2Required)) {
            HighlightObjects(slot2Objects, true);
            AddToActive(slot2Objects);
        }

        // Slot3
        if (diceCondition.slot3Met && IsSlotSatisfied(diceCondition.slot3Required)) {
            HighlightObjects(slot3Objects, true);
            AddToActive(slot3Objects);
        }

        waitingForClick = activeHighlightedObjects.Count > 0;
    }

    bool IsSlotSatisfied(List<string> requiredFaces) {
        if (requiredFaces.Count > diceResults.topFaceNames.Count) return false;

        int matchCount = 0;
        for (int i = 0; i < requiredFaces.Count; i++) {
            if (diceResults.topFaceNames.Contains(requiredFaces[i])) {
                matchCount++;
            }
        }

        return matchCount == requiredFaces.Count;
    }

    void HighlightObjects(List<GameObject> objects, bool enable) {
        for (int i = 0; i < objects.Count; i++) {
            Renderer rend = objects[i].GetComponent<Renderer>();
            if (rend != null) {
                if (enable) {
                    rend.material.color = Color.yellow; // åıÇÁÇπÇÈêF
                } else {
                    rend.material.color = Color.white;  // å≥ÇÃêF
                }
            }
        }
    }

    void AddToActive(List<GameObject> objects) {
        for (int i = 0; i < objects.Count; i++) {
            activeHighlightedObjects.Add(objects[i]);
        }
    }

    void Update() {
        if (waitingForClick && Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                for (int i = 0; i < activeHighlightedObjects.Count; i++) {
                    if (hit.collider.gameObject == activeHighlightedObjects[i]) {
                        Debug.Log("Clicked on slot object: " + activeHighlightedObjects[i].name);
                        waitingForClick = false;
                        HighlightObjects(slot1Objects, false);
                        HighlightObjects(slot2Objects, false);
                        HighlightObjects(slot3Objects, false);
                        break;
                    }
                }
            }
        }
    }
}