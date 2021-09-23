using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit1 : MonoBehaviour
{
    [SerializeField]
    float LevelLoadDelay = 2f;
    [SerializeField]
    float LevelExitSlowMo = 0.2f;

    void OnTriggerEnter2D(Collider2D other) 
    {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        Time.timeScale = LevelExitSlowMo;
        yield return new WaitForSecondsRealtime(LevelLoadDelay);
        Time.timeScale = 1.0f; // Reset back to normal

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

}
