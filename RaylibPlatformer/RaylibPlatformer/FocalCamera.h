#pragma once
#include"raylib.h"

class FocalCamera
{
public:
	FocalCamera();
	~FocalCamera();
	FocalCamera(Vector2 startingPos, Vector2 newScreenPos, float newLerp);
	Vector2 pos = { 0,0 };
	Vector2 screenPos = { 0,0 };
	float lerp = 0;
	bool lockX = false;
	bool lockY = false;

	void Update(Vector2 focus);
};