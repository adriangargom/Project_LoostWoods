using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu, _healthBar, _creditsInfo;
    [SerializeField] private int _mainMenuSceneId = 0;
    public bool IsGamePaused { get; private set; }


    void Start()
    {
        _creditsInfo = transform.Find("CreditsInfo").gameObject;
        _healthBar = transform.Find("HealthBar").gameObject;
        _pauseMenu = transform.Find("PauseMenu").gameObject;

        _pauseMenu.SetActive(false);
    }

    void Update()
    {
        HandleGamePause();
    }


    public void HandleExitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(_mainMenuSceneId);
    }

    private void HandleGamePause()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(IsGamePaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        _creditsInfo.SetActive(true);
        _healthBar.SetActive(true);
        _pauseMenu.SetActive(false);

        IsGamePaused = false;
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        _creditsInfo.SetActive(false);
        _healthBar.SetActive(false);
        _pauseMenu.SetActive(true);
        
        IsGamePaused = true;
        Time.timeScale = 0f;
    }

}