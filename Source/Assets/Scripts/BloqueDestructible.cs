using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloqueDestructible : MonoBehaviour
{
    public GameObject particles;
    AudioSource audioSource;

    //Para deshabilitar
    SpriteRenderer render;
    Collider2D coll;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        render = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Instantiate(particles, transform.position + Vector3.left * 0.5f, Quaternion.identity);
            audioSource.Play();
            render.enabled = false;
            coll.enabled = false;
            Destroy(gameObject, audioSource.clip.length);
        }
    }
}
