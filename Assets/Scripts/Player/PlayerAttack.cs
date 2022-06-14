using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float cooldown = 1f;

    private float cooldownTimer = 0f;


    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonDown(0) && other.CompareTag("Enemy") && Time.time > cooldownTimer)
        {
            other.gameObject.SetActive(false);
            cooldownTimer = cooldown + Time.time;
        }
    }
}
