using UnityEngine;

public class PlayerState : MonoBehaviour
    {
    public static PlayerState Instance { get; private set; }
    private static AnimationManager animationManager;
    public enum State
    {
        Idle,
        Walking,
        Running,
        Jumping
    }

    public static State CurrentState { get; private set; } = State.Idle;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Initialize animationManager by getting it from the current GameObject
            animationManager = GetComponent<AnimationManager>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void SetState(State newState)
    {
        if (CurrentState == newState) return;
        Debug.Log($"[PlayerState] Changing state from {CurrentState} to {newState}");
        CurrentState = newState;
        animationManager.UpdateAnimation();
    }
}
