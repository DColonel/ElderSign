using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card")]

public class Card : ScriptableObject {

    [SerializeField] public Sprite CardImage;
    public List<SlotData> slot1;
    public List<SlotData> slot2;
    public List<SlotData> slot3;
}

