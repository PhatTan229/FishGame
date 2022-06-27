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
    [SerializeField] Animator anim;
    TextMeshPro txtSize;
    float speed = Constants.PLAYER_SPEED;
    float stepSize;
    float spawnTime;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        //txtSize = transform.GetChild(0).GetComponent<TextMeshPro>();
        anim = GetComponent<Animator>();
        ChangeDestination();

        size = Random.Range(1f, 2f);
        transform.localScale = new Vector3(size, size);
        //txtSize.text = size.ToString();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (collision.TryGetComponent<PlayerController>(out var player))
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
    }

    void Update()
    {
        Move();
        if (Vector2.Distance(transform.position, destination) <= 0)
        {
            ChangeDestination();
        }
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
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
        var facing = transform.rotation;
        if(destination.x > transform.position.x)
        {
            facing.y = 0;
        }
        if(destination.x < transform.position.x)
        {
            facing.y = 180;
        }
        transform.rotation = facing;
    }
}
