using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float cooldown = 1f;
    private float cooldownTimer = 0f;
    public Animator animator;
    public Animator animator2;
    public Animator animator3;
    private bool enemyDetected;
    private GameObject enemy;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > cooldownTimer)
        {
            if (enemyDetected == true && enemy != null)
                enemy.gameObject.SetActive(false);
            cooldownTimer = cooldown + Time.time;
            animator.SetTrigger("Attack");
            animator2.SetTrigger("Attack");
            animator3.SetTrigger("Attack");
        }
    }

	private void OnTriggerEnter(Collider other)
	{
        if (other.CompareTag("Enemy"))
		{
            enemyDetected = true;
            enemy = other.gameObject;
        }        
        else
		{
            enemyDetected = false;
            enemy = null;
        }
    }
}
