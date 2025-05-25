using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class ZoneController : MonoBehaviour
{
    public static ZoneController Instance { get; private set; }

    [SerializeField] private VRSwimmingController swimmingController;
    [SerializeField] private DynamicMoveProvider moveProvider;
    [SerializeField] private XRMovementWithFootsteps footsteps;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private OxygenManager oxygenManager;
    public Rigidbody playerRigidbody;  
    private Zone.ZoneType currentZone = Zone.ZoneType.None;
    public bool IsWalksOnWater { get; set; } = false;

    public void SwitchToNoneZone()
    {
        IsWalksOnWater = false;
        currentZone = Zone.ZoneType.None;
    }
    public bool IsLand() => currentZone == Zone.ZoneType.Land;
    private void Awake()
    {
        Instance = this;
    }

    public void SwitchZone(Zone.ZoneType newZone, float? surfaceY)
    {
        //Debug.Log($"Try swıtched zone: {currentZone}");
        if (newZone == currentZone) return;

        currentZone = newZone;
        Debug.Log($"Switched to zone: {currentZone}");

        if (currentZone == Zone.ZoneType.Water)
        {
            EnableSwimming(surfaceY);
        }
        else // Land
        {
            DisableSwimming();
        }
    }
    
    public void EnableSwimming(float? surfaceY)
    {
        moveProvider.enabled = false;
        footsteps.enabled = false;
        characterController.enabled = false;
        swimmingController.Enable(surfaceY);
        while (swimmingController.IsUnderwater)
        {
            Vector3 pushDirection = Vector3.up * 10f;
            playerRigidbody.AddForce(pushDirection, ForceMode.Acceleration);
        }
        playerRigidbody.AddForce(Vector3.forward * 200, ForceMode.Acceleration);
    }
    
    public void DisableSwimming()
    {
        swimmingController.Disable();
        moveProvider.enabled = true;
        footsteps.enabled = true;
        characterController.enabled = true;
        IsWalksOnWater = true;
    }
}