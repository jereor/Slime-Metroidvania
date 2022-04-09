using UnityEngine;

namespace Enemies.States.Data
{
    [CreateAssetMenu(fileName = "newPlayerDetectedStateData", menuName = "Data/State Data/Player Detected State")]
    public class D_PlayerDetectedState : ScriptableObject
    {
        public float _longRangeActionTime = 1.5f;

    }
}
