using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; set; }
    
    [Header("Collectibles")] 
    public TextMeshProUGUI ironCount;
    public TextMeshProUGUI glassCount;
    public TextMeshProUGUI woodCount;
    public Image ironImage;
    public Image glassImage;
    public Image woodImage;
    
    [Header("Craftables")] 
    public TextMeshProUGUI damageArrowCount;
    public TextMeshProUGUI lightArrowCount;
    public TextMeshProUGUI healthPotionCount;
    public Image damageArrowImage;
    public Image lightArrowImage;
    public Image healthPotionImage;
    
    [Header("Oxygen UI")]
    public Image oxygenBar;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        ironImage.sprite = Resources.Load<GameObject>("IronImage").GetComponent<SpriteRenderer>().sprite;
        glassImage.sprite = Resources.Load<GameObject>("GlassImage").GetComponent<SpriteRenderer>().sprite;
        woodImage.sprite = Resources.Load<GameObject>("WoodImage").GetComponent<SpriteRenderer>().sprite;
        damageArrowImage.sprite = Resources.Load<GameObject>("DamageArrowImage").GetComponent<SpriteRenderer>().sprite;
        healthPotionImage.sprite = Resources.Load<GameObject>("HealthPotionImage").GetComponent<SpriteRenderer>().sprite;
        lightArrowImage.sprite = Resources.Load<GameObject>("LightArrowImage").GetComponent<SpriteRenderer>().sprite;
    }
    
    private void Update()
    {
        ironCount.text = $"{CollectibleManager.Instance.GetCollectibleCount(Collectible.CollectibleType.Iron)}";
        glassCount.text = $"{CollectibleManager.Instance.GetCollectibleCount(Collectible.CollectibleType.Glass)}";
        woodCount.text = $"{CollectibleManager.Instance.GetCollectibleCount(Collectible.CollectibleType.Wood)}";
        damageArrowCount.text = $"{InventoryManager.Instance.GetItemCount(InventoryManager.itemType.DamageArrow)}";
        healthPotionCount.text = $"{InventoryManager.Instance.GetItemCount(InventoryManager.itemType.HealthPotion)}";
        lightArrowCount.text = $"{InventoryManager.Instance.GetItemCount(InventoryManager.itemType.LightArrow)}";
    }

    public void UpdateOxygenBar(float fillAmount)
    {
        if (oxygenBar)
        {
            oxygenBar.fillAmount = Mathf.Clamp01(fillAmount);
        }
    }

    public void ShowOxygenBar(bool show)
    {
        if (oxygenBar)
        {
            oxygenBar.gameObject.SetActive(show);
        }
    }
    
}
