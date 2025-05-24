using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; set; }
    
    [Header("Collectibles")] 
    public TextMeshProUGUI crystalCount;
    public TextMeshProUGUI featherCount;
    public TextMeshProUGUI woodCount;
    public Image crystalImage;
    public Image featherImage;
    public Image woodImage;
    
    [Header("Craftables")] 
    public TextMeshProUGUI bowCount;
    public TextMeshProUGUI arrowCount;
    public TextMeshProUGUI oxygenPotionCount;
    public Image bowImage;
    public Image arrowImage;
    public Image oxygenPotionImage;
    
    [Header("Oxygen UI")]
    public Image oxygenBar;
    
    [Header("Checkpoint Feedback")]
    public TextMeshProUGUI checkpointMessage;
    public float messageDuration = 2f;

    public void ShowCheckpointMessage()
    {
        if (checkpointMessage == null) return;
        // StopAllCoroutines();
        StartCoroutine(ShowMessageRoutine());
    }

    private IEnumerator ShowMessageRoutine()
    {
        checkpointMessage.alpha = 1;
        checkpointMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(messageDuration);
        checkpointMessage.gameObject.SetActive(false);
    }

    
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
        crystalImage.sprite = Resources.Load<GameObject>("CrystalImage").GetComponent<SpriteRenderer>().sprite;
        featherImage.sprite = Resources.Load<GameObject>("FeatherImage").GetComponent<SpriteRenderer>().sprite;
        woodImage.sprite = Resources.Load<GameObject>("WoodImage").GetComponent<SpriteRenderer>().sprite;
        bowImage.sprite = Resources.Load<GameObject>("BowImage").GetComponent<SpriteRenderer>().sprite;
        oxygenPotionImage.sprite = Resources.Load<GameObject>("OxygenPotionImage").GetComponent<SpriteRenderer>().sprite;
        arrowImage.sprite = Resources.Load<GameObject>("ArrowImage").GetComponent<SpriteRenderer>().sprite;
    }
    
    private void Update()
    {
        crystalCount.text = $"{CollectibleManager.Instance.GetCollectibleCount(Collectible.CollectibleType.Crystal)}";
        featherCount.text = $"{CollectibleManager.Instance.GetCollectibleCount(Collectible.CollectibleType.Feather)}";
        woodCount.text = $"{CollectibleManager.Instance.GetCollectibleCount(Collectible.CollectibleType.Wood)}";
        bowCount.text = $"{InventoryManager.Instance.GetItemCount(InventoryManager.itemType.Bow)}";
        oxygenPotionCount.text = $"{InventoryManager.Instance.GetItemCount(InventoryManager.itemType.OxygenPotion)}";
        arrowCount.text = $"{InventoryManager.Instance.GetItemCount(InventoryManager.itemType.Arrow)}";
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
