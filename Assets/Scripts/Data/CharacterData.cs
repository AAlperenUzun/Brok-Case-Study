using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Game/Character Data")]
public class CharacterData : ScriptableObject
{
    [Header("Character Information")]
    public string characterName;
    public string characterDescription;
    public string addressableKey;

    [Header("Movement Properties")]
    public float baseWalkSpeed = 3f;
    public float baseRunSpeed = 6f;
    public float baseJumpForce = 5f;
    
    [Header("Upgrade Information")]
    public float walkSpeedUpgradeIncrement = 0.2f;
    public float runSpeedUpgradeIncrement = 0.3f;
    public float jumpForceUpgradeIncrement = 0.25f;
    
    [HideInInspector]
    public int walkSpeedLevel;
    [HideInInspector]
    public int runSpeedLevel;
    [HideInInspector]
    public int jumpForceLevel;
    public float CurrentWalkSpeed => baseWalkSpeed + (walkSpeedUpgradeIncrement * walkSpeedLevel);
    public float CurrentRunSpeed => baseRunSpeed + (runSpeedUpgradeIncrement * runSpeedLevel);
    public float CurrentJumpForce => baseJumpForce + (jumpForceUpgradeIncrement * jumpForceLevel);
    
    public void ResetUpgrades()
    {
        walkSpeedLevel = 0;
        runSpeedLevel = 0;
        jumpForceLevel = 0;
    }
}