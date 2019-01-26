using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour
{
    protected AttackObjectState _attackObjectState;

    Pawn _ownerPawn;

    public void Init(Pawn ownerPawn)
    {
        _ownerPawn = ownerPawn;
    }

    public void OnUpdate(float deltaTime)
    {
        transform.position += Vector3.forward;
    }

    protected virtual void Attack(Pawn attackedPawn)
    {
        attackedPawn.TakeDamage(_attackObjectState.damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        var pawn = other.GetComponent<Pawn>();

        if(pawn != null && pawn != _ownerPawn)
        {
            Attack(pawn);
        }
    }
}
