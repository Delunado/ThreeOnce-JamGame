using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDead : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().Dead();
            audioSource.Play();
            StartCoroutine(WaitAndReset());
        }
    }

    IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(1.2f);
        GameManager.instance.ResetLevel();
    }
}
