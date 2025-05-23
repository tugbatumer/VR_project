using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ConsumptionManager : MonoBehaviour
{
    public static ConsumptionManager Instance { get; set; }
    
    private InventoryManager.itemType[] consumableTypes = { InventoryManager.itemType.Bow,
        InventoryManager.itemType.OxygenPotion, };
    
    public InputActionReference cycleAction;
    public InputActionReference putBackAction;
    public InputActionReference useAction;
    
    internal GameObject heldConsumable = null;
    private int currentIndex = 0;
    public Transform leftHandTransform;
    public Transform rightHandTransform;
    public XRBaseInteractor handInteractor;
    
    [Header("Item Prefabs")]
    public GameObject bowPartPrefab;
    public GameObject oxygenPotionPartPrefab;
    public float OxygenPotionAmount = 0.5f;
    
    private Dictionary<InventoryManager.itemType, GameObject> itemPrefabs;
    
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
        
        itemPrefabs = new Dictionary<InventoryManager.itemType, GameObject>
        {
            { InventoryManager.itemType.Bow, bowPartPrefab },
            { InventoryManager.itemType.OxygenPotion, oxygenPotionPartPrefab }
        };
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnEnable()
    {
        cycleAction.action.performed += ctx => CycleNextItem();
        putBackAction.action.performed += ctx => PutBackConsumable();
        useAction.action.performed +=  ctx => Use();
        cycleAction.action.Enable();
        putBackAction.action.Enable();
        useAction.action.Enable();
    }

    void OnDisable()
    {
        cycleAction.action.Disable();
        putBackAction.action.Disable();
        useAction.action.Disable();
    }
    
    void CycleNextItem()
    {
        if (heldConsumable != null)
        {
            InventoryManager.Instance.IncrementItemCount(consumableTypes[currentIndex], 1);
            Destroy(heldConsumable);
            heldConsumable = null;
        }
        
        currentIndex = (currentIndex + 1) % consumableTypes.Length;
        InventoryManager.itemType selectedType = consumableTypes[currentIndex];
        

        if (InventoryManager.Instance.GetItemCount(selectedType) > 0)
        {   
            
            if (selectedType == InventoryManager.itemType.Bow)
            { 
                heldConsumable = Instantiate(GetItemPrefab(selectedType), leftHandTransform);
                var rb = heldConsumable.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.useGravity = false;
                    rb.isKinematic = true;
                }
                heldConsumable.GetComponent<BowController>().bowHand = leftHandTransform;
                heldConsumable.GetComponent<BowController>().otherHand = rightHandTransform;
                heldConsumable.GetComponent<BowController>().pullingHand = rightHandTransform;
                heldConsumable.GetComponent<BowController>().handInteractor = handInteractor;
                heldConsumable.GetComponent<BowController>().leftHand = leftHandTransform;
                heldConsumable.GetComponent<BowController>().rightHand = rightHandTransform;
                
                heldConsumable.transform.localPosition = Vector3.zero;
                heldConsumable.transform.localRotation = Quaternion.identity;
                
            }

            else
            {
                heldConsumable = Instantiate(GetItemPrefab(selectedType), leftHandTransform);
                var rb = heldConsumable.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.useGravity = false;
                    rb.isKinematic = true;
                }

                heldConsumable.transform.localPosition = Vector3.zero;
                heldConsumable.transform.localRotation = Quaternion.Euler(270, 0, 0);
            }
            InventoryManager.Instance.IncrementItemCount(selectedType, -1);
        }
    }
    
    public GameObject GetItemPrefab(InventoryManager.itemType type)
    {
        return itemPrefabs.TryGetValue(type, out GameObject prefab) ? prefab : null;
    }

    public void PutBackConsumable()
    {
        if (heldConsumable != null)
        {
            InventoryManager.Instance.IncrementItemCount(consumableTypes[currentIndex], 1);
            Destroy(heldConsumable);
            heldConsumable = null;
        }
    }

    public void Use()
    {
        if (heldConsumable != null)
        {
            InventoryManager.itemType selectedType = consumableTypes[currentIndex];
            
            if (selectedType == InventoryManager.itemType.Bow)
            {
                heldConsumable.GetComponent<BowController>().FireArrow();
            }

            else
            {
                OxygenManager.Instance.IncreaseOxygen(OxygenPotionAmount);
            }
        }

    }
}
