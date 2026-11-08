using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{

    [SerializeField] private PlayerScript player;
    private Animator animator;
    private float endGameDelay = 1f;
    public AudioSource exitSound;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        exitSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.tag=="EndGoal" && collision.gameObject.tag == "Player")
        {
            Debug.Log("Exit reached");
            player.endGame();
        }
        
    }

    public void close()
    {
        if(animator.GetBool("isClosed") == false)
        {
            exitSound.Play();
        }
        animator.SetBool("isClosed", true);
        endGameDelay -= Time.deltaTime;
            if(endGameDelay <= 0)
            {
                SceneManager.LoadScene("Menu");
            }
    }
}
