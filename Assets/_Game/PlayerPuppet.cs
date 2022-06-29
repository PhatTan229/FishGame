using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPuppet : MonoBehaviour
{
    Transform player;
    Rigidbody2D rb2d;
    //Vector3 playerLastPosition1;
    //Vector3 playerLastPosition2;

    float followRange = 0.5f;
    float speed = 200f;

    protected void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void InputMovement() {
        //if (rb2d.velocity == Vector2.zero) {
        //    skeletonAnim.AnimationName = "Idle";
        //} else {
        //    skeletonAnim.AnimationName = "Run";
        //}

        if (Vector2.Distance(transform.position, player.position) < followRange) {
            rb2d.velocity = Vector2.zero;
            return;
        }
        rb2d.velocity = (player.position - transform.position).normalized * Constants.PLAYER_SPEED;
        //if (Time.time < nextUpdateAnim) return;
    }

    private void Update()
    {
        InputMovement();
    }

    //public override void Die() {
    //    if (isDead) return;

    //    isDead = true;

    //    GetComponent<Collider>().enabled = false;
    //    GameSystem.userdata.currentHp = 0;
    //    skeletonAnim.AnimationName = "Die1";
    //    nextUpdateAnim = Time.time + 4f;
    //    var myAnimation = skeletonAnim.Skeleton.Data.FindAnimation("Die1");

    //    LeanTween.delayedCall(myAnimation.Duration, () => {
    //        foreach (Transform child in transform) {
    //            child.gameObject.SetActive(false);
    //        }
    //        this.enabled = false;
    //        rb2d.velocity = Vector2.zero;
    //    });
    //    //Gameplay.Instance.ShowGameOver();
    //}
}
