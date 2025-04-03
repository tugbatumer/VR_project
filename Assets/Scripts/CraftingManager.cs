using UnityEngine;
using System.Collections.Generic;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager Instance { get; set; }
    
    [Header("Crafted Item Sprites")]
    public Sprite[] resultSprites;

    [Header("Collectible Sprites")]
    public Sprite[] collectibleSprites;
    
    public Dictionary<(Collectible.CollectibleType, Collectible.CollectibleType), InventoryManager.itemType> recipes
        => new Dictionary<(Collectible.CollectibleType, Collectible.CollectibleType), InventoryManager.itemType>
        {
            { SortPair(Collectible.CollectibleType.Iron, Collectible.CollectibleType.Wood), InventoryManager.itemType.DamageArrow },
            { SortPair(Collectible.CollectibleType.Iron, Collectible.CollectibleType.Glass), InventoryManager.itemType.HealthPotion },
            { SortPair(Collectible.CollectibleType.Glass, Collectible.CollectibleType.Wood), InventoryManager.itemType.LightArrow }
        };

    
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
    
    public static (Collectible.CollectibleType, Collectible.CollectibleType) SortPair(
        Collectible.CollectibleType a,
        Collectible.CollectibleType b)
    {
        return a.CompareTo(b) < 0 ? (a, b) : (b, a);
    }
}
