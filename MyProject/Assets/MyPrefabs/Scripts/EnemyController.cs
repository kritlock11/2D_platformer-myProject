using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{   //Stats
    public float speed;
    public float burstSpeed;
    public float normalSpeed;

    public bool moovingLeft = true;
    public float currentHealth;
    public int maxHealth;
    //Status /buff/deBuff
    public bool poisonEnemyStatus;
    //Ground Detection
    public Transform groundDetector;
    public Transform wallDetector;
    public float wallDetectionDistance;
    public LayerMask whatIsWall; //Wall is map
    // PlayerDetection
    public Transform playerChase;
    private float distanseEnemyToPlayer;


    Rigidbody2D rigidbody2d;


    public LayerMask whatIsPlayer; //Player is player
    public float playerDetectionDistanceFront;
    public float playerDetectionDistanceBack;

    //meele attack
    public float meleeDamage;
    public float timeBtwDamage;
    private float timeBtwDamageCurrent;

    //ramge attack
    public float launchDamage;
    public float timeBtwLaunch;
    private float timeBtwLaunchCurrent;



    // slider hp bar
    public Slider healthBar;
    public Image hpFill;
    public Image hpBack;

    //Healing
    public int bottleHeal;

    private Vectors distansToPlayer;


    // Emoji
    public Transform emojiPos;
    public GameObject emojiImage;

    //loot
    public GameObject loot;

    //States
    bool inCombatState;

    // Projectile launch
    public GameObject projectilePrefab;
    public Transform projectilePos;

    //jumping


    void Start()
    {
        speed = normalSpeed;
        currentHealth = maxHealth;
        timeBtwDamageCurrent = timeBtwDamage;
        timeBtwLaunchCurrent = timeBtwLaunch;
        rigidbody2d = GetComponent<Rigidbody2D>();
        distansToPlayer = GetComponent<Vectors>();
        playerChase = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        Physics2D.queriesStartInColliders = false;
    }

    private void Update()
    {
        if (timeBtwDamageCurrent > 0) timeBtwDamageCurrent -= Time.deltaTime;
        if (timeBtwLaunchCurrent > 0) timeBtwLaunchCurrent -= Time.deltaTime;

        healthBar.value = currentHealth;
        //healthBar.maxValue = currentHealth;
    }

    void FixedUpdate()
    {

        PlayerDetection();
    }
    void EmojiSpawn()
    {
        GameObject emoji = Instantiate(emojiImage, emojiPos.position, transform.rotation);
    }


    //void RetreatDistance() // на завтра
    //{
    //    if (distansToPlayer.distansePE <= 4)
    //    {

    //        speed = burstSpeed;
    //        transform.position = Vector2.MoveTowards(transform.position, playerChase.transform.position, -speed * Time.deltaTime);
    //    }
    //}





    void LookDirectionChange()
    {
        if (moovingLeft == true)
        {
            transform.eulerAngles = new Vector2(0, -180);
            moovingLeft = false;
        }
        else
        {
            transform.eulerAngles = new Vector2(0, 0);
            moovingLeft = true;
        }
    }
    void Launch()
    {
        if (timeBtwLaunchCurrent <= 0)
        {
            if (moovingLeft)
            {
                GameObject projectileObject = Instantiate(projectilePrefab, projectilePos.position, Quaternion.Euler(0, 180, 0));
                Projectile projectile = projectileObject.GetComponent<Projectile>();
                projectile.Launch(Vector2.left, 400);
            }
            else if (!moovingLeft)
            {
                GameObject projectileObject = Instantiate(projectilePrefab, projectilePos.position, Quaternion.Euler(0, 0, 0));
                Projectile projectile = projectileObject.GetComponent<Projectile>();
                projectile.Launch(Vector2.right, 400);
            }
            timeBtwLaunchCurrent = timeBtwLaunch;
        }
    }
    void AttackOnDistance()
    {
        if (distansToPlayer.distansePE <= 1 * 1)
        {
            if (timeBtwDamageCurrent <= 0)
            {
                playerChase.gameObject.GetComponent<PlayerController>().currentHealth -= meleeDamage;
                timeBtwDamageCurrent = timeBtwDamage;
                EmojiSpawn();
            }
            if (playerChase.gameObject.GetComponent<PlayerController>().currentHealth <= 0)
            {
                playerChase.gameObject.GetComponent<PlayerController>().Die();
            }
        }
    }
    void GroundDetection()
    {
        speed = normalSpeed;
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetector.position, Vector2.down, wallDetectionDistance, whatIsWall);
        Debug.DrawLine(groundDetector.position, groundDetector.position - transform.up * wallDetectionDistance, Color.yellow);
        RaycastHit2D wallInfo = Physics2D.Raycast(wallDetector.position, Vector2.left, wallDetectionDistance, whatIsWall);
        Debug.DrawLine(wallDetector.position, wallDetector.position - transform.right * wallDetectionDistance, Color.yellow);
        Debug.DrawLine(transform.position, transform.position - transform.right * playerDetectionDistanceFront, Color.green);
        Debug.Log($"wallInfo-{wallInfo.collider},groundInfo- {groundInfo.collider}");
        if (wallInfo.collider == true || groundInfo.collider == false)
        {
            LookDirectionChange();
        }

    }
    void PlayerDetection()
    {
        RaycastHit2D playerInfoFront = Physics2D.Raycast(transform.position, -transform.right, playerDetectionDistanceFront, whatIsPlayer);
        RaycastHit2D playerInfoBack = Physics2D.Raycast(transform.position, transform.right, playerDetectionDistanceBack, whatIsPlayer);

        if (playerInfoFront.collider != null)
        {
            Debug.DrawLine(transform.position, playerInfoFront.point, Color.red);

            if (playerInfoFront.collider.CompareTag("Player") && distansToPlayer.distansePE < 4 * 4)
            {
                speed = burstSpeed;
                transform.position = Vector2.MoveTowards(transform.position, playerInfoFront.collider.transform.position, speed * Time.deltaTime);
                AttackOnDistance();
            }
            else if (playerInfoFront.collider.CompareTag("Player") && distansToPlayer.distansePE < 5 * 5)
            {
                speed = 0;
                Launch();
            }
            else
            {
                GroundDetection();
            }
        }
        else if (playerInfoBack.collider != null)
        {
            Debug.DrawLine(transform.position, playerInfoBack.point, Color.red);

            if (playerInfoBack.collider.CompareTag("Player") && distansToPlayer.distansePE < 3 * 3)
            {
                speed = burstSpeed;
                LookDirectionChange();
                transform.position = Vector2.MoveTowards(transform.position, playerInfoBack.collider.transform.position, speed * Time.deltaTime);
                AttackOnDistance();
            }
        }
        else GroundDetection();
    }
    //Normal Damage
    public void TakeNormalDamage(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        SpriteRenderer enemyBodyColor = rigidbody2d.gameObject.GetComponent<SpriteRenderer>();
        enemyBodyColor.color = Color.yellow;
        Debug.Log(currentHealth + "/" + maxHealth);
        if (currentHealth <= 0) Die();
    }
    //Backstab Damage
    public void TakeBackstabDamage(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount * 2, 0, maxHealth);
        var re = rigidbody2d.gameObject.GetComponent<SpriteRenderer>();
        re.color = Color.red;

        Debug.Log(currentHealth + "/" + maxHealth);

        if (currentHealth <= 0) Die();

    }
    //Damage over time
    public void TakePoisonDamage(int amount, int damageTime)
    {
        StartCoroutine(TakePoisonDamageCorotine(amount, damageTime));
    }
    IEnumerator TakePoisonDamageCorotine(float damageamount, float dutation)
    {
        poisonEnemyStatus = true;
        var enemySprite = rigidbody2d.gameObject.GetComponent<SpriteRenderer>();
        enemySprite.color = Color.green;
        hpFill.color = new Color(75, 96, 16, 225);
        hpBack.color = Color.gray;


        float amountDamaded = 0;
        float damagePerLoop = damageamount / dutation;
        while (amountDamaded < damageamount || amountDamaded <= currentHealth)
        {
            currentHealth = Mathf.Clamp(currentHealth - damagePerLoop, 0, maxHealth);
            amountDamaded += damagePerLoop;
            yield return new WaitForSeconds(1f);
            Debug.Log(currentHealth + "/" + maxHealth + "/" + amountDamaded + "/" + dutation);
            if (currentHealth <= 0)
            {
                Die();
            }
        }
        poisonEnemyStatus = false;
        enemySprite.color = Color.white;
        Debug.Log($"poisonEnemyStatus = {poisonEnemyStatus}");
    }

    public void Die()
    {
        playerChase.transform.GetComponent<PlayerController>().EmojiSpawn();
        Destroy(gameObject);
        healthBar.gameObject.SetActive(false);
        Instantiate(loot, transform.position, transform.rotation);

    }
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(groundDetector.position, wallDetectionDistance);
    //}
}
