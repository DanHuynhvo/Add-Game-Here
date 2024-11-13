using System.Collections.Generic;
using UnityEngine;

public interface ICardEffectable
{
    void UseEffect(List<GameObject> targets, Card card);
}
