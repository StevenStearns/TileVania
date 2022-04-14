using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float fltLevelLoadDelay = 1f;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(LoadNextLevel());

        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(fltLevelLoadDelay);
        int intCurrentSeneIndex = SceneManager.GetActiveScene().buildIndex;
        int intNextSceneIndex = intCurrentSeneIndex + 1;

        if(intNextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            intNextSceneIndex = 0;
        }

        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(intNextSceneIndex);
    }
}
