using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class ZoneController : MonoBehaviour
{
    public static ZoneController Instance { get; private set; }

    [SerializeField] private VRSwimmingController swimmingController;
    [SerializeField] private DynamicMoveProvider moveProvider;
    [SerializeField] private XRMovementWithFootsteps footsteps;
    [SerializeField] private CharacterController characterController;

    private Zone.ZoneType currentZone = Zone.ZoneType.Land;

    private void Awake()
    {
        Instance = this;
    }

    public void SwitchZone(Zone.ZoneType newZone, float? surfaceY)
    {
        if (newZone == currentZone) return;

        currentZone = newZone;
        Debug.Log($"Switched to zone: {currentZone}");

        if (currentZone == Zone.ZoneType.Water)
        {
            swimmingController.SetWaterSurfaceHeight((float)surfaceY);
            swimmingController.Enable();
            moveProvider.enabled = false;
            footsteps.enabled = false;
            characterController.enabled = false;
        }
        else // Land
        {
            swimmingController.Disable();
            moveProvider.enabled = true;
            footsteps.enabled = true;
            characterController.enabled = true;
        }
    }
}