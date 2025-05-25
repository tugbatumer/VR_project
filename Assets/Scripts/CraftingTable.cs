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
                AudioManager.Instance.insertCollectibleAudio.PlayOneShot(AudioManager.Instance.insertCollectibleAudio.clip, MenuManager.Instance.masterVolumeScaler);
                
                int index = (int)heldCollectible.collectibleType;
                insertedTypes.Add(heldCollectible.collectibleType);
                
                collectibleSlots[totalCollectibleCount].sprite = CraftingManager.Instance.collectibleSprites[index];
                totalCollectibleCount++;

                Destroy(InventoryManager.Instance.heldCollectible);
                InventoryManager.Instance.heldCollectible = null;
                
                CraftingUIManager.Instance.ShowCraftingPanel();
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
                resultPreviewSlot.sprite = CraftingManager.Instance.xSprite;
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
                AudioManager.Instance.craftingSuccessAudio.PlayOneShot(AudioManager.Instance.craftingSuccessAudio.clip, MenuManager.Instance.masterVolumeScaler);
                
            }

            clearTable();
            
            CraftingUIManager.Instance.ShowRecipesPanel();
        }
        
    }

    public void resetTable()
    {
        AudioManager.Instance.resetButtonAudio.PlayOneShot(AudioManager.Instance.resetButtonAudio.clip, MenuManager.Instance.masterVolumeScaler);
        
        foreach (var type in insertedTypes)
        {
            CollectibleManager.Instance.IncrementCollectibleCount(type, 1);
        }
        
        clearTable();
        
        CraftingUIManager.Instance.ShowRecipesPanel();
    }

    private void clearTable()
    {
        totalCollectibleCount = 0;
        insertedTypes.Clear();

        foreach (var img in collectibleSlots)
        {
            img.sprite = CraftingManager.Instance.transparentSprite;
        }

        resultPreviewSlot.sprite = CraftingManager.Instance.transparentSprite;
    }
}