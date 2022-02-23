using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Rendering;
using Unity.Mathematics;

public class Testing : MonoBehaviour
{
    [SerializeField] private Mesh _mesh;
    [SerializeField] private Material _material;


    private void Start()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        EntityArchetype entityArchetype = entityManager.CreateArchetype(
            typeof(MoveSpeedComponent),
            typeof(LevelComponent),
            typeof(Translation),
            typeof(RenderMesh),
            typeof(LocalToWorld),
            typeof(RenderBounds)
            );

        NativeArray<Entity> entityArray = new NativeArray<Entity>(1000, Allocator.Temp);
        entityManager.CreateEntity(entityArchetype, entityArray);

        foreach (Entity entity in entityArray)
        {
            int signX = 1;
            int signY = 1;

            if (UnityEngine.Random.Range(0, 2) == 0)
                signX = -1;
            if (UnityEngine.Random.Range(0, 2) == 0)
                signY = -1;

            entityManager.SetComponentData(entity, new MoveSpeedComponent { speedX = signX * UnityEngine.Random.Range(1.0f, 2.0f), speedY = signY * UnityEngine.Random.Range(1.0f, 2.0f), });
            
            entityManager.SetComponentData(entity, new LevelComponent { level = UnityEngine.Random.Range(0, 9999) });
            entityManager.SetComponentData(entity, new Translation { Value = new float3(UnityEngine.Random.Range(-8f, 8f), UnityEngine.Random.Range(-5f, 5f), 0)});
            entityManager.SetSharedComponentData(entity, new RenderMesh 
            {
                mesh = _mesh,
                material = _material,
            });
        }

        entityArray.Dispose();
    }
}
