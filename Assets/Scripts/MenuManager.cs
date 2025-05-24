using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class MenuManager : MonoBehaviour
{
    public GameObject startCanvas;
    public GameObject locomotionSystem;
    public GameObject hudCanvas;
    public Transform gameplaySpawn;
    public GameObject xrRig;
    public CanvasGroup fadeCanvas;
    public InputActionReference menuAction;
    public Transform MenuTransform;
    public GameObject rightController;
    public float masterVolumeScaler = 1.0f;

    public float fadeDuration = 3.0f;

    void Start()
    {
        locomotionSystem.SetActive(false);
        hudCanvas.SetActive(false);
        startCanvas.SetActive(true);

        // Optional: Start with full black fade
        fadeCanvas.alpha = 1f;
        fadeCanvas.blocksRaycasts = true;
        fadeCanvas.interactable = false;

        StartCoroutine(FadeIn());
    }
    
    void OnEnable()
    {
        menuAction.action.performed += ctx => OpenMenu();
        menuAction.action.Enable();
    }
    
    void OnDisable()
    {
        menuAction.action.Disable();
    }

    public void StartGame()
    {
        locomotionSystem.SetActive(true);
        hudCanvas.SetActive(true);
        startCanvas.SetActive(false);

        // Move XR Rig to game position
        xrRig.transform.SetPositionAndRotation(gameplaySpawn.position, gameplaySpawn.rotation);

        // Fade into game
        StartCoroutine(FadeIn());

        FindFirstObjectByType<TutorialManager>().ShowTutorial("Oh no you are trapped! Maybe there is a key somewhere.");
    }
    
    void OpenMenu()
    {
        locomotionSystem.SetActive(false);
        hudCanvas.SetActive(false);
        startCanvas.SetActive(true);
        
        xrRig.transform.SetPositionAndRotation(MenuTransform.position, MenuTransform.rotation);
        
        fadeCanvas.alpha = 1f;
        fadeCanvas.blocksRaycasts = true;
        fadeCanvas.interactable = false;

        StartCoroutine(FadeIn());
        
    }

    IEnumerator FadeIn()
    {
        float t = fadeDuration;
        while (t > 0)
        {
            t -= Time.deltaTime;
            fadeCanvas.alpha = t / fadeDuration;
            yield return null;
        }
        fadeCanvas.alpha = 0f;
        fadeCanvas.blocksRaycasts = false;
    }

    IEnumerator FadeOut()
    {
        float t = 0f;
        fadeCanvas.blocksRaycasts = true;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeCanvas.alpha = t / fadeDuration;
            yield return null;
        }
        fadeCanvas.alpha = 1f;
    }

    public void ChangeRotationType(Int32 rotationType)
    {
        if (rightController != null)
        {
            var controllerManager = rightController.GetComponent<ControllerInputActionManager>();

            if (rotationType == 0)
            {
                controllerManager.smoothTurnEnabled = false;
            }
            if (rotationType == 1)
            {
                controllerManager.smoothTurnEnabled = true;
            }
        }
    }

    public void ChangeMasterVolumeScaler(float volume)
    {
        masterVolumeScaler = volume;
    }
}
