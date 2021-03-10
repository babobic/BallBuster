using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    private Camera mainCamera;
    private float paddlePosY;
    private float defaultWidth = 200f;
    private SpriteRenderer renderer;

    private float leftClamp = 135f;
    private float rightClamp = 410f;

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
        float paddleChange = (defaultWidth - (defaultWidth / 2) * this.renderer.size.x) / 2;
        float mousePosPixels = Mathf.Clamp(Input.mousePosition.x, leftClamp, rightClamp);
        float mousePosWorldX = mainCamera.ScreenToWorldPoint(new Vector3(mousePosPixels, 0, 0)).x;
        this.transform.position = new Vector3(mousePosWorldX, paddlePosY, 0);
    }
}
