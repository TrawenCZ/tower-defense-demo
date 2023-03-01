using UnityEngine;

public class LazyEnemy : Enemy
{
    private float timeSinceLastMove = 0;
    private bool shouldStop = false;

    void Update()
    {
        this.TimeSinceLastStop += Time.deltaTime;
        if (shouldStop)
        {
            timeSinceLastMove += Time.deltaTime;
            if (timeSinceLastMove > 1)
            {
                shouldStop = false;
                this._movementComponent.MoveAlongPath();
                timeSinceLastMove = 0;
            }
        } else if (this.TimeSinceLastStop > 5)
        {
            shouldStop = true;
            this._movementComponent.CancelMovement();
            this.TimeSinceLastStop = 0;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        EnemyCollided(other, _damage * 2);
    }
}
