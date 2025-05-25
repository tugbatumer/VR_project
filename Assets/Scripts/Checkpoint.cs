using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            CheckpointManager.Instance.SetCheckpoint(transform.position);
            HUDManager.Instance?.ShowCheckpointMessage();
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}