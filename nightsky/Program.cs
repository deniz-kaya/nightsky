using System.Numerics;
using nightsky.Types;

namespace nightsky;
using Raylib_cs;
using rlImGui_cs;
using ImGuiNET;



class Program
{
    static void Main(string[] args)
    {
        string filepath = "C:\\Users\\blind\\RiderProjects\\nightsky\\nightsky\\Data\\BSC5";
        Star[] stars = StarParser.GetStarData(filepath);
        Console.Write("complete!");
        bak:
        Console.WriteLine("input index for the mfn star thing information");
        int i = int.Parse(Console.ReadLine());
        Console.WriteLine($"no: {i}");
        Console.WriteLine($"RA: {stars[i].RA}");
        Console.WriteLine($"dec: {stars[i].dec}");
        Console.WriteLine($"spec: {stars[i].color}");
        Console.WriteLine($"brightness: {stars[i].brightness}");
        Console.WriteLine($"dRA: {stars[i].dRA}");
        Console.WriteLine($"dDec: {stars[i].dDec}");
        goto bak;
    }
    static void zMain(string[] args)
    {
        const int screenWidth = 800;
        const int screenHeight = 600;
        const float DEG2RAD = 0.0174533f;
        Raylib.InitWindow(screenWidth, screenHeight, "Raylib-CS 3D Camera with WASD + Arrows");
        Raylib.SetTargetFPS(60);

        // Camera settings
        Camera3D camera = new Camera3D();
        camera.Position = new Vector3(0.0f, 2.0f, 4.0f);
        camera.Target = new Vector3(0.0f, 2.0f, 3.0f);
        camera.Up = new Vector3(0.0f, 1.0f, 0.0f);
        camera.FovY = 45.0f;
        camera.Projection = CameraProjection.Perspective;

        float yaw = 0.0f;
        float pitch = 0.0f;

        while (!Raylib.WindowShouldClose())
        {
            // ========== Input ==========
            float moveSpeed = 0.1f;
            float rotSpeed = 1.0f;

            // Arrow keys for pitch/yaw
            if (Raylib.IsKeyDown(KeyboardKey.Right)) yaw += rotSpeed;
            if (Raylib.IsKeyDown(KeyboardKey.Left)) yaw -= rotSpeed;
            if (Raylib.IsKeyDown(KeyboardKey.Up)) pitch += rotSpeed;
            if (Raylib.IsKeyDown(KeyboardKey.Down)) pitch -= rotSpeed;

            // Clamp pitch
            pitch = Math.Clamp(pitch, -89.0f, 89.0f);

            // Convert to radians
            float yawRad = DEG2RAD * yaw;
            float pitchRad = DEG2RAD * pitch;

            // Direction vectors
            Vector3 forward = new Vector3(
                (float)(Math.Cos(pitchRad) * Math.Sin(yawRad)),
                (float)(Math.Sin(pitchRad)),
                (float)(Math.Cos(pitchRad) * Math.Cos(yawRad))
            );

            Vector3 right = new Vector3(
                (float)Math.Sin(yawRad - MathF.PI / 2),
                0.0f,
                (float)Math.Cos(yawRad - MathF.PI / 2)
            );

            // WASD movement
            if (Raylib.IsKeyDown(KeyboardKey.W)) camera.Position += forward * moveSpeed;
            if (Raylib.IsKeyDown(KeyboardKey.S)) camera.Position -= forward * moveSpeed;
            if (Raylib.IsKeyDown(KeyboardKey.A)) camera.Position -= right * moveSpeed;
            if (Raylib.IsKeyDown(KeyboardKey.D)) camera.Position += right * moveSpeed;

            // Update camera target
            camera.Target = camera.Position + forward;

            // ========== Drawing ==========
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RayWhite);

            Raylib.BeginMode3D(camera);
            Raylib.DrawGrid(20, 1.0f);
            Raylib.DrawCube(new Vector3(0, 1, 0), 1, 1, 1, Color.Red);
            Raylib.EndMode3D();

            Raylib.DrawText("WASD = Move | Arrow Keys = Look", 10, 10, 20, Color.DarkGray);
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
    static void aMain(string[] args)
    {
        // before your game loop
        
        Raylib.InitWindow(1600, 900, "Night Sky");
        Raylib.SetExitKey(KeyboardKey.Null);
        rlImGui.Setup(true);
        Raylib.SetTargetFPS(60);
        Camera3D camera = new Camera3D();
        camera.Projection = CameraProjection.Perspective;
        
        
        //game loop begin
        
        Raylib.BeginDrawing();
        

        //imgui windows
        rlImGui.Begin();			

        rlImGui.End();
        //imgui windows
        
        Raylib.EndDrawing();
        
        //------
        //game loop end

        rlImGui.Shutdown();
    }
}