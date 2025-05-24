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
            AudioManager.Instance.lockedDoorAudio.Play();
        }
    }
    
    public void PlayDoorOpeningAudio()
    {
        AudioManager.Instance.doorOpeningAudio.Play();
    }
}
