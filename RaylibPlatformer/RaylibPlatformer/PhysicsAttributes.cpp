#include"PhysicsAttributes.h"

PhysicsAttributes::PhysicsAttributes() {};
PhysicsAttributes::PhysicsAttributes(Vector2 _velocity, float _drag, float _gravity, Vector2 _maxVelocity)
{
	velocity = _velocity;
	drag = _drag;
	gravity = _gravity;
	maxVelocity = _maxVelocity;
};
PhysicsAttributes::~PhysicsAttributes() {};

void PhysicsAttributes::Update()
{
	//Calculates velocity/movement changes
	velocity.y += gravity;
	velocity.x /= drag;
};