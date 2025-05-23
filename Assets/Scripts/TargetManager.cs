using UnityEngine;

public class TargetHit : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            Debug.Log("🎯 Target hit!");

            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true; // Freeze the arrow
            }

            // Parent the arrow to the target so it sticks
            collision.transform.SetParent(this.transform);
        }
    }
}