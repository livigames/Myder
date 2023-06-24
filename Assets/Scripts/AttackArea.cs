using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask attackable;

    private CameraShake cameraShake;


    void Start()
    {
        GetComponent();

        if (cameraShake != null)
        {
            Debug.Log("camera shake null");
        }
    }

    void GetComponent()
    {
        cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
    }

    void Update()
    {


        if (Input.GetMouseButtonDown(1))
        {
            AttackArean();
        }
    }


    public void AttackArean()
    {
        Collider2D[] hitenemy = Physics2D.OverlapCircleAll(attackPoint.position, distance, attackable);

        foreach (Collider2D enemy in hitenemy)
        {
            Enemy enemyHealt = enemy.GetComponent<Enemy>();
            if (enemyHealt != null)
            {
                enemyHealt.TakeDamage(20);

            }
            cameraShake.Shake();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(attackPoint.position, distance);
    }
}
