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
	if (lerp == 0)
	{
		if (!lockX) pos.x = focus.x;
		if (!lockY) pos.y = focus.y;
	}
	else
	{
		if (!lockX) pos.x = (int)(pos.x + ((focus.x - pos.x) * lerp));
		if (!lockY) pos.y = (int)(pos.y + ((focus.y - pos.y) * lerp));
	}
}