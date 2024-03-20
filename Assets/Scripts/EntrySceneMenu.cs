using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntrySceneMenu : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI highScore;

    void Start()
    {
        highScore.text = PlayerPrefs.GetInt("Score", 0).ToString();
    }
    // Button OnClick() Load GamePlay Scene
    public void PlayGame() => SceneManager.LoadScene(1, LoadSceneMode.Single);

    public void Salir() => Application.Quit();
}
