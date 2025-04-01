using UnityEngine;
using System.Collections.Generic;

public class CraftingTable : MonoBehaviour
{
    [Header("Collectible Counts")] 
    public int maxCount = 3;
    public int totalCollectibleCount = 0;

    // Use an array to hold counts for each collectible type
    public int[] collectibleCounts;
    
    private Dictionary<InventoryManager.itemType, int[]> recipes = new Dictionary<InventoryManager.itemType, int[]>
    {
        { InventoryManager.itemType.DamageArrow,  new int[] { 1, 0, 1 }},
        { InventoryManager.itemType.HealthPotion,  new int[] { 1, 1, 0 }}
    };

    void Awake()
    {
        int numTypes = System.Enum.GetValues(typeof(Collectible.CollectibleType)).Length;
        collectibleCounts = new int[numTypes];
    }

    public void insertCollectible()
    {
        if (InventoryManager.Instance.heldCollectible != null && totalCollectibleCount < maxCount)
        {
            Collectible heldCollectible = InventoryManager.Instance.heldCollectible.GetComponent<Collectible>();

            if (heldCollectible != null)
            {
                int index = (int)heldCollectible.collectibleType;
                collectibleCounts[index]++;
                totalCollectibleCount++;

                Destroy(InventoryManager.Instance.heldCollectible);
                InventoryManager.Instance.heldCollectible = null;
            }
        }
    }
    
    public void craftItem()
    {
        foreach (KeyValuePair<InventoryManager.itemType, int[]> recipe in recipes)
        {
            bool canCraft = true;
            for (int i = 0; i < recipe.Value.Length; i++)
            {
                if (collectibleCounts[i] != recipe.Value[i])
                {
                    canCraft = false;
                    break;
                }
            }

            if (canCraft)
            {
                totalCollectibleCount = 0;
                for (int i = 0; i < recipe.Value.Length; i++)
                {
                    collectibleCounts[i] = 0;
                }

                InventoryManager.Instance.IncrementItemCount(recipe.Key, 1);
                break;
            }
        }
    }

    public void resetTable()
    {
        totalCollectibleCount = 0;
        for (int i = 0; i < collectibleCounts.Length; i++)
        {
            CollectibleManager.Instance.IncrementCollectibleCount((Collectible.CollectibleType)i, collectibleCounts[i]);
            collectibleCounts[i] = 0;
        }
    }
}
