using System.Collections.Generic;
using UnityEngine;

public class ObstacleDetection : MonoBehaviour
{
    [field: Header("Player Attributes")]
    public Transform PlayerBody;

    [field: Space]
    [field: Header("Ground Check")]
    [field: SerializeField] public bool IsGrounded { get; private set; }
    public RaycastHit GroundCheckRaycastHit;
    [SerializeField] private float _groundCheckLength, _groundCheckRadius;
    [SerializeField] private LayerMask _groundCheckLayer;

    [field: Space]
    [field: Header("Lower Obstacle Check")]
    [field: SerializeField] public bool LowerObstacleCheck { get; private set; }
    public RaycastHit LowerObstacleRaycastHit;  
    [SerializeField] private float _lowerObstacleLength, _lowerObstacleRadius;

    [field: Space]
    [field: Header("Medium Obstacle Check")]
    [field: SerializeField] public bool MediumObstacleCheck { get; private set; }
    public RaycastHit MediumObstacleRaycastHit;
    [field: SerializeField] private float _mediumObstacleLength, _mediumObstacleRadius;

    
    void Update()
    {
        CheckGround();
        CheckLowerObstacles();
        CheckMediumObstacles();
    }

    private void CheckGround()
    {
        IsGrounded = Physics.SphereCast(
            PlayerBody.position,
            _groundCheckRadius,
            PlayerBody.up * -1,
            out GroundCheckRaycastHit,
            _groundCheckLength,
            _groundCheckLayer
        );
    }

    private void CheckLowerObstacles()
    {
        LowerObstacleCheck = Physics.SphereCast(
            PlayerBody.position - new Vector3(0, 0.5f, 0),
            _lowerObstacleRadius,
            PlayerBody.forward,
            out LowerObstacleRaycastHit,
            _lowerObstacleLength
        );
    }

    private void CheckMediumObstacles()
    {
        MediumObstacleCheck = Physics.SphereCast(
            PlayerBody.position, 
            _mediumObstacleRadius, 
            PlayerBody.forward, 
            out MediumObstacleRaycastHit, 
            _mediumObstacleLength
        );
    }

    
    void OnDrawGizmos()
    {
        // Ground Check Helpers
        Gizmos.color = Color.red;
        Vector3 groundCheckEndPos = PlayerBody.position + PlayerBody.up * -1 * _groundCheckLength;

        Gizmos.DrawLine(PlayerBody.position, groundCheckEndPos);
        Gizmos.DrawWireSphere(groundCheckEndPos, _groundCheckRadius);

        // Lower Obstacle Helpers
        Gizmos.color = Color.green;
        Vector3 lowerObstacleStartPos = PlayerBody.position - new Vector3(0, 0.5f, 0);
        Vector3 lowerObstacleEndPos = lowerObstacleStartPos + PlayerBody.transform.forward * _lowerObstacleLength;

        Gizmos.DrawLine(lowerObstacleStartPos, lowerObstacleEndPos);
        Gizmos.DrawWireSphere(lowerObstacleEndPos, _lowerObstacleRadius);


        // Medium Obstacle Helpers
        Gizmos.color = Color.blue;
        Vector3 mediumObstacleStartPoint = PlayerBody.position;
        Vector3 mediumObstacleEndPos = mediumObstacleStartPoint + PlayerBody.transform.forward * _mediumObstacleLength;

        Gizmos.DrawLine(mediumObstacleStartPoint, mediumObstacleEndPos);
        Gizmos.DrawWireSphere(mediumObstacleEndPos, _mediumObstacleRadius);
    }

}
