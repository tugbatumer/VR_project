using UnityEngine;

public class XRMovementWithFootsteps : MonoBehaviour
{
    public CharacterController characterController;
    public float gravity = 9.81f;
    public float stepInterval = 0.5f;
    public float minMoveThreshold = 0.01f;

    private float stepTimer;
    private Vector3 lastPosition;
    private Vector3 velocity;

    void Start()
    {
        lastPosition = transform.position;
        stepTimer = stepInterval;
    }

    void Update()
    {
        velocity.y -= gravity * Time.deltaTime;
        Vector3 totalMovement = velocity * Time.deltaTime;
        characterController.Move(totalMovement);
        
        float movementThisFrame = Vector3.Distance(transform.position, lastPosition);
        bool isMoving = characterController.isGrounded && movementThisFrame > minMoveThreshold;

        if (isMoving)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0.0f)
            {
                PlayFootstepAudio();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = stepInterval * 0.3f;
        }

        lastPosition = transform.position;
    }

    void PlayFootstepAudio()
    {
        int index = Random.Range(0, AudioManager.Instance.footstepsAudioClips.Length);
        AudioManager.Instance.footstepsAudio.PlayOneShot(AudioManager.Instance.footstepsAudioClips[index]);
    }
}
