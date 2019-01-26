using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class FlockGroup
{
    [SerializeField] private Flock flockPrefab;
    [SerializeField] private float offestRange = 0.5f;
    [SerializeField] private int flockNumbers = 10;
    [SerializeField] private float updateOffsetPeriod = 3;
    [SerializeField]private float flockGroupSize = 1f;

    [System.NonSerialized] public Vector3 flockCenterOffset;
    private Vector3 flockGoal = Vector3.zero;
    private float updateOffsetTimeCount = 0;

    private List<Flock> flocks = new List<Flock>();

    public void Init(Vector3 center)
    {
        flockGoal = center;
        Vector3 pos = Vector3.zero;
        for (int i = 0; i < flockNumbers; i++)
        {
            pos.x = Random.Range(-flockGroupSize, flockGroupSize);
            pos.y = Random.Range(-flockGroupSize, flockGroupSize);
            pos.z = Random.Range(-flockGroupSize, flockGroupSize);
            Flock flock = GameObject.Instantiate<Flock>(flockPrefab);
            flock.Init();
            flock.transform.position = pos;
            flocks.Add(flock);
        }
    }

    public void UpdatePosition(Vector3 center)
    {
        flockGoal = center + flockCenterOffset;
    }

    public void OnUpdate(float deltaTime)
    {
        updateOffsetTimeCount += deltaTime;
        if(updateOffsetTimeCount > updateOffsetPeriod)
        {
            updateOffsetTimeCount = 0;
            flockCenterOffset.x = Random.Range(-offestRange, offestRange);
            flockCenterOffset.y = Random.Range(-offestRange, offestRange);
            flockCenterOffset.z = Random.Range(-offestRange, offestRange);
        }
        Vector3 pos = Vector3.zero;
        Vector3 diff = Vector3.zero;
        for (int i = 0; i < flockNumbers; i++)
        {
            pos = flocks[i].transform.position;
            diff = (flockGoal - pos);
            float distance = diff.magnitude;
            if(distance > flockGroupSize)
            {
                flocks[i].transform.rotation = Quaternion.Slerp(flocks[i].transform.rotation, Quaternion.LookRotation(diff), flocks[i].rotationSpeed * deltaTime);
            }
            flocks[i].transform.Translate(new Vector3(0, 0, deltaTime * flocks[i].moveSpeed));
        }
    }
}
