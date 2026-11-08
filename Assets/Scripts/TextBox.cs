using UnityEngine;
using TMPro;

public class TextBox : MonoBehaviour
{
    
    TMP_Text text;
    bool isInside;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isInside && text.alpha < 1)
        {
            text.alpha += 0.01f;
        }
        else if(!isInside && text.alpha > 0)
        {
            text.alpha -= 0.01f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            
            isInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        isInside = false;
    }
}
