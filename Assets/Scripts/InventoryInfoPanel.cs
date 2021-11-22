using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryInfoPanel : MonoBehaviour
{
    #region Fields

    public static InventoryInfoPanel Instance;

    public Image Image;

    public TMP_Text InfoTitle;
    public TMP_Text Name;
    public TMP_Text Description;
    public TMP_Text Stat;

    #endregion

    #region Unity

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Updates the UI, according to the class data.
    /// </summary>
    /// <param name="item"> Item data. </param>
    public void SetItemInformation(InventoryItemUI itemClicked, InventoryItemDataSO item)
    {
        Image.sprite = itemClicked.Icon.sprite;

        InfoTitle.text = item.Name;
        Name.text = item.Name;
        Description.text = item.Description;
        Stat.text = item.Stat.ToString();
    }

    #endregion
}
