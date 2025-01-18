using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraBound : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float upperBound; // Giới hạn trên
    [SerializeField] private float lowerBound; // Giới hạn dưới

    private Transform cameraTransform;
    // Start is called before the first frame update
    void Start()
    {

        // cameraTransform = _camera.transform;
    }

    // Update is called once per frame
    void Update()
    {
         Vector3 cameraPosition = cameraTransform.position;

        cameraPosition.y = Mathf.Clamp(cameraPosition.y, lowerBound, upperBound);

        transform.position = cameraPosition;
        Debug.Log(cameraPosition);
    }
}
