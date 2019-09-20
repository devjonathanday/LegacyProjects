#include"Player.h"

Player::Player() {}
Player::Player(Texture2D _tex)
{
	tex = _tex;
	jumpSpeed = 4;
	rotation = 0;
	collider.rec = { 100, 100, (float)tex.width, (float)tex.height };
	phys.gravityScale = 0.1f;
	phys.drag = { 1.05f, 0 };
	phys.moveSpeed = {0, 0};
	phys.moveAccel = 0.15f;
	phys.moveMax = { 6, 4};
	phys.rotSpeed = -3;
	phys.rotDrag = 1.05f;
	tempPhys.gravityScale = 0;
	tempPhys.moveSpeed = {0, 0};
	tempPhys.rotSpeed = 0;
	jumpEffect.tex = LoadTexture("assets/jumpParticle.png");
	jumpEffect.startColor = { 255, 255, 255, 255 };
	jumpEffect.endColor = { 255, 255, 255, 0 };
	jumpEffect.lifeTime = 0.25f;
	paused = false;
}
Player::~Player() {}
void Player::Update()
{
	phys.Update();
	//bounds checking
	if (phys.moveSpeed.x < -phys.moveMax.x)
		phys.moveSpeed.x = -phys.moveMax.x;
	if (phys.moveSpeed.x > phys.moveMax.x)
		phys.moveSpeed.x = phys.moveMax.x;
	//if (phys.moveSpeed.y < -phys.moveMax.y)
	//	phys.moveSpeed.y = -phys.moveMax.y;
	if (phys.moveSpeed.y > phys.moveMax.y)
		phys.moveSpeed.y = phys.moveMax.y;
	if (IsKeyDown(KEY_A) && !paused)
		phys.moveSpeed.x -= phys.moveAccel;
	if (IsKeyDown(KEY_D) && !paused)
		phys.moveSpeed.x += phys.moveAccel;
	if (IsKeyPressed(KEY_P))
	{
		paused ? phys = tempPhys : tempPhys = phys;
		paused = !paused;
	}
	if (IsKeyPressed(KEY_SPACE) && !paused)
	{
		phys.moveSpeed.y = -jumpSpeed;
		phys.rotSpeed = 20;
		jumpEffect.Burst(6);
	}
	if (IsKeyDown(KEY_LEFT_CONTROL) && IsKeyPressed(KEY_F))
		ToggleFullscreen();
	jumpEffect.origin = { collider.rec.x, collider.rec.y + (float)tex.height / 2 };
	jumpEffect.xSpeed = { -3, 3 };
	jumpEffect.ySpeed = { -3, 3 };
	if (!paused)
	{
		jumpEffect.Update();
		rotation += phys.rotSpeed;
		collider.rec.x += phys.moveSpeed.x;
		collider.rec.y += phys.moveSpeed.y;
	}
}
void Player::Draw()
{
	DrawTexturePro(tex, { 0, 0, (float)tex.width, (float)tex.height }, collider.rec,
				  { (float)tex.width / 2, (float)tex.height / 2 }, rotation, WHITE);
	jumpEffect.Draw();
	if (paused)
		DrawText("PAUSED", 10, 10, 20, WHITE);
}