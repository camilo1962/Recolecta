using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FinishMenu : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    int curenntScore;

    public TextMeshProUGUI highScore;
    // public int highCurenntScore;

    /* [SerializeField]
     Canvas finishCanvas;*/
    //public bool timer;

    void Start()
    {
        highScore.text = PlayerPrefs.GetInt("Score", 0).ToString();
        SetScore();
       /* ParticleSystem ps = GameObject.Find("Confetti").GetComponentInChildren<ParticleSystem>();
        ps.Play();*/
    }

    void SetScore()
    {
        curenntScore = HudMenu.instance.curenntScore;
        scoreText.text = curenntScore.ToString();
    }
    public void SetHighScore()
    {
        /*if (HudMenu.instance.curenntScore > PlayerPrefs.GetInt("Score", 0))
        {
            PlayerPrefs.SetInt("Score", HudMenu.instance.curenntScore);
            highScore.text = HudMenu.instance.curenntScore.ToString();
        }*/
    }

    // Button OnClick() back to Entry Scene
    public void QuitGame() => SceneManager.LoadScene(0, LoadSceneMode.Single);

    // Button OnClick()  Restart game
    public void RetryGame() => SceneManager.LoadScene(1, LoadSceneMode.Single);

}
