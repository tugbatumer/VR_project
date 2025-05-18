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
    
}
