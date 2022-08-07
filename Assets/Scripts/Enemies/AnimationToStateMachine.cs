using Enemies.States;
using Enemies.Worm;
using JetBrains.Annotations;
using UnityEngine;

namespace Enemies
{
    public class AnimationToStateMachine : MonoBehaviour
    {
        public PlayerDetectedState PlayerDetectedState;
        public AttackState AttackState;
        public DamageState DamageState;

        [UsedImplicitly]
        private void FinishDetection()
        {
            PlayerDetectedState.FinishDetection();
        }
        
        [UsedImplicitly]
        private void TriggerAttack()
        {
            AttackState.TriggerAttack();
        }

        [UsedImplicitly]
        private void FinishAttack()
        {
            AttackState.FinishAttack();   
        }

        [UsedImplicitly]
        private void FinishDamageAnimation()
        {
            DamageState.FinishDamageAnimation();
        }
    }
}
