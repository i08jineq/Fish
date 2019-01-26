using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObjectManager
{
    List<AttackObject> _spawnedAttackObjects = new List<AttackObject>();
    int _attackObjectCount = 0;

    public void Init()
    {
        Singleton.instance.gameEvent.onSpawnedAttackObject.AddListener(_onSpawnAttackObject);
        Singleton.instance.gameEvent.onAttackObjectDestroyed.AddListener(_onDeadAttackObject);
    }

    public void OnUpdate(float deltaTime)
    {
        for(int i = 0; i < _attackObjectCount; i++)
        {
            _spawnedAttackObjects[i].OnUpdate(deltaTime);
        }
    }

    void _onSpawnAttackObject(AttackObject attackObject)
    {
        _spawnedAttackObjects.Add(attackObject);
        _attackObjectCount = _spawnedAttackObjects.Count;
    }

    void _onDeadAttackObject(AttackObject attackObject)
    {
        _spawnedAttackObjects.Remove(attackObject);
        _attackObjectCount = _spawnedAttackObjects.Count;
    }

    public void DestroyAllAttackObjects()
    {
        foreach(var attackObject in _spawnedAttackObjects)
        {
            Object.Destroy(attackObject.gameObject);
        }
        _spawnedAttackObjects.Clear();
    }
}
