using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    #region Singleton
    // Using a singleton to ensure only one instance of a manager exists
    private static BallManager _instance;

    public static BallManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this;
    }
    #endregion

    [SerializeField] private Ball ballPrefab;
    private Ball firstBall;
    private Rigidbody2D firstBallRb;

    public float startingBallSpeed = 250f;

    public List<Ball> Balls { get; set; }

    private void Start()
    {
        InitBall();
    }

    private void Update()
    {
        if(!GameManager.Instance.IsGameStarted)
        {
            // Keep the ball above the paddle until game starts
            Vector3 paddlePos = PaddleMovement.Instance.gameObject.transform.position;
            Vector3 ballPos = new Vector3(paddlePos.x, paddlePos.y + .27f, 0);
            firstBall.transform.position = ballPos;

            if(Input.GetMouseButtonDown(0))
            {
                firstBallRb.isKinematic = false;
                firstBallRb.AddForce(new Vector2(0, startingBallSpeed));

                GameManager.Instance.IsGameStarted = true;
            }
        }
    }

    private void InitBall()
    {
        Vector3 paddlePos = PaddleMovement.Instance.gameObject.transform.position;
        Vector3 startingPos = new Vector3(paddlePos.x, paddlePos.y + .27f, 0);
        firstBall = Instantiate(ballPrefab, startingPos, Quaternion.identity);
        firstBallRb = firstBall.GetComponent<Rigidbody2D>();

        this.Balls = new List<Ball>{firstBall};
    }
}
