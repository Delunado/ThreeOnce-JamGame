using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelController : MonoBehaviour
{
    private Animator anim;
    private AudioSource audioSource;

    public string nextLevelName;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger("DoorOpen");
            audioSource.Play();
            StartCoroutine(WaitToNextLevel());
        }
    }

    IEnumerator WaitToNextLevel()
    {
        yield return new WaitForSeconds(1f);
        GameManager.instance.LoadLevel(nextLevelName);
    }
}
