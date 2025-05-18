using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class VRSwimmingController : MonoBehaviour
{
    [Header("Swim Settings")] [SerializeField]
    public float propulsionStrength = 2f;

    [SerializeField] public float waterDrag = 2f;
    [SerializeField] private float requiredStrokeVelocity = 2f;
    [SerializeField] private float strokeCooldown = 2f;

    [Header("Input Actions")] [SerializeField]
    private InputActionReference leftHandGripInput;

    [SerializeField] private InputActionReference leftHandVelocityInput;

    [SerializeField] private InputActionReference rightHandGripInput;
    [SerializeField] private InputActionReference rightHandVelocityInput;


    [Header("References")] [SerializeField]
    private Transform orientationReference; // Usually XR Rig or Camera Offset

    [Header("Boundary Settings")] [SerializeField]
    private LayerMask waterBoundariesLayer;

    [SerializeField] private float boundaryCheckDistance = 0.5f;

    [SerializeField] private Rigidbody playerBody;
    private float cooldownTimer;
    private Camera mainCamera;
    private float currentWaterSurfaceY;
    
    [SerializeField] private float headAboveWaterThreshold = 0.3f;

    public void SetWaterSurfaceHeight(float surfaceY)
    {
        currentWaterSurfaceY = surfaceY;
    }
    private void Start()
    {
        // this.enabled = false; // Ensure swimming is off until triggered
    }

    private void Awake()
    {
        mainCamera = Camera.main;
        playerBody = GetComponent<Rigidbody>();
        playerBody.useGravity = false;
        playerBody.constraints = RigidbodyConstraints.FreezeRotation;
    }
    
    private void FixedUpdate()
    {
        cooldownTimer += Time.fixedDeltaTime;
    
        bool isLeftGripping = leftHandGripInput.action.IsPressed();
        bool isRightGripping = rightHandGripInput.action.IsPressed();
        
        float headY = mainCamera.transform.position.y;
        float maxAllowedHeadY = currentWaterSurfaceY + headAboveWaterThreshold;
        
        Vector3 forwardDirection1 = orientationReference.forward.normalized;
        
        if (!CanMoveInDirection(forwardDirection1))
        {
            playerBody.linearVelocity = Vector3.zero;
            cooldownTimer = 0f;
        }
        
        // Debug.Log($"Head: {headY}, maxAllowedHeadY: {maxAllowedHeadY}");
        if (headY > maxAllowedHeadY && CanMoveInDirection(forwardDirection1))
        {
            float excess = headY - maxAllowedHeadY;

            // Apply downward force to simulate water boundary
            float pullStrength = Mathf.Clamp(excess * 20f, 0f, 20f);
            playerBody.AddForce(Vector3.down * pullStrength, ForceMode.Acceleration);
        }
        
        //Use for test without headset
        if (Keyboard.current != null)
        {
            Vector3 moveDirection = Vector3.zero;

            if (Keyboard.current.wKey.isPressed) // Forward
                moveDirection += orientationReference.forward;

            if (Keyboard.current.sKey.isPressed) // Backward
                moveDirection -= orientationReference.forward;

            if (Keyboard.current.aKey.isPressed) // Left
                moveDirection -= orientationReference.right;

            if (Keyboard.current.dKey.isPressed) // Right
                moveDirection += orientationReference.right;

            if (Keyboard.current.zKey.isPressed) // Up
                moveDirection += orientationReference.up;

            if (Keyboard.current.xKey.isPressed) // Down
                moveDirection -= orientationReference.up;

            if (moveDirection != Vector3.zero)
            {
                Vector3 direction = moveDirection.normalized;
                if (CanMoveInDirection(direction))
                {
                    playerBody.AddForce(direction * propulsionStrength, ForceMode.Acceleration);
                }
                else
                {
                    Debug.Log("Blocked by obstacle while trying to swim.");
                    playerBody.linearVelocity = Vector3.zero;
                    cooldownTimer = 0f;
                }
            }
        }
        
        
        if (cooldownTimer > strokeCooldown && isLeftGripping && isRightGripping)
        {
            Vector3 leftVelocity = leftHandVelocityInput.action.ReadValue<Vector3>();
            Vector3 rightVelocity = rightHandVelocityInput.action.ReadValue<Vector3>();
    
            Vector3 combinedLocalStroke = -(leftVelocity + rightVelocity);
    
            if (combinedLocalStroke.sqrMagnitude > Math.Pow(requiredStrokeVelocity, 2))
            {
                Vector3 swimDirection = orientationReference.TransformDirection(combinedLocalStroke);
                if (CanMoveInDirection(swimDirection.normalized) && !(headY > maxAllowedHeadY))
                {
                    playerBody.AddForce(swimDirection * propulsionStrength, ForceMode.Acceleration);
                    cooldownTimer = 0f;
                }
                else
                {
                    playerBody.linearVelocity = Vector3.zero;
                    cooldownTimer = 0f;
                }
                
            }
        }
        
        if (playerBody.linearVelocity.sqrMagnitude > 0.01f)
        {
            playerBody.AddForce(-playerBody.linearVelocity * waterDrag, ForceMode.Acceleration);
        }
        
    }

    private bool CanMoveInDirection(Vector3 direction)
    {
        float checkRadius = 0.3f;
        float checkDistance = boundaryCheckDistance;

        if (Physics.SphereCast(transform.position, checkRadius, direction, out RaycastHit hit, checkDistance, waterBoundariesLayer))
        {
            // ✅ Check angle between movement direction and hit surface normal
            float angle = Vector3.Angle(direction, hit.normal);
            if (angle > 75f) // Mostly head-on hit → block
                return false;
            else
                return true; // Grazing or stair-like → allow
        }

        return true;
    }

    // private bool CanMoveInDirection(Vector3 direction)
    // {
    //     if (Physics.Raycast(transform.position, direction, boundaryCheckDistance, waterBoundariesLayer))
    //     {
    //         return false;
    //     }
    //     return true;
    // }
}