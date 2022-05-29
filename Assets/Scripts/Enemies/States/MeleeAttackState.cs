using Enemies.States.Data;
using UnityEngine;
using Utility;
using Utility.Constants;

namespace Enemies.States
{
    public class MeleeAttackState : AttackState
    {
        protected readonly D_MeleeAttackState StateData;
        
        protected AttackDetails AttackDetails;

        protected MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animatorBoolName, Transform attackPosition, D_MeleeAttackState stateData) : base(entity, stateMachine, animatorBoolName, attackPosition)
        {
            StateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            AttackDetails.DamageAmount = StateData._attackDamage;
            AttackDetails.Position = Entity.transform.position;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void HandleChecks()
        {
            base.HandleChecks();
        }

        public override void TriggerAttack()
        {
            base.TriggerAttack();
        }

        public override void FinishAttack()
        {
            base.FinishAttack();
            
            Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(
                AttackPosition.position, 
                StateData._attackRadius,
                StateData._playerLayer.value);

            foreach (Collider2D collider in detectedObjects)
            {
                collider.transform.SendMessage(EventConstants.DAMAGE, StateData._attackDamage);
            }
        }
    }
}
