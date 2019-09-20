#pragma once
#include"raylib.h"
#include<vector>

using std::vector;

class Animation
{
public:
	Texture2D sheet;
	int frameCount;
	Vector2 offset;
	Vector2 padding;
	Vector2 frameSize;
	float playSpeed;
	int row;
	bool endCondition = false;

	Animation();
	Animation(Texture2D _sheet, int _frameCount, Vector2 _offset, Vector2 _padding, Vector2 _frameSize, float _playSpeed, int _row);
	void DrawFrame(Vector2 position, Color tint, int frame, bool flipX, bool flipY, float rotation, Vector2 cameraOffset, Vector2 screenOffset);
}
;
class AnimTransition
{
public:
	bool condition;
	Animation nextAnimation;
};

class AnimationController
{
public:
	float timer;
	int currentFrame;
	Animation * currentAnimation;
	vector<AnimTransition> transitions;

	AnimationController();
	~AnimationController();

	void Update();
	void StartAnimation(Animation * animation);
	void DrawAnimation(Vector2 position, Color tint, bool flipX, bool flipY, float rotation, Vector2 cameraOffset, Vector2 screenOffset);
};

