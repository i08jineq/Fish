using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SpawnData
{
    public Pawn Pawn;
    public Vector3 spawnPosition;
    [Range(1, 50)]public float spawnTime;
}
