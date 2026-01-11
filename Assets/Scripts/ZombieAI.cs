using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    [Header("Hız Ayarları")]
    public float walkSpeed = 2f;
    public float initialRunSpeed = 5f;
    public float maxRunSpeed = 12f;
    public float accelerationRate = 0.5f;

    [Header("Konumlar ve Hedefler")]
    public Transform[] patrolPoints;
    public Transform player;

    [Header("Görüş Ayarları")]
    public float maxSightRange = 15f;
    public float backSightRange = 5f;
    public float viewAngle = 60f;
    public float attackRange = 2f;

    [Header("Karakter Etkileşimi")]
    public AudioSource playerAudioSource;
    public Animator camera;
    public MonoBehaviour playerControlScript;
 
    [Header("Ses Ayarları")]
    public AudioSource zombieAudioSource;
    public AudioClip[] moanSounds; // AudioSource dizisi değil, AudioClip dizisi olmalı

    private bool isAttacking = false;
    private NavMeshAgent agent;
    private Animator anim;
    private int currentPointIndex = 0;
    private float currentChaseSpeed;
    private float moanTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        currentChaseSpeed = initialRunSpeed;

        GoToNextPoint();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        bool canSeePlayer = CheckSight(distanceToPlayer);
        bool playerInAttackRange = distanceToPlayer < attackRange;

        // --- HAREKET VE SALDIRI MANTIĞI ---
        if (playerInAttackRange) AttackPlayer();
        else if (canSeePlayer) ChasePlayer();
        else if (distanceToPlayer <= backSightRange) LookAtPlayer();
        else { currentChaseSpeed = initialRunSpeed; Patrol(); }

        // --- SES MANTIĞI ---
        HandleMoaning(distanceToPlayer, canSeePlayer);

        anim.SetFloat("Speed", agent.velocity.magnitude);
    }

    void HandleMoaning(float distance, bool isChasing)
    {
        moanTimer -= Time.deltaTime;

        // Oyuncu belirli bir mesafedeyse ve inleme zamanı geldiyse
        if (distance < maxSightRange * 3f && moanTimer <= 0f)
        {
            if (moanSounds.Length > 0)
            {
                int randomIndex = Random.Range(0, moanSounds.Length);
                zombieAudioSource.PlayOneShot(moanSounds[randomIndex]);

                // İnleme aralığını belirle
                float minWait = isChasing ? 2f : 5f; // Kovalarken en az 2sn, gezerken 5sn
                float maxWait = isChasing ? 5f : 8f; // Kovalarken en fazla 5sn, gezerken 15sn
                moanTimer = Random.Range(minWait, maxWait);
            }
        }
    }

    // --- MEVCUT DİĞER FONKSİYONLARIN (Patrol, Chase, Attack vb.) DEĞİŞMEDEN KALABİLİR ---
    
    bool CheckSight(float distance)
    {
        if (distance <= maxSightRange)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            float angleBetween = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleBetween < viewAngle) return true;
        }
        return false;
    }

    void Patrol()
    {
        agent.speed = walkSpeed; 
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GoToNextPoint();
    }

    void GoToNextPoint()
    {
        if (patrolPoints == null || patrolPoints.Length == 0) return;
        agent.destination = patrolPoints[currentPointIndex].position;
        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
    }

    void LookAtPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; 
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 4f);
    }

    void ChasePlayer()
    {
        if (currentChaseSpeed < maxRunSpeed) currentChaseSpeed += accelerationRate * Time.deltaTime;
        agent.speed = currentChaseSpeed; 
        agent.destination = player.position;
        Vector3 lookPos = player.position;
        lookPos.y = transform.position.y;
        transform.LookAt(lookPos);
    }

    void AttackPlayer()
    {
        playerControlScript.enabled = false;    
        camera.enabled = true;
        camera.SetBool("dead", true);
        playerAudioSource.Play();
        agent.destination = transform.position; 
        transform.LookAt(player); 
        if (!isAttacking)
        {
            isAttacking = true;
            anim.SetTrigger("Attack");
            Invoke("ResetAttack", 1.5f); // Animasyon süresine göre ayarla
        }
    }
private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, backSightRange);
        Gizmos.color = Color.yellow;
        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, leftBoundary * maxSightRange);
        Gizmos.DrawRay(transform.position, rightBoundary * maxSightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    void ResetAttack() { isAttacking = false; }
}