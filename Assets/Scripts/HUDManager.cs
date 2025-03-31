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
    
    [Header("Potions")] 
    public TextMeshProUGUI healthPotionCount;
    public TextMeshProUGUI sprintPotionCount;
    public Image healthPotionImage;
    public Image sprintPotionImage;
    
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
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        ironCount.text = $"{CollectibleManager.Instance.ironAmount}";
        glassCount.text = $"{CollectibleManager.Instance.glassAmount}";
        woodCount.text = $"{CollectibleManager.Instance.woodAmount}";
    }
    
}
