using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour
{
    
    public static MenuScreen Instance { get; private set; }
    
    public Button mainTabButton;
    public Button controlsTabButton;
    public Button featuresTabButton;
    public Button checkpointButton;
    
    public GameObject startMenuCanvas;
    public GameObject mainMenuCanvas;
    public GameObject mainTabStart;
    public GameObject mainTab;
    public GameObject controlsTab;
    public GameObject featuresTab;
    
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
        mainTabButton.interactable = false;
        controlsTabButton.interactable = true;
        featuresTabButton.interactable = true;
        
        checkpointButton.interactable = false;
        
        ShowMainTab();
    }
    
    public void openControlsTab() 
    {
        SetActiveTab(controlsTab, true, false, true);
    }
    
    public void openFeaturesTab() 
    {
        SetActiveTab(featuresTab, true, true, false);
    }

    public void SetActiveTab(GameObject canvas, bool mainTabActive, bool controlsTabActive, bool featuresTabActive)
    {
        startMenuCanvas.SetActive(false);
        mainTabStart.SetActive(false);
        mainMenuCanvas.SetActive(true);
        mainTab.SetActive(false);
        controlsTab.SetActive(false);
        featuresTab.SetActive(false);

        canvas.SetActive(true);
        
        mainTabButton.interactable = mainTabActive;
        controlsTabButton.interactable = controlsTabActive;
        featuresTabButton.interactable = featuresTabActive;
    }
    
    public void ShowMainTab()
    {
        if (MenuManager.Instance.gameStarted)
        {
            SetActiveTab(mainTab, false, true, true);
        }
        else
        {
            SetActiveTab(mainTabStart, false, true, true);
        }
    }
    
    public void ShowStartMenu()
    {
        startMenuCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
        mainTabStart.SetActive(false);
        mainTab.SetActive(false);
        controlsTab.SetActive(false);
        featuresTab.SetActive(false);
        
    }
    
    
    
}