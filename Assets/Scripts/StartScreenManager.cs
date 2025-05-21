using UnityEngine;

public class StartScreenManager : MonoBehaviour
{
    public GameObject startCanvas;    
    public GameObject locomotionSystem;
    public GameObject hudCanvas;

    void Start()
    {
        locomotionSystem.SetActive(false);
        hudCanvas.SetActive(false);
        startCanvas.SetActive(true);
    }

    public void StartGame()
    {
        locomotionSystem.SetActive(true);
        hudCanvas.SetActive(true);
        startCanvas.SetActive(false); 
    }
}