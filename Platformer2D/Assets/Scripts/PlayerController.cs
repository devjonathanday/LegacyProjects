using System.Collections;
using UnityEngine;
using XInputDotNetPure;
public class PlayerController : MonoBehaviour
{
    [Header("Physics")]
    public float moveSpeed;
    public float jumpSpeed;
    public float wallJumpSpeed;
    public float drag;
    public float defaultGravity;
    public float wallSlideGravity;
    public float maxFallSpeed;
    public float hurtKnockback;
    public Rigidbody2D rBody;
    public RigidbodyConstraints2D defaultConstraints;
    public TriggerVerify groundCheck;
    public TriggerVerify leftWallJumpCheck;
    public TriggerVerify rightWallJumpCheck;

    [Header("Variables")]
    public bool facingRight;
    public bool dead;
    public int gameOverDuration;

    [Header("Visuals/Animation")]
    public GameObject smokePuff;
    public float attackDuration;
    private float attackTimer;
    public float attackAnimDuration;
    private float attackAnimTimer;
    public bool invincible;
    public float invincibilityDuration;
    private float invincibilityTimer;
    public float hurtShakeIntensity;
    public float hurtShakeDuration;
    public float deathShakeIntensity;
    public float deathShakeDuration;

    [Header("References - Self")]
    public Animator animator;
    public SpriteRenderer sprite;
    public HitEffect slash;
    public ParticleSystem deathEffect;

    [Header("References - Transforms")]
    public Transform weaponAnchor;
    public Transform smokeAnchor;
    public Transform smokeSpawnPos;

    [Header("References - Prefabs")]
    public GameObject smokePrefab;
    public GameObject hurtEffectPrefab;

    [Header("References - Other")]
    public GameManager GM;
    public CameraController cameraController;

    [Header("Audio")]
    public AudioSource mainChannel;
    public AudioClip jumpSound;
    public AudioClip hurtSound;
    public AudioClip deathSound;

    [Header("Input")]
    public bool joystick;
    public float joystickDeadzone;
    public bool inputDisabled;

    private bool[] buttonPressed;
    //0 = jump pressed (A)
    //1 = attack       (X)

    void Start()
    {
        if (!rBody) rBody = GetComponent<Rigidbody2D>();
        buttonPressed = new bool[] { false, false };
        Application.targetFrameRate = 30;
        attackTimer = attackDuration;
        attackAnimTimer = attackAnimDuration;
        //GM.health = GM.maxHealth; //TODO uncomment before actual gameplay
        invincibilityTimer = invincibilityDuration;
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;

        #region Frame-Specific Variable Resets

        if (!dead && !GM.paused)
        {
            if (attackAnimTimer >= attackAnimDuration) animator.SetBool("Attacking", false);
            else attackAnimTimer += deltaTime;
            if (attackTimer < attackDuration) attackTimer += deltaTime;

            if (attackTimer >= attackDuration)
            {
                if (facingRight)
                {
                    sprite.flipX = false;
                    weaponAnchor.localScale = Vector3.one;
                    smokeAnchor.localScale = Vector3.one;
                }
                else
                {
                    sprite.flipX = true;
                    weaponAnchor.localScale = new Vector3(-1, 1, 1);
                    smokeAnchor.localScale = new Vector3(-1, 1, 1);
                }
            }

            if (invincible)
            {
                //If we still have invincibility frames
                sprite.enabled = !sprite.enabled;
                invincibilityTimer += deltaTime;
                //Set our invincibility to false if the timer is up
                if (invincibilityTimer >= invincibilityDuration) invincible = false;
                //If we are less than halfway through the invincibility frames
                if (invincibilityTimer < invincibilityDuration / 2)
                {
                    //Set the animator to Hurt animation, and disable input
                    animator.SetBool("Hurt", true);
                    inputDisabled = true;
                }
                else //if we are more than halfway through invincibility frames
                {
                    //Re-enable normal animations and input
                    animator.SetBool("Hurt", false);
                    inputDisabled = false;
                }
            }
            else
            {
                sprite.enabled = true;
                animator.SetBool("Hurt", false);
                inputDisabled = false;
            }
        }

        #endregion

        #region Input

        if (!inputDisabled && !dead && !GM.paused)
        {
            //Get the current state of controller 1
            GamePadState controllerState = GamePad.GetState(PlayerIndex.One);

            //Moving left and right, can use left stick OR D-Pad
            if (joystick ? controllerState.DPad.Left == ButtonState.Pressed ||
                           controllerState.ThumbSticks.Left.X < -joystickDeadzone : Input.GetKey(KeyCode.A)) Move(false);
            if (joystick ? controllerState.DPad.Right == ButtonState.Pressed ||
                           controllerState.ThumbSticks.Left.X > joystickDeadzone : Input.GetKey(KeyCode.D)) Move(true);

            //Jumping, first frame pressed
            if (joystick ? controllerState.Buttons.A == ButtonState.Pressed && !buttonPressed[0] : Input.GetKeyDown(KeyCode.Space))
            {
                buttonPressed[0] = true;
                Jump();
            }

            //Falling, first frame released
            if (joystick ? controllerState.Buttons.A == ButtonState.Released && buttonPressed[0] : Input.GetKeyUp(KeyCode.Space))
            {
                buttonPressed[0] = false;
                Fall();
            }

            //Attacking, first frame pressed
            if (joystick ? controllerState.Buttons.X == ButtonState.Pressed && !buttonPressed[1] : Input.GetKeyDown(KeyCode.E))
            {
                buttonPressed[1] = true;
                Attack();
            }

            //Resetting first-frame button inputs
            if (controllerState.Buttons.A == ButtonState.Released) buttonPressed[0] = false; //Jump
            if (controllerState.Buttons.X == ButtonState.Released) buttonPressed[1] = false; //Attack
        }

        #endregion

        #region Physics Updates

        if (!dead)
            rBody.velocity = new Vector2(rBody.velocity.x * drag, Mathf.Max(rBody.velocity.y, -maxFallSpeed));
        else rBody.constraints = RigidbodyConstraints2D.FreezeAll;

        #endregion

        #region Animations

        animator.SetInteger("YVelocity", Mathf.RoundToInt(rBody.velocity.y));

        #region WallJumping

        if (leftWallJumpCheck.isColliding && !groundCheck.isColliding)
        {
            animator.SetBool("WallSliding", true);
            if (!inputDisabled) facingRight = true;
            rBody.gravityScale = wallSlideGravity;
        }
        else if (rightWallJumpCheck.isColliding && !groundCheck.isColliding)
        {
            animator.SetBool("WallSliding", true);
            if (!inputDisabled) facingRight = false;
            rBody.gravityScale = wallSlideGravity;
        }
        else
        {
            animator.SetBool("WallSliding", false);
            rBody.gravityScale = defaultGravity;
        }

        #endregion

        #region Running

        if (rBody.velocity.x > 10)
        {
            animator.SetBool("Running", true);
            if (!inputDisabled) facingRight = true;
        }
        else if (rBody.velocity.x < -10)
        {
            animator.SetBool("Running", true);
            if (!inputDisabled) facingRight = false;
        }

        else animator.SetBool("Running", false);

        #endregion

        #endregion
    }

