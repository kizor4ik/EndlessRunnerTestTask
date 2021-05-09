using UnityEngine;
using UnityEngine.UI;

public class UIview : MonoBehaviour
{
    public Text countText;
    public Text scoreText;
    public Text highScore;
    public GameObject deadMenu;

    public void RenderCount(int count)
    {
        countText.text = count.ToString();
    }

    public void RenderScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void ActivateDeadMenu()
    {
        deadMenu.SetActive(true);
    }

    public void RenderHighScore()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
}
