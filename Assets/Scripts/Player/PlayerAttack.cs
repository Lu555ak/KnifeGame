using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float cooldown = 2f;
    private float cooldownTimer = 0f;
    public Animator animator;
    public Animator animator2;
    public Animator animator3;
    private bool enemyDetected;
    private Collider enemy;

	private void Start()
	{
        animator.gameObject.SetActive(false);
        animator2.gameObject.SetActive(false);
        animator3.gameObject.SetActive(false);
        if (PlayerPrefs.GetInt("KnifeSelection") == 0)
            animator.gameObject.SetActive(true);
        else if (PlayerPrefs.GetInt("KnifeSelection") == 1)
            animator2.gameObject.SetActive(true);
        else if(PlayerPrefs.GetInt("KnifeSelection") == 2)
            animator3.gameObject.SetActive(true);
    }

	private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > cooldownTimer)
        {
            if (enemyDetected == true && enemy != null)
			{
                enemy.gameObject.GetComponent<EnemyMovement>().DeathEffect();
                LevelHandler.enemyCount++;
            }
            cooldownTimer = cooldown + Time.time;
            animator.SetTrigger("Attack");
            animator2.SetTrigger("Attack");
            animator3.SetTrigger("Attack");
            GetComponent<AudioSource>().Play();
        }
    }

	private void OnTriggerEnter(Collider other)
	{
        if (other.CompareTag("Enemy"))
		{
            enemyDetected = true;
            enemy = other;
        }        
        else
		{
            enemyDetected = false;
            enemy = null;
        }
    }
}
