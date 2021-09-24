using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{

    int sceneIndex;

    // void Awake()
    // {
    //     int numScenePersists = FindObjectsOfType<ScenePersist>().Length;
    //     if (numScenePersists > 1)
    //     {
    //         Destroy(gameObject);
    //     }
    //     else
    //     {
    //         DontDestroyOnLoad(gameObject);
    //     }
    // }

        private void Awake()
            {
                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //get active scene number
                
                ScenePersist[] persists = FindObjectsOfType<ScenePersist>(); //Create an array to hold scene persist objects
                
                foreach (var persist in persists) //Check for objects that are a part of scene persist
            {
                if (persist != this) //Check for scene persist in the scene
                {
                    if (persist.sceneIndex == currentSceneIndex)
                    {
                        Destroy(gameObject); //destroy persist object and return immediately (anything that is already in room upon awake)
                        return;
                    }
                    else
                    {
                        Destroy(persist.gameObject); //destroy if doesn't have the current scene index
                    }
                }
            }
            sceneIndex = currentSceneIndex; //set sceneIndex variable to current scene index
            DontDestroyOnLoad(gameObject); //create dont destroy on load object after scene index set correctly and all prior scene persists have been destroyed
            }

    // Start is called before the first frame update
    void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex != sceneIndex)
        {
            Destroy(gameObject);
        }
    }
}
