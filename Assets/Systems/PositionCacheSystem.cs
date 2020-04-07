using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using static Unity.Mathematics.math;
using NT = NativeQuadTree;

using UnitComponents;

public class PositionCacheSystem : ComponentSystem
{
    public NT.NativeQuadTree<ushort> botLocationQuadTree;
    //private NativeList<NT.QuadElement<ushort>> botLocations;

    protected override void OnStartRunning() {
        InitAll();
    }

    protected override void OnStopRunning() {
        DisposeAll();
    }

    void InitAll() {
        botLocationQuadTree = new NT.NativeQuadTree<ushort>(new NT.AABB2D(new float2(0,0), new float2(0xFFFF, 0xFFFF)), Allocator.Persistent);
        //botLocations = new NativeList<NT.QuadElement<ushort>>(Allocator.Persistent);
    }

    void DisposeAll() {
        botLocationQuadTree.Dispose();
        //botLocations.Dispose();
    }

    [BurstCompile]
    protected override void OnUpdate()
    {
        // DisposeAll();
        // InitAll();
        // If I use Allocator.Persistent and then dispose and reinitialise every frame, it seems to
        // be fine. If I do the same with Allocator.TempJob, the editor crashes, and if I keep it Persistent 
        // but just Clear() every frame, the editor crashes. I've left the crashy version uncommented with 
        // Persistent allocator
        botLocationQuadTree.Clear();

        //botLocations.Clear();
        var botLocations = new NativeList<NT.QuadElement<ushort>>(Allocator.Temp); 
        
        Entities.ForEach((ref IdComponent id, ref PositionComponent pos) => {
            botLocations.Add(new NT.QuadElement<ushort>() {
                pos = (float2) pos.position,
                element = id.Value
            });
        });

        botLocationQuadTree.ClearAndBulkInsert(botLocations);
        // If allocating botLocations TempJob, dispose here
        //botLocations.Dispose();
    }
}