using UnityEngine;

public class OxygenManager : MonoBehaviour
{
    public static OxygenManager Instance { get; set; }
    
    
    [Header("Oxygen Settings")]
    [Tooltip("Rate at which oxygen depletes underwater (normalized per second)")]
    public float depletionRate = 0.1f;

    [Tooltip("Rate at which oxygen replenishes at the surface (normalized per second)")]
    public float replenishRate = 0.2f;

    [Tooltip("Force applied to push player up when oxygen is depleted")]
    public float pushUpForce = 10f;

    [Header("References")] // XR Camera
    public Rigidbody playerRigidbody;       // Player Rigidbody

    private float currentOxygen = 1f;        // Normalized (0 to 1)
    public bool isUnderwater;
    
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
    
    private void Start()
    {
        currentOxygen = 1f;
        HUDManager.Instance?.UpdateOxygenBar(currentOxygen);
        HUDManager.Instance?.ShowOxygenBar(false);
    }
    
    public void IncreaseOxygen(float amount)
    {
        currentOxygen += amount;
        currentOxygen = Mathf.Clamp01(currentOxygen);
        HUDManager.Instance.UpdateOxygenBar(currentOxygen);
    }

    private void Update()
    {
        if (isUnderwater)
        {
            currentOxygen -= depletionRate * Time.deltaTime;
            HUDManager.Instance.ShowOxygenBar(true);

            if (currentOxygen <= 0.2f)
            {
                Vector3 pushDirection = Vector3.up * pushUpForce;
                playerRigidbody.AddForce(pushDirection, ForceMode.Acceleration);
            }
        }
        else
        {
            currentOxygen += replenishRate * Time.deltaTime;

            if (currentOxygen >= 1f)
            {
                currentOxygen = 1f;
                HUDManager.Instance.ShowOxygenBar(false);
            }
        }

        currentOxygen = Mathf.Clamp01(currentOxygen);
        HUDManager.Instance.UpdateOxygenBar(currentOxygen);
    }
}
