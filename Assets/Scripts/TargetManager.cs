using UnityEngine;

public class TargetManager : MonoBehaviour
{
    internal bool gameOver = false;
    private int[] IDOrder = new int[4] {4, 2, 1, 3};
    private int expectedIndex = 0;
    public static TargetManager Instance { get; set; }
    
    public GameObject archeryDoor;
    
    public GameObject[] lights = new GameObject[4];
    
    private void Awake()
    {
        if(Instance != null && Instance != this)
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HitEvent(int ID)
    {
        if (!gameOver)
        {
            int expectedID = IDOrder[expectedIndex];

            if (expectedID == ID)
            {
                lights[ID - 1].SetActive(true);
                expectedIndex++;
                if (expectedIndex == IDOrder.Length)
                {
                    AudioManager.Instance.puzzleSuccessAudio.Play();
                    gameOver = true;
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    lights[i].SetActive(false);
                }

                expectedIndex = 0;
            }
        }
    }
}
