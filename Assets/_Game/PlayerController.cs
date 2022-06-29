using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PlayerController : Cake
{
    public float Stamina => stamina;

    private Animator playerAnim;
    private Joystick joystick;
    private Rigidbody2D rb;
    private Camera mainCamera;

    private Vector2 lastDirection;

    float cameraWidth;
    float cameraHeight;
    bool isDashing = false;
    float stamina = Constants.MAX_STAMINA;

    private void Start()
    {
        playerAnim = gameObject.GetChildComponent<Animator>("skeletonAnim");
        rb = GetComponent<Rigidbody2D>();
        joystick = FindObjectOfType<Joystick>();
        txtSize = transform.GetChild(0).GetComponent<TextMeshPro>();
        mainCamera = Camera.main;
        cameraHeight = mainCamera.orthographicSize;
        cameraWidth = cameraHeight * mainCamera.aspect;

        size = Constants.MIN_PLAYER_SIZE;
        IncreaseSize(0);
        //txtSize.text = size.ToString();
        //transform.localScale = new Vector3(size, size);
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        //float x = Mathf.Clamp(transform.position.x, -cameraWidth, cameraWidth);
        //float y = Mathf.Clamp(transform.position.y, -cameraHeight, cameraHeight);
        //transform.position = new Vector3(x, y);

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
            stamina += Constants.DECREASE_STAMINA_SPEED * Time.deltaTime;
            if (stamina > Constants.MAX_STAMINA) stamina = Constants.MAX_STAMINA;
            rb.velocity = joystick.Direction.normalized * Constants.PLAYER_SPEED;
        }

        if (joystick.Direction != Vector2.zero)
        {
            lastDirection = joystick.Direction;
        }
        CheckFacing();

        if (transform.position.x < -6f)
            Die();
    }

    //public void EatCandy(Food food)
    //{
    //    stamina += 1f;
    //    IncreaseSize(food.stepSize);
    //}

    //public void EatOtherCake(Enemy otherCake)
    //{
    //    if (size >= otherCake.Size)
    //    {
    //        IncreaseSize(otherCake.Size / 4);
    //    } else
    //    {
    //        Die();
    //    }
    //}

    public void Die()
    {
        Gameplay.Instance.ShowGameOver();
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
    protected override void CheckFacing()
    {
        var facing = transform.rotation;
        if (joystick.Horizontal < 0)
        {
            facing.y = 180;
        }
        if (joystick.Horizontal > 0)
        {
            facing.y = 0;
        }
        playerAnim.transform.rotation = facing;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var cake = collision.GetComponent<Cake>();
        if (cake != null) Eat(cake);

        //if (collision.CompareTag(Constants.TAG_CAKE))
        //{
        //    var cake = collision.GetComponent<Cake>();
        //    Eat(cake);
        //}
    }

    public override void Eat(Cake whatToEat)
    {
        IncreaseSize(whatToEat.Size / 4);
        playerAnim.Play("Attack", -1, normalizedTime: 0f);
        GameSystem.DelayCall(Constants.EAT_TIME, () =>
        {
            if (whatToEat.gameObject != null)
                whatToEat.gameObject.SetActive(false);
        });
    }
}
