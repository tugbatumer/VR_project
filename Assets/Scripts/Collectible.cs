using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int amount = 1;
    public CollectibleType collectibleType;

    public enum CollectibleType
    {
        Iron,
        Glass,
        Wood
    }
    
    public void OnPickedUp()
    {
        CollectibleManager.Instance.PickUpCollectible(this);
        Destroy(gameObject);
    }
}
