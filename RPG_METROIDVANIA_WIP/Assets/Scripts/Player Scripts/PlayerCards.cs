using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerCards : MonoBehaviour    // Container to hold and maintain hand and deck (card storage), card function and turns handled elsewhere
{
    [SerializeField] private List<Card> playerHand = new List<Card>();
    [SerializeField] private List<Card> playerDeck = new List<Card>();
    [SerializeField] private Card demoCard;
    [SerializeField] private int deckSize;
    [SerializeField] private int STARTHANDSIZE = 5;



    private void Awake()
    {

    }

    private void Start()
    {
        GameEvents.current.onFilledDeck += StartHand;
        GameEvents.current.onBattleStart += FillDeck;
    }

    public void FillDeck()
    {
        if (demoCard && deckSize > 0)
        {
            for (int i = 0; i < deckSize; i++)
            {
                playerDeck.Add(demoCard);
            }

            GameEvents.current.FilledDeck();
        }

        else
        {
            Console.Error.WriteLine("SOMETHING NOT INITIALIZED WITH DECKSIZE OR THE CARDS");
        }

    }

    public void StartHand()
    {
        Debug.Log("Starting Hand");
        for (int i = 0; i < STARTHANDSIZE; i++)
        {
            playerHand.Add(demoCard);
        }
    }
}
