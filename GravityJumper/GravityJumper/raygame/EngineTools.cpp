#include"EngineTools.h"

bool BoxCollider::hit(Vector2 target)
{
	return ( target.x > rec.x && target.y > rec.y &&
	target.x < rec.x + rec.width && target.y < rec.y + rec.height );
}
void PhysicsAttributes::Update()
{
	if(drag.x != 0)
		moveSpeed.x /= drag.x;
	if(rotDrag != 0)
		rotSpeed /= rotDrag;
	moveSpeed.y += gravityScale;
}
void Particle::Update()
{
	phys.Update();
	rec.x += phys.moveSpeed.x;
	rec.y += phys.moveSpeed.y;
	timer += GetFrameTime();
}
void Particle::Draw()
{
	DrawTexture(tex, rec.x, rec.y, color);
}
void ParticleSystem::SpawnParticle()
{
	Particle newParticle;
	newParticle.tex = tex;
	newParticle.color = startColor;
	newParticle.rec = { origin.x, origin.y, (float)tex.width, (float)tex.height };
	newParticle.timer = 0;
	newParticle.phys.gravityScale = 0.2f;
	newParticle.phys.moveSpeed = { (float)GetRandomValue(xSpeed.x, xSpeed.y),
								   (float)GetRandomValue(ySpeed.x, ySpeed.y) };
	newParticle.phys.drag = { 0, 0 };
	particleGroup.push_back(newParticle);
}
void ParticleSystem::Burst(int count)
{
	for (int i = 0; i < count; i++)
		SpawnParticle();
}
void ParticleSystem::Update()
{
	for (int i = 0; i < particleGroup.size(); i++)
	{
		particleGroup[i].Update();
		Color difference = { (startColor.r < endColor.r ? endColor.r - startColor.r : startColor.r - endColor.r),
							 (startColor.g < endColor.g ? endColor.g - startColor.g : startColor.g - endColor.g),
							 (startColor.b < endColor.b ? endColor.b - startColor.b : startColor.b - endColor.b),
							 (startColor.a < endColor.a ? endColor.a - startColor.a : startColor.a - endColor.a) };
		particleGroup[i].color = { (unsigned char)(startColor.r + (difference.r * (lifeTime / particleGroup[i].timer))),
								   (unsigned char)(startColor.g + (difference.g * (lifeTime / particleGroup[i].timer))),
								   (unsigned char)(startColor.b + (difference.b * (lifeTime / particleGroup[i].timer))),
								   (unsigned char)(startColor.a + (difference.a * (lifeTime / particleGroup[i].timer))) };
		if (particleGroup[i].timer >= lifeTime)
			particleGroup.erase(particleGroup.begin() + i);
	}
}
void ParticleSystem::Draw()
{
	for (int i = 0; i < particleGroup.size(); i++)
		particleGroup[i].Draw();
}