using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class KeyDoor : MonoBehaviour
{
    public GameObject puzzleKey;
    public Animator doorAnimator;
    
    private void Start()
    {
        puzzleKey.gameObject.SetActive(false);
    }
    
    public void OpenDoor()
    {
        if (CollectibleManager.Instance.hasPuzzleKey)
        {
            CollectibleManager.Instance.hasPuzzleKey = false;
            puzzleKey.gameObject.SetActive(true);
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
