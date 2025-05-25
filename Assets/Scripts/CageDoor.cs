using UnityEngine;

public class CageDoor: MonoBehaviour
{
    public void PlayCageDoorOpeningAudio()
    {
        AudioManager.Instance.cageDoorOpeningAudio.PlayOneShot(AudioManager.Instance.cageDoorOpeningAudio.clip, MenuManager.Instance.masterVolumeScaler);
    }
}
