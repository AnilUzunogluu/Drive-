using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField][Tooltip("Sets the forward speed of the vehicle")] private float speed = 5f;
    [SerializeField][Tooltip("Sets the value to increment the speed every second")] private float speedIncrement = 0.3f;
    
    [Header("Rotation")]
    [SerializeField][Tooltip("Sets the base rotation speed of the vehicle")] private float rotationSpeed = 75f; 
    [SerializeField][Tooltip("Sets the value to increment the rotation speed every second")] private float rotationIncrement = 0.5f;
    
    private int _rotationValue;

    private void Update()
    {
        MoveForward();
        RotateCar();
        IncrementSpeedAndRotation();
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void Steer(int value)
    {
        _rotationValue = value;
    }

    private void RotateCar()
    {
        var yRotation = _rotationValue * rotationSpeed * Time.deltaTime;
        transform.Rotate(0f, yRotation, 0f);
    }

    private void IncrementSpeedAndRotation()
    {
        speed += Time.deltaTime * speedIncrement;
        rotationSpeed += Time.deltaTime * rotationIncrement;
    }
}
