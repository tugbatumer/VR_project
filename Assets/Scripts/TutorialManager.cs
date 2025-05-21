using UnityEngine;
using TMPro;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public CanvasGroup tutorialPanel;
    public TextMeshProUGUI tutorialText;

    private Coroutine currentRoutine;

    public void ShowTutorial(string message, float duration = -1f)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        tutorialText.text = message;
        tutorialPanel.alpha = 1;
        tutorialPanel.blocksRaycasts = true;
        tutorialPanel.interactable = true;

        if (duration > 0)
            currentRoutine = StartCoroutine(HideAfterDelay(duration));
    }

    IEnumerator HideAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);
        HideTutorial();
    }

    public void HideTutorial()
    {
        tutorialPanel.alpha = 0;
        tutorialPanel.blocksRaycasts = false;
        tutorialPanel.interactable = false;
        tutorialText.text = "";
    }
}