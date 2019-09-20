#include"Player.h"

Player::Player() {}
Player::Player(float _width, float _height, Texture2D _tex, Vector2 _pos, PhysicsAttributes _phys, float _moveSpeed, float _jumpSpeed)
{
	#pragma region Physics

	width = _width;
	height = _height;
	pos = _pos;
	phys = _phys;
	moveSpeed = _moveSpeed;
	jumpSpeed = _jumpSpeed;

	#pragma endregion

	#pragma region Animation

	//Initializing animations with slicing and playSpeed data
	idleAnimation = Animation(_tex, 1, { 1,1 }, { 1,1 }, { 18,18 }, 0, 0);
	runAnimation = Animation(_tex, 8, { 1,1 }, { 1,1 }, { 18,18 }, 15, 1);
	jumpAnimation = Animation(_tex, 1, { 1,1 }, { 1,1 }, { 18,18 }, 0, 2);
	fallAnimation = Animation(_tex, 1, { 1,1 }, { 1,1 }, { 18,18 }, 0, 3);
	wallJumpAnimation = Animation(_tex, 1, { 1,1 }, { 1,1 }, { 18,18 }, 0, 4);

	//Initializing animation transitions (transitions occur from any state)
	animationController.transitions.push_back({ false, idleAnimation });
	animationController.transitions.push_back({ false, runAnimation });
	animationController.transitions.push_back({ false, jumpAnimation });
	animationController.transitions.push_back({ false, fallAnimation });
	animationController.transitions.push_back({ false, wallJumpAnimation });

	//Assigining the default animation
	animationController.currentAnimation = &idleAnimation;

	//Texture2D smokePuffTex = LoadTexture("Assets/Textures/smokeParticle.png");
	//smokePuff = new Particle(smokePuffTex, { 100,100 }, WHITE, Animation(smokePuffTex, 6, { 1,1 }, { 1,1 }, { 8,8 }, 30, 0), true);

	#pragma endregion
}
Player::~Player()
{
	//delete smokePuff;
}

void Player::Update(Rectangle levelColliders[], int arraySize)
{
#pragma region Input

	if ((joystick ? IsGamepadButtonDown(GAMEPAD_PLAYER1, GAMEPAD_XBOX_BUTTON_LEFT) : IsKeyDown(KEY_A)) && canMoveLeft) phys.velocity.x -= moveSpeed;
	if ((joystick ? IsGamepadButtonDown(GAMEPAD_PLAYER1, GAMEPAD_XBOX_BUTTON_RIGHT) : IsKeyDown(KEY_D)) && canMoveRight) phys.velocity.x += moveSpeed;

	if (joystick ? IsGamepadButtonPressed(GAMEPAD_PLAYER1, GAMEPAD_XBOX_BUTTON_A) : IsKeyPressed(KEY_SPACE))
	{
		if (grounded)
		{
			pos.y--; //Separates collider from ground
			phys.velocity.y = -jumpSpeed;
		}
		else if (!canMoveLeft)
		{
			phys.velocity.x += moveSpeed * 10;
			pos.y--; //Separates collider from ground
			phys.velocity.y = -jumpSpeed;
		}
		else if (!canMoveRight)
		{
			phys.velocity.x -= moveSpeed * 10;
			pos.y--; //Separates collider from ground
			phys.velocity.y = -jumpSpeed;
		}
	}
	if ((joystick ? IsGamepadButtonReleased(GAMEPAD_PLAYER1, GAMEPAD_XBOX_BUTTON_A) : IsKeyReleased(KEY_SPACE)) && phys.velocity.y < 0)
	{
		phys.velocity.y /= 2;
	}

#pragma endregion

#pragma region Physics/Collision

	phys.Update();

	//Prevents Y velocity from exceeding maximum
	if (phys.velocity.y > phys.maxVelocity.y) phys.velocity.y = phys.maxVelocity.y;

	pos.x += phys.velocity.x;
	pos.y += phys.velocity.y;

	UpdateColliders();
	CheckColliders(levelColliders, arraySize);

#pragma endregion

#pragma region Animation

	animationController.transitions[0].condition = (abs(phys.velocity.x) <= 0.3f && grounded); //Idle
	animationController.transitions[1].condition = (abs(phys.velocity.x) > 0.3f && grounded); //Run
	animationController.transitions[2].condition = (phys.velocity.y < 0 && !grounded); //Jump
	animationController.transitions[3].condition = (phys.velocity.y > 0 && !grounded); //Fall
	if (!grounded && !canMoveLeft) //Left walljump
	{
		flipX = false;
		animationController.transitions[4].condition = true;
		phys.gravity = 0.15f;
	}
	else if (!grounded && !canMoveRight) //Right walljump
	{
		flipX = true;
		animationController.transitions[4].condition = true;
		phys.gravity = 0.15f;
	}
	else
	{
		animationController.transitions[4].condition = false;
		if (phys.velocity.x < 0) flipX = true;
		if (phys.velocity.x > 0) flipX = false;
		phys.gravity = 0.2f;
	}

	//smokePuff->Update();

#pragma endregion

}

