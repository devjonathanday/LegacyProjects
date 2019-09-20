using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;

namespace Platformer
{
    public class Camera
    {
        public Vector2 pos;
        public float lerp;

        public Camera(float newLerp)
        {
            lerp = newLerp; 
        }

        public void Update(Vector2 focus)
        {
            if (lerp == 0) pos = focus;
            else
            {
                //Round UP the desired float to an int, to avoid jitter
                pos.x += (int)Math.Ceiling((focus.x - pos.x) * lerp);
                pos.y += (int)Math.Ceiling((focus.y - pos.y) * lerp);
            }
        }
    }

    public class Animation
    {
        public Texture2D sheet;
        public int frameCount;
        public Vector2 offset;
        public Vector2 padding;
        public Vector2 frameSize;
        public float playSpeed;
        public int row;
        public bool endCondition;

        public Animation(Texture2D _sheet, int _frameCount, Vector2 _offset, Vector2 _padding, Vector2 _frameSize, float _playSpeed, int _row)
        {
            sheet = _sheet;
            frameCount = _frameCount;
            offset = _offset;
            padding = _padding;
            frameSize = _frameSize;
            playSpeed = _playSpeed;
            row = _row;
        }
        public void DrawFrame(Vector2 pos, Color tint, int frame, bool flipX, bool flipY, Vector2 pivot, float rotation, Vector2 cameraOffset, Vector2 screenOffset)
        {
            Rectangle sourceRec = new Rectangle(((frameSize.x + padding.x) * frame) + offset.x,
                                                ((frameSize.y + (frameSize.y * (flipY ? 1 : 0) + padding.y)) * row) + offset.y,
                                                  flipX ? -frameSize.x : frameSize.x, flipY ? -frameSize.y : frameSize.y);

            Rectangle destRec = new Rectangle(screenOffset.x + (pos.x - cameraOffset.x), screenOffset.y + (pos.y - cameraOffset.y), frameSize.x, frameSize.y);

            DrawTexturePro(sheet, sourceRec, destRec, pivot, rotation, tint);
        }
    }

    public class AnimationController
    {
        float playTimer;
        int currentFrame;
        public Animation currentAnimation;
        public List<AnimationTransition> transitions = new List<AnimationTransition>();

        public void Update()
        {
            playTimer += GetFrameTime();

            if (currentAnimation.playSpeed != 0 && playTimer >= 60 / currentAnimation.playSpeed / 60)
            {
                if (currentFrame == currentAnimation.frameCount - 1)
                {
                    currentFrame = 0;
                    currentAnimation.endCondition = true;
                }
                else currentFrame++;
                playTimer = 0;
            }

            for(int i = 0; i < transitions.Count; i++)
                if (transitions[i].condition) StartAnimation(transitions[i].nextAnimation);
        }

        public void StartAnimation(Animation animation)
        {
            if(currentAnimation != animation)
            {
                playTimer = 0;
                currentFrame = 0;
                currentAnimation = animation;
                currentAnimation.endCondition = false;
            }
        }

        public void Draw(Vector2 pos, Color tint, bool flipX, bool flipY, Vector2 pivot, float rotation, Vector2 cameraOffset, Vector2 screenOffset)
        {
            currentAnimation.DrawFrame(pos, tint, currentFrame, flipX, flipY, pivot, rotation, cameraOffset, screenOffset);
        }
    }

    public class AnimationTransition
    {
        public bool condition;
        public Animation nextAnimation;

        public AnimationTransition(bool _condition, Animation _nextAnimation)
        {
            condition = _condition;
            nextAnimation = _nextAnimation;
        }
    }

    public class Particle
    {
        public Texture2D tex;
        public Vector2 pos;
        public Color tint;
        public PhysicsAttributes physAtts;
        public float lifeTime;
        public float timer;

        public void Update()
        {
            timer += GetFrameTime();
        }
        public void Draw()
        {
            DrawTexture(tex, (int)pos.x, (int)pos.y, tint);
        }
    }

    public class ParticleSystem
    {
        public List<Particle> particles = new List<Particle>();

        public void AddParticle(Particle newParticle)
        {
            particles.Add(newParticle);
        }
        public void Update()
        {
            for(int i = 0; i < particles.Count; i++)
            {
                particles[i].Update();
                if (particles[i].timer >= particles[i].lifeTime)
                    particles.Remove(particles[i]);
            }
        }
        public void Draw()
        {
            for (int i = 0; i < particles.Count; i++)
                particles[i].Draw();
        }
    }
}
