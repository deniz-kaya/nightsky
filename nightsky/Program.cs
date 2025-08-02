using System.Numerics;
using System.Xml.Schema;
using Microsoft.VisualBasic;
using nightsky.Types;

namespace nightsky;
using Raylib_cs;
using rlImGui_cs;
using ImGuiNET;



class Program
{
    static void Main(string[] args)
    {
        const int screenWidth = 800;
        const int screenHeight = 600;
        const float DEG2RAD = 0.0174533f;
        Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);
        Raylib.InitWindow(screenWidth, screenHeight, "Raylib-CS 3D Camera with WASD + Arrows");
        rlImGui.Setup(true);
        Raylib.SetTargetFPS(60);

        float yearsSinceJ2000 = 0f;
        float extraBrightness = 0f; 
        float dYdT = 0;
        float dist = 60f;
        float scale = 1.3f;
        const int texSize = 128;

        Image starImage = Raylib.GenImageGradientRadial(texSize, texSize, 0.5f, Color.White, Color.Blank);
        Texture2D star = Raylib.LoadTextureFromImage(starImage);
        Raylib.UnloadImage(starImage);
        
        Mesh starMesh = Raylib.GenMeshSphere(1f, 16, 16); // low resolution sphere
        Model starModel = Raylib.LoadModelFromMesh(starMesh);
        
        // Camera settings
        Camera3D camera = new Camera3D();
        camera.Position = new Vector3(0.0f, 2.0f, 4.0f);
        camera.Target = new Vector3(0.0f, 2.0f, 3.0f);
        camera.Up = new Vector3(0.0f, 1.0f, 0.0f);
        camera.FovY = 45.0f;
        camera.Projection = CameraProjection.Perspective;

        float yaw = 0.0f;
        float pitch = 0.0f;
        string filepath = "C:\\Users\\blind\\RiderProjects\\nightsky\\nightsky\\Data\\BSC5";
        Star[] stars = StarParser.GetStarData(filepath);
        while (!Raylib.WindowShouldClose())
        {
            // ========== Input ==========
            float moveSpeed = 0.1f;
            float rotSpeed = 1.0f;

            // Arrow keys for pitch/yaw
            if (Raylib.IsKeyDown(KeyboardKey.Right)) yaw -= rotSpeed;
            if (Raylib.IsKeyDown(KeyboardKey.Left)) yaw += rotSpeed;
            if (Raylib.IsKeyDown(KeyboardKey.Up)) pitch += rotSpeed;
            if (Raylib.IsKeyDown(KeyboardKey.Down)) pitch -= rotSpeed;

            // Clamp pitch
            pitch = Math.Clamp(pitch, -89.0f, 89.0f);

            // Convert to radians
            float yawRad = DEG2RAD * yaw;
            float pitchRad = DEG2RAD * pitch;

            // Direction vectors
            Vector3 forwardMovement = new Vector3(
                (float)(Math.Cos(pitchRad) * Math.Sin(yawRad)),
                (float)(Math.Sin(pitchRad)),
                (float)(Math.Cos(pitchRad) * Math.Cos(yawRad))
            );

            Vector3 rightMovement = new Vector3(
                (float)Math.Sin(yawRad - MathF.PI / 2),
                0.0f,
                (float)Math.Cos(yawRad - MathF.PI / 2)
            );

            // WASD movement
            if (Raylib.IsKeyDown(KeyboardKey.W)) camera.Position += forwardMovement * moveSpeed;
            if (Raylib.IsKeyDown(KeyboardKey.S)) camera.Position -= forwardMovement * moveSpeed;
            if (Raylib.IsKeyDown(KeyboardKey.A)) camera.Position -= rightMovement * moveSpeed;
            if (Raylib.IsKeyDown(KeyboardKey.D)) camera.Position += rightMovement * moveSpeed;

            // Update camera target
            camera.Target = camera.Position + forwardMovement;
            
            //Update current date
            yearsSinceJ2000 += dYdT * Raylib.GetFrameTime();
            // ========== Drawing ==========
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            Raylib.BeginMode3D(camera);
            
            Raylib.DrawSphere(new Vector3(0, 0, 0), 3, Color.DarkGreen);
            Raylib.DrawSphereWires(new Vector3(0, 0, 0), 3, 16,16,Color.Black);
            
            foreach (Star s in stars)
            {
                Vector3 unitPos = s.ConstructPositionVector(yearsSinceJ2000);
                Vector3 starPos = unitPos * dist;
                if (Vector3.Dot(starPos - camera.Position, unitPos) > 0.1f)
                {
                     Vector3 forward = -unitPos;
                     Vector3 right = Vector3.Cross(forward, Vector3.UnitY);
                     Vector3 up = Vector3.Cross(right, forward);
                     Vector2 size = new Vector2(StarParser.MagnitudeToBrightness(s.mag) * scale);
                     Rectangle rect = new Rectangle(0,0 , texSize, texSize);
                     Vector2 origin = Vector2.Divide(size, 2f);
                     Color c = Raylib.ColorAlpha(s.color, StarParser.BrightnessAlpha(s.brightness) + extraBrightness);
                     Raylib.DrawBillboardPro(camera, star, rect, starPos, up, size, origin, 0f, c);
                     // Raylib.DrawModelEx(starModel, starPos, Vector3.UnitY, 0f,
                     //    new Vector3(s.brightness * scale), s.color);
                }
            }
            
            Raylib.EndMode3D();
            
            rlImGui.Begin();

            ImGui.Begin("Settings");
            
            ImGui.Text($"FPS: {1f/(float)Raylib.GetFrameTime()}");
            ImGui.SliderFloat("distance", ref dist, 10f, 400f);
            ImGui.SliderFloat("scale", ref scale, 1f, 10f);
            ImGui.SliderFloat("relative brightness factor", ref StarParser.visibility, 0.001f, 1f);
            ImGui.SliderFloat("relative size factor", ref StarParser.lowerBound, 0.03f, 0.99f);
            ImGui.SliderFloat("gamma", ref extraBrightness, 0.0f, 0.99f);
            ImGui.SeparatorText("Years");
            ImGui.Text($"Current year: {2000f + yearsSinceJ2000}");
            ImGui.SliderFloat("Years per second", ref dYdT, -10000f, 10000f);
            
            ImGui.End();
            
            rlImGui.End();
            
            Raylib.EndDrawing();
        }
        rlImGui.Shutdown();
        
        Raylib.CloseWindow();
    }
}