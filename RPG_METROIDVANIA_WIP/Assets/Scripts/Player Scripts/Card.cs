using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [Header("Card Components")]
    [SerializeField] private string cardName;   // string for name
    [SerializeField] private string description;    //short text for card flavor
    [SerializeField] private string type;   // tbd, possibly damage, utility or defence
    [SerializeField] private string aoe;    // string determining what enemies it may target such as 'single' 'row' 'adjacent'
    [SerializeField] private int duration;  // number of turns a card effect should be 
    [SerializeField] private List<float> damage = new List<float>();  // damage card should do. List because certain cards might do different AOE damage
    [SerializeField] private List<ResourceCost> resourceCosts = new List<ResourceCost>();  // List to serialize the resource dictionary
    [SerializeField] private Dictionary<string, int> resourceCostsDict = new Dictionary<string, int>(); // Holds resource costs for cards such as "stamina, 4"
    [SerializeField] private Sprite sprite; // Sprite for card, used for dynamic images
    [SerializeField] private ICardEffectable effect;    // Reference to the unique card effect every card shuld have

    // Create a serializable key-value pair class for resources
    [System.Serializable]
    public class ResourceCost
    {
        public string resourceName; // Resource name (e.g., "Mana", "Gold")
        public int cost;            // Resource cost value
    }

    // IUsable

    public void UseCard(List<GameObject> targets, Card card)   //Pass targets and card information to the card effect
    {
        effect = gameObject.transform.GetComponent<ICardEffectable>();  // This is an interface all variable card effects should have

        if (effect != null)
        {
            effect.UseEffect(targets, card);
        }
    }

    // Getters and Setters

    public string CardName
    {
        get { return cardName; }
        set { cardName = value; }
    }

    // Getter and Setter for description
    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    // Getter and Setter for type
    public string Type
    {
        get { return type; }
        set { type = value; }
    }

    // Getter and Setter for aoe
    public string AOE
    {
        get { return aoe; }
        set { aoe = value; }
    }

    // Getter and Setter for duration
    public int Duration
    {
        get { return duration; }
        set { duration = Mathf.Max(0, value); } // Ensure duration is non-negative
    }

    // Getter and Setter for damage
    public List<float> Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    // Getter and Setter for sprite
    public Sprite Sprite
    {
        get { return sprite; }
        set { sprite = value; }
    }

    // Getter and Setter for effect
    public ICardEffectable Effect
    {
        get { return effect; }
        set { effect = value; }
    }

    // Getter and Setter for resourceCosts (List<ResourceCost>)
    public List<ResourceCost> ResourceCosts
    {
        get { return resourceCosts; }
        set { resourceCosts = value; }
    }

    // Getter and Setter for resourceCostsDict (Dictionary<string, int>)
    public Dictionary<string, int> ResourceCostsDict
    {
        get { return resourceCostsDict; }
        set
        {
            // Optionally, you could copy the values if you want to ensure the integrity of data
            resourceCostsDict.Clear();
            foreach (var pair in value)
            {
                resourceCostsDict[pair.Key] = pair.Value;
            }
        }
    }

    // Method to retrieve the cost of a specific resource from resourceCostsDict
    public int GetResourceCost(string resourceName)
    {
        if (resourceCostsDict.ContainsKey(resourceName))
        {
            return resourceCostsDict[resourceName];
        }
        else
        {
            Debug.LogWarning($"Resource '{resourceName}' not found.");
            return 0; // Default value if not found
        }
    }

    // Method to add a resource cost to the resourceCostsDict
    public void AddResourceCost(string resourceName, int cost)
    {
        if (resourceCostsDict.ContainsKey(resourceName))
        {
            resourceCostsDict[resourceName] = cost; // Update existing resource cost
        }
        else
        {
            resourceCostsDict.Add(resourceName, cost); // Add new resource cost
        }
    }

    // Method to display the costs for debugging
    public void DisplayResourceCosts()
    {
        foreach (var resource in resourceCostsDict)
        {
            Debug.Log($"Resource: {resource.Key}, Cost: {resource.Value}");
        }
    }

    private void Awake()
    {
        // Clear the existing dictionary
        resourceCostsDict.Clear();

        // Populate the dictionary with the serialized data
        foreach (var resource in resourceCosts)
        {
            resourceCostsDict[resource.resourceName] = resource.cost;
        }

        if (gameObject.transform.GetComponent<SpriteRenderer>())    // Ensure sprite is always set for a card
        {
            sprite = gameObject.transform.GetComponent<SpriteRenderer>().sprite;
        }
    }
}
