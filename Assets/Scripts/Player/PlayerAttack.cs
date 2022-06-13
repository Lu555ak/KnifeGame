using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float cooldown = 1f;

    private float cooldownTimer = 0f;

    private Transform groundCheck;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("lol");
        if (Input.GetMouseButtonDown(0) && other.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            cooldownTimer = cooldown + Time.time;
        }
    }
}
