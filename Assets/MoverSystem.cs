using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class MoverSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation translation, ref MoveSpeedComponent moveSpeedComponent) =>
        {
            translation.Value.x += moveSpeedComponent.speedX * Time.DeltaTime;
            translation.Value.y += moveSpeedComponent.speedY * Time.DeltaTime;
            if (translation.Value.x > 8f)
            {
                moveSpeedComponent.speedX = -math.abs(moveSpeedComponent.speedX);
            }
            if (translation.Value.x < -8f)
            {
                moveSpeedComponent.speedX = +math.abs(moveSpeedComponent.speedX);
            }
            if (translation.Value.y > 5f)
            {
                moveSpeedComponent.speedY = -math.abs(moveSpeedComponent.speedY);
            }
            if (translation.Value.y < -5f)
            {
                moveSpeedComponent.speedY = +math.abs(moveSpeedComponent.speedY);
            }
        });
    }
}
