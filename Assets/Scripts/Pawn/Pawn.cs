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
    public Vector3 hpbarOffset = new Vector3(0, 1, -1);
    private const float positionMinZ = -7;
    private const float positionMaxZ = 10;
    private const float positionXRange = 20;
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
        Vector3 pos = transform.position + direction * pawnState.Speed * Time.deltaTime;
        pos.z = Mathf.Clamp(pos.z, positionMinZ, positionMaxZ);
        pos.x = Mathf.Clamp(pos.x, -positionXRange, positionXRange);
        transform.position = pos;
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
