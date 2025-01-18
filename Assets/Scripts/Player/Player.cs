using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speedMove = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] Rigidbody2D rb2d;
    // [SerializeField] private Camera cam;
    [SerializeField] private bool isUnderWater = true;
    [SerializeField] private bool canMove = true;
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject bubble;
    private bool isFalling = false;

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
        if (isUnderWater)
        {
            Messenger.Broadcast(EventKey.ONREGETMANA);
        }
        else
        {
            Messenger.Broadcast(EventKey.ONUSEMANA);
        }
        // if (isFalling) MovingDown();
        // MovingAndJumping();
    }

    private void MovingAndJumping()
    {
        // Set movement speed based on environment
        speedMove = isUnderWater ? 8f : 2f;

        // Get input values
        float vertical = Input.GetAxis("Vertical");
        float jump = isUnderWater ? Input.GetAxis("Jump") : 0f; // Jump only applies underwater
        if (jump > 0)
        {
            Messenger.Broadcast(EventKey.ONUSEMANAFORDASH, 0.5f);
            Debug.Log("Jump");
        }
        float horizontal = isUnderWater ? 2f : 0.5f;
        float verticalVelocity = vertical * speedMove + jump * jumpForce;
        animator.SetFloat("Vertical", verticalVelocity);
        // Combine horizontal and vertical/jump movement
        // Debug.Log("Vertical: " + vertical + " Jump: " + jump);
        Vector2 movement = new Vector2(horizontalSpeed, verticalVelocity);
        rb2d.velocity = movement;

        // Uncomment if needed
        // CameraFollowPlayer();
    }

    private void MovingDown()
    {
        Vector2 movement = new Vector2(0, -1 * jumpForce);
        rb2d.velocity = movement;
    }
    // private void CameraFollowPlayer()
    // {
    //     // cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    // }

    private void OnBreakBubble()
    {
        // isUnderWater = false;
        canMove = false;
        SetDynamicRigidbody();
    }

    private void SetMoveAble()
    {
        canMove = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Water")
        {
            isUnderWater = false;
            isFalling = true;

            ProcessSpawnBubble();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Water")
        {
            ProcessTriggerEnterWater();



        }
        else if (other.gameObject.tag == "Trap")
        {
            ProcessTriggerEnterTrap();
            // Debug.Log("Trap");
        }
    }

    private void ProcessTriggerEnterWater()
    {
        // Debug.Log("Underwater");
        isUnderWater = true;
        // SetStaticRigiBody();
        SetKinematicRigidbody();
        if (isFalling)
        {
            Messenger.Broadcast(EventKey.ONBREAKBUBBLE2);

            StartCoroutine(MoveDownForDuration(0.5f));
        }

    }

    private void ProcessTriggerEnterTrap()
    {
        // Debug.Log("Trap");
        Time.timeScale = 0;
        // isUnderWater = false;
        // SetDynamicRigidbody();

        Messenger.Broadcast(EventKey.ENDGAME);
    }
    private IEnumerator MoveDownForDuration(float duration)
    {
        float elapsedTime = 0f;
        float initialSpeed = 8f; // Tốc độ ban đầu
        float targetSpeed = 0f;         // Tốc độ cuối cùng (chậm dần về 0)

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // Tính tốc độ hiện tại với Lerp để giảm dần
            float currentSpeed = Mathf.Lerp(initialSpeed, targetSpeed, elapsedTime / duration);

            // Áp dụng vận tốc cho Rigidbody2D
            rb2d.velocity = new Vector2(horizontalSpeed, -currentSpeed);

            yield return null; // Chờ tới frame tiếp theo
        }

        // Sau khi hết thời gian, dừng chuyển động
        rb2d.velocity = Vector2.zero;
        Debug.Log("Stop Moving Down");
    }
    private void SetDynamicRigidbody()
    {

        rb2d.bodyType = RigidbodyType2D.Dynamic;
        rb2d.gravityScale = 1.6f;
    }

    private void SetKinematicRigidbody()
    {
        rb2d.bodyType = RigidbodyType2D.Kinematic;
        // rb2d.velocity = Vector2.zero;
        // Debug.Log("Rigidbody set to Kinematic");
        // Debug.Log("velocity: " + rb2d.velocity);

        SetMoveAble();
    }

    private void SetStaticRigiBody()
    {
        rb2d.bodyType = RigidbodyType2D.Static;
    }

    private void ProcessSpawnBubble()
    {
        Debug.Log("Spawn Bubble");
        bubble.SetActive(true);
    }
}
