using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{

    public static GameEvents current;

    public Action onPlayerCast;
    public Action onPlayerDisabledComponents;
    public Action onCardChosen;
    public Action onBattleStart;
    public Action onFilledDeck;
    public Action onFilledHand;

    public void Awake()
    {
        current = this;
    }
    public void PlayerCast()
    {
        if (onPlayerCast != null)
        {
            onPlayerCast();
        }
    }

    public void PlayerDisabledComponents()
    {
        if (onPlayerDisabledComponents != null)
        {
            onPlayerDisabledComponents();
        }
    }

    public void CardChosen()
    {
        if (onCardChosen != null)
        {
            onCardChosen();
        }
    }

    public void BattleStart()
    {
        if (onBattleStart != null)
        {
            onBattleStart();
        }
    }

    public void FilledDeck()
    {
        if (onFilledDeck != null)
        {
            onFilledDeck();
        }
    }

    public void FilledHand()
    {
        if (onFilledHand != null)
        {
            onFilledHand();
        }
    }

    // Example of delegate
    // delegate void onPlayerDeath();
    // public static OnPlayerDeath onPlayerDeath;

    // private void Start ()
    // { onPlayerDeath += FunctionThatDoesSomething; }

    // private void OnEnable
    // { onPlayerDeath += FunctionThatDoesSomething; }

    // private void OnDisable
    // { onPlayerDeath -= FunctionThatDoesSomething; }

    // Example of C# Event
    // public static event Action onPlayerDeath;
    // public static event Action<float> onPlayerHurt;

    // public void RemoveHealth(float amount)
    // {
    //      hp -= amount;

    //      onPlayerHurt?.invoke();
    // }

    /*public static GameEvents current; // Working Example

    public Action onPlayerCast;

    public void Awake()
    {
        current = this;
    }
    public void PlayerCast()
    {
        if (onPlayerCast != null)
        {
            onPlayerCast();
        }
    }*/

}
