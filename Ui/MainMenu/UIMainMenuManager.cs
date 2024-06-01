using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu, _settingsMenu, _playMenu, _infoMenu;

    [SerializeField] private int _gameSceneId = 1;
    [SerializeField] private float _animationSpeed = .2f;
    private Vector3 _menuCloseScale = new Vector3(0, 1, 1);


    void Start()
    {
        _settingsMenu.transform.localScale = _menuCloseScale;

        _mainMenu.SetActive(true);
        LeanTween.scale(_mainMenu, Vector3.one, _animationSpeed);

        LeanTween.scale(_settingsMenu, _menuCloseScale, _animationSpeed)
            .setOnComplete(() => _settingsMenu.SetActive(false));

        LeanTween.scale(_playMenu, Vector3.zero, _animationSpeed)
            .setOnComplete(() => _playMenu.SetActive(false));

        LeanTween.scale(_infoMenu, Vector3.zero, _animationSpeed)
            .setOnComplete(() => _playMenu.SetActive(false));
    }


    public void StartGame()
    {
        SoundEffectsAudioManager soundEffectsAudioManager = SoundEffectsAudioManager.Instance;
        soundEffectsAudioManager.PlaySoundEffect(soundEffectsAudioManager.OpenMenu);

        SceneManager.LoadScene(_gameSceneId);
    }

    public void ExitGame()
    {
        SoundEffectsAudioManager soundEffectsAudioManager = SoundEffectsAudioManager.Instance;
        soundEffectsAudioManager.PlaySoundEffect(soundEffectsAudioManager.CloseMenu);
        
        Application.Quit();
    }

    public void OpenSettings()
    {
        SoundEffectsAudioManager soundEffectsAudioManager = SoundEffectsAudioManager.Instance;
        soundEffectsAudioManager.PlaySoundEffect(soundEffectsAudioManager.OpenMenu);

        LeanTween.scale(_mainMenu, _menuCloseScale, _animationSpeed)
            .setOnComplete(() => _mainMenu.SetActive(false));

        _settingsMenu.SetActive(true);
        LeanTween.scale(_settingsMenu, Vector3.one, _animationSpeed);
    }

    public void CloseSettings()
    {
        SoundEffectsAudioManager soundEffectsAudioManager = SoundEffectsAudioManager.Instance;
        soundEffectsAudioManager.PlaySoundEffect(soundEffectsAudioManager.CloseMenu);

        LeanTween.scale(_settingsMenu, _menuCloseScale, _animationSpeed)
            .setOnComplete(() => _settingsMenu.SetActive(false));

        _mainMenu.SetActive(true);
        LeanTween.scale(_mainMenu, Vector3.one, _animationSpeed);

        StorageSystem.Instance.WriteSaveFile();
    }

    public void OpenPlayMenu()
    {
        SoundEffectsAudioManager soundEffectsAudioManager = SoundEffectsAudioManager.Instance;
        soundEffectsAudioManager.PlaySoundEffect(soundEffectsAudioManager.OpenMenu);

        LeanTween.scale(_mainMenu, _menuCloseScale, _animationSpeed)
            .setOnComplete(() => _mainMenu.SetActive(false));

        _playMenu.SetActive(true);
        LeanTween.scale(_playMenu, Vector3.one, _animationSpeed);
    }

    public void ClosePlayMenu()
    {
        SoundEffectsAudioManager soundEffectsAudioManager = SoundEffectsAudioManager.Instance;
        soundEffectsAudioManager.PlaySoundEffect(soundEffectsAudioManager.CloseMenu);

        LeanTween.scale(_playMenu, Vector3.zero, _animationSpeed)
            .setOnComplete(() => _playMenu.SetActive(false));

        _mainMenu.SetActive(true);
        LeanTween.scale(_mainMenu, Vector3.one, _animationSpeed);
    }

    public void OpenInfoMenu()
    {
        SoundEffectsAudioManager soundEffectsAudioManager = SoundEffectsAudioManager.Instance;
        soundEffectsAudioManager.PlaySoundEffect(soundEffectsAudioManager.OpenMenu);

        LeanTween.scale(_mainMenu, _menuCloseScale, _animationSpeed)
            .setOnComplete(() => _mainMenu.SetActive(false));

        _infoMenu.SetActive(true);
        LeanTween.scale(_infoMenu, Vector3.one, _animationSpeed);
    }

    public void CloseInfoMenu()
    {
        SoundEffectsAudioManager soundEffectsAudioManager = SoundEffectsAudioManager.Instance;
        soundEffectsAudioManager.PlaySoundEffect(soundEffectsAudioManager.CloseMenu);

        LeanTween.scale(_infoMenu, Vector3.zero, _animationSpeed)
            .setOnComplete(() => _infoMenu.SetActive(false));

        _mainMenu.SetActive(true);
        LeanTween.scale(_mainMenu, Vector3.one, _animationSpeed);
    }
}