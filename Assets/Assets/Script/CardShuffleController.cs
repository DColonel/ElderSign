using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/*========ゲーム開始時、カードを読み込み、山札をシャッフルするためのscript=========*/
public class CardShuffleController : MonoBehaviour {

    /*===========Core==============*/
    public List<Card> deck = new List<Card>();

    void Start() {

        LoadAllCards();
        ShufflDeck();
        DeckData deckData = FindAnyObjectByType<DeckData>();
        deckData.Deck = deck;
        CardShuffleEvent.Instance.TriggerDeckShuffled();
    }

    /*============Resourcesフォルダの中のCardを取得============*/
    void LoadAllCards() {
        deck = Resources.LoadAll<Card>("Card").ToList();
    }

    /*=============DeckListの中身をシャッフル===============*/
    void ShufflDeck() {
        for (int i = 0; i < deck.Count; i++) {
            var temp = deck[i];
            int randomIndex = UnityEngine.Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }
}
