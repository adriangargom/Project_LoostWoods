using UnityEngine;

public class BlobIdleState : BaseState
{
    private readonly BlobFiniteStateMachine _blobStateMachine;
    private readonly EnviromentDetection _enviromentDetection;
    private readonly Animator _animator;
    private readonly HealthSystem _healthSystem;


    public BlobIdleState(FiniteStateMachine finiteStateMachine) 
        : base(finiteStateMachine)
        {
            _blobStateMachine = finiteStateMachine as BlobFiniteStateMachine;

            _enviromentDetection = _blobStateMachine.BlobController.EnviromentDetection;
            _animator = _blobStateMachine.BlobController.Animator;
            _healthSystem = _blobStateMachine.BlobController.HealthSystem;
        }

        
    public override void StateStart() 
    {
        _animator.SetBool("idle", true);
    }

    public override void StateUpdate()
    {
        // Change to DIE State
        if(_healthSystem.ActualHealth <= 0)
        {
            ChangeTo<BlobDieState>();
            return;
        }

        // Change to FOLLOW State
        if(_enviromentDetection.PlayerDetectionCheck)
        {
            ChangeTo<BlobFollowState>();
            return;
        }
    }

    public override void StateFixedUpdate() {}

    public override void StateExit()
    {
        _animator.SetBool("idle", false);
    }
}