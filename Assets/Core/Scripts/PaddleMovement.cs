using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    #region Singleton

    private static PaddleMovement _instance;

    public static PaddleMovement Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this;
    }
    #endregion

    private Camera mainCamera;
    private float paddlePosY;
    private float defaultWidth = 200f;
    private SpriteRenderer renderer;

    private float defaultLeftClamp = 135f;
    private float defaultRightClamp = 410f;

    private void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        renderer = GetComponent<SpriteRenderer>();
        paddlePosY = this.transform.position.y;
    }

    void Update()
    {
        MovePaddle();
    }

    private void MovePaddle()
    {
        float paddleSizeChange = (defaultWidth - ((defaultWidth / 2) * this.renderer.size.x)) / 2;
        float leftClamp = defaultLeftClamp - paddleSizeChange;
        float rightClamp = defaultRightClamp + paddleSizeChange;
        float mousePosPixels = Mathf.Clamp(Input.mousePosition.x, leftClamp, rightClamp);
        float mousePosWorldX = mainCamera.ScreenToWorldPoint(new Vector3(mousePosPixels, 0, 0)).x;
        this.transform.position = new Vector3(mousePosWorldX, paddlePosY, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Ball>() != null)
        {
            // Todo - depending on the position relative to center, change ball trajectory
            Debug.Log("Here");
        }
    }
}
