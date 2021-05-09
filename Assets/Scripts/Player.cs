using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    [Header("Inject variables")]
    public UIview uiView;

    private int count = 0;
    private int score = 0;

    [Inject]
    private void Contruct(UIview _uiView)
    {
        uiView = _uiView;
    }

    private void Start()
    {
        count = PlayerPrefs.GetInt("Count");
        uiView.RenderCount(count);
    }

    private void Update()
    {
        score = (int)transform.position.z;
        uiView.RenderScore(score);
    }

    // Action in case of death
    public void ActionPlayer()
    {
        //Disable player
        GetComponentInChildren<MeshRenderer>().enabled=false;
        gameObject.GetComponent<Player>().enabled = false;
        gameObject.GetComponent<JumpMovement>().enabled = false;

        SaveScoreAndCount();
        uiView.RenderHighScore();
        uiView.ActivateDeadMenu();
    }

    public void PickUpCoin(int value)
    {
        count += value;
        uiView.RenderCount(count);
       
    }

    private void SaveScoreAndCount()
    {
        PlayerPrefs.SetInt("Count", count);
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    public void SpeedUp(int speedFactor)
    {
        JumpMovement movement = gameObject.GetComponent<JumpMovement>();
        if(movement.speed*speedFactor<movement.maxSpeed)
        {
            movement.speed *= speedFactor;
            movement.isSpeedIncreased = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Coin coin = other.transform.GetComponent<Coin>();
        if (coin != null)
        {
            PickUpCoin(coin.value);
            Destroy(other.transform.gameObject);
        }

        SpeedBoost booster = other.transform.GetComponent<SpeedBoost>();
        if (booster != null)
        {
            SpeedUp(booster.speedFactor);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Obstacle obstacle = collision.transform.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            ActionPlayer();
        }

    }
}
