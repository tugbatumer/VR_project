// using System;
// using UnityEngine;
// using UnityEngine.XR;
// using UnityEngine.XR.Interaction.Toolkit;
//
// public class PullInteractionV2 : MonoBehaviour
// {
//     public static event Action<float> PullActionReleased;
//
//     [Header("Bowstring Setup")]
//     public Transform start, end; // Defines bowstring limits
//     public GameObject notch; // The arrow notch position
//     public LineRenderer lineRenderer; // LineRenderer for bowstring
//
//     [Header("XR Controller Input")]
//     public XRController controller; // The VR controller (left or right)
//
//     private float pullAmount = 0.0f; // Pull percentage (0-1)
//     private Vector3 initialNotchPosition;
//     
//     private void Start()
//     {
//         if (notch)
//             initialNotchPosition = notch.transform.localPosition; // Store original notch position
//     }
//
//     private void Update()
//     {
//         // Get trigger button value
//         if (controller.inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f)
//         {
//             Vector3 pullPosition = controller.transform.position; // Get controller position
//             pullAmount = CalculatePull(pullPosition);
//             UpdateString();
//         }
//         else if (triggerValue <= 0.1f) // If trigger is released, fire the arrow
//         {
//             Release();
//         }
//     }
//
//     private float CalculatePull(Vector3 pullPosition)
//     {
//         Vector3 pullDirection = pullPosition - start.position;
//         Vector3 targetDirection = end.position - start.position;
//         float maxLength = targetDirection.magnitude;
//
//         targetDirection.Normalize();
//         float pullValue = Vector3.Dot(pullDirection, targetDirection) / maxLength;
//         return Mathf.Clamp(pullValue, 0, 1);
//     }
//
//     private void UpdateString()
//     {
//         Vector3 linePosition = Vector3.forward * Mathf.Lerp(start.transform.localPosition.z, end.transform.localPosition.z, pullAmount);
//         notch.transform.localPosition = new Vector3(notch.transform.localPosition.x, notch.transform.localPosition.y, linePosition.z + .2f);
//         lineRenderer.SetPosition(1, linePosition);
//     }
//
//     private void Release()
//     {
//         PullActionReleased?.Invoke(pullAmount);
//         pullAmount = 0f;
//         notch.transform.localPosition = initialNotchPosition; // Reset notch position
//         UpdateString();
//     }
// }
