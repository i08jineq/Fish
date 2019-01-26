using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [SerializeField]
    AttackObject _attackObject;

    int _hp;

    public void Init()
    {
        
    }

    protected virtual void TakeDamage(int damageValue)
    {
        _hp -= damageValue;
        Singleton.instance.gameEvent.takeDamageEvent.Invoke(this, damageValue);

        if (_hp < 0)
        {
            Singleton.instance.gameEvent.deadEvent.Invoke(this);
            Destroy(gameObject);
        }
    }

    protected virtual void SpawnAttackObject()
    {
        var attackObject = Instantiate(_attackObject, transform);
        attackObject.Init();
    }

}
