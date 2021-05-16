using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UniRx;

public class UIview : MonoBehaviour
{
    public Text CountText;
    public Text ScoreText;
    public Text HighScore;
    public GameObject DeadMenu;

    private Player _player;

    [Inject]
    private void Construct(Player player)
    {
        _player = player;
    }

    private void Start()
    {
        DataService.CoinCount
            .ObserveEveryValueChanged(x => x.Value)
            .Subscribe(xs =>
            {
                this.RenderCount(xs);
            }).AddTo(this);
        DataService.Score
            .ObserveEveryValueChanged(x => x.Value)
            .Subscribe(xs =>
            {
                this.RenderScore(xs);
            }).AddTo(this);
        _player.IsPlayerDied
            .ObserveEveryValueChanged(x => x.Value)
            .Subscribe(xs =>
            {
                if (xs)
                {
                    ActivateDeadMenu();
                    RenderHighScore();
                }
            }
            ).AddTo(this);
    }

    public void RenderCount(int count)
    {
        CountText.text = count.ToString();
    }

    public void RenderScore(int score)
    {
        ScoreText.text = score.ToString();
    }

    public void ActivateDeadMenu()
    {
        DeadMenu.SetActive(true);
    }

    public void RenderHighScore()
    {
        HighScore.text = DataService.HighScore.ToString();
    }


}
