using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Size size;
    [SerializeField] private Transform patrolPoint;
    [SerializeField] private SpriteRenderer sprite;

    private Vector2 destination;

    public float speed;
    public float stepSize;
    public float spawnTime;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        if(size == Size.SMALL)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        if(size == Size.MEDIUM)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if(size == Size.BIG)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        ChangeDestination();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (collision.TryGetComponent<PlayerController>(out var player))
            {
                if(player.size >= size)
                {
                    player.GrowUp(stepSize);
                    StartCoroutine(ResetPosition());
                }
                else
                {
                    player.Die();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position,destination)<=0)
        {
            ChangeDestination();
        }
    }

    private void ChangeDestination()
    {
        float Y = Random.Range
                    (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
        float X = Random.Range
            (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

        Vector2 newDestination = new Vector2(X, Y);
        patrolPoint.transform.position = newDestination;
        destination = patrolPoint.transform.position;
    }

    private IEnumerator ResetPosition()
    {
        var temptStepSize = stepSize;
        sprite.enabled = false;
        stepSize = 0;
        yield return new WaitForSeconds(spawnTime);
        stepSize = temptStepSize;
        sprite.enabled = true;
        float spawnY = Random.Range
                    (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
        float spawnX = Random.Range
            (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

        Vector2 newPosition = new Vector2(spawnX, spawnY);
        transform.position = newPosition;
        StopCoroutine(ResetPosition());
    }
}
