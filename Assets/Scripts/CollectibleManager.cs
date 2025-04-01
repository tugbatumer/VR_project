using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance { get; set; }

    [Header("Collectibles")]
    public GameObject ironPartPrefab;
    public GameObject glassPartPrefab;
    public GameObject woodPartPrefab;

    private Dictionary<Collectible.CollectibleType, int> collectibleCounts;
    private Dictionary<Collectible.CollectibleType, GameObject> collectiblePrefabs;

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
            { Collectible.CollectibleType.Iron, 0 },
            { Collectible.CollectibleType.Glass, 0 },
            { Collectible.CollectibleType.Wood, 0 }
        };

        collectiblePrefabs = new Dictionary<Collectible.CollectibleType, GameObject>
        {
            { Collectible.CollectibleType.Iron, ironPartPrefab },
            { Collectible.CollectibleType.Glass, glassPartPrefab },
            { Collectible.CollectibleType.Wood, woodPartPrefab }
        };
    }

    internal void PickUpCollectible(Collectible collectible)
    {
        if (collectibleCounts.ContainsKey(collectible.collectibleType))
        {
            collectibleCounts[collectible.collectibleType] += collectible.amount;
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
