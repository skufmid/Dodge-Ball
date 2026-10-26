using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate()
    {
        Vector3 velocity = rb.linearVelocity;

        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
        {
            // Stay put after game over, even if obstacles keep hitting us
            rb.linearVelocity = new Vector3(0f, velocity.y, 0f);
            return;
        }

        float x = 0f;
        float z = 0f;

        Keyboard keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard.wKey.isPressed) z += 1f;
            if (keyboard.sKey.isPressed) z -= 1f;
            if (keyboard.dKey.isPressed) x += 1f;
            if (keyboard.aKey.isPressed) x -= 1f;
        }

        Vector3 move = new Vector3(x, 0f, z);
        if (move.sqrMagnitude > 1f) move.Normalize();
        move *= speed;

        rb.linearVelocity = new Vector3(move.x, velocity.y, move.z);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }
    }
}
