using UnityEngine;

public class Target : MonoBehaviour
{
    public int ID;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true; // Freeze the arrow
            }
            collision.transform.SetParent(this.transform);
            TargetManager.Instance.HitEvent(ID);
        }
    }
}