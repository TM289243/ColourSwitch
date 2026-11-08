using UnityEngine;
using UnityEngine.Tilemaps;

public class YellowGround : MonoBehaviour
{

    [SerializeField]private GameManager gm;
    private TilemapCollider2D col;
    [SerializeField] private int colour;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col = GetComponent<TilemapCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        col.enabled = gm.colour == colour;
    }



}
