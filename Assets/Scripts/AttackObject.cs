using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour
{
    Pawn _ownerPawn;
    float _damage;

    public void Init(Pawn ownerPawn)
    {
        _ownerPawn = ownerPawn;
    }

    protected virtual void Attack(Pawn attackedPawn)
    {
        attackedPawn.TakeDamage(_damage);
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
