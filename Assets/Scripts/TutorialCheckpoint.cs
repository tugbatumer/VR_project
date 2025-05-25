using UnityEngine;

public class TutorialCheckpoint : MonoBehaviour
{
    
    public string tutorialMessage ="";
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            TutorialManager.Instance.ShowTutorial(tutorialMessage);
        }
    }
}