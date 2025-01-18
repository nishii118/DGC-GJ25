using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessTilemap : MonoBehaviour
{
    [SerializeField] private TilePool tilePool;
    [SerializeField] private Transform player;
    [SerializeField] private float chunkLength = 10f;
    // [SerializeField] private int tilesPerChunk = 10;
    [SerializeField] private int tilesAhead = 5;

    private float nextChunkSpawnZ = 0f;
    private Queue<GameObject> activeTiles = new Queue<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < tilesAhead; i++)
        {
            SpawnChunk();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.z > nextChunkSpawnZ - (tilesAhead * chunkLength))
        {
            SpawnChunk();
            RemoveOldChunk();
        }
    }

    private void SpawnChunk()
    {
        GameObject tile = tilePool.GetPoolObject(); // Lấy tile từ pool
        tile.transform.position = new Vector3(nextChunkSpawnZ, 0, 0); // Đặt vị trí
        tile.SetActive(true); // Kích hoạt tile
        activeTiles.Enqueue(tile); // Thêm tile vào hàng đợi

        nextChunkSpawnZ += chunkLength; // Cập nhật vị trí spawn tiếp theo
    }


    private void RemoveOldChunk()
    {
        if (activeTiles.Count > tilesAhead)
        {
            GameObject oldTile = activeTiles.Dequeue(); // Lấy tile cũ từ hàng đợi
            tilePool.ReturnPoolObject(oldTile); // Trả tile về pool
        }
    }
}
