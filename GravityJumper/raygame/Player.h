#pragma once
#include"raylib.h"
#include"EngineTools.h"

class Player
{
	PhysicsAttributes tempPhys;

public:
	Player();
	Player(Texture2D _tex, Vector2 defaultPos);
	~Player();

	Texture2D tex;
	float jumpSpeed;
	float rotation;
	bool paused;

	BoxCollider collider;
	PhysicsAttributes phys;
	ParticleSystem jumpEffect;

	void Update();
	void Draw(Vector2 cameraOffset, Vector2 screenOffset);
};