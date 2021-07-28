using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _start_screen : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
    public void onClick_game()
    {
        SceneManager.LoadScene(1);
    }
    public void onClick_info()
    {
        SceneManager.LoadScene(2);
    }
    public void onClick_exit()
    {
        Application.Quit();
    }

}
