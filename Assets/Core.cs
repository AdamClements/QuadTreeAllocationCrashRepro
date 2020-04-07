using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using Unity.Mathematics;
using UnitComponents;
using UnityEngine;

public class Core : MonoBehaviour
{
    private EntityManager em;
    private ushort lastUsedId = 0;

    void Start()
    {
        em = World.DefaultGameObjectInjectionWorld.EntityManager;
        CreateEntity(0, 0, Color.red);
        CreateEntity(1, 0, Color.red);
        CreateEntity(6, 0, Color.black);
        CreateEntity(7, 0, Color.black);
        CreateEntity(15, 0, Color.black);
        CreateEntity(16, 0, Color.black);
        CreateEntity(60, 60, Color.black);
        
    }

    void CreateEntity(int x, int y, Color c) {
        var bot = em.CreateEntity(
            ComponentType.ReadOnly<LocalToWorld>(),
            ComponentType.ReadOnly<RenderBounds>()
        );
        em.AddComponentData(bot, new Translation
        {
            Value = new float3(x, y, 0)
        });

        //em.AddSharedComponentData(bot, SpriteMesh.MakeSlicedSpriteComponent("Sprites/t1sheet", c));

        em.AddComponentData(bot, new IdComponent {
            Value = lastUsedId++
        });
        
        em.AddComponentData(bot, new PositionComponent {
            position = new int2(x, y),
            prevPosition = new int2(x, y),
            inMotion = false
        });

        em.AddComponentData(bot, new MovementComponent {
            target = new int2(x, y),
            moveRate = 0.5f
        });

    }
}
