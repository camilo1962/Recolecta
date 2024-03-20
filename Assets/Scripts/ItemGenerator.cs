using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    [SerializeField]
    List<Vector3> spawnPos = new List<Vector3>();

    [SerializeField]
    List<GameObject> elements = new List<GameObject>();

    // public float spownTime;
    public float itemCreationDelay = 2f;
    float creationDelayFactor = 0.9f;

    // Timer 
    public TextMeshProUGUI timerText;
    public float startingTimer = 120f;
    // checks if game end
    bool endGame = false;

    public float currentFallingSpeed = 2f;
    float speedFactor = 1.1f;

    [SerializeField]
    Canvas finishCanvas;
    GameObject generatedElement;

    //  public TextMeshProUGUI scoreText;
    public Camera mainCamera;
    private Vector2 screenBounds;
    private float playerWidth;
    float distanceSpawn;

   // public int randomElements;

  

    void Start()
    {
       
       // generatedElement.tag = "generatedElement";
        timerText.text = startingTimer.ToString();

        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        playerWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        //   Debug.Log(screenBounds + "      " + (screenBounds.x * 2f - playerWidth) / 5f);
        distanceSpawn = (screenBounds.x * 2f - (playerWidth * 2)) / 4f;
        //   Debug.Log(distanceSpawn);

        StartCoroutine(SpawnRoutine());
        StartCoroutine(IncreaseCreationRoutine());
        StartCoroutine(IncreaseFallingRoutine());
        // InitialiseList();
    }
    void Update()
    {
        MovingElements();
        TimerCounting();
    }
    private void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + playerWidth, screenBounds.x - playerWidth);
    }
    //  Moving instantiated game object
    void MovingElements()
    {
        // stops moving 
        if (endGame == false && generatedElement != null)
        {
            generatedElement.transform.Translate(currentFallingSpeed * Time.deltaTime * Vector2.down);

        }
    }

    void TimerCounting()
    {
        if (startingTimer <= 0)
        {
            // game has stops
            endGame = true;
            finishCanvas.gameObject.SetActive(true);
            HudMenu.instance.isPause = true;
        }
        // Game start
        else
        {
            startingTimer -= Time.deltaTime;
            timerText.text = startingTimer.ToString("f0");
            TimeWarning();
        }
    }

    //  Advertencia simple para resaltar cuando queda poco tiempo para cambiar de color
    void TimeWarning()
    {
        if (startingTimer <= 20)
        {
            timerText.color = Color.yellow;
        }
        if (startingTimer < 10)
        {
            timerText.color = Color.red;
        }
    }

    // Choosing element and position  and Instantiate
    void ChoosingElement()
    {
        int randomElements = Random.Range(0, elements.Count);
        int randomPos = Random.Range(0, spawnPos.Count + 1);
        if (spawnPos.Count < 1)
        {
            InitialiseList();
        }
        for (int i = 0; i < randomPos; i++)
        {

            Vector3 rand = RandomPosition();

            generatedElement = Instantiate(elements[randomElements], rand, Quaternion.identity);
            //  HudMenu.instance.SetSpriteElements(randomElements);
        }

    }
    public void InitialiseList()
    {

        //  Debug.Log("DDDD" + distanceSpawn);
        spawnPos.Clear();
        for (float x = (-screenBounds.x + playerWidth); x <= screenBounds.x + 1 - playerWidth; x += distanceSpawn)
        {

            spawnPos.Add(new Vector3(x, 5.8f, 0f));
            //At each index add a new Vector3 to our list with the x and y coordinates of that position.
        }
    }

    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, spawnPos.Count);
        Vector3 randomPosition = spawnPos[randomIndex];
        spawnPos.RemoveAt(randomIndex);
        return randomPosition;
    }

    // Spawning chosen element 
    IEnumerator SpawnRoutine()
    {
        while (endGame == false)
        {
            ChoosingElement();
            yield return new WaitForSeconds(itemCreationDelay);
        }
    }
    // Controling falling speed
    IEnumerator IncreaseFallingRoutine()
    {
        while (endGame == false)
        {
            currentFallingSpeed *= speedFactor;
            yield return new WaitForSeconds(10f);
        }
    }
    // Controling Creation Delay
    IEnumerator IncreaseCreationRoutine()
    {
        while (endGame == false)
        {
            itemCreationDelay *= creationDelayFactor;
            yield return new WaitForSeconds(15f);
        }
    }
 
}