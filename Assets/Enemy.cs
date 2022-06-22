using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Cake
{
    private SpriteRenderer sprite;
    private Vector2 destination;

    float speed = Constants.PLAYER_SPEED;
    float stepSize;
    float spawnTime;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        ChangeDestination();
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
                    player.Die();
                }
            }
        }
    }

    void Update()
    {
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
}
