using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallObject : MonoBehaviour
{
    [SerializeField] float minHeight;
    [SerializeField] float maxHeight;

    private void OnEnable()
    {
        transform.position = new Vector3(transform.position.x, Random.Range(minHeight, maxHeight));
    }
}
