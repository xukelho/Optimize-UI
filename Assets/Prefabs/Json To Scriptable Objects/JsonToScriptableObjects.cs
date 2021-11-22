using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public class JsonToScriptableObjects : MonoBehaviour
{
    #region Fields

    [Multiline]
    [SerializeField] private string StringJSON;

    [Space(29)]
    [SerializeField] private Sprite[] _icons;

    #endregion Fields

    #region Methods

    public void CreateScriptableObjectsFromJson()
    {
        var data = JsonConvert.DeserializeObject<InventoryItemData[]>(StringJSON);

        for (var i = 0; i < data.Length; i++)
        {
            InventoryItemDataSO aux = ScriptableObject.CreateInstance<InventoryItemDataSO>();

            aux.IconName = _icons[data[i].IconIndex].name;
            aux.Name = data[i].Name;
            aux.Description = data[i].Description;
            aux.Stat = data[i].Stat;

            ProjectWindowUtil.CreateAsset(aux, $"Assets/Resources/Inventory Items/Inventory Item {i}.asset");
            AssetDatabase.SaveAssets();
        }
    }

    #endregion Methods

    #region Classes

    class InventoryItemData
    {
        public int IconIndex;
        public string Name;
        public string Description;
        public int Stat;
    }

    #endregion Classes
}