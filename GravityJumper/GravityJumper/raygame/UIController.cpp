#include"UIController.h"

void UIController::Update()
{
	for (int i = 0; i < 3; i++)
		IsKeyDown(buttonList[i].key) ? buttonList[i].currentTex = buttonList[i].pressed : buttonList[i].currentTex = buttonList[i].idle;
}
void UIController::Draw()
{
	DrawTextureEx(buttonList[0].currentTex, { 24, 332 }, 180, 1, WHITE);
	DrawTextureEx(buttonList[1].currentTex, { 28, 316 }, 0, 1, WHITE);
	DrawTexture(buttonList[2].currentTex, 10, 336, WHITE);
}