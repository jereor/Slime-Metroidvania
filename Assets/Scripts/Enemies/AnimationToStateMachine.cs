using Enemies.States;
using UnityEngine;

namespace Enemies
{
    public class AnimationToStateMachine : MonoBehaviour
    {
        public PlayerDetectedState PlayerDetectedState;
        public AttackState AttackState;

        private void FinishDetection()
        {
            PlayerDetectedState.FinishDetection();
        }
        
        private void TriggerAttack()
        {
            AttackState.TriggerAttack();
        }

        private void FinishAttack()
        {
            AttackState.FinishAttack();   
        }
    }
}
