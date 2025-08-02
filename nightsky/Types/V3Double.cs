using System.Numerics;

namespace nightsky.Types;

public struct V3Double
{
    public double x;
    public double y;
    public double z;

    public static Vector3 ToVector3(V3Double vector)
    {
        return new Vector3((float)vector.x, (float)vector.y, (float)vector.z);
    }

    public override string ToString()
    {
        return $"({x}, {y}, {z})";
    }
    public V3Double(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}