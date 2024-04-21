using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;

    AsyncOperation loadingOperation;
    public Slider progressBar;

    [SerializeField] private CanvasGroup cvGroupMenu;
    [SerializeField] private CanvasGroup cvGroupOptions;

    [SerializeField] private bool fadeInMenu = false;
    [SerializeField] private bool fadeOutMenu = false;


    void Start()
    {
        optionsMenu.SetActive(false);
        progressBar.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(progressBar.IsActive() == true)
        {
            progressBar.value = Mathf.Clamp01(loadingOperation.progress / 0.9f);
        }

        if(fadeInMenu)
        {
            if(cvGroupMenu.alpha < 1)
            {
                cvGroupMenu.alpha += Time.deltaTime;
                cvGroupOptions.alpha -= Time.deltaTime;

                if (cvGroupMenu.alpha >= 1)
                {
                    mainMenu.SetActive(true);
                    optionsMenu.SetActive(false);
                    fadeInMenu = false;
                }
            }
        }

        if (fadeOutMenu)
        {
            if (cvGroupMenu.alpha >= 0)
            {
                cvGroupMenu.alpha -= Time.deltaTime;
                cvGroupOptions.alpha += Time.deltaTime;

                if (cvGroupMenu.alpha == 0)
                {
                    mainMenu.SetActive(false);
                    optionsMenu.SetActive(true);
                    fadeOutMenu = false;
                }
            }
        }
    }

    public void Play()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        progressBar.gameObject.SetActive(true);

        loadingOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ShowMenu()
    {
        fadeInMenu = true;
    }

    public void HideMenu()
    {
        fadeOutMenu = true;
    }
}
