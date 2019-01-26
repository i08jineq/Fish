using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [SerializeField]
    AttackObject _attackObject = null;

    public PawnState pawnState;
    public IController controller;

    public HpBarController barController; 

    public void Init()
    {
        barController.UpdateHPBar(pawnState.Hp, pawnState.MaxHp);
    }

    public virtual void OnUpdate(float deltaTime)
    {
        controller.OnUpdate(deltaTime);
    }

    public void TakeDamage(float damageValue)
    {
        pawnState.Hp -= damageValue;
        barController.UpdateHPBar(pawnState.Hp, pawnState.MaxHp);
        Singleton.instance.gameEvent.takeDamageEvent.Invoke(this, damageValue);

        if (pawnState.Hp < 0)
        {
            Singleton.instance.gameEvent.deadEvent.Invoke(this);
            Destroy(gameObject);
        }
    }

    public void SpawnAttackObject()
    {
        var attackObject = Instantiate(_attackObject, transform.position, Quaternion.identity);
        attackObject.Init(this);
        Singleton.instance.gameEvent.onSpawnedAttackObject.Invoke(attackObject);
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
