using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [SerializeField]
    AttackObject _attackObject = null;
    public int score = 1;

    public PawnState pawnState;
    public IController controller;

    public virtual void Init()
    {

    }

    public virtual void OnUpdate(float deltaTime)
    {
        controller.OnUpdate(deltaTime);
    }

    public virtual void TakeDamage(float damageValue)
    {
        pawnState.Hp -= damageValue;

        Singleton.instance.gameEvent.takeDamageEvent.Invoke(this, damageValue);
        Singleton.instance.gameEvent.onPawnStateChange.Invoke(this);
        if (pawnState.Hp <= 0)
        {
            Dead();
        }
    }

    public void SpawnAttackObject()
    {
        var attackObject = Instantiate(_attackObject, transform.position, Quaternion.identity);
        attackObject.Init(this);
        Singleton.instance.gameEvent.onSpawnedAttackObject.Invoke(attackObject);
    }

    public virtual void Move(Vector3 direction)
    {
        transform.position += direction * pawnState.Speed * Time.deltaTime;
    }

    public void TurnTo(Vector3 direction, float speed)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), speed * Time.deltaTime);
    }

    public void MoveForward(float speed)
    {
        transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
    }

    public void FaceTo(Vector3 position)
    {
        transform.LookAt(position);
    }

    protected virtual void Dead()
    {
        Singleton.instance.gameEvent.deadEvent.Invoke(this);
        Destroy(gameObject);
    }

}
