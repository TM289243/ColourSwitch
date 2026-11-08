using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{ 

    public void Play()
    {
        SceneManager.LoadScene("ColourSwitch");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
