using UnityEngine;

public class EnemyHook : MonoBehaviour
{
    public float hookSpeed = 5f;
    public float hookReturnSpeed = 10f;
    public float hookCooldown = 3f;
    public float detectionDelay = 2f;
    public float releaseDistance = 1f;

    private EnemyMoveHook enemyMove;
    private Transform playerTransform;
    private bool isHookActive = false;
    private bool isPlayerCaught = false;
    private float hookTimer = 0f;
    private float detectionTimer = 0f;

    void Start()
    {
        enemyMove = GetComponentInParent<EnemyMoveHook>();
        playerTransform = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (isHookActive)
        {
            if (hookTimer <= hookCooldown)
            {
                hookTimer += Time.deltaTime;
                return;
            }

            if (isPlayerCaught)
            {
                playerTransform.position = Vector3.MoveTowards(playerTransform.position, transform.parent.position, hookSpeed * Time.deltaTime);

                if (Vector3.Distance(playerTransform.position, transform.parent.position) <= releaseDistance)
                {
                    isPlayerCaught = false;
                    isHookActive = false;
                    Debug.Log("Player released by the hook");
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, playerTransform.position) <= enemyMove.detectionRadius)
                {
                    detectionTimer += Time.deltaTime;
                    if (detectionTimer >= detectionDelay)
                    {
                        LaunchHook();
                    }
                }
                else
                {
                    detectionTimer = 0f;
                    if (Vector3.Distance(transform.position, transform.parent.position) > 0.1f)
                    {
                        isHookActive = false;
                    }
                }
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, transform.parent.position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, hookReturnSpeed * Time.deltaTime);
            }
            else
            {
                isHookActive = true;
                hookTimer = 0f;
            }
        }
    }

    void LaunchHook()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, hookSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player") && isHookActive && isPlayerCaught != true)
        {
            Debug.Log("I got you");
            isPlayerCaught = true;
        }
    }
}
