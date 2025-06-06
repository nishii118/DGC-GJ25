
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform target;
    private float offSetX;
    // Start is called before the first frame update
    void Awake()
    {
        offSetX = transform.position.x - target.position.x;
        // transform.position = new Vector3(target.position.x + offSetX - 2f, transform.position.y , transform.position.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.position.x + offSetX - 2f, transform.position.y, transform.position.z);
        }
    }


}
