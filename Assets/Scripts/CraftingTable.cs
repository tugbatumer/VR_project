using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CraftingTable : MonoBehaviour
{
 // ← Reference to CraftingManager
    [Header("Crafting Limits")]
    public int maxCount = 2;
    public int totalCollectibleCount = 0;

    [Header("Collectible Slots")]
    public Image[] collectibleSlots;

    [Header("Crafted Result Preview")]
    public Image resultPreviewSlot;

    private List<Collectible.CollectibleType> insertedTypes = new();

    public void insertCollectible()
    {
        if (InventoryManager.Instance.heldCollectible != null && totalCollectibleCount < maxCount)
        {
            Collectible heldCollectible = InventoryManager.Instance.heldCollectible.GetComponent<Collectible>();

            if (heldCollectible != null)
            {
                int index = (int)heldCollectible.collectibleType;
                insertedTypes.Add(heldCollectible.collectibleType);
                totalCollectibleCount++;

                for (int i = 0; i < collectibleSlots.Length; i++)
                {
                    if (collectibleSlots[i].sprite == null)
                    {
                        collectibleSlots[i].sprite = CraftingManager.Instance.collectibleSprites[index];
                        break;
                    }
                }

                Destroy(InventoryManager.Instance.heldCollectible);
                InventoryManager.Instance.heldCollectible = null;
            }
        }

        if (insertedTypes.Count == 2)
        {
            var key = CraftingManager.SortPair(insertedTypes[0], insertedTypes[1]);

            if (CraftingManager.Instance.recipes.TryGetValue(key, out var resultType))
            {
                resultPreviewSlot.sprite = CraftingManager.Instance.resultSprites[(int)resultType];
            }
            else
            {
                resultPreviewSlot.sprite = null;
            }
        }
    }

    public void craftItem()
    {
        if (insertedTypes.Count == 2)
        {
            var key = CraftingManager.SortPair(insertedTypes[0], insertedTypes[1]);

            if (CraftingManager.Instance.recipes.TryGetValue(key, out var result))
            {
                InventoryManager.Instance.IncrementItemCount(result, 1);
            }

            clearTable();
        }
    }

    public void resetTable()
    {
        foreach (var type in insertedTypes)
        {
            CollectibleManager.Instance.IncrementCollectibleCount(type, 1);
        }

        clearTable();
    }

    private void clearTable()
    {
        totalCollectibleCount = 0;
        insertedTypes.Clear();

        foreach (var img in collectibleSlots)
        {
            img.sprite = null;
        }

        resultPreviewSlot.sprite = null;
    }
}