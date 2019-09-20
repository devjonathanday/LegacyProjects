#pragma once
#include"raylib.h"
#include<vector>

struct BoxCollider
{
	Rectangle rec;
	bool hit(Vector2 target);
};

struct PhysicsAttributes
{
	float gravityScale; //vertical acceleration
	Vector2 drag; //horizontal/vertical deceleration
	Vector2 moveSpeed; //movement speed
	float moveAccel; //horizontal acceleration
	Vector2 moveMax; //maximum speed (x,y)
	float rotSpeed; //rotational speed
	float rotDrag; //deceleration of rotSpeed
	void Update();
};
struct Particle
{
	Texture2D tex;
	Color color;
	Rectangle rec;
	float timer;
	PhysicsAttributes phys;
	void Update();
	void Draw(Vector2 cameraOffset, Vector2 screenOffset);
};
struct ParticleSystem
{
	std::vector<Particle> particleGroup;
	Texture2D tex;
	Color startColor;
	Color endColor;
	Vector2 origin;
	float lifeTime;
	Vector2 xSpeed; //min/max horizontal speed
	Vector2 ySpeed; //min/max vertical speed
	void SpawnParticle();
	void Burst(int count);
	void Update();
	void Draw(Vector2 cameraOffset, Vector2 screenOffset);
};