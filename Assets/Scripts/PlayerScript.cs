using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerScript : MonoBehaviour
{

    public int speed = 2;
    private Vector2 move;
    private Rigidbody2D rb;
    private int direction = 1;
    private Animator animator;
    public float jumpHeight = 2.4f;
    public int jumpCountBase = 1;
    private int jumpCount;
    private int score=0;
    [SerializeField] public GameManager gm;
    [SerializeField] private Exit exit;
    public int lives = 3;
    private Vector2 lastPosition;
    private SpriteRenderer sr;
    Colour obstacle;
    Projectile projectile;
    public bool canSwitchColour = false;
    public float endGameDelay = 1f;
    private bool endingGame = false;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource switchSound;
    [SerializeField] private AudioSource hurtSound;
    [SerializeField] private AudioSource abilitySound;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpCount = jumpCountBase;
        lastPosition = rb.position;
        sr = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        move = InputSystem.actions["Move"].ReadValue<Vector2>();

        if (InputSystem.actions["Jump"].IsPressed() && jumpCount > 0)
        {
            InputSystem.actions["Jump"].Reset();
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(new Vector2(0, Mathf.Sqrt(-2 * Physics2D.gravity.y * jumpHeight)), ForceMode2D.Impulse);
            jumpCount -= 1;
            jumpSound.Play();
        }
 
        if (InputSystem.actions["Attack"].IsPressed() && canSwitchColour)
        {
            InputSystem.actions["Attack"].Reset();
            gm.colourChange();
            switchSound.Play();
            if(gm.colour == 1)
            {
                sr.color = gm.color1;
            }
            else
            {
                sr.color = gm.color2;
            }
        }

        if(move.x != 0)
        {
            direction = move.x < 0 ? 1 : -1;
            animator.SetInteger("Direction", direction);
        }
        animator.SetFloat("Move", move.x);
        if (Mathf.Abs(rb.linearVelocity.y) > 0)
        {
            animator.SetBool("inAir", true);
        }
        else
        {
            animator.SetBool("inAir", false);
        }

        if(endingGame)
        {
            if(sr.color.a > 0)
            {
                sr.color -= new Color(0, 0, 0, 0.005f);
                Debug.Log(sr.color.a);
            }
            else
            {
                exit.close();
            }
        }
        
    }

    void FixedUpdate()
    {
        Vector2 position = rb.transform.position;
        position.x += (move.x*speed)*Time.fixedDeltaTime;
        rb.transform.position = position;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        obstacle = collision.gameObject.GetComponent<Colour>();
        projectile = collision.gameObject.GetComponent<Projectile>();
        if (!collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Not an obstacle");
            if (collision.gameObject.CompareTag("Ground"))
            {
                animator.SetBool("inAir", false);
            }
            return;
        }

        Debug.Log("Collided with an obstacle");

        if (obstacle != null && gm.colour != obstacle.colour)
        {
            Debug.Log("Hit an obstacle");
            lives--;
            gm.UpdateLives(lives);
            if (lives > 0)
            {
                rb.transform.position = lastPosition;
            }
            
            hurtSound.Play();
        }
        else if (projectile != null && gm.colour == projectile.colour)
        {
            Debug.Log("Hit a projectile");
            lives--;
            gm.UpdateLives(lives);
            if (lives > 0)
            {
                rb.transform.position = lastPosition;
            }
            rb.linearVelocity = Vector2.zero;
            hurtSound.Play();}
        else
        {
            Debug.Log("Hit an obstacle but colours match");
        }
    }

    public void AddCollectible()
    {
        score++;
        gm.UpdateScore(score);
    } 

    public void reachCheckpoint()
    {
        lastPosition = rb.transform.position;
    }

    public void resetJumps()
    {
        jumpCount = jumpCountBase;
    }

    public void CanSwitchColour()
    {
        abilitySound.Play();
        canSwitchColour = true;
    }

    public void endGame()
    {
        speed = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        endingGame = true;

    }
}
