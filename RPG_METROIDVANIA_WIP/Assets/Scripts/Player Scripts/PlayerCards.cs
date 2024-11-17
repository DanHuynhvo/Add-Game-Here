using UnityEngine;
using System.Collections.Generic;

public class PlayerCards : MonoBehaviour    // Container to hold and maintain hand and deck (card storage), card function and turns handled elsewhere
{
    [SerializeField] private List<Card> playerHand = new List<Card>();
    [SerializeField] private List<Card> playerDeck = new List<Card>();
    [SerializeField] private Card demoCard;
    [SerializeField] private int deckSize;

    private void PlaceHolderFill()
    {
        for (int i = 0; i < deckSize; i++)
        {
            playerDeck.Add(demoCard);
        }
    }

    private void Awake()
    {
        PlaceHolderFill();
    }
}
