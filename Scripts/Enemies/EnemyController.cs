using UnityEngine;

public class EnemyController: MonoBehaviour
{
    [field: SerializeField] public EnemyType EnemyType { get; private set; }
    [field: SerializeField] public EnviromentDetection EnviromentDetection { get; private set;}
    [field: SerializeField] public Animator Animator { get; private set;}
    [field: SerializeField] public HealthSystem HealthSystem { get; private set;}
    [field: SerializeField] public WeaponSystem WeaponSystem { get; private set;}
    [field: SerializeField] public BaseStatsManager StatsManager { get; private set;}
    [field: SerializeField] public FiniteStateMachine FiniteStateMachine { get; private set;}
    public ActualRoomManager ActualRoomManager = null;


    void Start()
    {
        EnviromentDetection = GetComponent<EnviromentDetection>();
        Animator = GetComponent<Animator>();
        HealthSystem = GetComponent<HealthSystem>();
        
        switch(EnemyType) {
            case EnemyType.Blob:
                StatsManager = new BlobStatsManager(this);
                FiniteStateMachine = new BlobFiniteStateMachine(this);
                break;

            case EnemyType.Plant:
                StatsManager = new PlantStatsManager(this);
                FiniteStateMachine = new PlantFiniteStateMachine(this);
                break;
                
            case EnemyType.Spike:
                StatsManager = new SpikeStatsManager(this);
                FiniteStateMachine = new SpikeFiniteStateMachine(this);
                break;
        }
    
        ActualRoomManager = FindObjectOfType<ActualRoomManager>();
    }
    
    void Update()
    {
        FiniteStateMachine.Update();
    }

    void FixedUpdate()
    {
        FiniteStateMachine.FixedUpdate();
    }


    public void EnemyKill()
    {
        if(ActualRoomManager == null){
            Destroy(gameObject);
            return;
        }

        StatsManager.UpgradeAllPowerUps();
        ActualRoomManager.ActualRoom.EnemyDefeated(gameObject);
    }
}