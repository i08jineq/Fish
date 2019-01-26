using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObjectManager : MonoBehaviour
{
    List<AttackObject> spawnedAttackObjects = new List<AttackObject>();

    public void OnUpdate(float deltaTime)
    {
        foreach(var attackObject in spawnedAttackObjects)
        {
            
        }
    }
}
