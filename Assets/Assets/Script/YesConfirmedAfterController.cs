using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*==========手札の中から選んだプレイカードを確定した時の処理==========*/
public class YesConfirmedAfterController : MonoBehaviour
{

    [SerializeField] GameObject confirmCardPlay;
    [SerializeField] GameObject DiceGroup;

    public void PlayDiceSet() {



        confirmCardPlay.SetActive(false);
        DiceGroup.SetActive(true);
    }
}
