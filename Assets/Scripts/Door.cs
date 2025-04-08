using UnityEngine;

public class DoorSoundEvents : MonoBehaviour
{
    public void PlayDoorOpeningAudio()
    {
        AudioManager.Instance.doorOpeningAudio.Play();
    }

    public void PlayDoorClosingAudio()
    {
        AudioManager.Instance.doorClosingAudio.Play();
    }
}
