using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleManager : MonoBehaviour  // This script should handle turn order and completing actions for players and enemies. Attempt to keep modifiers and status effects away from here, values should pass through not be modified
{
    [SerializeField] private List<GameObject> turnOrder = new List<GameObject>();  // Turn order will start from 0, 1, 2, 3. The player should always be 0 and therefore first to take their turn
    [SerializeField] private InputActionReference clickRef;
    [SerializeField] private ICreature selectedEnemy;
    [SerializeField] private PlayerCards playerCards;
    [SerializeField] private TurnState currentTurn;
    [SerializeField] Camera battleCam;

    private void OnEnable()
    {
        clickRef.action.started += Click;
    }

    private void OnDisable()
    {
        clickRef.action.started -= Click;
    }

    private void Start()
    {
        battleCam = Camera.main;
        if (turnOrder[0] != null && turnOrder[0].GetComponent<Player>())
        {
            playerCards = turnOrder[0].GetComponent<PlayerCards>();
        }
    }

    void SelectEnemy()
    {
        // Convert the mouse position to a Ray in 2D space
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);  // Convert screen to world position
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);  // Raycast in 2D space

        if (hit.collider != null)  // Check if we hit something
        {
            // Check if the hit object has the ICreature component
            ICreature enemy = hit.collider.GetComponent<ICreature>();
            if (enemy != null)
            {
                // Set this enemy as the selected one
                selectedEnemy = enemy;

                // Call the attack function or any other logic here
                Debug.Log("Hit Enemy: " + enemy);
                // AttackEnemy(enemy);  // You can call your attack method here
            }
        }
        else
        {
            Debug.Log("Hit Nothing!");
        }
    }
    private void Click(InputAction.CallbackContext obj)
    {
        Debug.Log("Testing Click Button!");
        SelectEnemy();
    }

}

public class TurnState
{
    enum turnPhase { PlayerTurn, EnemyTurn }
}
