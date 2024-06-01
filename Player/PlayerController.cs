using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [field: SerializeField] public InputManager InputManager { get; private set; }
    [field: SerializeField] public ObstacleDetection ObstacleDetection { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public HealthSystem HealthSystem { get; private set; }
    [field: SerializeField] public WeaponSystem WeaponSystem { get; private set; }
    [field: SerializeField] public BaseStatsManager BaseStatsManager { get; private set; }
    [field: SerializeField] public FiniteStateMachine FiniteStateMachine { get; private set; }

    void Awake()
    {
        InputManager = GetComponent<InputManager>();
        ObstacleDetection = GetComponent<ObstacleDetection>();
        Animator = GetComponent<Animator>();
        HealthSystem = GetComponent<HealthSystem>();
        
        BaseStatsManager = new PlayerStatsManager(this);
        FiniteStateMachine = new PlayerFiniteStateMachine(this);
    }

    void Update()
    {
        FiniteStateMachine.Update();
    }

    void FixedUpdate()
    {
        FiniteStateMachine.FixedUpdate();
    }
}