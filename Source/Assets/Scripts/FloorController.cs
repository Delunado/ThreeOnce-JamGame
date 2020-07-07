using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    public Color touchedColor;

    private AudioSource audioSource;

    public GameObject particleFire;

    private SpriteRenderer sprite;
    public Sprite[] sprites;

    private bool isTouched;

    // Start is called before the first frame update
    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        sprite.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isTouched)
            {
                isTouched = true;
                Instantiate(particleFire, transform.position + Vector3.up, Quaternion.identity);
                sprite.color = touchedColor;
            } 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isTouched)
            {
                collision.GetComponent<Player>().Dead();
                audioSource.Play();
                StartCoroutine(WaitAndReset());
            }
        }
    }

    IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(1.2f);
        GameManager.instance.ResetLevel();
    }
}
