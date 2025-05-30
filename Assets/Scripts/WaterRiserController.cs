using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRiserController : MonoBehaviour
{
    public static WaterRiserController Instance { get; set; }
    [Header("Rising Settings")]
    public Transform waterObject;
    public Vector3 targetLocalPosition;
    public float riseDuration = 2f;

    private Vector3 startLocalPosition;
    
    [Header("Colliders To Activate After Rising")]
    public List<BoxCollider> activateAfterRise;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        if (waterObject != null)
        {
            startLocalPosition = waterObject.localPosition;
        }
    }

    public void StartRising()
    {
        if (waterObject != null)
        {
            // StopAllCoroutines();
            StartCoroutine(RiseCoroutine());
        }
    }

    private IEnumerator RiseCoroutine()
    {
        float elapsed = 0f;
        Vector3 startPos = waterObject.localPosition;

        while (elapsed < riseDuration)
        {
            waterObject.localPosition = Vector3.Lerp(startPos, targetLocalPosition, elapsed / riseDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        waterObject.localPosition = targetLocalPosition;
        
        foreach (var col in activateAfterRise)
        {
            if (col)
                col.enabled = true;
        }
    }
}
