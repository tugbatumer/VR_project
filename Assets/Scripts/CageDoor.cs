using UnityEngine;

public class CageDoor: MonoBehaviour
{
    public void PlayCageDoorOpeningAudio()
    {
        AudioManager.Instance.cageDoorOpeningAudio.Play();
    }
    
}
