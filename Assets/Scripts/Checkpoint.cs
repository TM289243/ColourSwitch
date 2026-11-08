using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    [SerializeField] private PlayerScript player;
    //private Animator animator;
    private bool reachedCheckpoint = false;
    private AudioSource checkpointSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //animator = GetComponent<Animator>();
        checkpointSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //animator.SetBool("reachedCheckpoint", reachedCheckpoint);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.tag == "Checkpoint" && collision.gameObject.tag == "Player")
        {
            if(!reachedCheckpoint)
            {
                player.lives = 3;
                player.gm.UpdateLives(player.lives);
                checkpointSound.Play();
            }

            player.reachCheckpoint();
            reachedCheckpoint = true;
            Debug.Log("Checkpoint reached");
        }
    }
}
