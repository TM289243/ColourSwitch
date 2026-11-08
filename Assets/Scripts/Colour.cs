using UnityEngine;

public class Colour : MonoBehaviour
{
    [SerializeField] private GameManager gm;
    private Collider2D obstacleCollider;
    [SerializeField] public int colour;

    void Awake()
    {
        obstacleCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        obstacleCollider.enabled = gm.colour != colour;
    }
}
