using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    public float time = 120.0f;

    void Start()
    {
        StartCoroutine(LoadLevelAfterTime(time));
    }

    IEnumerator LoadLevelAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
