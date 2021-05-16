using UnityEngine;

public class InputManager : MonoBehaviour
{
    private MovementModel _movement;

    private void Start()
    {
        _movement = gameObject.GetComponent<MovementModel>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _movement.TryToMoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _movement.TryToMoveRight();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) )
        {
            _movement.TryToMoveUp();
        }
    }
}
