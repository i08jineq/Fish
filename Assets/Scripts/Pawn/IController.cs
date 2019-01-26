using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController
{
    void Init(Pawn pawn);
    void OnUpdate(float deltaTime);
}
