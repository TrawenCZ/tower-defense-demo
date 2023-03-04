using UnityEngine;

public class LazyEnemy : Enemy
{
    private float timeSinceLastMove = 0;
    private bool shouldStop = false;

    void Update()
    {
        TimeSinceLastStop += Time.deltaTime;
        if (shouldStop)
        {
            timeSinceLastMove += Time.deltaTime;
            if (timeSinceLastMove > 1)
            {
                shouldStop = false;
                _movementComponent.MoveAlongPath();
                timeSinceLastMove = 0;
                TimeSinceLastStop = 0;
            }
        } else if (TimeSinceLastStop > 5)
        {
            shouldStop = true;
            _movementComponent.CancelMovement();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        EnemyCollided(other, _damage * 2);
    }
}
