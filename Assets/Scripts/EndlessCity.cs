using UnityEngine;

public class EndlessCity : MonoBehaviour
{
    [SerializeField] Transform playerCarTransform;
    [SerializeField] Transform otherCityTransform; // City2 at 240
    [SerializeField] float segmentLength = 240f; // Matches your city spacing
    
    void Update()
    {
        // Determine which city is currently behind the player
        Transform cityBehind = playerCarTransform.position.z > otherCityTransform.position.z 
            ? otherCityTransform : transform;

        // Check if player passed this city segment
        if (playerCarTransform.position.z > cityBehind.position.z + segmentLength)
        {
            // Move the behind city ahead by both segments' length
            cityBehind.position += Vector3.forward * (segmentLength * 2);
            
            Debug.Log($"Moved {cityBehind.name} to {cityBehind.position.z}");
        }
    }
}