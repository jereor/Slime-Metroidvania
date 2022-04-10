using Enemies.States.Data;
using UnityEngine;

namespace Enemies.States
{
    public class MeleeAttackState : AttackState
    {
        protected readonly D_MeleeAttackState StateData;

        protected MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animatorBoolName, Transform attackPosition, D_MeleeAttackState stateData) : base(entity, stateMachine, animatorBoolName, attackPosition)
        {
            StateData = stateData;
        }
    }
}
