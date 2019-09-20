#include"raylib.h"
#include"Player.h"
#include"UIController.h"
#include"LevelBuilder.h"
#include"FocalCamera.h"
#include<time.h>

int main()
{
	int screenWidth = 640;
	int screenHeight = 360;

	InitWindow(screenWidth, screenHeight, "Gravity Jumper");

	SetTargetFPS(60);

	srand(time(NULL));

	Player player(LoadTexture("assets/player.png"), { 100,100 });
	UIController UI;
	LevelBuilder levelBuilder;
	FocalCamera camera({ player.collider.rec.x, player.collider.rec.y }, {(float)screenWidth / 2, (float)screenHeight / 2}, 0.1f);
	levelBuilder.lineColor = LIME;

	levelBuilder.Generate("assets/levelTest.txt"); //Generate points from .txt file

	while (!WindowShouldClose())
	{
		BeginDrawing();

		ClearBackground(BLACK);

		player.Update();
		UI.Update();
		camera.Update({player.collider.rec.x, player.collider.rec.y });
		
		levelBuilder.Draw(camera.pos, camera.screenPos);
		player.Draw(camera.pos, camera.screenPos);
		UI.Draw();

		EndDrawing();
	}

	CloseWindow();

	return 0;
}