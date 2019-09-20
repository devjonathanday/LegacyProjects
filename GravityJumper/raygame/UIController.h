#pragma once
#include<raylib.h>

class UIController
{
	struct Button
	{
		Texture2D currentTex;
		Texture2D idle;
		Texture2D pressed;
		int key;
	};

	Texture2D arrowKey = LoadTexture("assets/arrowKey.png");
	Texture2D arrowPressed = LoadTexture("assets/arrowPressed.png");
	Texture2D spaceBar = LoadTexture("assets/spaceBar.png");
	Texture2D spacePressed = LoadTexture("assets/spacePressed.png");

	Button buttonList[3] = { { arrowKey, arrowKey, arrowPressed, KEY_A },
							 { arrowKey, arrowKey, arrowPressed, KEY_D },
							 { spaceBar, spaceBar, spacePressed, KEY_SPACE } };
public:
	void Update();
	void Draw();
};