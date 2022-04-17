namespace Assets.Scripts.Player
{
    internal class ExplosiveBarrel : Damagable
    {
        protected override void OnDeath()
        {
            Destroy(gameObject);
        }
    }
}
