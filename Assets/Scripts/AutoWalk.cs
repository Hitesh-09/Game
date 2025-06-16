using UnityEngine;

public class AutoWalk : MonoBehaviour
{
    public float speed = 1.5f;

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}