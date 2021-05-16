using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MovementModel : MonoBehaviour
{
    public MovementParameters Parameters;
    public Transform CubeBody;
    public float Speed;
    public bool IsSpeedIncreased = false;

    private RoadLine[] _lines;
    private Rigidbody _rb;
    // Target rotation eulers
    private Vector3 _targetRot = new Vector3(0, 0, 0);
    // Current rotation eulers
    private Vector3 _curRot = new Vector3(0, 0, 0);
    private float _distance;
    private int _currentLine = 1;
    private bool _canMove = false;
    private bool _isMoving = false;

    [Inject]
    private void Construct(RoadLine[] linesInGame, MovementParameters MovementSettings)
    {
        _lines = linesInGame;
        Parameters = MovementSettings;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Speed = Parameters.InitialSpeed;
    }

    private void FixedUpdate()
    {
        StraightMove();
        FixSpeed();
    }

    public void StraightMove()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, Speed);
    }

    public void TryToMoveLeft()
    {
        if (_currentLine > 0 && !_isMoving)
        {
            _distance = _lines[_currentLine - 1].transform.position.x - _lines[_currentLine].transform.position.x;
            _currentLine -= 1;
            _canMove = true;
            _targetRot += new Vector3(0, 0, 180);

            StartJumpMove();
        }
    }

    public void TryToMoveRight()
    {
        if (_currentLine < _lines.Length - 1 && !_isMoving)
        {
            _distance = _lines[_currentLine + 1].transform.position.x - _lines[_currentLine].transform.position.x;
            _currentLine += 1;
            _canMove = true;
            _targetRot += new Vector3(0, 0, -180);

            StartJumpMove();
        }
    }

    public void TryToMoveUp()
    {
        if (!_isMoving)
        {
            _distance = 0;
            _canMove = true;
            _targetRot += new Vector3(180, 0, 0);

            StartJumpMove();
        }
    }

    public void StartJumpMove()
    {
        if (_canMove)
        {
            _isMoving = true;
            Vector3 targetPosition = transform.position + new Vector3(_distance, 0, 0);

            StartCoroutine(CubeMovementX(targetPosition.x));
        }
    }


    // Slowdown player after interaction with speed booster.
    public void FixSpeed()
    {
        if (IsSpeedIncreased)
        {
            Speed -= Time.deltaTime * Speed / Parameters.SlowDownRate;
            if (Speed < Parameters.InitialSpeed)
            {
                IsSpeedIncreased = false;
                Speed = Parameters.InitialSpeed;
            }
        }
    }

    // Move with jump and animation to target position.
    IEnumerator CubeMovementX(float targetX)
    {
        _canMove = false;
        float progress = 0;

        float currentX = transform.position.x;
        float currentY = transform.position.y;

        while (progress < Parameters.Duration)
        {
            progress += Time.deltaTime;
            float percent = Mathf.Clamp01(progress / Parameters.Duration);
            float height = Parameters.JumpHeight * Mathf.Sin(Mathf.PI * percent);
            float newX = Mathf.Lerp(currentX, targetX, percent);
            transform.position = new Vector3(newX, currentY, transform.position.z) + new Vector3(0, height, 0);
            Vector3 newEuler = Vector3.Lerp(_curRot, _targetRot, percent);
            CubeBody.rotation = Quaternion.Euler(newEuler);
            yield return null;
        }
        _curRot = _targetRot;
        currentX = targetX;
        _isMoving = false;
    }
}
