using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPawn : Pawn
{
    [SerializeField] private Animator animator;

    public override void SpawnAttackObject()
    {
        base.SpawnAttackObject();
        animator.Play("Attack", 0, 0);
    }

    public override void TakeDamage(float damageValue)
    {
        base.TakeDamage(damageValue);
        animator.Play("TakeDamage");
    }
}
