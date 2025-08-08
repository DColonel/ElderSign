using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesConfirmedAfterController : MonoBehaviour
{

    [SerializeField] GameObject confirmCardPlay;
    [SerializeField] GameObject DiceGroup;

    public void PlayDiceSet() {
        confirmCardPlay.SetActive(false);
        DiceGroup.SetActive(true);
    }
}
