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

	Button buttonList[3] = { { LoadTexture("assets/arrowKey.png"), LoadTexture("assets/arrowKey.png"), LoadTexture("assets/arrowPressed.png"), KEY_A },
							 { LoadTexture("assets/arrowKey.png"), LoadTexture("assets/arrowKey.png"), LoadTexture("assets/arrowPressed.png"), KEY_D },
							 { LoadTexture("assets/spaceBar.png"), LoadTexture("assets/spaceBar.png"), LoadTexture("assets/spacePressed.png"), KEY_SPACE } };
public:
	void Update();
	void Draw();
};