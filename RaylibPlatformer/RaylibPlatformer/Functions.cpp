#include"Functions.h"

void DrawRectangleRecLines(Rectangle rec, Color color)
{
	DrawRectangleLines(rec.x, rec.y, rec.width, rec.height, color);
}
CollisionRectangle CheckColliders(Rectangle myCollider, Rectangle * colliders, int arraySize)
{
	//Returns a CollisionRectangle indicating:
	//Whether there is a collision or not (bool isColliding), and
	//With which rectangle the collision occurred (Rectangle collider).
	for (int i = 0; i < arraySize; i++)
		if (CheckCollisionRecs(myCollider, *(colliders + i)))
			return { true, *(colliders + i) };
	return { false, {0,0,0,0} };
}