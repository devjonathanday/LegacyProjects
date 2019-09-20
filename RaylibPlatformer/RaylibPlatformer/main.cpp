#include"raylib.h"
#include"Player.h"
#include"FocalCamera.h"
#include"Particles.h"

int main()
{
	int screenWidth = 640;
	int screenHeight = 360;

	InitWindow(screenWidth, screenHeight, "Raylib Platformer");

	SetTargetFPS(60);

	bool quitGame = false;

	//Level collision rectangles
	Rectangle levelColliders[8] = { {20,200,50,50},
									{70,150,50,100},
									{120,200,50,50},
									{170,150,50,100},
									{220,100,50,150},
									{270,200,150,50},
									{420,150,50,100},
									{320,50,50,100} };

	//Texture loading
	Texture2D playerTex = LoadTexture("Assets/Textures/playerSheet.png");
	//Texture2D smokePuffTex = LoadTexture("Assets/Textures/smokeParticle.png");

	//Initializing variables
	PhysicsAttributes playerPhysAtts({ 0,0 }, 1.2f, 0.2f, { 0, 5 });
	Player player(16, 16, playerTex, { 50, 100 }, playerPhysAtts, 0.5f, 5);
	//Particle smokePuff = Particle(smokePuffTex, { 100,100 }, WHITE, Animation(smokePuffTex, 6, { 1,1 }, { 1,1 }, { 8,8 }, 30, 0), false);
	player.joystick = false;

	FocalCamera camera({ player.pos.x, player.pos.y }, { (float)(screenWidth / 2) - player.width, (float)(screenHeight / 2) - player.height }, 0.1f);
	camera.lockY = true;

	while (!quitGame && !WindowShouldClose())
	{
		for (int i = 0; i < 8; i++)
		{
			DrawRectangle(camera.screenPos.x + (levelColliders[i].x - camera.pos.x),
						  camera.screenPos.y + (levelColliders[i].y - camera.pos.y),
						  levelColliders[i].width, levelColliders[i].height, GRAY);
		}
		//Update
		player.Update(levelColliders, 8);
		camera.Update(player.pos);
		//smokePuff.Update();

		//Draw
		BeginDrawing();

		ClearBackground(BLACK);

		player.Draw(camera.pos, camera.screenPos);
		//smokePuff.Draw(camera.pos, camera.screenPos);

		EndDrawing();
	}

	CloseWindow();

	return 0;
}