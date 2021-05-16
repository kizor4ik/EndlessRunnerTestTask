using UnityEngine;
using UniRx;

public static class DataService 
{
    public static ReactiveProperty<int> CoinCount = new ReactiveProperty<int>(0);
    public static ReactiveProperty<int> Score = new ReactiveProperty<int>(0);
    public static int HighScore = 0;

    const string _highScoreKey = "HighScore";
    const string _coinCountKey ="Count";

    public static void InitializeData()
    {
        CoinCount.Value = PlayerPrefs.GetInt(_coinCountKey);
        HighScore = PlayerPrefs.GetInt(_highScoreKey);
    }

    public static void AddCoin(int amount)
    {
        CoinCount.Value += amount;
    }

    public static void SaveScoreAndCount()
    {
        PlayerPrefs.SetInt(_coinCountKey, CoinCount.Value);
        if (Score.Value > PlayerPrefs.GetInt(_highScoreKey))
        {
            HighScore = Score.Value;
            PlayerPrefs.SetInt(_highScoreKey, HighScore);
        }
    }
}
