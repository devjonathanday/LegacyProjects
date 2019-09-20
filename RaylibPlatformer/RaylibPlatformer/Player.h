#pragma once
#include"raylib.h"
#include"PhysicsAttributes.h"
#include"Functions.h"
#include"Animations.h"
#include"Particles.h"

class Player
{
public:
	float width;
	float height;
	Texture2D tex;
	Vector2 pos;
	PhysicsAttributes phys;
	float moveSpeed;
	float jumpSpeed;
	float rotation;
	AnimationController animationController;
	Animation idleAnimation;
	Animation jumpAnimation;
	Animation fallAnimation;
	Animation runAnimation;
	Animation wallJumpAnimation;
	Rectangle collisionRecs[7];
	bool grounded;
	bool canMoveLeft;
	bool canMoveRight;
	bool flipX = false;
	bool flipY = false;
	bool joystick = false;
	//Particle * smokePuff;

	Player();
	~Player();
	Player(float _width, float _height, Texture2D _tex, Vector2 _pos, PhysicsAttributes _phys, float _moveSpeed, float _jumpSpeed);

	void Update(Rectangle levelColliders[], int arraySize);
	void UpdateColliders();
	void CheckColliders(Rectangle levelColliders[], int arraySize);
	void Draw(Vector2 cameraOffset, Vector2 screenOffset);
	void DrawCollisionRecs();
};