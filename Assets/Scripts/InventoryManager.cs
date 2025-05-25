using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; set; }
    
    public InputActionReference cycleAction;
    public InputActionReference dropAction;
    public InputActionReference putBackAction;

    internal GameObject heldCollectible = null;
    private Collectible.CollectibleType[] collectibleTypes = {  Collectible.CollectibleType.Crystal,
                                                                Collectible.CollectibleType.Feather,
                                                                Collectible.CollectibleType.Wood };
    
    public enum itemType
    {
        Bow,
        Arrow,
        OxygenPotion
    }
    
    
    private int currentIndex = 0;
    public Transform handTransform;

    
    public Dictionary<itemType, int> itemCounts = new Dictionary<itemType, int>
    {
        { itemType.Bow, 0 },
        { itemType.Arrow, 0 },
        { itemType.OxygenPotion, 0 }
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
    
    void OnEnable()
    {
        cycleAction.action.performed += ctx => CycleNextItem();
        dropAction.action.performed += ctx => DropItem();
        putBackAction.action.performed += ctx => PutBackItem();
        cycleAction.action.Enable();
        dropAction.action.Enable();
        putBackAction.action.Enable();
    }

    void OnDisable()
    {
        cycleAction.action.Disable();
        dropAction.action.Disable();
        putBackAction.action.Disable();
    }
    
    void CycleNextItem()
    {
        if (heldCollectible != null)
        {
            CollectibleManager.Instance.IncrementCollectibleCount(collectibleTypes[currentIndex], 1);
            Destroy(heldCollectible);
            heldCollectible = null;
        }
        
        currentIndex = (currentIndex + 1) % collectibleTypes.Length;
        Collectible.CollectibleType selectedType = collectibleTypes[currentIndex];
        

        if (CollectibleManager.Instance.GetCollectibleCount(selectedType) > 0)
        {   
            CollectibleManager.Instance.IncrementCollectibleCount(selectedType, -1);
            heldCollectible = Instantiate(CollectibleManager.Instance.GetCollectiblePrefab(selectedType), handTransform);
            var rb = heldCollectible.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
                rb.isKinematic = true;
            }
            heldCollectible.transform.localPosition = Vector3.zero;
            if (selectedType != Collectible.CollectibleType.Wood)
            {
                heldCollectible.transform.localRotation = Quaternion.identity;
            }
            else 
            {
                heldCollectible.transform.localRotation = Quaternion.Euler(90, 0, 0);
            }
        }
    }

    void DropItem()
    {
        if (heldCollectible != null)
        {
            var rb = heldCollectible.GetComponent<Rigidbody>();
            if (rb != null)
            {
                heldCollectible.transform.position += handTransform.forward * 0.1f;
                rb.linearVelocity = handTransform.forward * 1.5f;
                rb.useGravity = true;
                rb.isKinematic = false;
            }
            
            heldCollectible.transform.SetParent(null);
            heldCollectible = null;
        }
    }
    
    public int GetItemCount(itemType type)
    {
        return itemCounts.TryGetValue(type, out int count) ? count : 0;
    }
    
    public void IncrementItemCount(itemType type, int amount)
    {
        if (itemCounts.ContainsKey(type))
        {
            itemCounts[type] += amount;
        }
    }

    public void PutBackItem()
    {
        if (heldCollectible != null)
        {
            CollectibleManager.Instance.IncrementCollectibleCount(collectibleTypes[currentIndex], 1);
            Destroy(heldCollectible);
            heldCollectible = null;
        }
    }
    
}
