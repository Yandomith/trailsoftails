using UnityEngine;

public class AnimationManager: MonoBehaviour
{
    private PlayerState.State currentState; // Use PlayerState.State
    [SerializeField]private Animator animator; // Reference to the Animator component

    public AnimationManager(Animator animator)
    {
        this.animator = animator;
        
    }

    public void UpdateAnimation()
    {
        currentState = PlayerState.CurrentState;
        Debug.Log($"[AnimationManager] Current state: {currentState}");
        switch (currentState)
        {
            case PlayerState.State.Idle:
                animator.Play("Idle");
                break;
            case PlayerState.State.Running:
                animator.Play("Run");
                break;
            case PlayerState.State.Jumping:
                animator.Play("Jump");
                break;

        }
    }

    public PlayerState.State GetState()
    {
        return currentState;
    }
}
