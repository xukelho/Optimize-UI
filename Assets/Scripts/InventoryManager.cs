using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class InventoryManager : MonoBehaviour
{
    #region Fields

    public static InventoryManager Instance;

    public InventoryInfoPanel InfoPanel;

    public GameObject Container;

    public SpriteAtlas Atlas;

    [Tooltip(tooltip: "This is used in generating the items list. The number of additional copies to concat the list parsed from ItemJson.")]
    public int ItemGenerateScale = 10;

    [SerializeField] InventoryItemUI InventoryItemPrefab;

    private InventoryItemUI _previouslyClickedButton = null;

    #endregion Fields

    #region Unity

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        var itemDatas = GenerateItemDatas(ItemGenerateScale);

        PopulateListUI(itemDatas);
    }

    #endregion Unity

    #region Methods

    /// <summary>
    /// Generates an item list.
    /// </summary>
    /// <param name="json">JSON to generate items from. JSON must be an array of InventoryItemData.</param>
    /// <param name="scale">Concats additional copies of the array parsed from json.</param>
    /// <returns>An array of InventoryItemData</returns>
    private List<InventoryItemDataSO> GenerateItemDatas(int scale)
    {
        InventoryItemDataSO[] items = Resources.LoadAll<InventoryItemDataSO>("Inventory Items");

        List<InventoryItemDataSO> completeItemsList = new List<InventoryItemDataSO>();

        for (int i = 0; i < scale; i++)
            completeItemsList.AddRange(items);

        return completeItemsList;
    }

    /// <summary>
    /// Method that is called on UI item click.
    /// </summary>
    /// <param name="itemClicked"> UI object. </param>
    /// <param name="itemData"> Item data. </param>
    private void InventoryItemOnClick(InventoryItemUI itemClicked, InventoryItemDataSO itemData)
    {
        #region Handle UI behaviour

        _previouslyClickedButton.Background.color = Color.white;
        itemClicked.Background.color = Color.red;
        _previouslyClickedButton = itemClicked;

        #endregion Handle UI behaviour

        InventoryInfoPanel.Instance.SetItemInformation(itemClicked, itemData);
    }

    /// <summary>
    /// Populates the UI List with the 'database' items.
    /// </summary>
    /// <param name="dataSO"> 'Database' items. </param>
    private void PopulateListUI(List<InventoryItemDataSO> dataSO)
    {
        List<InventoryItemUI> uiItems = new List<InventoryItemUI>();
        Dictionary<string, Sprite> alreadyExtractedImages = new Dictionary<string, Sprite>();

        foreach (InventoryItemDataSO itemData in dataSO)
        {
            var newItem = Instantiate(InventoryItemPrefab);
            newItem.Name.text = itemData.Name;
            newItem.transform.SetParent(Container.transform);
            newItem.Button.onClick.AddListener(() => { InventoryItemOnClick(newItem, itemData); });

            if (!alreadyExtractedImages.ContainsKey(itemData.IconName))
                alreadyExtractedImages.Add(itemData.IconName, Atlas.GetSprite(itemData.IconName));
            newItem.Icon.sprite = Atlas.GetSprite(itemData.IconName);

            uiItems.Add(newItem);
        }


        // Select the first item.
        _previouslyClickedButton = uiItems[0];
        InventoryItemOnClick(uiItems[0], dataSO[0]);
    }

    #endregion Methods
}