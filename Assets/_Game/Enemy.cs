using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Spine.Unity;
enum State
{
    Wandering, ChaseEnemy, RunAway
}

public class Enemy : Cake
{
    public Cake beingChased;

    private SpriteRenderer sprite;
    private Vector2 destination;

    float speed = Constants.PLAYER_SPEED;
    float stepSize;
    float spawnTime;
    float activeTime;

    Animator anim;
    TextMeshPro txtName;
    PlayerController player;
    Rigidbody2D rb2d;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = gameObject.GetChildComponent<Animator>("skeletonAnim");
        txtName = gameObject.GetChildComponent<TextMeshPro>("txtName");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        activeTime = Random.Range(0f, 1f);
        size = Random.Range(0.75f, 1.25f) * Constants.MIN_PLAYER_SIZE;
        transform.localScale = new Vector3(size, size);
        anim.enabled = false;
        int sizeDisplay = (int)(size * 100);
        txtName.text = sizeDisplay.ToString();
        ChangeDestination();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.TAG_PLAYER)) CollidePlayer(collision);
        else if (collision.CompareTag(Constants.TAG_CANDY)) CollideCandy(collision);
        else if (collision.CompareTag(Constants.TAG_CAKE)) CollideOtherCake(collision);
    }

    void CollidePlayer(Collider2D collision)
    {
        if (player.Size >= size)
        {
            player.EatOtherCake(this);
            //StartCoroutine(ResetPosition());
        }
        else
        {
            anim.Play("Attack", -1, normalizedTime: 0f);

            GameSystem.DelayCall(Constants.EAT_TIME, () =>
            {
                player.Die();
            });
        }
    }

    void CollideOtherCake(Collider2D collision)
    {
        anim.Play("Attack", -1, normalizedTime: 0f);
        var food = collision.GetComponent<Food>();

        IncreaseSize(Random.Range(0.75f, 1.25f) * food.stepSize);
        GameSystem.DelayCall(Constants.EAT_TIME, () =>
        {
            if (collision != null && collision.gameObject != null)
                collision.gameObject.SetActive(false);
        });
    }

    void CollideCandy(Collider2D collision)
    {
        var enemy = collision.GetComponent<Enemy>();

        if (size > enemy.size)
        {
            //anim.SetTrigger("Attack");
            anim.Play("Attack", -1, normalizedTime: 0f);

            IncreaseSize(enemy.size / 4);
            GameSystem.DelayCall(Constants.EAT_TIME, () =>
            {
                if (collision.gameObject != null)
                    collision.gameObject.SetActive(false);
            });
        }
    }

    void Update()
    {
        anim.enabled = Time.time > activeTime;

        if (Vector2.Distance(transform.position, destination) <= 2f)
        {
            ChangeDestination();
        }

        if (player != null && Vector2.Distance(transform.position, player.transform.position) < 3f)
        {
            rb2d.velocity = -(player.transform.position - transform.position).normalized;
        } else
        {
            rb2d.velocity = (destination - (Vector2)transform.position).normalized;
        }
    }

    private void ChangeDestination()
    {
        float spawnX = Random.Range(-20, 20);
        float spawnY = Random.Range(-20, 20);
        destination = new Vector2(spawnX, spawnY);
        CheckFacing();
    }

    private IEnumerator ResetPosition()
    {
        stepSize = 0;
        sprite.enabled = false;
        var temptStepSize = stepSize;

        yield return new WaitForSeconds(spawnTime);
        stepSize = temptStepSize;
        sprite.enabled = true;

        Vector2 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        float x = Random.Range(bottomLeft.x, topRight.x);
        float y = Random.Range(bottomLeft.y, topRight.y);

        transform.position = new Vector2(x, y);
        StopCoroutine(ResetPosition());
    }
    protected override void CheckFacing()
    {
        var facing = anim.transform.rotation;
        if (destination.x > transform.position.x)
        {
            facing.y = 0;
        }
        if (destination.x < transform.position.x)
        {
            facing.y = 180;
        }
        anim.transform.rotation = facing;
    }

    public void IncreaseSize(float increase)
    {
        size += increase;

        if (size > Constants.MAX_PLAYER_SIZE) size = Constants.MAX_PLAYER_SIZE;

        transform.localScale = new Vector3(size, size);

        int sizeDisplay = (int)(size * 100);
        txtName.text = sizeDisplay.ToString();
        //float ratio = size / Constants.MAX_PLAYER_SIZE;
        //float cameraSize = Mathf.Lerp(Constants.CAMERA_MIN_SIZE, Constants.CAMERA_MAX_SIZE, ratio);
        //mainCamera.orthographicSize = cameraSize;
    }
}