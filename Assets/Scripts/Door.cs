using UnityEngine;

public class Door : MonoBehaviour
{
    public void PlayDoorOpeningAudio()
    {
        AudioManager.Instance.doorOpeningAudio.PlayOneShot(AudioManager.Instance.doorOpeningAudio.clip, MenuManager.Instance.masterVolumeScaler);
    }

    public void PlayDoorClosingAudio()
    {
        AudioManager.Instance.doorClosingAudio.PlayOneShot(AudioManager.Instance.doorClosingAudio.clip, MenuManager.Instance.masterVolumeScaler);
    }
}