using System;
using Unity.VisualScripting;
using UnityEngine;

public class Zombie : MonoBehaviour
{

    [SerializeField] private float walkTime;
    [SerializeField] private float idleTime;
    private float elapsedTime;
    private int direction = 1;
    private bool isWalking = false;
    private Rigidbody2D rb;
    private Animator animator;
    private float fireTimer = 0.5f;
    private float fireCountdown;
    [SerializeField] private GameObject projectile;
    private int strength = 3;
    private bool isInvincible;
    private float invincibleCount;
    private SpriteRenderer sr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetInteger("Direction", direction);
        animator.SetInteger("Move", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(isWalking && elapsedTime < walkTime)
        {
            Vector2 position = rb.transform.position;
            position.x += direction * Time.deltaTime;
            rb.transform.position = position;
        }
        else if(!isWalking && elapsedTime > walkTime){
            isWalking = true;
            elapsedTime = 0;
            direction *= -1;
            animator.SetFloat("Move", direction);
            animator.SetInteger("Direction", direction);
        }
        else if(isWalking && elapsedTime > walkTime)
        {
            isWalking = false;
            elapsedTime = 0;
            animator.SetFloat("Move", 0);
        }

        elapsedTime += Time.deltaTime;

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
        RaycastHit2D hit = Physics2D.Raycast(rb.transform.position, new Vector2(direction, 0),
            5f, LayerMask.GetMask("Player"));
        if(hit.collider != null)
        {
            if (hit.collider.GetComponent<PlayerScript>() != null)
            {
                Fire();
            }
        }

        fireCountdown += Time.fixedDeltaTime;

        
    }

    public void Fire()
    {
        if (fireCountdown > fireTimer)
        {
            GameObject go = Instantiate(projectile, rb.transform.position, Quaternion.identity);
            Projectile pro = go.GetComponent<Projectile>();
            fireCountdown = 0;
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
