using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int healt = 100;
    public int currentHealt;

    Rigidbody2D rb;

    public Color hitColor = Color.white;
    private Color originalColor; // Orijinal renk
    private SpriteRenderer spriteRenderer;

    public GameObject hitEffect; // Vuru� efekti objesi

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealt = healt;

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealt -= damageAmount;

        spriteRenderer.color = hitColor; // Beyaz renkteki sprite kullan�l�yor
        Instantiate(hitEffect, transform.position, Quaternion.identity); // Vuru� efekti olu�turuluyor
        Invoke("ResetCharacter", 0.2f); // 0.1 saniye sonra karakterin rengi ve sprite'� resetleniyor

        if (currentHealt < 0) 
        {
            Die();
        }
    }

    private void ResetCharacter()
    {
        spriteRenderer.color = originalColor; // Orijinal renk uygulan�yor
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
