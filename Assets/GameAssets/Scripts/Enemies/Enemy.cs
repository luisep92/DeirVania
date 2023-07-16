using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    //Change angry for a better naming
    public enum EnemyState { IDLE, PATROL }


    public EnemyState enemyState;
    protected List<Vector3> wayPoints;
    protected Rigidbody2D rb;
    private Player player;

    private void Awake()
    {
        player = Player.Instance;
        rb = GetComponent<Rigidbody2D>();
    }

    protected abstract void Idle();

    protected abstract void Patrol();

    protected abstract void Move(Vector2 position);

    protected abstract void Attack();

    protected virtual void DetectPlayer()
    {

    }
}
