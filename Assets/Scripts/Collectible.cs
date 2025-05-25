using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int amount = 1;
    public CollectibleType collectibleType;
    public enum CollectibleType
    {
        Crystal,
        Feather,
        Wood,
        PuzzleKey
    }
    
    public void OnPickedUp()
    {
        AudioManager.Instance.collectItemAudio.PlayOneShot(AudioManager.Instance.collectItemAudio.clip, MenuManager.Instance.masterVolumeScaler);
        CollectibleManager.Instance.PickUpCollectible(this);
        Destroy(gameObject);
    }
}
