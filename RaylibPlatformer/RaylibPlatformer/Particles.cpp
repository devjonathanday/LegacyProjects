#include"Particles.h"

Particle::Particle() {}
//Standard Particle
Particle::Particle(Texture2D _tex, Vector2 _pos, Color _tint)
{
	tex = _tex;
	pos = _pos;
	tint = _tint;
	animated = false;
}
//Animated Particle
Particle::Particle(Texture2D _sheet, Vector2 _pos, Color _tint, Animation _primaryAnimation, bool _destroyOnAnimationEnd)
{
	tex = _sheet;
	pos = _pos;
	tint = _tint;
	animated = true;
	animationController.StartAnimation(&_primaryAnimation);
	destroyOnAnimationEnd = _destroyOnAnimationEnd;
}
Particle::~Particle()
{
	//delete animationController;
}
void Particle::Update()
{
	if(animated) animationController.Update();
	destroyFlag = (destroyOnAnimationEnd && animationController.currentAnimation->endCondition);
}
void Particle::Draw(Vector2 cameraOffset, Vector2 screenOffset)
{
	if(animated) animationController.DrawAnimation(pos, tint, false, false, 0, cameraOffset, screenOffset);
	else DrawTexture(tex, screenOffset.x + (pos.x - cameraOffset.x),
						  screenOffset.y + (pos.y - cameraOffset.y), tint);
}