using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    
    public static CollectibleManager Instance { get; set; }
    
    [Header("Collectibles")] 
    public int ironAmount = 0;
    public int glassAmount = 0;
    public int woodAmount = 0;
    
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
    }
    
    internal void PickUpCollectible(Collectible collectible)
    {
        switch (collectible.collectibleType)
        {
            case Collectible.CollectibleType.Iron:
                ironAmount += collectible.ironAmount;
                break;
            
            case Collectible.CollectibleType.Glass:
                glassAmount += collectible.glassAmount;
                break;
            
            case Collectible.CollectibleType.Wood:
                woodAmount += collectible.woodAmount;
                break;
            
            default:
                return;
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
