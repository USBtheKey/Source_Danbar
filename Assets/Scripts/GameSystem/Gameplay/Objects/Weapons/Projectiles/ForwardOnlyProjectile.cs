namespace GameSystem.Weapons.Projectiles
{
    public sealed class ForwardOnlyProjectile : Projectile
    {

        protected override void Move()
        {
            MoveForward();
        }
    }
}

