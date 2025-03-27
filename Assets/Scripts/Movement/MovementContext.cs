using System;
using UnityEngine;

public class MovementContext : MonoBehaviour
{
    private IMovementStrategy movementStrategy;
    private Rigidbody rb;
    private CharacterData characterData;
    
    public void Construct(IMovementStrategy movementStrategy)
    {
        this.movementStrategy = movementStrategy;
        rb = GetComponent<Rigidbody>();
        
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            ConfigureRigidbody(rb);
        }
        else
        {
            ConfigureRigidbody(rb);
        }
    }

    private void Update()
    {
        movementStrategy.UpdateChecks(transform);
    }

    private void ConfigureRigidbody(Rigidbody rb)
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }
    
    public void SetCharacterData(CharacterData data)
    {
        characterData = data;
        if (movementStrategy != null)
        {
            movementStrategy.UpdateMovementData(data);
        }
    }
    
    public void Move(Vector3 direction, bool isRunning)
    {
        if (movementStrategy != null)
        {
            movementStrategy.Move(transform, direction, isRunning);
        }
    }
    
    public void Jump()
    {
        if (movementStrategy != null && rb != null)
        {
            movementStrategy.Jump(rb);
        }
    }
    
    public void UpdateMovementData()
    {
        if (characterData != null && movementStrategy != null)
        {
            movementStrategy.UpdateMovementData(characterData);
        }
    }
}