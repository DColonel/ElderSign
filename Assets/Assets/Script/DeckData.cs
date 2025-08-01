using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckData : MonoBehaviourÅ@{

    private List<Card> deck = new List<Card>();
    private List<Card> tableCard = new List<Card>();

    public List<Card> Deck { get => deck; set => deck = value; }
    public List<Card> TableCard { get => tableCard; set => tableCard = value; }

    private void Update() {
    }
}
