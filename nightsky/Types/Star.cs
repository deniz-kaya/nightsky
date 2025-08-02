using System.Numerics;
using Raylib_cs;

namespace nightsky.Types;

public struct Star
{
    public Vector3 ConstructPositionVector(float yearsSinceJ2000 = 0f)
    {
        double correctedRA = (RA + (dRA * yearsSinceJ2000))/ Math.Cos(dec);
        double correctedDec = dec + (dDec * yearsSinceJ2000);
        
        return new Vector3(
            (float)(Math.Cos(correctedDec) * Math.Cos(correctedRA)),
            (float)(Math.Cos(correctedDec) * Math.Sin(correctedRA)),
            (float)(Math.Sin(correctedDec))
        );
        
    }
    public Vector3 position;
    public double RA;
    public double dec;
    public Color color;
    public float mag;
    public float brightness;
    public float dRA;
    public float dDec;
}