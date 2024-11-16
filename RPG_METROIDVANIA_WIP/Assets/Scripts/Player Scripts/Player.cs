using UnityEngine;
using System.Collections.Generic;
using UnityEngine.U2D;
using UnityEngine.SceneManagement;
using System.Xml.Linq;

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
    [SerializeField] private Collider2D p_Collider;
    [SerializeField] private Rigidbody2D p_RigidBody;
    [SerializeField] private PlayerMovement p_PlayerMovement;
    [SerializeField] private Camera p_Camera;

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
    public Collider2D Collider
    {
        get { return p_Collider; }
        set { p_Collider = value; }
    }

    public void takeDamage(float damage)
    {
        Health -= damage;
    }

    public void hasDied()
    {
        IsDead = false;
    }

    public void checkSceneCollider()
    {
        // Get the current scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Get the name of the current scene
        string sceneName = currentScene.name;

        // Output the scene name
        if (currentScene.name == "BattleScene")
        {
            Debug.Log("In Battle");
            p_Collider.enabled = false;
            p_RigidBody.gravityScale = 0;
            p_PlayerMovement.enabled = false;
            p_Camera.transform.SetParent(null);
            p_Camera.transform.position = new Vector3(0, 0, -10);
            this.gameObject.transform.position = new Vector3(-5, 0, 0);
        }
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

    private void Start()
    {
        if (this.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            p_RigidBody = this.gameObject.GetComponent<Rigidbody2D>();
        }

        else
        {
            Debug.Log(p_Name + " Does not have a rigid body " + "Instance ID is " + gameObject.GetInstanceID());
        }

        if (this.gameObject.GetComponent<Collider2D>() != null)
        {
            p_Collider = this.gameObject.GetComponent<Collider2D>();
        }

        else
        {
            Debug.Log(p_Name + " Does not have a collider " + "Instance ID is " + gameObject.GetInstanceID());
        }

        if (this.gameObject.GetComponent<PlayerMovement>() != null)
        {
            p_PlayerMovement = this.gameObject.GetComponent<PlayerMovement>();
        }

        else
        {
            Debug.Log(p_Name + " Does not have a movement script " + "Instance ID is " + gameObject.GetInstanceID());
        }

        if (this.gameObject.GetComponentInChildren<Camera>())
        {
            p_Camera = this.gameObject.GetComponentInChildren<Camera>();
        }

        else
        {
            Debug.Log(p_Name + " Does not have a camera " + "Instance ID is " + gameObject.GetInstanceID());
        }

        checkSceneCollider();
    }
}
