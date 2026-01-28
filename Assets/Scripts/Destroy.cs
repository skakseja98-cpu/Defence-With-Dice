using UnityEngine;

public class Destroy : MonoBehaviour
{

    public float destroyTime;
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        
    }
}
