using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollToLeft : MonoBehaviour
{
    private void Update()
    {
        transform.position -= new Vector3(Constants.GAMEPLAY_SCROLL_SPEED, 0) * Time.deltaTime;
        if (transform.position.x < -6f) gameObject.SetActive(false);
    }
}
