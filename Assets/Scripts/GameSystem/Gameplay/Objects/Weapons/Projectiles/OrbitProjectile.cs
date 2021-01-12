namespace GameSystem.Weapons.Projectiles
{
    public class OrbitProjectile : TurnableProjectile
    {
        protected override void Move()
        {
            MoveForward();
            Turn();
        }
    }
}

