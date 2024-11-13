using System.Collections.Generic;
using UnityEngine;

public class DaggerSlash : MonoBehaviour, ICardEffectable
{
    public void UseEffect(List<GameObject> targets, Card card)
    {
        Debug.Log("STAB");
    }
}