    #region InputFunctions

    void Move(bool right)
    {
        if (right)
            rBody.AddRelativeForce(Vector2.right * moveSpeed);
        else rBody.AddRelativeForce(-Vector2.right * moveSpeed);
    }
    void Jump()
    {
        if (groundCheck.isColliding)
        {
            rBody.velocity = new Vector2(rBody.velocity.x, jumpSpeed);
            mainChannel.PlayOneShot(jumpSound);
        }
        else if (leftWallJumpCheck.isColliding)
        {
            rBody.velocity = new Vector2(wallJumpSpeed, jumpSpeed);
            Instantiate(smokePrefab, smokeSpawnPos.position, Quaternion.identity);
            mainChannel.PlayOneShot(jumpSound);
        }
        else if (rightWallJumpCheck.isColliding)
        {
            rBody.velocity = new Vector2(-wallJumpSpeed, jumpSpeed);
            Instantiate(smokePrefab, smokeSpawnPos.position, Quaternion.identity);
            mainChannel.PlayOneShot(jumpSound);
        }
    }
    void Fall()
    {
        if (rBody.velocity.y > 0)
            rBody.velocity = new Vector2(rBody.velocity.x, rBody.velocity.y / 2);
    }
    void Attack()
    {
        if (attackTimer >= attackDuration)
        {
            attackTimer = 0;
            attackAnimTimer = 0;
            animator.SetBool("Attacking", true);
            slash.Play();
        }
    }

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            TakeDamage(collision.transform, collision.GetComponent<EnemyBehavior>().contactDamage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            TakeDamage(collision.transform, collision.GetComponent<EnemyBehavior>().contactDamage);
        }
    }

    public void TakeDamage(Transform other, int amount)
    {
        if (!invincible)
        {
            if (GM.health > 0)
            {
                GameObject hurtEffect = Instantiate(hurtEffectPrefab, transform.position, Quaternion.identity);
                hurtEffect.transform.parent = transform;
                cameraController.Shake(hurtShakeIntensity, hurtShakeDuration);
                mainChannel.PlayOneShot(hurtSound);
                GM.health -= amount;
            }

            if (GM.health <= 0 && !dead)
            {
                //Death Sequence
                GM.health = 0;
                dead = true;
                cameraController.StartCoroutine(cameraController.GameOverFade(gameOverDuration));
                StartCoroutine(DeathSequence());
                Collider2D[] selfColliders = GetComponents<Collider2D>();
                for (int i = 0; i < selfColliders.Length; i++) selfColliders[i].enabled = false;
                return;
            }

            //If we are to the left of the enemy
            if (transform.position.x < other.position.x)
            {
                rBody.velocity = new Vector2(-hurtKnockback, rBody.velocity.y);
                facingRight = true;
            }
            //Else if we are to the right of the enemy
            else
            {
                rBody.velocity = new Vector2(hurtKnockback, rBody.velocity.y);
                facingRight = false;
            }
            invincibilityTimer = 0;
            invincible = true;
            inputDisabled = true;
        }
    }

    public IEnumerator DeathSequence()
    {
        for (int i = 0; i < gameOverDuration; i++)
        {
            animator.SetBool("Hurt", true);
            sprite.enabled = !sprite.enabled;
            yield return null;
        }
        cameraController.Shake(deathShakeIntensity, deathShakeDuration);
        deathEffect.Play();
        mainChannel.PlayOneShot(deathSound);
        sprite.enabled = false;
    }
}