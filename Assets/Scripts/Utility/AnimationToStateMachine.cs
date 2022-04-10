using Enemies.States;
using UnityEngine;

namespace Utility
{
    public class AnimationToStateMachine : MonoBehaviour
    {
        public AttackState AttackState;
    
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