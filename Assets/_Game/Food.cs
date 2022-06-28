using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Cake
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float spawnTime;
    public float stepSize;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.TryGetComponent<PlayerController>(out var player);
            player.EatCandy(this);
            StartCoroutine(ResetPosition());
        }
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

    protected override void CheckFacing()
    {

    }

    public override void Eat(Cake whatToEat)
    {

    }
}
