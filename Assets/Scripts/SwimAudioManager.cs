using UnityEngine;

public class SwimAudioManager : MonoBehaviour
{
    public void PlaySurfaceSwim()
    {
        AudioManager.Instance.surfaceSwimAudio.PlayOneShot(AudioManager.Instance.surfaceSwimAudio.clip, MenuManager.Instance.masterVolumeScaler);
    }
    
    public void PlayUnderwater()
    {
        //AudioManager.Instance.underwaterAudio.loop = true;
        AudioManager.Instance.underwaterAudio.PlayOneShot(AudioManager.Instance.underwaterAudio.clip, MenuManager.Instance.masterVolumeScaler);
     
    }
    
    public void StopUnderwater()
    {
        AudioManager.Instance.underwaterAudio.Stop();
    }
    
    public void PlayUnderwaterSwim()
    {
        int index = Random.Range(0, AudioManager.Instance.underwaterSwimClips.Length);
        AudioManager.Instance.surfaceSwimAudio.PlayOneShot(AudioManager.Instance.underwaterSwimClips[index]);
    }
    

}