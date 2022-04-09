using UnityEngine;

namespace Enemies.States.Data
{
    [CreateAssetMenu(fileName = "newChargeStateData", menuName = "Data/State Data/Charge State")]
    public class D_ChargeState : ScriptableObject
    {
        public float _chargeSpeed = 6f;
        public float _chargeTime = 2f;

    }
}
