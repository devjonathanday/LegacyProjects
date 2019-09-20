using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;

namespace Platformer
{
    public class Sector
    {
        public Rectangle[] colliders;

        public Sector(string dataFilePath) { LoadSectorData(dataFilePath); }

        public void LoadSectorData(string filePath)
        {
            //Initialize a new list of colliders (Rectangle)
            List<Rectangle> newColliders = new List<Rectangle>();
            //Open the file and read each line
            string[] lines = File.ReadAllLines(filePath);

            for(int i = 0; i < lines.Length; i++)
            {
                switch(lines[i][0])
                {
                    case 'c': //Indicates a collider
                        string[] values = lines[i].Split(',');
                        newColliders.Add(new Rectangle(Convert.ToInt32(values[1]),
                                                       Convert.ToInt32(values[2]),
                                                       Convert.ToInt32(values[3]),
                                                       Convert.ToInt32(values[4])));
                        break;
                }
            }

            colliders = newColliders.ToArray();
        }
    }

    public static class DebugOptions
    {
        public static void DrawSectorColliders(Sector sector, Vector2 cameraOffset, Vector2 screenOffset)
        {
            for(int i = 0; i < sector.colliders.Length; i++)
            {
                DrawRectangle((int)screenOffset.x + ((int)sector.colliders[i].x - (int)cameraOffset.x),
                              (int)screenOffset.y + ((int)sector.colliders[i].y - (int)cameraOffset.y),
                              (int)sector.colliders[i].width, (int)sector.colliders[i].height, Color.GRAY);
            }
        }
    }
}
