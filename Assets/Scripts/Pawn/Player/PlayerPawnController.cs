using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPawnController : IController
{

    Pawn targetPawn;
    //向き
    float direction = 0;

    Plane plane = new Plane();

    public void Init(Pawn pawn)
    {
        targetPawn = pawn;
    }

    public void OnUpdate(float deltaTime)
    {
        //これあとで消す
        float dz = Input.GetAxis("Vertical");
        float dx = Input.GetAxis("Horizontal");

        targetPawn.Move(new Vector3(dx, 0.0f, dz));

        //レイ発生
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // プレイヤーの高さにPlaneを更新して、カメラの情報を元に地面判定して距離を取得
        plane.SetNormalAndPosition(Vector3.up, targetPawn.transform.localPosition);

        if (plane.Raycast(ray, out direction))
        {

            // 距離を元に交点を算出して、交点の方を向く
            var lookPoint = ray.GetPoint(direction);
            targetPawn.FaceTo(lookPoint);
        }
        if (Input.GetMouseButton(0))
        {
            //攻撃
            targetPawn.SpawnAttackObject();
        }

    }
}
