using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }
    
    public GameObject menuCanvas;
    public GameObject locomotionSystem;
    public GameObject hudCanvas;
    public Transform gameplaySpawn;
    public GameObject xrRig;
    public CanvasGroup fadeCanvas;
    public InputActionReference menuAction;
    public Transform MenuTransform;
    public GameObject rightController;
    public float masterVolumeScaler = 1.0f;
    public int timeLimit = 15 * 60;
    public float remainingTime; 
    private bool timerPaused = true;
    
    private Vector3 beforePosition;
    private Quaternion beforeRotation;
    

    public float fadeDuration = 3.0f;
    private bool isMenuOpen;

    public bool gameStarted = false;
    
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

    void Start()
    {
        locomotionSystem.SetActive(false);
        hudCanvas.SetActive(false);
        menuCanvas.SetActive(true);
        isMenuOpen = true;

        // Optional: Start with full black fade
        fadeCanvas.alpha = 1f;
        fadeCanvas.blocksRaycasts = true;
        fadeCanvas.interactable = false;

        StartCoroutine(FadeIn());
        
        
    }
    
    void OnEnable()
    {
        menuAction.action.performed += ctx => ToggleMenu();
        menuAction.action.Enable();
    }
    
    void OnDisable()
    {
        menuAction.action.Disable();
    }

    public void StartGame()
    {
        CloseMenu(gameplaySpawn.position, gameplaySpawn.rotation);

        // Move XR Rig to game position
        remainingTime = timeLimit;
        timerPaused = false;
        gameStarted = true;
        isMenuOpen = false;
        
    }
    
    void OpenMenu()
    {
        timerPaused = true;
        StartCoroutine(FadeIn());
        
        locomotionSystem.SetActive(false);
        hudCanvas.SetActive(false);
        menuCanvas.SetActive(true);
        
        xrRig.transform.SetPositionAndRotation(MenuTransform.position, MenuTransform.rotation);
        
        fadeCanvas.alpha = 1f;
        fadeCanvas.blocksRaycasts = true;
        fadeCanvas.interactable = false;

        
        
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

    public void CloseMenu(Vector3 position, Quaternion rotation)
    {
        xrRig.transform.SetPositionAndRotation(position, rotation);
        Debug.Log("Closing menu");
        timerPaused = false;
        // Fade into game
        StartCoroutine(FadeIn());
        locomotionSystem.SetActive(true);
        hudCanvas.SetActive(true);
        menuCanvas.SetActive(false);
        isMenuOpen = false;
        
        
    }

    public void ToggleMenu()
    {
        Debug.Log(isMenuOpen);
        if (isMenuOpen)
        {
            CloseMenu(beforePosition, beforeRotation);
            isMenuOpen = false;
        }
        else
        {
            beforePosition = xrRig.transform.position;
            beforeRotation = xrRig.transform.rotation;
            OpenMenu();
            isMenuOpen = true;
        }
    }

    public void setTimeLimit(int _timeLimit)
    {
        timeLimit = _timeLimit;
    }
    
    void Update()
    {
        if (!timerPaused)
        {
            remainingTime -= Time.deltaTime;
            remainingTime = Mathf.Max(0, remainingTime);
            

            if (remainingTime <= 0)
            {
                OnTimeOver();
            }
        }
    }
    
    void OnTimeOver()
    {
        // Handle time over logic here
        Debug.Log("Time is over!");
        // You can trigger a game over screen or reset the game
        FindFirstObjectByType<TutorialManager>().ShowTutorial("Time is over! Try again.");
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void exitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
