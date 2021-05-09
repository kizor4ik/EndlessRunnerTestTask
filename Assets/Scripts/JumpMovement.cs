using System.Collections;
using UnityEngine;
using Zenject;

public class JumpMovement : MonoBehaviour
{
    [Header("Movement properties")]
    public float initialSpeed = 15;
    public float speed;
    public float maxSpeed = 200f;
    public float slowDownStep;
    public float jumpHeight;
    // Duration of jump
    public float duration;

    public bool isSpeedIncreased = false;

    [Header("Injected variables")]
    public Transform cubeBody;
    public Transform[] lines;

    private bool canMove = false;
    private bool isMoving = false;
    private float distance;
    // Target for rotation eulers
    private Vector3 targetRot=new Vector3(0,0,0);
    // Current rotation eulers
    private Vector3 curRot=new Vector3(0,0,0);
    private Rigidbody rb;
    private int currentLine = 1;

    [Inject]
    private void Contruct(Transform[] linesInGame)
    {
        lines = linesInGame;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = initialSpeed;
    }

    private void Update()
    {
        // Detect Input and prepare variables for action
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !isMoving)
        {
            if (currentLine > 0)
            {
                distance = lines[currentLine - 1].position.x - lines[currentLine].position.x;
                currentLine -= 1;
                canMove = true;
                targetRot += new Vector3(0, 0, 180);
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && !isMoving)
        {
            if (currentLine < lines.Length - 1)
            {
                distance = lines[currentLine + 1].position.x - lines[currentLine].position.x;
                currentLine += 1;
                canMove = true;
                targetRot += new Vector3(0, 0, -180);
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)&&!isMoving)
        {
            distance = 0;
            canMove = true;
            targetRot += new Vector3(180, 0, 0);
        }

        // Run movement jump couroutine
        if (canMove)
        {
            isMoving = true;
            Vector3 targetPosition = transform.position + new Vector3(distance, 0, 0);
            StartCoroutine(CubeMovementX(targetPosition.x));
        }

        // Slowdown player
        FixSpeed();
    }

    private void FixedUpdate()
    {
        // Constant move forward
        StraightMove();
    }
    void StraightMove()
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
    }

    // Jump movement
    IEnumerator CubeMovementX(float targetX)
    {
        canMove = false;
        float progress = 0;

        float currentX = transform.position.x;
        float currentY = transform.position.y;

        while (progress < duration)
        {
            progress += Time.deltaTime;
            float percent = Mathf.Clamp01(progress / duration);
            float height = jumpHeight * Mathf.Sin(Mathf.PI * percent);
            float newX=Mathf.Lerp(currentX, targetX,percent);
            transform.position = new Vector3(newX,currentY,transform.position.z) + new Vector3(0, height, 0);
            Vector3 newEuler = Vector3.Lerp(curRot, targetRot, percent);
            cubeBody.rotation = Quaternion.Euler(newEuler);
            yield return null;
        }
        curRot = targetRot;
        currentX = targetX;
        isMoving = false;
    }
    private void FixSpeed()
    {
        if (isSpeedIncreased)
        {
            speed -= Time.deltaTime*speed/3;
            if (speed < initialSpeed)
            {
                isSpeedIncreased = false;
                speed = initialSpeed;
            }
        }
    }
}
