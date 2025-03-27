// Movement Strategy Interface
using UnityEngine;

public interface IMovementStrategy
{
    void Move(Transform transform, Vector3 direction, bool isRunning);
    void Jump(Rigidbody rigidbody);
    void UpdateMovementData(CharacterData characterData);
    void UpdateChecks(Transform transform);
}