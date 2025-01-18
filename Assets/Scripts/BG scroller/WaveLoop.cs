using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveLoop : MonoBehaviour
{
    [SerializeField] private GameObject wave1; // Sóng biển đầu tiên
    [SerializeField] private GameObject wave2; // Sóng biển thứ hai
    [SerializeField] private Transform player; // Tham chiếu đến vị trí của player
    private float waveLength; // Chiều dài một sóng biển

    void Start()
    {
        // Lấy chiều dài của sóng từ SpriteRenderer
        waveLength = wave1.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // Kiểm tra nếu player đã đi qua toàn bộ wave1
        if (player.position.x > wave1.transform.position.x + waveLength / 2 + 5f)
        {
            ResetWavePosition(wave1, wave2);
        }

        // Kiểm tra nếu player đã đi qua toàn bộ wave2
        if (player.position.x > wave2.transform.position.x + waveLength / 2 + 5f)
        {
            ResetWavePosition(wave2, wave1);
        }
    }

    private void ResetWavePosition(GameObject waveToReset, GameObject referenceWave)
    {
        // Đặt sóng cần reset phía sau sóng tham chiếu
        waveToReset.transform.position = new Vector3(
            referenceWave.transform.position.x + waveLength,
            waveToReset.transform.position.y,
            waveToReset.transform.position.z
        );
    }
}
