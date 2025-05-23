using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance { get; set; }

    [Header("Collectibles")]
    public GameObject crystalPartPrefab;
    public GameObject featherPartPrefab;
    public GameObject woodPartPrefab;
    public GameObject puzzleKeyPartPrefab;

    private Dictionary<Collectible.CollectibleType, int> collectibleCounts;
    private Dictionary<Collectible.CollectibleType, GameObject> collectiblePrefabs;

    public bool hasPuzzleKey = false;

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
        
        collectibleCounts = new Dictionary<Collectible.CollectibleType, int>
        {
            { Collectible.CollectibleType.Crystal, 0 },
            { Collectible.CollectibleType.Feather, 0 },
            { Collectible.CollectibleType.Wood, 0 }
        };

        collectiblePrefabs = new Dictionary<Collectible.CollectibleType, GameObject>
        {
            { Collectible.CollectibleType.Crystal, crystalPartPrefab },
            { Collectible.CollectibleType.Feather, featherPartPrefab },
            { Collectible.CollectibleType.Wood, woodPartPrefab },
            { Collectible.CollectibleType.PuzzleKey, puzzleKeyPartPrefab } // Assuming PuzzleKey doesn't have a prefab
        };
    }

    internal void PickUpCollectible(Collectible collectible)
    {
        if (collectibleCounts.ContainsKey(collectible.collectibleType))
        {
            collectibleCounts[collectible.collectibleType] += collectible.amount;
        }
        
        else if (collectible.collectibleType == Collectible.CollectibleType.PuzzleKey)
        {
            hasPuzzleKey = true;
        }
    }

    public int GetCollectibleCount(Collectible.CollectibleType type)
    {
        return collectibleCounts.TryGetValue(type, out int count) ? count : 0;
    }

    public GameObject GetCollectiblePrefab(Collectible.CollectibleType type)
    {
        return collectiblePrefabs.TryGetValue(type, out GameObject prefab) ? prefab : null;
    }

    public void IncrementCollectibleCount(Collectible.CollectibleType type, int amount)
    {
        if (collectibleCounts.ContainsKey(type))
        {
            collectibleCounts[type] += amount;
        }
    }
}
