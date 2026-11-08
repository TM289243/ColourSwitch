using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Projectile : MonoBehaviour
{

    private SpriteRenderer sr;

    private Rigidbody2D rb;
    [SerializeField] private GameManager gm;
    public int colour;
    private Collider2D oc;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        oc = GetComponent<Collider2D>();
    }

    public void Launch(Vector2 direction, float force, int colour, GameManager gm)
    {
        this.colour = colour;
        this.gm = gm;
        rb.AddForce(direction * force);
        if(colour == 1)
        {
            sr.color = gm.color2;
        }
        else
        {
            sr.color = gm.color1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        oc.enabled = gm.colour == this.colour;
    }
}
