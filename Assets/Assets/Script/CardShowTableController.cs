using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*==========画面に手札を描写するscript============*/
public class CardShowTableController : MonoBehaviour {

    /*===============Core==============*/
    [SerializeField] List<Image> cardPointGroup;
    [SerializeField] DeckData deckData;
    List<Card> deck;
    List<Card> tableCard;

    /*==============手札の作成と描写==============*/
    public void ShowTableCard() {

        deck = deckData.Deck;
        tableCard = deckData.TableCard;

        //手札の作成
        for (int i = tableCard.Count; i < 6; i++) {
            tableCard.Add(deck[0]);
            deck.RemoveAt(0);
        }

        //手札の描写
        for (int i = 0; i < 6; i++) {
            cardPointGroup[i].sprite = tableCard[i].CardImage;
            cardPointGroup[i].GetComponent<CardAttachedCardData>().cardData = tableCard[i];
        }

        deckData.Deck = deck;
        deckData.TableCard = tableCard;
    }
}
