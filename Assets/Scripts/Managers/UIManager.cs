using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIManager : MonoBehaviour
{
    private CharacterSelectionUI characterSelectionUI;
    private UpgradeUI upgradeUI;
    
    [Inject]
    public void Construct(CharacterSelectionUI characterSelectionUI, UpgradeUI upgradeUI)
    {
        this.characterSelectionUI = characterSelectionUI;
        this.upgradeUI = upgradeUI;
    }
    
    public void Initialize(List<CharacterData> characters, Action<CharacterData> onCharacterSelected, Action<CharacterData> onCharacterUpgraded)
    {
        characterSelectionUI.Initialize(characters, onCharacterSelected);
        upgradeUI.Initialize(onCharacterUpgraded);
        ShowCharacterSelection();
    }
    
    public void ShowCharacterSelection()
    {
        characterSelectionUI.Show();
        upgradeUI.Hide();
    }
    
    public void ShowUpgradeUI(CharacterData characterData)
    {
        characterSelectionUI.Hide();
        upgradeUI.Show();
        upgradeUI.UpdateUI(characterData);
    }
}