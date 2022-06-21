using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private Rigidbody2D rb;
    public Size size;

    public UnityEvent onDie;
    
    public float speed;

    private Camera mainCamera;
    private float cameraWidth;
    private float cameraHeight;

    private void Start()
    {
        size = Size.TINY;
        mainCamera = Camera.main;
        cameraHeight = mainCamera.orthographicSize;
        cameraWidth = cameraHeight * mainCamera.aspect;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -cameraWidth, cameraWidth), Mathf.Clamp(transform.position.y, -cameraHeight, cameraHeight));
        Move();
        if(transform.localScale.x >= 0.5)
        {
            size = Size.SMALL;
        }
        if (transform.localScale.x >= 1)
        {
            size = Size.MEDIUM;
        }
        if (transform.localScale.x >= 1.5)
        {
            size = Size.BIG;
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(joystick.Horizontal, joystick.Vertical) * speed;
    }
    public void GrowUp(float step)
    {
        if(transform.localScale.x < 1.5)
        {
            transform.localScale += new Vector3(step, step, step);
        }       
    }

    public void Die()
    {
        if (onDie != null) onDie.Invoke();
        gameObject.SetActive(false);       
    }
}
