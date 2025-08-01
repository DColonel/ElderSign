using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardShowTableController : MonoBehaviour {

    /*===============Core==============*/
    [SerializeField] List<Image> cardPointGroup;
    [SerializeField] DeckData deckData;
    List<Card> deck;
    List<Card> tableCard;

    /*==============èD‚Ìì¬‚Æ•`Ê==============*/
    public void ShowTableCard() {

        deck = deckData.Deck;
        tableCard = deckData.TableCard;

        //èD‚Ìì¬
        for (int i = tableCard.Count; i < 6; i++) {
            tableCard.Add(deck[0]);
            deck.RemoveAt(0);
        }

        //èD‚Ì•`Ê
        for (int i = 0; i < 6; i++) {
            cardPointGroup[i].sprite = tableCard[i].CardImage;
        }

        deckData.Deck = deck;
        deckData.TableCard = tableCard;
    }
}