void Player::Draw(Vector2 cameraOffset, Vector2 screenOffset)
{
	//DrawCollisionRecs();
	animationController.Update();
	animationController.DrawAnimation({ pos.x - 1, pos.y - 2 }, WHITE, flipX, flipY, rotation, cameraOffset, screenOffset);
	//smokePuff->Draw(cameraOffset, screenOffset);
}

void Player::DrawCollisionRecs()
{
	DrawRectangleRec(collisionRecs[0], DARKGRAY);
	DrawRectangleRec(collisionRecs[1], RED);
	DrawRectangleRec(collisionRecs[2], BLUE);
	DrawRectangleRec(collisionRecs[5], PINK);
	DrawRectangleRec(collisionRecs[6], ORANGE);
	DrawRectangleRec(collisionRecs[3], GREEN);
	DrawRectangleRec(collisionRecs[4], YELLOW);
}

void Player::UpdateColliders()
{
	collisionRecs[0] = { pos.x, pos.y, width, height }; //Center
	collisionRecs[1] = { pos.x + 4, pos.y - 1, width - 8, 1 }; //Top
	collisionRecs[2] = { pos.x + 4, pos.y + height, width - 8, 1 }; //Bottom
	collisionRecs[3] = { pos.x, pos.y + 4, 1, height - 8 }; //Left
	collisionRecs[4] = { pos.x + width - 2, pos.y + 4, 1, height - 8 }; //Right
	collisionRecs[5] = { pos.x - 1, pos.y + 4, 2, height - 8}; //Left input catch
	collisionRecs[6] = { pos.x + width - 2, pos.y + 4, 2, height - 8}; //Right input catch
}

void Player::CheckColliders(Rectangle levelColliders[], int arraySize)
{
	grounded = false;
	canMoveLeft = true;
	canMoveRight = true;

	for (int i = 0; i < arraySize; i++)
	{
		if (CheckCollisionRecs(collisionRecs[1], levelColliders[i])) //Top
		{
			pos.y += (levelColliders[i].y + levelColliders[i].height + 1) - pos.y;
			phys.velocity.y = 0;
		}
		if (CheckCollisionRecs(collisionRecs[2], levelColliders[i])) //Bottom
		{
			pos.y -= ((pos.y + height) - levelColliders[i].y);
			phys.velocity.y = 0;
		}
		if (CheckCollisionRecs(collisionRecs[3], levelColliders[i])) //Left
		{
			pos.x++;
			phys.velocity.x = 0;
		}
		if (CheckCollisionRecs(collisionRecs[4], levelColliders[i])) //Right
		{
			pos.x--;
			phys.velocity.x = 0;
		}
		//Additional check for bottom collider + one additional pixel below for jumping
		if (CheckCollisionRecs({ pos.x + 4, pos.y + height - 2, width - 8, 2 }, levelColliders[i])) grounded = true;
		//Left collider to prevent input
		if (CheckCollisionRecs(collisionRecs[5], levelColliders[i])) canMoveLeft = false;
		//Right collider to prevent input
		if (CheckCollisionRecs(collisionRecs[6], levelColliders[i])) canMoveRight = false;
	}
}