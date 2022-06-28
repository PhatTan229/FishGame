using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Spine.Unity;
enum State
{
    Wandering, ChaseEnemy, RunAway, ChasePlayer
}

public class Enemy : Cake
{
    public Cake beingChased;

    List<string> eatableTags;

    Animator anim;
    TextMeshPro txtName;
    TextMeshPro txtState;
    PlayerController player;
    Rigidbody2D rb2d;
    State state;
    Vector2 destination;

    float speed = Constants.PLAYER_SPEED;
    float stepSize;
    float spawnTime;
    float activeTime;
    float nextChangeState;

    private void Awake()
    {
        anim = gameObject.GetChildComponent<Animator>("skeletonAnim");
        txtName = gameObject.GetChildComponent<TextMeshPro>("txtName");
        txtState = gameObject.GetChildComponent<TextMeshPro>("txtState");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        rb2d = GetComponent<Rigidbody2D>();

        eatableTags = new List<string>() { Constants.TAG_CAKE, Constants.TAG_CANDY, Constants.TAG_PLAYER };
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Cake")){
            var cake = collision.GetComponent<Cake>();
            if (size > cake.Size) Eat(cake);
            else cake.Eat(this);
        }
    }

    void Update()
    {
        anim.enabled = Time.time > activeTime;
        if (Time.time > nextChangeState)
        {
            nextChangeState = Time.time + Random.Range(3f, 5f);
            state = (State)(Random.Range(0, 3));
            txtState.text = state.ToString();
        }

        if (Vector2.Distance(transform.position, destination) <= 2f)
        {
            ChangeDestination();
        }

        if (Vector2.Distance(transform.position, player.transform.position) <= 5f)
        {
            if (size > player.Size) state = State.ChasePlayer;
            else state = State.RunAway;
        }

        rb2d.velocity = (destination - (Vector2)transform.position).normalized;
        //switch (state)
        //{
        //    case State.Wandering:
        //        rb2d.velocity = (destination - (Vector2)transform.position).normalized;
        //        break;
        //    case State.RunAway:
        //        rb2d.velocity = -(player.transform.position - transform.position).normalized;
        //        break;
        //    case State.ChaseEnemy:
        //        rb2d.velocity = (destination - (Vector2)transform.position).normalized;
        //        //speed = 2f;
        //        break;
        //}
        CheckFacing();
    }

    private void ChangeDestination()
    {
        float spawnX = Random.Range(-20, 20);
        float spawnY = Random.Range(-20, 20);
        destination = new Vector2(spawnX, spawnY);
    }

    private IEnumerator ResetPosition()
    {
        stepSize = 0;
        var temptStepSize = stepSize;

        yield return new WaitForSeconds(spawnTime);
        stepSize = temptStepSize;

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
        if (rb2d.velocity.x > 0) facing.y = 0;
        if (rb2d.velocity.x < 0) facing.y = 180;
        anim.transform.rotation = facing;
    }

    public override void Eat(Cake whatToEat)
    {
        IncreaseSize(whatToEat.Size / 4);
        anim.Play("Attack", -1, normalizedTime: 0f);
        GameSystem.DelayCall(Constants.EAT_TIME, () =>
        {
            if (whatToEat.gameObject != null)
                whatToEat.gameObject.SetActive(false);

            if (whatToEat.CompareTag(Constants.TAG_PLAYER))
            {
                Gameplay.Instance.ShowGameOver();
            }
        });
    }
}