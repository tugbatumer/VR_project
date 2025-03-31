using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int ironAmount = 5;
    public int glassAmount = 5;
    public int woodAmount = 5;
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
