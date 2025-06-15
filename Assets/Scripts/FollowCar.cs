using UnityEngine;

public class FollowCar : MonoBehaviour
{
    public Transform carTransform;
    public Transform cameraPointTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(carTransform);
        carTransform.position = cameraPointTransform.position;
    }
}
