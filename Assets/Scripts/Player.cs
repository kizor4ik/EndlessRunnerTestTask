using UnityEngine;
using UniRx;

public class Player : MonoBehaviour
{
    public ReactiveProperty<bool> IsPlayerDied = new ReactiveProperty<bool>(false);
    public ReactiveProperty<int> PlayerZPosition = new ReactiveProperty<int>(0);

    public MovementModel Movement;

    // Action in case of death.
    public void ActionPlayer()
    {
        // Disable player.
        GetComponentInChildren<MeshRenderer>().enabled=false;
        gameObject.GetComponent<Player>().enabled = false;
        gameObject.GetComponent<InputManager>().enabled = false;
        Movement.enabled = false;

        DataService.SaveScoreAndCount();
        IsPlayerDied.Value = true;
    }

    public void SpeedUp(int speedFactor)
    {
        if(Movement.Speed * speedFactor < Movement.Parameters.MaxSpeed)
        {
            Movement.Speed *= speedFactor;
            Movement.IsSpeedIncreased = true;
        }
    }

    private void Update()
    {
        DataService.Score.Value = (int)transform.position.z;
    }
}
