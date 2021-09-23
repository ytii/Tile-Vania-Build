using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

   public void StartFirstLevel()
   {
       SceneManager.LoadScene(1);
        //FindObjectOfType<GameSession>().ResetGameSession();
   }

   public void mainMenu()
   {
       SceneManager.LoadScene(0);
        FindObjectOfType<GameSession>().ResetGameSession();
   }

    public void ExitGame()
    {
        Application.Quit();
    }
}
