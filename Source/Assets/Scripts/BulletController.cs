using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;

    private short direction;
    public short Direction { get => direction; set => direction = value; }

    void Update()
    {
        Vector2 movement = Vector2.right * speed * Direction * Time.deltaTime;
        transform.position += (Vector3)movement;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            Destroy(gameObject);
    }
}
