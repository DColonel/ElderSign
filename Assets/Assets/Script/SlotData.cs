using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class SlotData {

    public anchorValue anchor; // UI上のアンカーを直接参照
    public DiceValue requiredDice;
}

public enum DiceValue {

    Attack1,
    Attack2,
    Attack3,
    Skull,
    Idea,
    Shield
}

public enum anchorValue {

    Ancher1,
    Ancher2,
    Ancher3,
    Ancher4,
}
