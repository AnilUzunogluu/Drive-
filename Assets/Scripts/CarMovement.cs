using System;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private CollisionManager collisionManager;
    [Header("Speed")]
    [SerializeField][Tooltip("Sets the forward speed of the vehicle")] private float speed = 5f;
    [SerializeField][Tooltip("Sets the value to increment the speed every second")] private float speedIncrement = 0.3f;
    
    [Header("Rotation")]
    [SerializeField][Tooltip("Sets the base rotation speed of the vehicle")] private float rotationSpeed = 75f; 
    [SerializeField][Tooltip("Sets the value to increment the rotation speed every second")] private float rotationIncrement = 0.5f;

    private bool _hasCrashed;
    private int _rotationValue;
    private Vector3 _startingPosition;
    private Quaternion _startingRotation;

    private void Awake()
    {
        _startingPosition = transform.position;
        _startingRotation = transform.rotation;
    }

    private void OnEnable()
    {
        collisionManager.OnCrash += SetCrashedState;
    }

    private void OnDestroy()
    {
        collisionManager.OnCrash -= SetCrashedState;
    }

    private void Update()
    {
        if (_hasCrashed) return;
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

    public void SetCrashedState(bool value)
    {
        _hasCrashed = value;
    }

    public void SetCarPositionToStart()
    {
        transform.position = _startingPosition;
        transform.rotation = _startingRotation;
    }
}
