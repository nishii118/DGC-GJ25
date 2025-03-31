using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2 : MonoBehaviour
{
    [SerializeField] private Transform upperPoint; // Điểm cận trên
    [SerializeField] private Transform lowerPoint; // Điểm cận dưới
    [SerializeField] private float speed = 2f; // Tốc độ di chuyển
    private Vector3 targetPoint; // Điểm đích hiện tại của monster
    private float direction = 1f;

    void Start()
    {
        // Bắt đầu bằng cách di chuyển đến điểm cận trên
        targetPoint = upperPoint.position;
    }

    void Update()
    {
        transform.position += new Vector3(0, speed * direction * Time.deltaTime, 0);

        // Kiểm tra nếu monster vượt qua cận trên hoặc cận dưới
        if (transform.position.y >= upperPoint.position.y && direction > 0)
        {
            direction = -1f; // Đảo ngược hướng xuống
        }
        else if (transform.position.y <= lowerPoint.position.y && direction < 0)
        {
            direction = 1f; // Đảo ngược hướng lên
        }
    }
}
