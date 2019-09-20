using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;

namespace Platformer
{
    class Program
    {
        public static void Main()
        {
            #region Window Initialization

            Vector2 screenSize = new Vector2(256, 224);
            SetTargetFPS(60);

            InitWindow((int)screenSize.x, (int)screenSize.y, "Platformer");

            #endregion

            #region General Initialization

            Player player = new Player(new Vector2(200, 20));

            Camera camera = new Camera(0.25f);
            camera.pos = player.pos;
            Vector2 screenOffset = screenSize / 2;

            #endregion

            #region Level Initialization

            Sector testLevel = new Sector("data/level1.txt");
            
            #endregion

            while (!WindowShouldClose())
            {
                //Greater state machine for game state

                #region Update

                player.Update(testLevel.colliders);
                camera.Update(player.pos);

                #endregion

                #region Draw

                //Initialization

                BeginDrawing();
                ClearBackground(Color.BLACK);
                screenOffset = screenSize / 2;

                //Draw background color or gradient
                DrawRectangleGradientV(0, 0, (int)screenSize.x, (int)screenSize.y, Color.DARKBLUE, Color.BLACK);

                //Draw background elements, farthest to closest from camera

                //Draw foreground elements

                DebugOptions.DrawSectorColliders(testLevel, camera.pos, screenOffset);
                player.Draw(camera.pos - (player.size / 2), screenOffset);
                //player.DrawColliders();

                //Draw particle effects and other screen space effects

                //Draw UI elements

                //Termination

                EndDrawing();

                #endregion
            }

            CloseWindow();
        }
    }
}
