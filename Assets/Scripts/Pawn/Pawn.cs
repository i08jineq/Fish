using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [SerializeField]
    AttackObject _attackObject = null;

    public PawnState pawnState;
    public IController controller;

    public void Init()
    {
        
    }

    public virtual void OnUpdate(float deltaTime)
    {
        controller.OnUpdate(deltaTime);
    }

    public void TakeDamage(float damageValue)
    {
        pawnState.Hp -= damageValue;
        Singleton.instance.gameEvent.takeDamageEvent.Invoke(this, damageValue);

        if (pawnState.Hp < 0)
        {
            Singleton.instance.gameEvent.deadEvent.Invoke(this);
            Destroy(gameObject);
        }
    }

    public void SpawnAttackObject()
    {
        var attackObject = Instantiate(_attackObject, transform);
        attackObject.Init(this);
    }

    public void Move(Vector3 direction)
    {
        transform.position += direction * pawnState.Speed * Time.deltaTime;
    }

    public void FaceTo(Vector3 position)
    {
        transform.LookAt(position);
    }

}
