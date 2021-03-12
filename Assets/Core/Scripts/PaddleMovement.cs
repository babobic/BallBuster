using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    private Camera mainCamera;
    private float paddlePosY;
    private float defaultWidth = 200;
    private SpriteRenderer renderer;

    private float defaultLeftClamp = 135;
    private float defaultRightClamp = 410;

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
}
