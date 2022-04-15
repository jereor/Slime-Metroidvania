namespace Player.Core
{
    public class PlayerHealth : Health
    {
        public override void Damage(float damageAmount)
        {
            base.Damage(damageAmount);
        }

        public override void CheckDeath()
        {
            base.CheckDeath();
        }
    }
}