using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIDieMenuManager : MonoBehaviour, IObserver
{

    public static UIDieMenuManager Instance { get; private set; }
    [SerializeField] private GameObject _dieMenu, _healthBar, _creditsInfo;
    [SerializeField] private TextMeshProUGUI _roomsQuantityFrame;
    [SerializeField] private HealthSystem _playerHealthSystem;
    [SerializeField] private int _mainMenuSceneId = 0;
    [SerializeField] private float _animationSpeed = .2f;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        _playerHealthSystem.Attach(this);
    }

    public void HandleExitToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(_mainMenuSceneId);
    }

    public void HandleExitGame()
    {
        Application.Quit();
    }

    public void LaunchOpenDieMenu()
    {
        StartCoroutine(OpenDieMenu());
    }

    IEnumerator OpenDieMenu()
    {
        RoomRotationManager roomRotationManager = FindObjectOfType<RoomRotationManager>();
        _roomsQuantityFrame.text = string.Format("Room: {0}", roomRotationManager.CompletedRooms);
        
        yield return new WaitForSeconds(3);
     
        LeanTween.scale(_creditsInfo, Vector3.zero, _animationSpeed)
            .setOnComplete(() => _creditsInfo.SetActive(false));

        LeanTween.scale(_healthBar, Vector3.zero, _animationSpeed)
            .setOnComplete(() => _healthBar.SetActive(false));
            
        _dieMenu.SetActive(true);
        LeanTween.scale(_dieMenu, Vector3.one, _animationSpeed)
            .setOnComplete(() => Time.timeScale = 0f);
    }


    // IObserver
    public void ObserverUpdate()
    {
        if(_playerHealthSystem.ActualHealth <= 0)
        {
            LaunchOpenDieMenu();
        }
    }
}