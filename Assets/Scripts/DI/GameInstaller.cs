using System.Collections.Generic;
using SaveSystem;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private List<CharacterData> characterDataList;
    [SerializeField] private CharacterSelectionUI characterSelectionUIPrefab;
    [SerializeField] private UpgradeUI upgradeUIPrefab;
    private Dictionary<string, Material> sharedMaterials = new Dictionary<string, Material>();

    public override void InstallBindings()
    {
        SetupSharedMaterials();
        
        Container.Bind<ISaveSystem>().To<JsonFileSaveSystem>().AsSingle();
        Container.Bind<SaveManager>().AsSingle();
        Container.Bind<ICharacterFactory>().To<CharacterFactory>().AsSingle();
        Container.Bind<CharacterSelectionUI>().FromComponentInNewPrefab(characterSelectionUIPrefab).AsSingle();
        Container.Bind<UpgradeUI>().FromComponentInNewPrefab(upgradeUIPrefab).AsSingle();
        Container.Bind<UIManager>().FromNewComponentOnNewGameObject().AsSingle();
        Container.Bind<List<CharacterData>>().FromInstance(characterDataList).AsSingle();
        Container.Bind<GameManager>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
    }
    private void SetupSharedMaterials()
    {
        Material[] allMaterials = Resources.FindObjectsOfTypeAll<Material>();
        
        foreach (Material material in allMaterials)
        {
            if (!sharedMaterials.ContainsKey(material.name))
            {
                if (!material.name.StartsWith("Default-") && 
                    !material.name.Contains("(Instance)") &&
                    !material.name.StartsWith("Hidden/"))
                {
                    sharedMaterials.Add(material.name, material);
                    Debug.Log($"Added shared material: {material.name}");
                }
            }
        }
        
        Dictionary<Shader, List<Material>> materialsByShader = new Dictionary<Shader, List<Material>>();
        
        foreach (var material in sharedMaterials.Values)
        {
            Shader shader = material.shader;
            
            if (!materialsByShader.ContainsKey(shader))
            {
                materialsByShader[shader] = new List<Material>();
            }
            
            materialsByShader[shader].Add(material);
        }
    }
}