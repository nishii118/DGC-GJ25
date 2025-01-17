using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speedMove = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] private Camera cam;
    [SerializeField] private bool isUnderWater = true;
    [SerializeField] private bool canMove = true;

    // Update is called once per frame

    void OnEnable()
    {
        Messenger.AddListener(EventKey.ONBREAKBUBBLE, OnBreakBubble);

    }

    void OnDisable()
    {
        Messenger.RemoveListener(EventKey.ONBREAKBUBBLE, OnBreakBubble);
    }
    void Update()
    {
        if (canMove) MovingAndJumping();
        if (isUnderWater) {
            Messenger.Broadcast(EventKey.ONREGETMANA);
        } else {
            Messenger.Broadcast(EventKey.ONUSEMANA);
        }

        // MovingAndJumping();
    }

    private void MovingAndJumping()
    {
        // Set movement speed based on environment
        speedMove = isUnderWater ? 5f : 2f;

        // Get input values
        float vertical = Input.GetAxis("Vertical");
        float jump = isUnderWater ? Input.GetAxis("Jump") : 0f; // Jump only applies underwater
        // float horizontal = isUnderWater ? 1f : 0.5f;
        // Combine horizontal and vertical/jump movement
        Debug.Log("Vertical: " + vertical + " Jump: " + jump);
        Vector2 movement = new Vector2(0, (vertical * speedMove) + (jump * jumpForce));
        rb2d.velocity = movement;

        // Uncomment if needed
        // CameraFollowPlayer();
    }

    private void CameraFollowPlayer()
    {
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    private void OnBreakBubble()
    {
        // isUnderWater = false;
        canMove = false;
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Water")
        {
            isUnderWater = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Water")
        {
            isUnderWater = true;
        }
    }
}
