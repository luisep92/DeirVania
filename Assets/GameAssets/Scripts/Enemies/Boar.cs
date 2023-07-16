using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Enemy
{

    private void Start()
    {
        print(Player.Instance.gameObject);
    }

    protected override void Attack()
    {
        throw new System.NotImplementedException();
    }

    protected override void Idle()
    {
        
    }

    protected override void Move(Vector2 position)
    {
        throw new System.NotImplementedException();
    }

    protected override void Patrol()
    {
        throw new System.NotImplementedException();
    }
}
