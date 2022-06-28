using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Spine.Unity;
enum State
{
    RUN, ATTACK
}
public class Enemy : Cake
{
    private SpriteRenderer sprite;
    private Vector2 destination;

    float speed = Constants.PLAYER_SPEED;
    float stepSize;
    float spawnTime;
    float activeTime;

    Animator anim;
    TextMeshPro txtSize;
    TextMeshPro txtName;
    PlayerController player;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = gameObject.GetChildComponent<Animator>("skeletonAnim");
        txtName = gameObject.GetChildComponent<TextMeshPro>("txtName");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Start()
    {
        activeTime = Random.Range(0f, 1f);
        size = Constants.MIN_PLAYER_SIZE;
        transform.localScale = new Vector3(size, size);
        anim.enabled = false;
        ChangeDestination();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            if (player.Size >= size)
            {
                player.EatOtherCake(this);
                StartCoroutine(ResetPosition());
            }
            else
            {
                anim.SetTrigger("Attack");
                player.Die();
            }
        }
    }

    void Update()
    {
        anim.enabled = Time.time > activeTime;

        transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, destination) <= 0)
        {
            ChangeDestination();
        }
    }

    private void ChangeDestination()
    {
        Vector2 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        float x = Random.Range(bottomLeft.x, topRight.x);
        float y = Random.Range(bottomLeft.y, topRight.y);
        destination = new Vector2(x, y);
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
        if(destination.x > transform.position.x)
        {
            facing.y = 0;
        }
        if(destination.x < transform.position.x)
        {
            facing.y = 180;
        }
        anim.transform.rotation = facing;
    }
}