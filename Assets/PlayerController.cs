using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : Cake
{
    public float Stamina => stamina;

    private Joystick joystick;
    private Rigidbody2D rb;
    private Camera mainCamera;
    public Vector2 lastDirection;

    float cameraWidth;
    float cameraHeight;
    bool isDashing = false;
    float stamina = Constants.MAX_STAMINA;

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

        if (isDashing)
        {
            stamina -= Constants.DECREASE_STAMINA_SPEED * Time.deltaTime;
            if (stamina < 0) stamina = 0;
            else
            {
                rb.velocity = joystick.Direction.normalized * Constants.PLAYER_SPEED * Constants.PLAYER_DASH_SPEED_RATIO;
            }
        } else
        {
            rb.velocity = joystick.Direction.normalized * Constants.PLAYER_SPEED;
        }

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

    public void EatCandy(Food food)
    {
        float step = food.stepSize;
        transform.localScale += new Vector3(step, step, step);
        stamina += 1f;
    }

    public void EatOtherCake(Enemy otherCake)
    {
        float step = otherCake.Size / 4;
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
        Debug.Log("This is dash");
        rb.AddForce(lastDirection.normalized * Constants.DASH_FORCE);
    }

    public void DashButtonDown()
    {
        isDashing = true;
    }

    public void DashButtonUp()
    {
        isDashing = false;
    }
}
