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

   public void optionsMenu()
   {
       SceneManager.LoadScene(4);
   }

   public void returnToMain()
   {
       // Used for Options -> Main
       SceneManager.LoadScene(0);
   }

    public void ExitGame()
    {
        Application.Quit();
    }
}
