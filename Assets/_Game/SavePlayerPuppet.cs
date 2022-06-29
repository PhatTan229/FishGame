using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayerPuppet : MonoBehaviour
{
    bool spawned;
    private void OnEnable() {
        spawned = false;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            if (spawned) return;
            spawned = true;
            ObjectPool.Instance.GetGameObjectFromPool("playerPuppet", transform.position);
            gameObject.SetActive(false);
        }
    }
}
