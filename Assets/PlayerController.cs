using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : Cake
{
    private Joystick joystick;
    private Rigidbody2D rb;
    private Camera mainCamera;
    private Vector2 lastDirection;

    private float speed = 5;
    private float cameraWidth;
    private float cameraHeight;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        joystick = FindObjectOfType<Joystick>();
        mainCamera = Camera.main;
        cameraHeight = mainCamera.orthographicSize;
        cameraWidth = cameraHeight * mainCamera.aspect;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        float x = Mathf.Clamp(transform.position.x, -cameraWidth, cameraWidth);
        float y = Mathf.Clamp(transform.position.y, -cameraHeight, cameraHeight);
        transform.position = new Vector3(x, y);
        rb.velocity = joystick.Direction.normalized * speed;
        if (joystick.Direction != Vector2.zero)
        {
            lastDirection = joystick.Direction;
        }

        var facing = transform.rotation;
        if (joystick.Horizontal < 0)
        {
            facing.y = 180;
        }
        if (joystick.Horizontal > 0)
        {
            facing.y = 0;
        }
        transform.rotation = facing;
    }

    public void GrowUp(float step)
    {
        if (transform.localScale.x < 1.5)
        {
            transform.localScale += new Vector3(step, step, step);
        }
    }

    public void Die()
    {
        GameFlow.Instance.ShowGameOver();
        gameObject.SetActive(false);
    }

    public void Dash()
    {
        rb.AddForce(lastDirection.normalized * 2000);
    }
}
