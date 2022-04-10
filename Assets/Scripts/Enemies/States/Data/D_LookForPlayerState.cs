using UnityEngine;

namespace Enemies.States.Data
{
    [CreateAssetMenu(fileName = "newLookForPlayerStateData", menuName = "Data/State Data/Look For Player State")]
    public class D_LookForPlayerState : ScriptableObject
    {
        public int _maxAmountOfTurns = 2;
        public float _timeBetweenTurns = 0.75f;

    }
}
