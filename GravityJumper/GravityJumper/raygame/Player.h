#pragma once
#include"raylib.h"
#include"EngineTools.h"

class Player
{
	PhysicsAttributes tempPhys;

public:
	Player();
	Player(Texture2D _tex);
	~Player();

	Texture2D tex;
	float jumpSpeed;
	float rotation;
	bool paused;

	BoxCollider collider;
	PhysicsAttributes phys;
	ParticleSystem jumpEffect;

	void Update();
	void Draw();
};