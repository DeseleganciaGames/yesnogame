using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBase : MonoBehaviour
{

    public string inputName = "1P";
    public bool facingRight = true;
    public bool movingUp = true;
    public Vector2 yMoveLength = new Vector2(-2, 2);
    public int hp = 10;
    public GameObject projectile;
    public float chargeToShoot = 0.1f;
    public Slider hpSlider;
    public Slider chargeSlider;
    public float speed = 3;

    private int _hp;
    private float _chargeToShoot;
    private bool _isAlive = true;
    private Vector3 _startPos;

    void Start()
    {
        _hp = hp;
        hpSlider.maxValue = _hp;
        hpSlider.value = _hp;
        _startPos = transform.position;
    }

    void Update()
    {
        if (!_isAlive || MyGameManager.instance.gameOver)
            return;
        if (Input.GetButton(inputName))
        {
            _chargeToShoot += Time.deltaTime;
            _chargeToShoot = (_chargeToShoot > chargeToShoot) ? chargeToShoot : _chargeToShoot;
            chargeSlider.value = _chargeToShoot / chargeToShoot;
            ApplyMove();
        }
        if (Input.GetButtonUp(inputName))
        {
            if (_chargeToShoot >= chargeToShoot)
            {
                GameObject go = Instantiate(projectile, transform.GetChild(0).position, Quaternion.identity);
                go.GetComponent<Projectile>().SetDirection(facingRight);
            }
            _chargeToShoot = 0;
            chargeSlider.value = 0;
        }
    }

    private void ApplyMove()
    {
        var direction = (movingUp) ? Vector3.up : Vector3.down;
        transform.Translate(direction * speed * Time.deltaTime);
        if (transform.position.y >= yMoveLength.y || transform.position.y <= yMoveLength.x)
            movingUp = !movingUp;
    }

    public void ApplyDamage(int dmg)
    {
        _hp -= dmg;
        hpSlider.value = _hp;
        Debug.Log("DAMAGE " + dmg + " hp " + _hp);
        if (_hp <= 0)
        {
            hpSlider.value = 0;
            _isAlive = false;
            MyGameManager.instance.GameOver();
        }
    }
}
