using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDataComponent : MonoBehaviour
{

    [System.NonSerialized] public float attackCountTime = 0;
    [System.NonSerialized] public float attackInterval = 0.5f;
    [System.NonSerialized] public float chargeTimeCount = 0;
    [System.NonSerialized] public float chargePeriod = 2;
    [System.NonSerialized] public bool isCharging = false;
    [SerializeField] protected float minChargePeriod = 2;
    [SerializeField] protected float maxChargePeriod = 3;
    [SerializeField] protected float minAttackInterval = 5;
    [SerializeField] protected float maxAttackInterval = 8;
    public void Init()
    {
        attackInterval = Random.Range(minAttackInterval, maxAttackInterval);
        chargePeriod = Random.Range(minChargePeriod, maxChargePeriod);
    }
}
