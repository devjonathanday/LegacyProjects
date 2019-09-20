#pragma once
#include"raylib.h"
#include"Animations.h"

class Particle
{
	bool animated = false;

public:

	//Particle
	Texture2D tex;
	Vector2 pos;
	Color tint;
	bool destroyFlag = false;

	//Animated Particle
	AnimationController animationController;
	bool destroyOnAnimationEnd;

	Particle();
	//Standard Particle
	Particle(Texture2D _tex, Vector2 _pos, Color _tint);
	//Animated Particle
	Particle(Texture2D _sheet, Vector2 _pos, Color _tint, Animation _primaryAnimation, bool _destroyOnAnimationEnd);
	~Particle();

	void Update();
	void Draw(Vector2 cameraOffset, Vector2 screenOffset);
};