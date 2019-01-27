using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Pawn
{
    [SerializeField] AttackObject localAttackObject;
    public Renderer meshRenderer;
    [SerializeField] private float resetEmmitionValue = 0;
    [SerializeField] private float emmitionValue = 1;
    [SerializeField] private float takeDamageEmmitPeriod = 0.5f;
    private bool isEmmitting = false;
    private float emmitTImeCount = 0f;

    public override void Init()
    {
        base.Init();
        localAttackObject.Init(this);
    }

    public override void TakeDamage(float damageValue)
    {
        base.TakeDamage(damageValue);
        emmitTImeCount = 0;
        if (isEmmitting)
        {
            return;
        }
        StartCoroutine(DamageEmitEnumerator());


    }

    private IEnumerator DamageEmitEnumerator()
    {
        isEmmitting = true;
        meshRenderer.material.SetColor("_EmissionColor", Color.red * emmitionValue);
        while (emmitTImeCount < takeDamageEmmitPeriod)
        {
            emmitTImeCount += Time.deltaTime;
            yield return null;

        }
        meshRenderer.material.SetColor("_EmissionColor", Color.white * resetEmmitionValue);
        isEmmitting = false;
    }
}
