using Enemies.States;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.Worm
{
    public class WormMeleeAttackState : MeleeAttackState
    {
        
        
        protected WormMeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animatorBoolName, Transform attackPosition, D_MeleeAttackState stateData) : base(entity, stateMachine, animatorBoolName, attackPosition, stateData)
        {
        }
    }
}
