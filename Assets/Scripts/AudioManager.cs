using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; set; }

    public AudioSource doorOpeningAudio;
    public AudioSource doorClosingAudio;
    
    public AudioSource cageDoorOpeningAudio;

    public AudioSource collectItemAudio;
    
    public AudioSource footstepsAudio;
    public AudioClip[] footstepsAudioClips;
    
    public AudioSource surfaceSwimAudio;
    public AudioSource underwaterAudio;
    public AudioClip[] underwaterSwimClips;
    public AudioClip[] waterStepClips;
    
    public AudioSource turnUIPageAudio;
    public AudioSource craftingSuccessAudio;
    public AudioSource resetButtonAudio;
    public AudioSource insertCollectibleAudio;
    
    public AudioSource lockedDoorAudio;
    public AudioSource arrowShootingAudio;

    public AudioSource puzzleSuccessAudio;
    public AudioSource puzzleFailureAudio;
    
    public AudioSource soundtrackAudio;
    
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void ChangeSoundtrackVolume(float volume)
    {
        soundtrackAudio.volume = volume;
    }
    
}
