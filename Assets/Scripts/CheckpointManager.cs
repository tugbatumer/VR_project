using UnityEngine;
using UnityEngine.InputSystem;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }

    [SerializeField] private Transform playerRigRoot;

    private Vector3 lastCheckpointPosition = Vector3.zero;

    public bool LastCheckPointExist()
    {
        return lastCheckpointPosition != Vector3.zero;
    }
    
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


    public void SetCheckpoint(Vector3 position)
    {
        lastCheckpointPosition = position;
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
            playerRigRoot.position = lastCheckpointPosition;
            MenuManager.Instance.CloseMenu();
            ZoneController.Instance.DisableSwimming();
        }
    }
}