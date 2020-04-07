using Unity.Entities;
using Unity.Mathematics;

namespace UnitComponents
{
    public struct IdComponent : IComponentData
    {
        public ushort Value;
    }
    public struct PositionComponent : IComponentData
    {
        public int2 position;
        public int2 prevPosition;
        public bool inMotion;
    }

    public struct MovementComponent : IComponentData {
        public int2 target;
        public float moveRate;
    }
    
}
