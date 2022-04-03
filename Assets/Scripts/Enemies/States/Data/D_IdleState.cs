using UnityEngine;

namespace Enemies.States.Data
{
    [CreateAssetMenu(fileName = "newIdleStateData", menuName = "Data/State Data/Idle State")]
    public class D_IdleState : ScriptableObject
    {
        public float minIdleTime;
        public float maxIdleTime;
        
    }
}
