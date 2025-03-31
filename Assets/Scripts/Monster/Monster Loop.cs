using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLoop : MonoBehaviour
{
    [SerializeField] private List<Transform> waves; // Danh sách các wave
    [SerializeField] private Transform player; // Vị trí của player
    [SerializeField] private float waveLength; // Chiều dài của một wave

    void Start()
    {
        if (waveLength <= 0 && waves.Count > 1)
        {
            // Tính chiều dài sóng dựa trên khoảng cách giữa wave đầu tiên và wave tiếp theo
            waveLength = Vector3.Distance(waves[0].position, waves[1].position);
        }
    }

    void Update()
    {
        for (int i = 0; i < waves.Count; i++)
        {
            Transform currentWave = waves[i];
            Transform nextWave = waves[(i + 1) % waves.Count]; // Lấy wave tiếp theo theo vòng lặp

            // Kiểm tra nếu player đã đi qua toàn bộ currentWave
            if (player.position.x > currentWave.position.x + waveLength   + 5f)
            {
                ResetWavePosition(currentWave, nextWave);
            }
        }
    }

    private void ResetWavePosition(Transform waveToReset, Transform referenceWave)
    {
        // Đặt sóng cần reset phía sau sóng tham chiếu
        waveToReset.position = new Vector3(
            referenceWave.position.x + waveLength,
            waveToReset.position.y,
            waveToReset.position.z
        );
    }
}
