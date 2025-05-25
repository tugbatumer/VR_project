using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ArcheryDoor : MonoBehaviour
{
    public Animator doorAnimator;
    
    public void OpenDoor()
    {
        if (TargetManager.Instance.gameOver)
        {
            doorAnimator.SetTrigger("ToggleDoor");
            this.GetComponent<XRSimpleInteractable>().enabled = false;
        }
        else
        {
            AudioManager.Instance.lockedDoorAudio.PlayOneShot(AudioManager.Instance.lockedDoorAudio.clip, MenuManager.Instance.masterVolumeScaler);
        }
    }
    
    public void PlayDoorOpeningAudio()
    {
        AudioManager.Instance.doorOpeningAudio.PlayOneShot(AudioManager.Instance.doorOpeningAudio.clip, MenuManager.Instance.masterVolumeScaler);
    }
}
