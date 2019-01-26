using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PawnState
{
    public float Hp;

    public float Speed;

    public float Range;

    [System.NonSerialized] public float attackCountTime = 0;

    public float attackInterval = 0.5f;
}
