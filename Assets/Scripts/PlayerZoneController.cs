
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class PlayerZoneController : MonoBehaviour
{
    private enum Zone { Land, Water }

    private Zone currentZone = Zone.Land;

    private VRSwimmingController swimmingController;
    private DynamicMoveProvider moveProvider;
    private Rigidbody playerRb;

    private void Awake()
    {
        swimmingController = GetComponentInChildren<VRSwimmingController>();
        moveProvider = GetComponentInChildren<DynamicMoveProvider>();
        playerRb = GetComponentInChildren<Rigidbody>();
    }
    

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered zone with tag: " + other.tag);
        if (other.CompareTag("Water"))
        {
            BoxCollider waterCollider = other.GetComponent<BoxCollider>();
            if (waterCollider != null)
            {
                // Calculate world Y position of the top surface of the water
                float surfaceY = waterCollider.transform.position.y
                                 + waterCollider.center.y
                                 + waterCollider.size.y / 2f;

                swimmingController.SetWaterSurfaceHeight(surfaceY);
            }
            currentZone = Zone.Water;
            ApplyMode();
        }
      
        else if (other.CompareTag("Land"))
        {
            currentZone = Zone.Land;
            ApplyMode();
        }
    }

    private void ApplyMode()
    {
        switch (currentZone)
        {
            case Zone.Land:
                swimmingController.enabled = false;
                moveProvider.enabled = true;
                playerRb.linearVelocity = Vector3.zero;
                break;
            
            case Zone.Water:
                swimmingController.enabled = true;
                moveProvider.enabled = false;
                playerRb.useGravity = false;
                break;
        }
    }
}
