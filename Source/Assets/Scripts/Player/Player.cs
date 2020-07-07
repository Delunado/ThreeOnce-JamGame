using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public PlayerInput input;
    [HideInInspector] public SpriteRenderer sprite;
    [HideInInspector] public Animator anim;

    private AudioSource audioSource;

    public AudioClip dashClip;
    public AudioClip jumpClip;
    public AudioClip shootClip;

    public float speed;
    public float jumpSpeed;
    public GameObject bullet;
    public GameObject deadParticles;

    public GameObject echoEffect;

    [Header("Action Sprites")]
    public SpriteRenderer dodgeSpriteRenderer;
    public SpriteRenderer jumpSpriteRenderer;
    public SpriteRenderer shootSpriteRenderer;

    public Sprite dodgeOffSprite;
    public Sprite jumpOffSprite;
    public Sprite shootOffSprite;

    public int dodgeCount;
    public float dodgeSpeed;

    private int jumpNumber;
    public int JumpNumber { get => jumpNumber; set => jumpNumber = value; }

    private int shootNumber;
    public int ShootNumber { get => shootNumber; set => shootNumber = value; }

    private int dodgeNumber;
    public int DodgeNumber { get => dodgeNumber; set => dodgeNumber = value; }

    private StateBase state;

    private short lastDirection;
    public short LastDirection { get => lastDirection; set => lastDirection = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        SetState(new StateWalkJump(this));
        JumpNumber = 1;
        ShootNumber = 1;
        DodgeNumber = 1;
        LastDirection = 1;
    }

    void Update()
    {
        state.Tick();
    }

    public void SetState(StateBase newState)
    {
        if (state != null)
        {
            state.OnStateExit();
        }

        state = newState;

        if (state != null)
        {
            state.OnStateEnter();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ExitDoor"))
        {
            speed = 0;
        }
    }

    //ACCIONES
    public void Shoot()
    {
        BulletController bulletController = Instantiate(bullet, transform.position + Vector3.up * 0.3f, Quaternion.identity).GetComponent<BulletController>();
        bulletController.Direction = LastDirection;
    }

    public void FlipSprite()
    {
        if (LastDirection == 1)
        {
            sprite.flipX = false;
        } else
        {
            sprite.flipX = true;
        }
    }

    public void Dead()
    {
        Instantiate(deadParticles, transform.position + (Vector3.up * 0.2f) + (Vector3.left * 0.5f), Quaternion.identity);
        Destroy(gameObject);
    }

    private void PutOffSprite(SpriteRenderer render, Sprite sprite)
    {
        render.sprite = sprite;
    }

    public void UseJump()
    {
        audioSource.PlayOneShot(jumpClip);
        PutOffSprite(jumpSpriteRenderer, jumpOffSprite);
        JumpNumber--;
    }

    public void UseDodge()
    {
        audioSource.PlayOneShot(dashClip);
        PutOffSprite(dodgeSpriteRenderer, dodgeOffSprite);
        DodgeNumber--;
    }

    public void UseShoot()
    {
        audioSource.PlayOneShot(shootClip);
        PutOffSprite(shootSpriteRenderer, shootOffSprite);
        ShootNumber--;
    }

    public void CreateEcho()
    {
        GameObject echo = Instantiate(echoEffect, transform.position + (Vector3.left / 3 * LastDirection), Quaternion.identity);
        if (LastDirection == -1)
        {
            echo.GetComponent<SpriteRenderer>().flipX = true;
        }
        Destroy(echo, 0.2f);
    }

    //COMPROBACIONES
    public void UpdateLastDirection()
    {
        if (input.horizontal < 0)
            LastDirection = -1;
        else if (input.horizontal > 0)
            LastDirection = 1;
    }
}
