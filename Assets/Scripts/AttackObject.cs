using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour
{
    [SerializeField]
    AttackObjectData _attackObjectState;

    Pawn _ownerPawn;

    public void Init(Pawn ownerPawn)
    {
        _ownerPawn = ownerPawn;
        transform.forward = ownerPawn.transform.forward;
        _attackObjectState.startPosition = ownerPawn.transform.position;
        _attackObjectState.startForward = ownerPawn.transform.forward;
    }

    public void OnUpdate(float deltaTime)
    {
        _attackObjectState.lifeTime += deltaTime;
        if(_attackObjectState.lifeTime >= _attackObjectState.destroyTime)
        {
            Singleton.instance.gameEvent.onAttackObjectDestroyed.Invoke(this);
            Destroy(gameObject);
        }
        transform.position += transform.forward * deltaTime * _attackObjectState.speed;
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
