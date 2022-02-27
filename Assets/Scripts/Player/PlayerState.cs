using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance;

    public enum State
    {
        Idle,
        Moving,
        Jumping,
        Ascending,
        Falling,
        Landing
    }

    [SerializeField] private State _currentState = State.Idle;

    private void Awake()
    {
        if (Instance == null) 
        { 
            Instance = this; 
        }
    }
   

    public void SetToMoving()
    {
        if (IsNotState(State.Moving))
        {
            _currentState = State.Moving;
        }
    }

    public void SetToIdle()
    {
        if (IsNotState(State.Idle))
        {
            _currentState = State.Idle;
        }
    }

    private bool IsNotState(State state)
    {
        return state != _currentState;
    }

    private bool IsState(State state)
    {
        return state == _currentState;
    }
}

