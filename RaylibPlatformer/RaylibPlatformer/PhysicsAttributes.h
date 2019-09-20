#pragma once
#include"raylib.h"

class PhysicsAttributes
{
public:
	Vector2 velocity;
	float drag;
	float gravity;
	Vector2 maxVelocity;

	PhysicsAttributes();
	PhysicsAttributes(Vector2 _velocity, float _drag, float _gravity, Vector2 _maxVelocity);
	~PhysicsAttributes();

	void Update();
};