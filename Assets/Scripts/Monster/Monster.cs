using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private float speedMove = 5f;
    [SerializeField] private Rigidbody2D rb2d;
    // Start is called before the first frame update
    private int direction = -1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
    }

    private void Moving()
    {
        // Move monster
        Vector2 movement = new Vector2(0, speedMove * direction);
        rb2d.velocity = movement;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            // Destroy(other.gameObject);
            Debug.Log("Monster is out of water");
            ProcessMoving();
        }
    }

    private void ProcessMoving() {
        FlipMonster();
    }

    private void FlipMonster()
    {
        // Flip monster
        direction *= -1;

        transform.rotation = Quaternion.Euler(0, 0, direction == 1 ? 180 : 0);
    }
}
