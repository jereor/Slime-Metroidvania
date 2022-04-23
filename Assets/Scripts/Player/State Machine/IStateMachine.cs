using UnityEngine;

namespace Player.State_Machine
{
    public interface IStateMachine
    {
        Animator Animator { get; }
        void DealDamage();
        void FinishMeleeAttacking();
        bool IsGrounded();
    }
}