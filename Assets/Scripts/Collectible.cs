using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    [SerializeField] private PlayerScript player;
    public int direction = 1;
    private float height = .3f;
    private float timePassed = 0;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }   
    

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        position.y += (height * Time.deltaTime*direction);
        transform.position = position;

        timePassed += Time.deltaTime;
        if (timePassed > 2)
        {
            direction *= -1;
            timePassed = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.tag=="Collectible" && collision.gameObject.tag == "Player")
        {
            player.AddCollectible();
            Destroy(this.gameObject);
        }
        else if(this.tag=="AbilityGiver" && collision.gameObject.tag == "Player")
        {
            player.CanSwitchColour();
            Destroy(this.gameObject);
        }
    }
}
