using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    // These variables can be adjusted in the Unity Inspector
    public float moveSpeed = 5f;
    public float sprintSpeedMultiplier = 2f;
    public float attackCooldown = 0.5f;

    // A variable to hold a reference to the Rigidbody2D component
    private Rigidbody2D rb;

    // A variable to store the current speed of the player
    private float currentSpeed;

    // A variable to track the player's last non-zero movement direction
    private Vector2 lastMovementDirection = new Vector2(0, -1); // Default to down

    // A boolean to control the attack cooldown
    private bool canAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody2D component attached to the player object
        rb = GetComponent<Rigidbody2D>();
        // Set the initial speed to the base movement speed
        currentSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from the horizontal and vertical axes (A/D and W/S or arrow keys)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Create a Vector2 to represent the movement direction (for 2D physics)
        Vector2 movement = new Vector2(horizontalInput, verticalInput);

        // Normalize the movement vector to ensure consistent speed in all directions
        // (prevents faster diagonal movement)
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }
        
        // If the player is moving, update the last movement direction
        if (movement != Vector2.zero)
        {
            lastMovementDirection = movement;
        }

        // Check if the left shift key is being held down
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // If sprinting, multiply the move speed by the sprint multiplier
            currentSpeed = moveSpeed * sprintSpeedMultiplier;
        }
        else
        {
            // If not sprinting, use the base move speed
            currentSpeed = moveSpeed;
        }

        // Set the Rigidbody2D's velocity to apply the movement
        // This is the correct way to move an object controlled by physics
        rb.velocity = movement * currentSpeed;

        // Check for player attack input (left mouse button)
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            // Call the Attack method
            Attack();
        }
    }

    // Handles the player's attack action
    void Attack()
    {
        // Set canAttack to false to start the cooldown
        canAttack = false;

        // Log the attack direction to the console for testing
        Debug.Log("Attacking in direction: " + lastMovementDirection);

        // You would add your attack logic here. For example, instantiating an attack prefab
        // or a particle effect in the direction of 'lastMovementDirection'.
        // This is a placeholder for your attack visual and damage logic.

        // Start the cooldown coroutine
        StartCoroutine(AttackCooldown());
    }

    // Coroutine to handle the attack cooldown
    private IEnumerator AttackCooldown()
    {
        // Wait for the specified cooldown time
        yield return new WaitForSeconds(attackCooldown);
        // Reset canAttack to true, allowing the next attack
        canAttack = true;
    }
}
