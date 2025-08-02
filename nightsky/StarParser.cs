using System.Text;
using nightsky.Types;

namespace nightsky;

public class StarParser
{
    //-1.46f, float maxMag = 7.96f
    private static readonly float MagMin = -1.46f;
    private static readonly float MagMax = 7.96f;
    private static readonly float BrightnessMin = MathF.Pow(10f, -0.4f * MagMax); // ~0.000575
    private static readonly float BrightnessMax = MathF.Pow(10f, -0.4f * MagMin); // ~3.63
    public static float lowerBound = 0.2f;
    public static float visibility = 0.05f;
    public static float MagnitudeToBrightness(float magnitude)
    {
        float rawBrightness = MathF.Pow(10f, -0.4f * magnitude);
        float normalized = (rawBrightness - BrightnessMin) / (BrightnessMax - BrightnessMin);
        return lowerBound + (Math.Max(0, Math.Min(1, normalized)) * (1.0f - lowerBound));
    }

    public static float BrightnessAlpha(float brightness)
    {
        return visibility + (Math.Max(0, Math.Min(1, brightness)) * (1.0f - visibility));
    }
    
    public static Star[] GetStarData(string filepath)
    {
        Star[] stars = new Star[0];
        using (BinaryReader reader = new BinaryReader(File.Open(filepath, FileMode.Open), encoding: Encoding.UTF8))
        {
            int s0 = reader.ReadInt32();
            int s1 = reader.ReadInt32();
            int s2 = reader.ReadInt32();
            int snum = reader.ReadInt32();
            int mprop = reader.ReadInt32();
            int nmag = reader.ReadInt32();
            int nbent = reader.ReadInt32();
            
            stars = new Star[-s2];

            int iterCount = -s2;
            for (int i = 0; i < iterCount; i++)
            {
                reader.ReadBytes(4);
                stars[i].RA = reader.ReadDouble();
                stars[i].dec = reader.ReadDouble();
                string spectral = new string(reader.ReadChars(2));
                int magInt = reader.ReadInt16();
                stars[i].mag = magInt / 100f;
                stars[i].dRA = reader.ReadSingle();
                stars[i].dDec = reader.ReadSingle();

                if (!RawStar.SpectralRGB.ContainsKey(spectral)||(stars[i].RA == stars[i].dec && stars[i].dRA == stars[i].dDec && stars[i].RA == 0 &&
                                                               stars[i].dec == stars[i].dDec)) 
                {
                    i--;
                    iterCount--; 
                }
                else 
                {
                    stars[i].color = RawStar.SpectralRGB[spectral];
                    stars[i].brightness = MagnitudeToBrightness(magInt / 100f);
                    stars[i].position = stars[i].ConstructPositionVector();
                }
            }
        Console.WriteLine($"number of stars dropped: {(-s2) - iterCount}");
        }
        return stars;
    }
}

/*
stars[i] = new Star(
                    reader.ReadDouble(),
                    reader.ReadDouble(),
                    reader.ReadChar(), 
                    reader.ReadChar(),
                    reader.ReadInt16()/100f, 
                    reader.ReadSingle(), 
                    reader.ReadSingle() );
                /*
                
                
                /*
                 *double RA = reader.ReadDouble();
                double dec = reader.ReadDouble();
                char spectral1 = reader.ReadChar();
                char spectral2 = reader.ReadChar();
                float mag = reader.ReadSingle()/100f;
                float dRA = reader.ReadSingle();
                float dDec = reader.ReadSingle();
                 * 
                 * /
            }
            // Console.WriteLine(s0);
            // Console.WriteLine(s1);
            // Console.WriteLine(s2);
            // Console.WriteLine(snum);
            // Console.WriteLine(mprop);
            // Console.WriteLine(nmag);
            // Console.WriteLine(nbent);
            // Console.ReadKey();
            
            // else if ()
                // {
                //     stars[i].brightness = MagnitudeToBrightness(magInt / 100f);
                // Console.WriteLine($"no: {i}");
                // Console.WriteLine(spectral);
                // Console.WriteLine($"RA: {stars[i].RA}");
                // Console.WriteLine($"dec: {stars[i].dec}");
                // // Console.WriteLine($"SPEC: {spectral}");
                // Console.WriteLine($"brightness: {stars[i].brightness}");
                // Console.WriteLine($"dRA: {stars[i].dRA}");
                // Console.WriteLine($"dDec: {stars[i].dDec}");
                //     i--;
                //     iterCount--; 
                // }
*/