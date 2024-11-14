using System.Collections.Generic;
using UnityEngine;
using static Card;
public interface ICreature
{
    // Property signatures (no field or logic)
    int ID { get; set; }
    string Name { get; set; }
    float Health { get; set; }
    bool IsDead { get; set; }
    Sprite Sprite { get; set; }
    List<ResourceCost> Resources { get; set; }
    Dictionary<string, int> ResourceDict { get; set; }
    Dictionary<string, int> Conditions { get; set; }
}
