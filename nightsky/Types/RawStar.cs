using Raylib_cs;

namespace nightsky.Types;

public struct RawStar
{
    public static Dictionary<string, Color> SpectralRGB = new Dictionary<string, Color>
    {
        //new ones
        ["C0"] = new Color(255, 204, 153),
        ["C1"] = new Color(255, 199, 143),
        ["C2"] = new Color(255, 196, 137),
        ["C3"] = new Color(255, 190, 126),
        ["C4"] = new Color(255, 187, 120),
        ["C5"] = new Color(255, 180, 107),
        ["C6"] = new Color(255, 177, 101),
        ["S0"] = new Color(255, 209, 163),
        ["S1"] = new Color(255, 204, 153),
        ["S2"] = new Color(255, 201, 148),
        ["S3"] = new Color(255, 196, 137),
        ["S4"] = new Color(255, 193, 132),
        ["S5"] = new Color(255, 187, 120),
        ["S6"] = new Color(255, 184, 114),
        ["S7"] = new Color(255, 177, 101),

        
        //odd ones
        ["Ap"] = new Color(210, 223, 255),
        ["Am"] = new Color(215, 226, 255),
        ["Bp"] = new Color(174, 201, 255),
        ["Bm"] = new Color(181, 205, 255),
        ["Be"] = new Color(171, 198, 255),

        //to be removed
        // ["GI"] = new Color(0,0,0),
        // ["WN"] = new Color(0,0,0),
        // ["WC"] = new Color(0,0,0),
        // ["N0"] = new Color(0,0,0),
        // ["pe"] = new Color(0,0,0),
        // ["A/"] = new Color(0,0,0),
        // ["Fm"] = new Color(0,0,0),
        // ["K+"] = new Color(0,0,0),
        // ["KI"] = new Color(0,0,0),
        //default
        ["O0"] = new Color(147, 182, 255),
        ["O1"] = new Color(148, 183, 255),
        ["O2"] = new Color(149, 183, 255),
        ["O3"] = new Color(150, 184, 255),
        ["O4"] = new Color(151, 185, 255),
        ["O5"] = new Color(152, 186, 255),
        ["O6"] = new Color(154, 187, 255),
        ["O7"] = new Color(155, 188, 255),
        ["O8"] = new Color(157, 189, 255),
        ["O9"] = new Color(159, 190, 255),
        ["B0"] = new Color(159, 190, 255),
        ["B1"] = new Color(161, 192, 255),
        ["B2"] = new Color(163, 193, 255),
        ["B3"] = new Color(166, 195, 255),
        ["B4"] = new Color(169, 197, 255),
        ["B5"] = new Color(173, 200, 255),
        ["B6"] = new Color(177, 203, 255),
        ["B7"] = new Color(183, 206, 255),
        ["B8"] = new Color(190, 211, 255),
        ["B9"] = new Color(202, 218, 255),
        ["A0"] = new Color(202, 218, 255),
        ["A1"] = new Color(204, 219, 255),
        ["A2"] = new Color(206, 221, 255),
        ["A3"] = new Color(208, 222, 255),
        ["A4"] = new Color(211, 223, 255),
        ["A5"] = new Color(213, 225, 255),
        ["A6"] = new Color(217, 227, 255),
        ["A7"] = new Color(220, 229, 255),
        ["A8"] = new Color(225, 232, 255),
        ["A9"] = new Color(230, 235, 255),
        ["F0"] = new Color(230, 235, 255),
        ["F1"] = new Color(233, 237, 255),
        ["F2"] = new Color(238, 239, 255),
        ["F3"] = new Color(243, 242, 255),
        ["F4"] = new Color(249, 245, 255),
        ["F5"] = new Color(255, 250, 255),
        ["F6"] = new Color(255, 254, 250),
        ["F7"] = new Color(255, 252, 246),
        ["F8"] = new Color(255, 249, 241),
        ["F9"] = new Color(255, 246, 237),
        ["G0"] = new Color(255, 246, 237),
        ["G1"] = new Color(255, 245, 234),
        ["G2"] = new Color(255, 243, 232),
        ["G3"] = new Color(255, 242, 229),
        ["G4"] = new Color(255, 240, 227),
        ["G5"] = new Color(255, 238, 224),
        ["G6"] = new Color(255, 237, 221),
        ["G7"] = new Color(255, 235, 218),
        ["G8"] = new Color(255, 234, 216),
        ["G9"] = new Color(255, 232, 213),
        ["K0"] = new Color(255, 232, 213),
        ["K1"] = new Color(255, 229, 207),
        ["K2"] = new Color(255, 225, 201),
        ["K3"] = new Color(255, 222, 195),
        ["K4"] = new Color(255, 218, 189),
        ["K5"] = new Color(255, 215, 182),
        ["K6"] = new Color(255, 211, 175),
        ["K7"] = new Color(255, 207, 168),
        ["K8"] = new Color(255, 202, 160),
        ["K9"] = new Color(255, 198, 151),
        ["M0"] = new Color(255, 198, 151),
        ["M1"] = new Color(255, 194, 144),
        ["M2"] = new Color(255, 190, 136),
        ["M3"] = new Color(255, 186, 127),
        ["M4"] = new Color(255, 181, 118),
        ["M5"] = new Color(255, 176, 108),
        ["M6"] = new Color(255, 172, 98),
        ["M7"] = new Color(255, 166, 86),
        ["M8"] = new Color(255, 161, 74),
        ["M9"] = new Color(255, 155, 61),
    };
    
    public RawStar(double RA, double dec, char Spectral1, char Spectral2, float mag, float dRa, float dDec)
    {
        this.RA = RA;
        this.dec = dec;
        this.Spectral1 = Spectral1;
        this.Spectral2 = Spectral2;
        this.mag = mag;
        this.dRA = dRa;
        this.dDec = dDec;
    }
    public double RA;
    public double dec;
    public string spectralType;
    public char Spectral1;
    public char Spectral2;
    public Color FinalColor;
    public float mag;
    public float dRA;
    public float dDec;
}