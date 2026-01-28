using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject whereToFollow;

    public float cameraOffsetX;
    public float cameraOffsetY;
    public float cameraOffsetZ;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void LateUpdate()
    {
        transform.position = whereToFollow.transform.position +
         new Vector3(cameraOffsetX, cameraOffsetY, cameraOffsetZ);
    }

}
