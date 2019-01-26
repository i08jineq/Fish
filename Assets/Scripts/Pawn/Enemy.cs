using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Pawn
{
    [SerializeField] AttackObject localAttackObject;
    public override void Init()
    {
        base.Init();
        localAttackObject.Init(this);
    }
}
