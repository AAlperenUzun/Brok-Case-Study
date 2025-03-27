using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private Button walkSpeedButton;
    [SerializeField] private Button runSpeedButton;
    [SerializeField] private Button jumpForceButton;
    
    [SerializeField] private TextMeshProUGUI walkSpeedText;
    [SerializeField] private TextMeshProUGUI runSpeedText;
    [SerializeField] private TextMeshProUGUI jumpForceText;
    
    [SerializeField] private GameObject upgradePanel;
    
    private CharacterData currentCharacterData;
    private Action<CharacterData> onCharacterUpgraded;
    
    public void Initialize(Action<CharacterData> onUpgraded)
    {
        onCharacterUpgraded = onUpgraded;
        walkSpeedButton.onClick.AddListener(() => UpgradeWalkSpeed());
        runSpeedButton.onClick.AddListener(() => UpgradeRunSpeed());
        jumpForceButton.onClick.AddListener(() => UpgradeJumpForce());
    }
    
    public void UpdateUI(CharacterData characterData)
    {
        currentCharacterData = characterData;
        walkSpeedText.text = $"Walk Speed: {characterData.CurrentWalkSpeed:F1} (Level {characterData.walkSpeedLevel})";
        runSpeedText.text = $"Run Speed: {characterData.CurrentRunSpeed:F1} (Level {characterData.runSpeedLevel})";
        jumpForceText.text = $"Jump Force: {characterData.CurrentJumpForce:F1} (Level {characterData.jumpForceLevel})";
    }
    
    private void UpgradeWalkSpeed()
    {
        if (currentCharacterData != null)
        {
            currentCharacterData.walkSpeedLevel++;
            UpdateUI(currentCharacterData);
            onCharacterUpgraded?.Invoke(currentCharacterData);
        }
    }
    
    private void UpgradeRunSpeed()
    {
        if (currentCharacterData != null)
        {
            currentCharacterData.runSpeedLevel++;
            UpdateUI(currentCharacterData);
            onCharacterUpgraded?.Invoke(currentCharacterData);
        }
    }
    
    private void UpgradeJumpForce()
    {
        if (currentCharacterData != null)
        {
            currentCharacterData.jumpForceLevel++;
            UpdateUI(currentCharacterData);
            onCharacterUpgraded?.Invoke(currentCharacterData);
        }
    }
    
    public void Show()
    {
        upgradePanel.SetActive(true);
    }
    
    public void Hide()
    {
        upgradePanel.SetActive(false);
    }
}