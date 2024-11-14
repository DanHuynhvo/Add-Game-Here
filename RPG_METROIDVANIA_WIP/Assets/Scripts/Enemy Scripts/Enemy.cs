using UnityEngine;
using System.Collections.Generic;
using UnityEngine.U2D;

public class Enemy : MonoBehaviour, ICreature
{
    // Private fields with 'e_' prefix for Enemy
    [SerializeField] private int e_ID;
    [SerializeField] private string e_Name;
    [SerializeField] private float e_Health;
    [SerializeField] private bool e_IsDead;
    [SerializeField] private List<ResourceCost> e_Resources = new List<ResourceCost>();
    [SerializeField] private Dictionary<string, int> e_ResourceDict = new Dictionary<string, int>();
    [SerializeField] private Dictionary<string, int> e_Conditions = new Dictionary<string, int>();
    [SerializeField] private Sprite e_Sprite;

    // Implementing the ICreature properties with 'e_' backing fields
    public int ID
    {
        get { return e_ID; }
        set { e_ID = value; }
    }

    public string Name
    {
        get { return e_Name; }
        set { e_Name = value; }
    }

    public float Health
    {
        get { return e_Health; }
        set { e_Health = Mathf.Max(0, value); }  // Ensure health doesn't go below 0
    }

    public bool IsDead
    {
        get { return e_IsDead; }
        set { e_IsDead = value; }
    }

    public List<ResourceCost> Resources
    {
        get { return new List<ResourceCost>(e_Resources); }  // Return a copy to avoid modification of the original list
        set { e_Resources = new List<ResourceCost>(value); }  // Accept a new list of resources
    }

    public Dictionary<string, int> ResourceDict
    {
        get { return new Dictionary<string, int>(e_ResourceDict); }  // Return a copy to avoid external modifications
        set { e_ResourceDict = new Dictionary<string, int>(value); }  // Set a new dictionary
    }

    public Dictionary<string, int> Conditions
    {
        get { return new Dictionary<string, int>(e_Conditions); }  // Return a copy to avoid external modifications
        set { e_Conditions = new Dictionary<string, int>(value); }  // Set a new dictionary
    }

    public Sprite Sprite
    {
        get { return e_Sprite; }
        set { e_Sprite = value; }
    }

    private void Awake()
    {
        // Clear the existing dictionary
        e_ResourceDict.Clear();

        // Populate the dictionary with the serialized data
        // Assuming you want to add resources to e_ResourceDict
        foreach (var resource in e_Resources) // Assuming e_Resources is a List<ResourceCost>
        {
            // Assuming ResourceCost has resourceName and cost properties
            e_ResourceDict[resource.resourceName] = resource.cost;
        }

        // Ensure sprite is always set for a creature
        if (gameObject.transform.GetComponent<SpriteRenderer>())
        {
            e_Sprite = gameObject.transform.GetComponent<SpriteRenderer>().sprite;
        }

        this.ID = gameObject.GetInstanceID();
    }
}
