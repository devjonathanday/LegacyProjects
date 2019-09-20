using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;

namespace Platformer
{
    public class Player
    {
        //General
        public Vector2 pos;
        public Vector2 size;

        //Physics
        public PhysicsAttributes physAtts;
        public float moveSpeed;
        public float jumpSpeed;
        public Rectangle[] collisionRecs = new Rectangle[8];
        public bool grounded;
        public bool initialJump;
        public bool canWallJumpLeft;
        public bool canWallJumpRight;
        public float wallJumpForce;

        //Graphics
        public Texture2D tex;
        public Color tint;
        bool flipX;
        bool flipY;

        AnimationController animationController;
        Animation idleAnimation;
        Animation runAnimation;
        Animation jumpAnimation;
        Animation fallAnimation;
        Animation wallJumpAnimation;

        public Player(Vector2 startPos)
        {
            //General Initialization
            pos = startPos;
            size = new Vector2(16, 16);

            //Physics Attributes
            physAtts = new PhysicsAttributes();
            physAtts.drag = 0.75f;
            physAtts.gravity = 0.2f;
            physAtts.velocity = Vector2.Zero;
            physAtts.maxVelocity = new Vector2(2, 5);

            //Other Physics
            moveSpeed = 0.75f;
            jumpSpeed = 5f;
            grounded = false;
            initialJump = false;
            canWallJumpLeft = false;
            canWallJumpRight = false;
            wallJumpForce = 2;

            //Graphics/Animation
            tex = LoadTexture("assets/playerSheet.png");
            tint = Color.WHITE;
            flipX = false;
            flipY = false;

            Animation idleAnimation     = new Animation(tex, 1, new Vector2(1, 1), new Vector2(1, 1), new Vector2(18, 18), 0,  0);
            Animation runAnimation      = new Animation(tex, 8, new Vector2(1, 1), new Vector2(1, 1), new Vector2(18, 18), 15, 1);
            Animation jumpAnimation     = new Animation(tex, 1, new Vector2(1, 1), new Vector2(1, 1), new Vector2(18, 18), 0,  2);
            Animation fallAnimation     = new Animation(tex, 1, new Vector2(1, 1), new Vector2(1, 1), new Vector2(18, 18), 0,  3);
            Animation wallJumpAnimation = new Animation(tex, 1, new Vector2(1, 1), new Vector2(1, 1), new Vector2(18, 18), 0,  4);

            animationController = new AnimationController();
            animationController.transitions.Add(new AnimationTransition(false, idleAnimation));
            animationController.transitions.Add(new AnimationTransition(false, runAnimation));
            animationController.transitions.Add(new AnimationTransition(false, jumpAnimation));
            animationController.transitions.Add(new AnimationTransition(false, fallAnimation));
            animationController.transitions.Add(new AnimationTransition(false, wallJumpAnimation));
            animationController.currentAnimation = idleAnimation;

            UpdateColliders();
        }

        public void Update(Rectangle[] levelColliders)
        {
            #region Input

            if (IsKeyDown(KeyboardKey.KEY_A) || 
                IsGamepadButtonDown(GamepadNumber.GAMEPAD_PLAYER1, GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_LEFT))
                physAtts.velocity.x -= moveSpeed;

            if (IsKeyDown(KeyboardKey.KEY_D) || 
                IsGamepadButtonDown(GamepadNumber.GAMEPAD_PLAYER1, GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_RIGHT))
                physAtts.velocity.x += moveSpeed;

            if (IsKeyPressed(KeyboardKey.KEY_SPACE) ||
                IsGamepadButtonPressed(GamepadNumber.GAMEPAD_PLAYER1, GamepadButton.GAMEPAD_BUTTON_RIGHT_FACE_DOWN))
            {
                if (grounded)
                {
                    physAtts.velocity.y = -jumpSpeed;
                    initialJump = true;
                }
                else if (canWallJumpLeft)
                {
                    physAtts.velocity.x = wallJumpForce;
                    physAtts.velocity.y = -jumpSpeed;
                    initialJump = true;
                }
                else if (canWallJumpRight)
                {
                    physAtts.velocity.x = -wallJumpForce;
                    physAtts.velocity.y = -jumpSpeed;
                    initialJump = true;
                }
            }

            if (initialJump && physAtts.velocity.y < 0 && (IsKeyReleased(KeyboardKey.KEY_SPACE) ||
                IsGamepadButtonReleased(GamepadNumber.GAMEPAD_PLAYER1, GamepadButton.GAMEPAD_BUTTON_RIGHT_FACE_DOWN)))
            {
                physAtts.velocity.y /= 2;
                initialJump = false; //TODO ensure the player cannot start fast falling when they're not supposed to
            }

            #endregion

            #region Physics

            physAtts.Update(grounded);
            //Prevents velocity from exceeding the maximum
            if (physAtts.velocity.y > physAtts.maxVelocity.y) physAtts.velocity.y = physAtts.maxVelocity.y;
            if (physAtts.velocity.x < -physAtts.maxVelocity.x) physAtts.velocity.x = -physAtts.maxVelocity.x;
            if (physAtts.velocity.x > physAtts.maxVelocity.x) physAtts.velocity.x = physAtts.maxVelocity.x;
            //Adds the velocity to the player's position
            pos += physAtts.velocity;
            //Adjust colliders to their new positions
            UpdateColliders();
            //Check for collision and correct physics accordingly
            CheckPhysicsColliders(levelColliders);

            #endregion

            #region Animation

            animationController.transitions[0].condition = (Math.Abs(physAtts.velocity.x) <= 0.3f && grounded); //Idle
            animationController.transitions[1].condition = (Math.Abs(physAtts.velocity.x) > 0.3f && grounded); //Run
            animationController.transitions[2].condition = (physAtts.velocity.y < 0 && !grounded); //Jump
            animationController.transitions[3].condition = (physAtts.velocity.y > 0 && !grounded); //Fall

            if (canWallJumpLeft) //Left Wall Jump
            {
                flipX = false;
                animationController.transitions[4].condition = true;
            }
            else if (canWallJumpRight) //Right Wall Jump
            {
                flipX = true;
                animationController.transitions[4].condition = true;
            }
            else
            {
                animationController.transitions[4].condition = false;
                if (physAtts.velocity.x < 0) flipX = true;
                if (physAtts.velocity.x > 0) flipX = false;
            }

            #endregion
        }

