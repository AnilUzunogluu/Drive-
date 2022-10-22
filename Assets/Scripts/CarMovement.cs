using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float speedIncrement = 0.3f;
    [SerializeField] private float rotationSpeed = 75f; 
    [SerializeField] private float rotationIncrement = 0.5f;
    
    private int _rotationValue;

    void Update()
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
