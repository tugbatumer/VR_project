using UnityEngine;

public class XRMovementWithFootsteps : MonoBehaviour
{
    public CharacterController characterController;
    public float stepInterval = 0.5f;
    public float minMoveThreshold = 0.01f;
    public float rayThreshold = 0.1f;

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
        float movementThisFrame = Vector3.Distance(transform.position, lastPosition);
        bool isMoving = IsGrounded() && movementThisFrame > minMoveThreshold;

        if (isMoving)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0.0f)
            {
                if (ZoneController.Instance.IsWalksOnWater)
                    PlayWaterFootstepAudio();
                else
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

    bool IsGrounded()
    {
        return true;
        Vector3 rayOrigin = transform.TransformPoint(characterController.center);
        float rayDistance = (characterController.height / 2f) + rayThreshold;
        return Physics.Raycast(rayOrigin, Vector3.down, rayDistance);
    }

    void PlayWaterFootstepAudio()
    {
        int index = Random.Range(0, AudioManager.Instance.waterStepClips.Length);
        AudioManager.Instance.surfaceSwimAudio.PlayOneShot(AudioManager.Instance.waterStepClips[index]);
    }

    void PlayFootstepAudio()
    {
        int index = Random.Range(0, AudioManager.Instance.footstepsAudioClips.Length);
        AudioManager.Instance.footstepsAudio.PlayOneShot(AudioManager.Instance.footstepsAudioClips[index]);
    }
}