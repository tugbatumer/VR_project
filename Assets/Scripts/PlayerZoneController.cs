using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class PlayerZoneController : MonoBehaviour
{
    private enum Zone
    {
        Land,
        Water
    }

    private Zone currentZone = Zone.Land;

    [SerializeField] private VRSwimmingController swimmingController;
    private DynamicMoveProvider moveProvider;

    private XRMovementWithFootsteps footsteps;
    // private CharacterController characterController;
    // private SwimAudioManager swimAudioManager;

    private void Awake()
    {
        swimmingController = GetComponent<VRSwimmingController>();
        moveProvider = GetComponentInChildren<DynamicMoveProvider>();
        footsteps = GetComponentInChildren<XRMovementWithFootsteps>();
        // characterController = GetComponent<CharacterController>();
        // swimAudioManager = GetComponentInChildren<SwimAudioManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Land"))
        {
            if (currentZone != Zone.Land)
            {
                Debug.Log("Switched to Land");
                currentZone = Zone.Land;
                ApplyMode();
            }
        }
        else if (other.CompareTag("Water"))
        {
            if (currentZone != Zone.Water)
            {
                Debug.Log("Switched to Water");
                currentZone = Zone.Water;


                BoxCollider waterCollider = other.GetComponent<BoxCollider>();

                float surfaceY = waterCollider.transform.position.y
                                 + waterCollider.center.y
                                 + waterCollider.size.y / 2f;

                swimmingController.SetWaterSurfaceHeight(surfaceY);
                ApplyMode();
            }
        }
    }

    private void ApplyMode()
    {
        switch (currentZone)
        {
            case Zone.Land:
                swimmingController.enabled = false;
                moveProvider.enabled = true;
                footsteps.enabled = true;
                GetComponent<CharacterController>().enabled = true;
                // if (swimAudioManager != null)
                //     swimAudioManager.StopAllSounds();
                break;

            case Zone.Water:
                moveProvider.enabled = false;
                footsteps.enabled = false;
                GetComponent<CharacterController>().enabled = false;
                swimmingController.enabled = true;

                break;
        }
    }
}