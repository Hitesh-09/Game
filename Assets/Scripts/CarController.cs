using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider backRightWheelCollider;
    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider backLeftWheelCollider;

    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform backRightWheelTransform;
    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform backLeftWheelTransform;

    [SerializeField] private Transform carCentreOfMassTransform;
     

    [SerializeField] private float motorForce = 100f;
    [SerializeField] private float steeringAngle = 30f;
    [SerializeField] private float brakeforce = 1000f;

    private Rigidbody rigidbody;
    private float verticalInput;
    private float horizontalInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = carCentreOfMassTransform.localPosition; // Set the center of mass to the car's center of mass transform
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetInput();       // 👈 First get input
        MotorForce();     // Then apply force
        UpdateWheels();   // Finally update visuals
        Steering();      // 👈 Steering logic
        ApplyBrakes();  // 👈 Apply brakes if needed
}
    void GetInput()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

    }
    void ApplyBrakes()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            frontRightWheelCollider.brakeTorque = brakeforce;
            backRightWheelCollider.brakeTorque = brakeforce;
            frontLeftWheelCollider.brakeTorque = brakeforce;
            backLeftWheelCollider.brakeTorque = brakeforce;
        }
        else
        {
            frontRightWheelCollider.brakeTorque = 0f;
            backRightWheelCollider.brakeTorque = 0f;
            frontLeftWheelCollider.brakeTorque = 0f;
            backLeftWheelCollider.brakeTorque = 0f;
        }
        
    }
    void MotorForce()
    {
        frontRightWheelCollider.motorTorque = motorForce * verticalInput;
        frontLeftWheelCollider.motorTorque = motorForce * verticalInput;
    }
    void Steering()
    {
        frontRightWheelCollider.steerAngle = steeringAngle * horizontalInput; 
        frontLeftWheelCollider.steerAngle = steeringAngle * horizontalInput;
    }
    void UpdateWheels()
    {
        RotateWheel(frontRightWheelCollider, frontRightWheelTransform);
        RotateWheel(backRightWheelCollider, backRightWheelTransform);
        RotateWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        RotateWheel(backLeftWheelCollider, backLeftWheelTransform);
        
    }
    void RotateWheel(WheelCollider wheelCollider, Transform transform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        transform.position = pos;
        transform.rotation = rot;
    }
}













