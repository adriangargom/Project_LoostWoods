using UnityEngine;
using UnityEngine.AI;

public class EnviromentDetection: MonoBehaviour
{
    [field: Header("Enemy Attributes")]
    [field: SerializeField] public NavMeshAgent NavMeshAgent;
    [field: SerializeField] public Transform PlayerBody { get; private set; }
    [field: SerializeField] public Transform Enemy { get; private set; }
    [field: SerializeField] public Transform EnemyBody { get; private set; }
    [field: SerializeField] public Transform ShootPoint { get; private set; }


    [field: Space]
    [field: Header("Player Detection")]
    [field: SerializeField] public bool PlayerDetectionCheck { get; private set; }
    [field: SerializeField] public float ActualDistanceToPlayer { get; private set; }
    [field: SerializeField] public float DetectionRange;


    void Awake()
    {
        TryGetComponent(out NavMeshAgent);
        PlayerBody = GameObject.Find("PlayerBody").transform;
        Enemy = transform;
        EnemyBody = transform.Find("EnemyBody");
    }

    void Update()
    {
        CheckPlayerInRange();
    }

    
    // Checks the actual distance between the player and this entity
    private void CheckPlayerInRange()
    {
        ActualDistanceToPlayer = Vector3.Distance(PlayerBody.position, EnemyBody.position);
        PlayerDetectionCheck = ActualDistanceToPlayer <= DetectionRange;
    }

    public void SetDetectionRange(float value)
    { 
        DetectionRange = value; 
    }

    public void IncreaseDetectionRange(float increment)
    { 
        DetectionRange += increment;
    }

    public void DecreaseDetectionRange(float decrement)
    {
        DetectionRange -= decrement;
    }

    
    void OnDrawGizmos()
    {
        //Detection Zone Helpers
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(EnemyBody.position, DetectionRange);
    }
}