#include"FocalCamera.h"

FocalCamera::FocalCamera() {}
FocalCamera::~FocalCamera() {}
FocalCamera::FocalCamera(Vector2 startingPos, Vector2 newScreenPos, float newLerp)
{
	pos = startingPos;
	screenPos = newScreenPos;
	lerp = newLerp;
}

void FocalCamera::Update(Vector2 focus)
{
	if (lerp == 0) pos = focus;
	else
	{
		pos.x = pos.x + ((focus.x - pos.x) * lerp);
		pos.y = pos.y + ((focus.y - pos.y) * lerp);
	}
}
void FocalCamera::Draw()
{
	//DrawTexture(tex, screenPos.x, screenPos.y, WHITE);
}