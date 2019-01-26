using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackObjectData
{
    [System.NonSerialized]
    public Vector3 startPosition;

    [System.NonSerialized]
    public Vector3 startForward;

    public float damage;

    public float speed;

    public float destroyTime;

    [System.NonSerialized]
    public float lifeTime;
}
