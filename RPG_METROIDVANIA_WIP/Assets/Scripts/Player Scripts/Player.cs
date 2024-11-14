using UnityEngine;
using System.Collections.Generic;
using UnityEngine.U2D;

public class Player : MonoBehaviour, ICreature
{
    // Private fields with 'p_' prefix for Player
    [SerializeField] private int p_ID;
    [SerializeField] private string p_Name;
    [SerializeField] private float p_Health;
    [SerializeField] private bool p_IsDead;
    [SerializeField] private List<ResourceCost> p_Resources = new List<ResourceCost>();
    [SerializeField] private Dictionary<string, int> p_ResourceDict = new Dictionary<string, int>();
    [SerializeField] private Dictionary<string, int> p_Conditions = new Dictionary<string, int>();
    [SerializeField] private Sprite p_Sprite;

    // Implementing the ICreature properties with 'p_' backing fields
    public int ID
    {
        get { return p_ID; }
        set { p_ID = value; }
    }

    public string Name
    {
        get { return p_Name; }
        set { p_Name = value; }
    }

    public float Health
    {
        get { return p_Health; }
        set { p_Health = Mathf.Max(0, value); }  // Ensure health doesn't go below 0
    }

    public bool IsDead
    {
        get { return p_IsDead; }
        set { p_IsDead = value; }
    }

    public List<ResourceCost> Resources
    {
        get { return new List<ResourceCost>(p_Resources); }  // Return a copy to avoid modification of the original list
        set { p_Resources = new List<ResourceCost>(value); }  // Accept a new list of resources
    }

    public Dictionary<string, int> ResourceDict
    {
        get { return new Dictionary<string, int>(p_ResourceDict); }  // Return a copy to avoid external modifications
        set { p_ResourceDict = new Dictionary<string, int>(value); }  // Set a new dictionary
    }

    public Dictionary<string, int> Conditions
    {
        get { return new Dictionary<string, int>(p_Conditions); }  // Return a copy to avoid external modifications
        set { p_Conditions = new Dictionary<string, int>(value); }  // Set a new dictionary
    }

    public Sprite Sprite
    {
        get { return p_Sprite; }
        set { p_Sprite = value; }
    }

    private void Awake()
    {
        // Clear the existing dictionary
        p_ResourceDict.Clear();

        // Populate the dictionary with the serialized data
        // Assuming you want to add resources to p_ResourceDict
        foreach (var resource in p_Resources) // Assuming p_Resources is a List<ResourceCost>
        {
            // Assuming ResourceCost has resourceName and cost properties
            p_ResourceDict[resource.resourceName] = resource.cost;
        }

        // Ensure sprite is always set for a creature
        if (gameObject.transform.GetComponent<SpriteRenderer>())
        {
            p_Sprite = gameObject.transform.GetComponent<SpriteRenderer>().sprite;
        }

        this.ID = gameObject.GetInstanceID();
    }
}
