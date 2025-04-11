using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class VRSwimmingController : MonoBehaviour
{
    [Header("Swim Settings")]
    [SerializeField] public float propulsionStrength = 2f;
    [SerializeField] public float waterDrag = 2f;
    [SerializeField] private float requiredStrokeVelocity = 2f;
    [SerializeField] private float strokeCooldown = 2f;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference leftHandGripInput;
    [SerializeField] private InputActionReference leftHandVelocityInput;

    [SerializeField] private InputActionReference rightHandGripInput;
    [SerializeField] private InputActionReference rightHandVelocityInput;

    [Header("References")]
    [SerializeField] private Transform orientationReference; // Usually XR Rig or Camera Offset
    
    [Header("Boundary Settings")]
    [SerializeField] private LayerMask waterBoundariesLayer;
    [SerializeField] private float boundaryCheckDistance = 0.5f;

    private Rigidbody playerBody;
    private float cooldownTimer;

    
    private void Start()
    {
        this.enabled = false; // Ensure swimming is off until triggered
    }
    private void Awake()
    {
        playerBody = GetComponent<Rigidbody>();
        playerBody.useGravity = false;
        playerBody.constraints = RigidbodyConstraints.FreezeRotation;
    }
    
    
    private void FixedUpdate()
    {
        cooldownTimer += Time.fixedDeltaTime;
    
        bool isLeftGripping = leftHandGripInput.action.IsPressed();
        bool isRightGripping = rightHandGripInput.action.IsPressed();
    
        if (cooldownTimer > strokeCooldown && isLeftGripping && isRightGripping)
        {
            Vector3 leftVelocity = leftHandVelocityInput.action.ReadValue<Vector3>();
            Vector3 rightVelocity = rightHandVelocityInput.action.ReadValue<Vector3>();
    
            Vector3 combinedLocalStroke = -(leftVelocity + rightVelocity);
    
            if (combinedLocalStroke.sqrMagnitude > Math.Pow(requiredStrokeVelocity, 2))
            {
                Vector3 swimDirection = orientationReference.TransformDirection(combinedLocalStroke);
                if (CanMoveInDirection(swimDirection.normalized))
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
    
        // Apply drag to simulate water resistance
        if (playerBody.linearVelocity.sqrMagnitude > 0.01f)
        {
            playerBody.AddForce(-playerBody.linearVelocity * waterDrag, ForceMode.Acceleration);
        }
        
    }
    
    private bool CanMoveInDirection(Vector3 direction)
    {
        if (Physics.Raycast(transform.position, direction, boundaryCheckDistance, waterBoundariesLayer))
        {
            return false;
        }
        return true;
    }
}

