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

    /*==============��D�̍쐬�ƕ`��==============*/
    public void ShowTableCard() {

        deck = deckData.Deck;
        tableCard = deckData.TableCard;

        //��D�̍쐬
        for (int i = tableCard.Count; i < 6; i++) {
            tableCard.Add(deck[0]);
            deck.RemoveAt(0);
        }

        //��D�̕`��
        for (int i = 0; i < 6; i++) {
            cardPointGroup[i].sprite = tableCard[i].CardImage;
        }

        deckData.Deck = deck;
        deckData.TableCard = tableCard;
    }
}
