using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class WaterZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            SwitchMode(other, true);
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SwitchMode(other, false); // Disable swimming
        }
    }


    private void SwitchMode(Collider player, bool enableSwimming)
    {
        Debug.Log("Swimming mode: " + enableSwimming);
        var swimmer = player.GetComponentInChildren<VRSwimmingController>();
        var mover = player.GetComponentInChildren<DynamicMoveProvider>();
        
        if (swimmer != null)
            swimmer.enabled = enableSwimming;

        if (mover != null)
            mover.enabled = !enableSwimming;
    }

  
}
