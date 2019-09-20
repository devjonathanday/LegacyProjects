#include"raylib.h"
#include"Player.h"
#include"UIController.h"
#include"LevelBuilder.h"
#include<time.h>

int main()
{
	int screenWidth = 640;
	int screenHeight = 360;

	InitWindow(screenWidth, screenHeight, "Gravity Jumper");

	SetTargetFPS(60);

	srand(time(NULL));

	Player player(LoadTexture("assets/player.png"));
	UIController UI;
	LevelBuilder levelBuilder;
	levelBuilder.lineColor = LIME;

	levelBuilder.Generate("assets/levelTest.txt"); //Generate points from .txt file

	while (!WindowShouldClose())
	{
		BeginDrawing();

		ClearBackground(BLACK);

		player.Update();
		UI.Update();
		
		levelBuilder.Draw();
		player.Draw();
		UI.Draw();

		EndDrawing();
	}

	CloseWindow();

	return 0;
}