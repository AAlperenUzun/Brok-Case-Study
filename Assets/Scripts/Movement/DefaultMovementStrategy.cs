using UnityEngine;

public class DefaultMovementStrategy : IMovementStrategy
{
    private float walkSpeed;
    private float runSpeed;
    private float jumpForce;

    private bool isGrounded = true;
    private float groundCheckDistance = 1f;

    private LayerMask groundLayerMask = ~0;

    public DefaultMovementStrategy(CharacterData characterData)
    {
        UpdateMovementData(characterData);
    }

    public void Move(Transform transform, Vector3 direction, bool isRunning)
    {
        Rigidbody rb = transform.GetComponent<Rigidbody>();
        if (rb == null) return;
        CheckGrounded(transform);
        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        Vector3 move = direction.normalized * currentSpeed;
        Vector3 velocity = new Vector3(move.x, rb.linearVelocity.y, move.z);
        rb.linearVelocity = velocity;
    }

    public void Jump(Rigidbody rigidbody)
    {
        CheckGrounded(rigidbody.transform);
        Vector3 vel = rigidbody.linearVelocity;
        vel.y = 0;
        rigidbody.linearVelocity = vel;
        rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    public void UpdateMovementData(CharacterData characterData)
    {
        walkSpeed = characterData.CurrentWalkSpeed;
        runSpeed = characterData.CurrentRunSpeed;
        jumpForce = characterData.CurrentJumpForce;

        Debug.Log($"Updated movement data: Walk={walkSpeed}, Run={runSpeed}, Jump={jumpForce}");
    }

    public void UpdateChecks(Transform transform)
    {
        CheckGrounded(transform);
    }

    public void CheckGrounded(Transform transform)
    {
        Vector3 rayStart = transform.position + Vector3.up * 0.05f;
        float rayDistance = groundCheckDistance + 0.1f;
        isGrounded = Physics.Raycast(rayStart, Vector3.down, out _, rayDistance, groundLayerMask);
        Debug.DrawRay(rayStart, Vector3.down * rayDistance, isGrounded ? Color.green : Color.red, 0.1f);
    }
}
