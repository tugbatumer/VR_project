using UnityEngine;
using UnityEngine.UI;

public class StartMenuCanvas : MonoBehaviour
{
    
    [Header("Difficulty Levels")]
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;
    

    void Start()
    {
        easyButton.interactable = true;
        mediumButton.interactable = false;
        hardButton.interactable = true;
    }
    
    public void SetDifficulty(string difficulty)
    {
        switch (difficulty.ToLower())
        {
            case "easy":
                easyButton.interactable = false;
                mediumButton.interactable = true;
                hardButton.interactable = true;
                MenuManager.Instance.setTimeLimit(60* 60); // 60 minutes
                break;
            case "medium":
                easyButton.interactable = true;
                mediumButton.interactable = false;
                hardButton.interactable = true;
                MenuManager.Instance.setTimeLimit(15*60);
                break;
            case "hard":
                easyButton.interactable = true;
                mediumButton.interactable = true;
                hardButton.interactable = false;
                MenuManager.Instance.setTimeLimit(5*60);
                break;
            default:
                Debug.LogWarning("Unknown difficulty level: " + difficulty);
                break;
        }
    }
}
