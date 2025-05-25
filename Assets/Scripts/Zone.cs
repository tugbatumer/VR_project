using UnityEngine;

public class Zone : MonoBehaviour
{
    public enum ZoneType
    {
        None,
        Land,
        Water
    }

    public ZoneType zoneType;
    [SerializeField] private BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (zoneType == ZoneType.Water)
                ZoneController.Instance.SwitchZone(zoneType, GetSurface());
            else
                ZoneController.Instance.SwitchZone(zoneType, null);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            if (zoneType == ZoneType.Land)
                ZoneController.Instance.SwitchToNoneZone();
    }

    private float GetSurface()
    {
        return boxCollider.transform.position.y
               + (boxCollider.center.y + boxCollider.size.y / 2f) * boxCollider.transform.lossyScale.y;
        ;
    }
}