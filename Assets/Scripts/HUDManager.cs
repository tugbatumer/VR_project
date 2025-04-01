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
    public TextMeshProUGUI healthPotionCount;
    public Image damageArrowImage;
    public Image healthPotionImage;
    
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
    }
    
    private void Update()
    {
        ironCount.text = $"{CollectibleManager.Instance.GetCollectibleCount(Collectible.CollectibleType.Iron)}";
        glassCount.text = $"{CollectibleManager.Instance.GetCollectibleCount(Collectible.CollectibleType.Glass)}";
        woodCount.text = $"{CollectibleManager.Instance.GetCollectibleCount(Collectible.CollectibleType.Wood)}";
        damageArrowCount.text = $"{InventoryManager.Instance.GetItemCount(InventoryManager.itemType.DamageArrow)}";
        healthPotionCount.text = $"{InventoryManager.Instance.GetItemCount(InventoryManager.itemType.HealthPotion)}";
    }
    
}
