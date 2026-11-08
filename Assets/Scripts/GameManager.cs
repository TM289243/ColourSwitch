using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    //[SerializeField] TMP_Text scoreText;
    [SerializeField] Image[] images;
    [SerializeField] private PlayerScript player;
    [SerializeField] private Exit exit;
    [SerializeField] private TMP_Text gameOverText;
    public int colour = 1;
    public Color color1 = new Color(1, 1, 0);
    public Color color2 = new Color(0, 1, 1);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void colourChange()
    {
        colour*=-1;
    }

    public void UpdateScore(int score)
    {
        //scoreText.text = score.ToString(); 
    } 

    public void UpdateLives(int lives)
    {
        switch (lives)
        {
            case 0:
                images[0].enabled = false;
                images[1].enabled = false;
                images[2].enabled = false;
                exit.exitSound.volume=0;
                player.endGame();
                gameOverText.enabled = true;
                break;
            case 1:
                images[0].enabled = false;
                images[1].enabled = false;
                images[2].enabled = true;
                break;
            case 2:
                images[0].enabled = false;
                images[1].enabled = true;
                images[2].enabled = true;
                break;
            case 4:
                images[0].enabled = true;
                images[1].enabled = true;
                images[2].enabled = true;
                break;
        } 

    }
}
