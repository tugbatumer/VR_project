using UnityEngine;
using UnityEngine.InputSystem;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }

    [SerializeField] private Transform playerRigRoot;

    private Transform lastCheckpointPosition;
    
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


    public void SetCheckpoint(Transform position)
    {
        lastCheckpointPosition = position;
        MenuScreen.Instance.checkpointButton.interactable = true;
    }


    private void Update()
    {
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            TeleportToCheckpoint();
        }
    }

    public void TeleportToCheckpoint()
    {
        if (playerRigRoot)
        {
            MenuManager.Instance.CloseMenu(lastCheckpointPosition.position, lastCheckpointPosition.rotation);
            ZoneController.Instance.DisableSwimming();
        }
    }
}