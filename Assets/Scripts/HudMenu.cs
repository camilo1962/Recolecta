using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class HudMenu : MonoBehaviour
{
    //Static instance allows it to be accessed by any other script.
    public static HudMenu instance = null;

    public TextMeshProUGUI scoreText;
    public int curenntScore;

    [SerializeField]
    Canvas pauseCanvas;
    [HideInInspector]
    public bool isPause;
    public bool stopPlayer;

    public Image[] elementFields;
    public List<Sprite> spriteElements = new List<Sprite>();
    int curenntField = -1;
    int curenntElement;

    //Bonus
    // a temporary list with five intex of Elements that were picked up
    public List<int> bonusCombination = new List<int>();
    [SerializeField]
    Image bonusImage;
    [SerializeField]
    TextMeshProUGUI bonusValue;
    int curenntBonus;
    bool isFullHouse = true;

    //Bonus Animation
    Animator animator;
    const string BONUS_TRANSITION = "Bonus Transition In";

    private void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            instance = this;
        //If instance already exists and it's not this:
        else if (instance = this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a HudMenu.
            Destroy(gameObject);
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        AddPoints(curenntScore);
        // bonusPoints.Clear();
    }


    //  Takes value points of elements in theirs OnCollisionEnter2D in Element script
    public void AddPoints(int enemyValue)
    {
        curenntScore += enemyValue;
        curenntScore += curenntBonus;
        scoreText.text = curenntScore.ToString();
        bonusValue.text = "+" + curenntBonus.ToString();
        if (HudMenu.instance.curenntScore > PlayerPrefs.GetInt("Score", 0))
        {
            PlayerPrefs.SetInt("Score", HudMenu.instance.curenntScore);
        }
    }

    // Button OnClick()  Sets Pause on and pause time
    public void PauseMenu()
    {
        // stop player
        isPause = true;

        Time.timeScale = 0;
        pauseCanvas.gameObject.SetActive(true);
    }
    public void ContinueGame()
    {
        isPause = false;

        Time.timeScale = 1;
        pauseCanvas.gameObject.SetActive(false);
    }
    public void QuitGamePause()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void SetElementFields()
    {
        curenntField++;

        if (curenntField > 8)
        {
            curenntField = 0;
            for (int i = 0; i <= elementFields.Length - 1; i++)
            {
                //Bonus

                //  Destroy(elementFields[i].transform.gameObject);
                elementFields[i].transform.gameObject.SetActive(false);

            }

        }


        elementFields[curenntField].sprite = spriteElements[curenntElement];
        elementFields[curenntField].transform.gameObject.SetActive(true);


        //  BonusSet();
    }


    public void SetSpriteElements(int indexElement)
    {
        curenntElement = indexElement;

        /* foreach (elemento Sprite en spriteElements)
          {
           // puntosbonificados.Add(elementoactual)
              Debug.Log(item.name.ToString());
          }*/

    }
    public void BonusSet(int bonus)
    {
        bonusCombination.Add(bonus);  

        if (bonusCombination.Count > 4)
        {
            for (int i = 0; i < bonusCombination.Count; i++)
            {

            if (bonusCombination[0] != bonusCombination[1] && bonusCombination[1] != bonusCombination[2] && bonusCombination[2] != bonusCombination[3] && bonusCombination[3] != bonusCombination[4] && !bonusCombination.Contains(bonus))
            {
                curenntBonus = 30;
                Debug.Log("AllDifferent");
                animator.Play(BONUS_TRANSITION);
            }

            }
            FifeSame();
            FullHouse();

            //   FullHouse();

            bonusCombination.Clear();
            //   Debug.Log("CLEAR");
        }

    }

    private void FifeSame()
    {
        if (bonusCombination[0] == bonusCombination[0] && bonusCombination[0] == bonusCombination[1] && bonusCombination[0] == bonusCombination[2] && bonusCombination[0] == bonusCombination[3] && bonusCombination[0] == bonusCombination[4] ||
      bonusCombination[1] == bonusCombination[1] && bonusCombination[1] == bonusCombination[2] && bonusCombination[1] == bonusCombination[3] && bonusCombination[1] == bonusCombination[4] && bonusCombination[1] == bonusCombination[4] && bonusCombination[6] == bonusCombination[7])
        {

            bonusImage.gameObject.SetActive(true);
            curenntBonus = 40;
            animator.Play(BONUS_TRANSITION);
            Debug.Log("5 PARRRR");
            isFullHouse = false;
        }
    }

    private void FullHouse()
    {
        if (isFullHouse && (bonusCombination[0] == bonusCombination[1] && bonusCombination[2] == bonusCombination[3] && bonusCombination[2] == bonusCombination[4] ||
                bonusCombination[0] == bonusCombination[1] && bonusCombination[0] == bonusCombination[2] && bonusCombination[3] == bonusCombination[4] ))

        {
            bonusImage.gameObject.SetActive(true);
            curenntBonus = 35;
            animator.Play(BONUS_TRANSITION);
            Debug.Log("FULL HOUSE");

        }

        isFullHouse = true;
        // isFullHouse = true;

    }

    /*  private void ThreeSame()
      {
          *//* if (bonusPoints[0] == bonusPoints[1] && bonusPoints[0] == bonusPoints[2] ||
            bonusPoints[1] == bonusPoints[2] && bonusPoints[1] == bonusPoints[3] ||
            bonusPoints[2] == bonusPoints[3] && bonusPoints[2] == bonusPoints[4])
           {*//*

          //  bonusImage.gameObject.SetActive(true);
          //  Debug.Log("3 PARRRR");
          *//*    if ((bonusPoints[0] == bonusPoints[1] || bonusPoints[1] == bonusPoints[2] ||
                      bonusPoints[2] == bonusPoints[3] || bonusPoints[3] == bonusPoints[4]) && (bonusPoints[0] == bonusPoints[1] && bonusPoints[0] == bonusPoints[2] ||
     bonusPoints[1] == bonusPoints[2] && bonusPoints[1] == bonusPoints[3] ||
     bonusPoints[2] == bonusPoints[3] && bonusPoints[2] == bonusPoints[4]))*//*
      }*/


    /* bool IsEnemyAlive()
     {
         if (_searchCountdown <= 0f)
         {
             int enemyCounting = GameObject.FindGameObjectsWithTag("SkyEnemy").Length;
             _searchCountdown = 1f;
             if (enemyCounting <= 0)
             {
                 return true;
             }
         }
         return false;
     }*/


}

