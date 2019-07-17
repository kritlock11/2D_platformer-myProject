using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rigidbody2d;
    public bool poisonWeaponStatus;
    private SpriteRenderer spritePlayerWeapon;
    public Sprite[] weaponSprites = new Sprite[2];



    private bool isfeetDust;
    public ParticleSystem feetDust;
    public ParticleSystem poofUp;
    public ParticleSystem poofDown;
    public ParticleSystem dash;

    public ParticleSystem hpPotion;
    public ParticleSystem poisonPotion;

    private ResurseCounterController potions;

    public float speed;
    public int maxHealth;
    public Slider healthBar;
    public float currentHealth;

    bool isInvincible;
    public float timeInvincible;
    float invincibleTimer;

    private bool isGrounded;
    public float jumpForce;
    public float doubleJumpForce;
    
    public Transform feetPos;
    public Transform projectilePos;
    public Transform ghostDashPos;

    //jumping
    public float checkRadius;
    public LayerMask whatIsGround;
    private float jumpTimerCounter;
    public float jumpTime;
    private bool isJumping = false;
    private int extraJumps;
    public int extraJumpsValue;



    //dash 
    private float DushTimerCounter;
    public float DushTime;
    public bool isDushing = false;
    public float dashSpeed;
    public float startDashtime;
    private int direction;
    EnemyController dashThruEnemy;


    //projectilePrefab
    public GameObject projectilePrefab;
    //emoji
    public Transform emojiPos;
    public GameObject emojiImage;
    //UI counters
    public float KeyCounter = 0;
    public float hpPotionCounter = 0;
    public float poisonPotionCounter = 0;
    public float coinCounter = 0;
    //Healing
    public int HealOverTimeAmount;
    public int HealOverTimePeriod;

    //easy Flip
    //private bool facingRight = true;
    //void Flip()
    //{
    //    facingRight = !facingRight;
    //    Vector2 scaler = transform.localScale;
    //    scaler.x *= -1;
    //    transform.localScale = scaler;
    //}




    void Start()
    {
        //EmojiSpawn();

        currentHealth = maxHealth;
        extraJumps = extraJumpsValue;
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        dashThruEnemy = GetComponent<EnemyController>();
        spritePlayerWeapon = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {




        Moving();
        Dushing();
        InstantiateMoveJumpParticles();


        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isRuning", true);
        }
        else
        {
            animator.SetBool("isRuning", false);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
            animator.SetBool("isAttacking", true);
        else
        {
            animator.SetBool("isAttacking", false);

        }





        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

    }




    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            currentHealth -= 5;
            Debug.Log($"healStatus = {currentHealth}/ {maxHealth}");
        } // Debug currentHealth -= 5;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Launch();
        } // Launch();
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        } // Restart Scene
        if (Input.GetKeyDown(KeyCode.H))
        {
            if ((currentHealth < maxHealth) && hpPotionCounter > 0)
            {
                TakeHealOverTime(HealOverTimeAmount, HealOverTimePeriod);
                hpPotionCounter--;
            }
        }

        if (Input.GetKeyDown(KeyCode.P) && poisonPotionCounter>0)
        {
            spritePlayerWeapon = transform.GetChild(4).GetChild(0).GetComponent<SpriteRenderer>();
            poisonWeaponStatus = !poisonWeaponStatus;
            spritePlayerWeapon.sprite = poisonWeaponStatus == true ? weaponSprites[1] : weaponSprites[0];
            poisonPotionCounter--;
            Debug.Log($"poisonWeaponStatus == {poisonWeaponStatus}");
        }


        //Slider HealthBar
        HpBarCurrentHealth();



        Jumping();
    }



    void Moving()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 V = new Vector2(horizontal * speed, rigidbody2d.velocity.y);

        rigidbody2d.velocity = V;
        if (!Mathf.Approximately(V.x, 0.0f))
        {
            animator.SetBool("idleLeft", true);
            if (V.x < 0)
            {
                transform.eulerAngles = new Vector2(0, 180);
                direction = 3;
            }
            else
            {
                transform.eulerAngles = new Vector2(0, 0);
                direction = 4;
            }
        }
    }

    void Dushing()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isDushing = true;
            DushTimerCounter = DushTime;
            if (direction == 3)
            {
                rigidbody2d.velocity = Vector3.left * dashSpeed;
            }

            else if (direction == 4)
            {
                rigidbody2d.velocity = Vector3.right * dashSpeed;
            }
        }

        if (Input.GetKey(KeyCode.LeftShift) && isDushing)
        {

            if (DushTimerCounter > 0)
            {
                dash.Play();

                if (direction == 3)
                {
                    rigidbody2d.velocity = Vector3.left * dashSpeed;
                }

                else if (direction == 4)
                {

                    rigidbody2d.velocity = Vector3.right * dashSpeed;
                }
                DushTimerCounter -= Time.deltaTime;
            }
            else
            {
                isDushing = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isDushing = false;
        }

    }

    void Jumping()
    {
        Debug.Log($"{extraJumps}, {extraJumpsValue}");
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if (isGrounded) extraJumps = extraJumpsValue;
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimerCounter = jumpTime;
            rigidbody2d.velocity = Vector2.up * jumpForce;
            poofUp.Play();
        }
        if (Input.GetKey(KeyCode.Space) && isJumping )
        {
            if (jumpTimerCounter > 0)
            {
                rigidbody2d.velocity = Vector2.up * jumpForce;
                jumpTimerCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && jumpTimerCounter > 0 && extraJumps > 0)
        {
            rigidbody2d.velocity = Vector2.up * doubleJumpForce;
            extraJumps--;
        }
    }

    public void TakeNormalDamage(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
        if (currentHealth <= 0) Die();
    }
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, projectilePos.position, transform.rotation);

        Projectile projectile = projectileObject.GetComponent<Projectile>();

        if (direction == 3)
        {
            projectile.Launch(Vector2.left, 400);
        }

        else if (direction == 4)
        {
            projectile.Launch(Vector2.right, 400);
        }

    }

    public void EmojiSpawn()
    {
        GameObject emoji = Instantiate(emojiImage, emojiPos.position, transform.rotation);
    }

    public void Die()
    {
        Destroy(gameObject);
        healthBar.gameObject.SetActive(false);

    }
    void HpBarCurrentHealth()
    {
        healthBar.value = currentHealth;
    }

    public void TakeHealOverTime(int amount, int healTime)
    {
        StartCoroutine(TakeHealOverTimeCorotine(amount, healTime));
    }
    IEnumerator TakeHealOverTimeCorotine(float healamount, float dutation)
    {
        float amountHealed = 0;
        float HealPerLoop = healamount / dutation;
        while (amountHealed < healamount)
        {
            currentHealth = Mathf.Clamp(currentHealth + HealPerLoop, 0, maxHealth);
            amountHealed += HealPerLoop;
            Debug.Log(currentHealth + "/" + maxHealth + "/" + amountHealed + "/" + dutation);
            yield return new WaitForSeconds(1f);
        }
    }
    void InstantiateMoveJumpParticles()
    {
        // walking particles
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && isGrounded) feetDust.Play();
        else feetDust.Stop();
        // jumping particles
        if (isGrounded)
        {
            if (isfeetDust)
            {
                poofDown.Play();
                isfeetDust = false;
            }
        }
        else isfeetDust = true;
    }













}
