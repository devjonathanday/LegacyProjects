#include"Animations.h"

Animation::Animation() {};
Animation::Animation(Texture2D _sheet, int _frameCount, Vector2 _offset, Vector2 _padding, Vector2 _frameSize, float _playSpeed, int _row)
{
	sheet = _sheet;
	frameCount = _frameCount;
	offset = _offset;
	padding = _padding;
	frameSize = _frameSize;
	playSpeed = _playSpeed;
	row = _row;
};
void Animation::DrawFrame(Vector2 position, Color tint, int frame, bool flipX, bool flipY, float rotation, Vector2 cameraOffset, Vector2 screenOffset)
{
	DrawTexturePro(sheet, { (((frameSize.x + padding.x) * frame) + offset.x),
							((frameSize.y + (frameSize.y * flipY ? 1 : 0) + padding.y) * row) + offset.y,
					          flipX ? -frameSize.x : frameSize.x, flipY ? -frameSize.y : frameSize.y },
							{ screenOffset.x + ((position.x + (frameSize.x / 2)) - cameraOffset.x), screenOffset.y + ((position.y + (frameSize.y / 2)) - cameraOffset.y),
							  frameSize.x, frameSize.y }, { frameSize.x / 2, frameSize.y / 2 }, rotation, tint);
	//DrawTextureRec(sheet, { ((frameSize.x + padding.x) * frame) + offset.x,
	//						((frameSize.y + padding.y) * row) + offset.y,
	//						  frameSize.x, frameSize.y }, position, tint);
}
AnimationController::AnimationController()
{
	timer = 0;
	currentFrame = 0;
}
void AnimationController::Update()
{
	//Increments the animation timer
	timer += GetFrameTime();
	//If we need to advance the frame (based on the animation's playSpeed)
	if ((*currentAnimation).playSpeed != 0)
	{
		if (timer >= 60 / (*currentAnimation).playSpeed / 60)
		{
			//If we are on the last frame
			if (currentFrame == (*currentAnimation).frameCount - 1)
			{
				//Reset to the first frame
				currentFrame = 0;
				(*currentAnimation).endCondition = true;
			}
			else currentFrame++; //else, advance to the next frame

			timer = 0;
		}
	}

	for (int i = 0; i < transitions.size(); i++)
		if (transitions[i].condition) StartAnimation(&transitions[i].nextAnimation);
}
void AnimationController::StartAnimation(Animation * animation)
{
	if (currentAnimation != animation)
	{
		timer = 0;
		currentFrame = 0;
		currentAnimation = animation;
		(*currentAnimation).endCondition = false;
	}
}
void AnimationController::DrawAnimation(Vector2 position, Color tint, bool flipX, bool flipY, float rotation, Vector2 cameraOffset, Vector2 screenOffset)
{
	currentAnimation->DrawFrame(position, tint, currentFrame, flipX, flipY, rotation, cameraOffset, screenOffset);
}
AnimationController::~AnimationController() {}