using System;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{

    [SerializeField] private float walkTime;
    [SerializeField] private float idleTime;
    private float elapsedTime;
    private int direction = 1;
    private bool isWalking = false;
    private Rigidbody2D rb;
    private Animator animator;
    private float fireTimer = 1.5f;
    private float fireCountdown;
    [SerializeField] private GameObject projectile;
    private int strength = 3;
    private bool isInvincible;
    private float invincibleCount;
    [SerializeField] private GameManager gm;
    private SpriteRenderer sr;
    private AudioSource shootSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        shootSound = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetInteger("Direction", direction);
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.colour == 1)
        {
            sr.color = gm.color2;
        }
        else
        {
            sr.color = gm.color1;
        }
        

        if (isInvincible)
        {
            invincibleCount += Time.deltaTime;
            if(invincibleCount > 1f)
            {
                isInvincible = false;
            }
        }
    }

    
    private void FixedUpdate()
    {
        fireCountdown += Time.fixedDeltaTime;


        int playerDirection = GetPlayerDirection();

        animator.SetInteger("Direction", playerDirection);

        if (playerDirection != 0)
        {
            Fire(playerDirection);
            
        }
    }

    private int GetPlayerDirection()
    {
        
        RaycastHit2D leftHit = Physics2D.Raycast(rb.position, Vector2.left, 5f, LayerMask.GetMask("Player"));
        RaycastHit2D rightHit = Physics2D.Raycast(rb.position, Vector2.right, 5f, LayerMask.GetMask("Player"));

        bool playerOnLeft = leftHit.collider != null &&
                            leftHit.collider.GetComponent<PlayerScript>() != null;
        bool playerOnRight = rightHit.collider != null &&
                             rightHit.collider.GetComponent<PlayerScript>() != null;

        

        if (playerOnLeft)
        {
            return -1;
        }
        else if(playerOnRight)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    private void Fire(int fireDirection)
    {
        if (fireCountdown > fireTimer)
        {
            GameObject go = Instantiate(projectile, rb.transform.position + Vector3.up * 0.124f, Quaternion.identity);
            Projectile pro = go.GetComponent<Projectile>();
            pro.Launch(new Vector2(fireDirection, 0), 300, gm.colour, gm);
            fireCountdown = 0;
            shootSound.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Projectile>() != null)
        {
            rb.linearVelocity = new Vector2(0, 0);
            Hit();
        }
    }

    private void Hit()
    {
        if (!isInvincible) 
        { 
            strength--;
            if (strength <= 0)
            {
                Destroy(this.gameObject);
            }

            isInvincible = true;
        }
        
    }
}
