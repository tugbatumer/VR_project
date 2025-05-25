using UnityEngine;
using UnityEngine.InputSystem;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }

    [SerializeField] private Transform playerRigRoot;
    [SerializeField] private InputActionReference teleportAction; // X

    private Vector3 lastCheckpointPosition;

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

    private void OnEnable()
    {
        teleportAction.action.performed += OnTeleportPressed;
        teleportAction.action.Enable();
    }

    private void OnDisable()
    {
        teleportAction.action.performed -= OnTeleportPressed;
        teleportAction.action.Disable();
    }

    public void SetCheckpoint(Vector3 position)
    {
        lastCheckpointPosition = position;
    }

    private void OnTeleportPressed(InputAction.CallbackContext context)
    {
        TeleportToCheckpoint();
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
        }
    }
}