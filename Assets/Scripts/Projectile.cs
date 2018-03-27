using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int power = 1;
    public float speed = 3;
    private bool _facingRight = true;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (MyGameManager.instance.gameOver)
            Destroy(gameObject);
        var direction = (_facingRight) ? Vector3.right : Vector3.left;
        transform.Translate(direction * Time.deltaTime * speed);
    }

    public void SetDirection(bool facingRight)
    {
        _facingRight = facingRight;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerBase>().ApplyDamage(power);
            Destroy(gameObject);
        }
        else
        {
            Projectile enemy = collision.transform.GetComponent<Projectile>();
            if(enemy.power>power)
            {
                enemy.CalculateDamage(power);
                Destroy(gameObject);
            }
            else if(enemy.power == power)
            {
                Destroy(enemy.gameObject);
                Destroy(gameObject);
            }
        }
    }

    void CalculateDamage(int dmg)
    {
        power -= dmg;
        if (power <= 0)
            Destroy(gameObject);
    }
}
