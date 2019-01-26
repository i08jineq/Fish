using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PawnState
{
    public float Hp;

    public float Speed;
    public float TurnSpeed = 5;
    public float ChargeSpeed;
    public float ChargeTurnSpeed = 5;
    public float Range;

    public float Damage;
}
