#pragma once
#include"raylib.h"
#include<array>
#include<iostream>

struct CollisionRectangle
{
	bool isColliding;
	Rectangle collider;
};

void DrawRectangleRecLines(Rectangle rec, Color color);
CollisionRectangle CheckColliders(Rectangle myCollider, Rectangle * colliders, int arraySize);