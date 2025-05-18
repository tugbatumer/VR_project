using UnityEngine;

public class SwimSound : MonoBehaviour
{
    [Header("Audio Source")] [SerializeField]
    private AudioSource audioSource;

    [Header("Swim Sounds")] [SerializeField]
    private AudioClip swimStrokeSound;

    [SerializeField] private AudioClip underwaterLoop;
    [SerializeField] private AudioClip surfaceLoop;

    private bool isUnderwater;
    private bool isLoopPlaying;

    public void PlaySwimStroke()
    {
        if (swimStrokeSound != null)
            audioSource.PlayOneShot(swimStrokeSound);
    }

    public void UpdateAmbientLoop(bool currentlyUnderwater)
    {
        if (currentlyUnderwater != isUnderwater || !isLoopPlaying)
        {
            isUnderwater = currentlyUnderwater;
            audioSource.loop = true;
            audioSource.clip = isUnderwater ? underwaterLoop : surfaceLoop;
            audioSource.Play();
            isLoopPlaying = true;
        }
    }

    public void StopAllSounds()
    {
        audioSource.Stop();
        isLoopPlaying = false;
    }
}