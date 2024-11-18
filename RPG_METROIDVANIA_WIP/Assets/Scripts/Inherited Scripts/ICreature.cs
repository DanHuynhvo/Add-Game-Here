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
    bool IsTheirTurn { get; set; }
    Sprite Sprite { get; set; }
    Collider2D Collider { get; set; }
    List<ResourceCost> Resources { get; set; }
    Dictionary<string, int> ResourceDict { get; set; }
    Dictionary<string, int> Conditions { get; set; }
    GameObject Creature { get; }

    void takeDamage(float damage);
    void hasDied();
    void checkSceneCollider();
}
