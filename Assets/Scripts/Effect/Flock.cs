using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    [SerializeField] private float minMoveSpeed = 1f;
    [SerializeField] private float maxMoveSpeed = 3f;
    [SerializeField] private float minRotationSpeed = 8f;
    [SerializeField] private float maxRotationSpeed = 15f;
    [System.NonSerialized]public float moveSpeed = 0;
    [System.NonSerialized]public float rotationSpeed = 10;

    public void Init()
    {
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
    }
}
