using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CharacterSelectionUI : MonoBehaviour
{
    [SerializeField] private Button[] characterButtons;
    [SerializeField] private TextMeshProUGUI[] characterNameTexts;
    [SerializeField] private GameObject selectionPanel;
    
    private List<CharacterData> availableCharacters;
    private Action<CharacterData> onCharacterSelected;
    
    public void Initialize(List<CharacterData> characters, Action<CharacterData> onSelected)
    {
        availableCharacters = characters;
        onCharacterSelected = onSelected;
        
        for (int i = 0; i < characterButtons.Length && i < characters.Count; i++)
        {
            CharacterData characterData = characters[i];
            characterNameTexts[i].text = characterData.characterName;
            
            int index = i;
            characterButtons[i].onClick.AddListener(() => SelectCharacter(index));
        }
    }
    
    private void SelectCharacter(int index)
    {
        if (index < availableCharacters.Count)
        {
            onCharacterSelected?.Invoke(availableCharacters[index]);
            selectionPanel.SetActive(false);
        }
    }
    
    public void Show()
    {
        selectionPanel.SetActive(true);
    }
    
    public void Hide()
    {
        selectionPanel.SetActive(false);
    }
}