        public void UpdateColliders()
        {
            collisionRecs[0] = new Rectangle(pos.x, pos.y, size.x, size.y);             // Center
            collisionRecs[1] = new Rectangle(pos.x + 4, pos.y - 1, size.x - 8, 1);      // Top
            collisionRecs[2] = new Rectangle(pos.x + 4, pos.y + size.y, size.x - 8, 1); // Bottom
            collisionRecs[3] = new Rectangle(pos.x + 1, pos.y + 4, 1, size.y - 8);      // Left
            collisionRecs[4] = new Rectangle(pos.x + size.x - 2, pos.y + 4, 1, size.y - 8); // Right
            collisionRecs[5] = new Rectangle(pos.x + 4, pos.y + size.y, size.x - 8, 2); // Ground Check (floor)
            collisionRecs[6] = new Rectangle(pos.x - 1, pos.y + 4, 2, size.y - 8);      // Left Wall Jump
            collisionRecs[7] = new Rectangle(pos.x + size.x - 1, pos.y + 4, 2, size.y - 8); // Right Wall Jump
        }

        public void CheckPhysicsColliders(Rectangle[] otherRecs)
        {
            grounded = false;
            canWallJumpLeft = false;
            canWallJumpRight = false;

            for (int i = 0; i < otherRecs.Length; i++)
            {
                if (physAtts.velocity.y < 0 && CheckCollisionRecs(collisionRecs[1], otherRecs[i])) //Top
                {
                    pos.y += (otherRecs[i].y + otherRecs[i].height + 1) - pos.y;
                    physAtts.velocity.y = 0;
                }
                if (physAtts.velocity.y > 0 && CheckCollisionRecs(collisionRecs[2], otherRecs[i])) //Bottom
                {
                    pos.y -= ((pos.y + size.y) - otherRecs[i].y);
                    physAtts.velocity.y = 0;
                }
                if (CheckCollisionRecs(collisionRecs[3], otherRecs[i])) //Left
                {
                    pos.x++;
                    physAtts.velocity.x = 0;
                }
                if (CheckCollisionRecs(collisionRecs[4], otherRecs[i])) //Right
                {
                    pos.x--;
                    physAtts.velocity.x = 0;
                }
                if (CheckCollisionRecs(collisionRecs[5], otherRecs[i])) //Ground Check
                    grounded = true;
                if (!grounded && CheckCollisionRecs(collisionRecs[6], otherRecs[i])) //Left Wall Jump Check
                    canWallJumpLeft = true;
                if (!grounded && CheckCollisionRecs(collisionRecs[7], otherRecs[i])) //Right Wall Jump Check
                    canWallJumpRight = true;
            }
        }

        public void Draw(Vector2 cameraOffset, Vector2 screenOffset)
        {
            animationController.Update();
            animationController.Draw(new Vector2(pos.x - 1, pos.y - 2), tint, flipX, flipY, size / 2, 0, cameraOffset, screenOffset);
        }
        public void DrawColliders()
        {
            Color[] debugColors = new Color [] { Color.GRAY, Color.RED, Color.BLUE, Color.YELLOW, Color.GREEN, Color.BLANK, Color.BLANK, Color.BLANK };
            for (int i = 0; i < collisionRecs.Length; i++)
                DrawRectangle((int)collisionRecs[i].x, (int)collisionRecs[i].y, (int)collisionRecs[i].width, (int)collisionRecs[i].height, debugColors[i]);
        }
    }
}
