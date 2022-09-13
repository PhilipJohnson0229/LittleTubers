using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//we implement an interface simply by separating the parent class with a comma
//if you see an error thats because you have not implemented the require methods for IDamageable
public class GiantRat : Enemy, IDamageable
{
    public int health { get; set; }

    public Material playerMat;

    public Color playerColor;

    [SerializeField]
    Vector3 direction;
    public override void Init()
    {
        base.Init();
        health = base._health;
        _currentTarget = _pointA.position;
    }


    public override void Movement(){base.Movement();}
    public void Damage(int amount)
    {
        if (health < 1)
        {
            Vector3 _offset = new Vector3(0, 0.5f, 0);
            anim.SetTrigger("Death");
            _isDead = true;
            //casting
            GameObject _coinDrop = Instantiate(_coin, this.transform.localPosition + _offset, Quaternion.identity) as GameObject;
            //here we would put code to handle assigning multiple coins if we want to go that route
           // _coinDrop.GetComponent<Coin>()._amount = base._coinsAmount;
        }
        else 
        {
            if (_isDead == true) { return; }
            StartCoroutine(Blink(0.8f));
            health--;
            _isHit = true;
            anim.SetTrigger("Hit");
            anim.SetBool("InCombat", true);
            //UIManager _UIManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
           // _UIManager.Notification(this.name + " was damaged!");
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (!_isDead)
        {
            if (other.TryGetComponent<Player>(out var player))
            {
                player.Damage(1);
                anim.SetTrigger("Attack");
            }

            if (other.tag == "PlayerHurtBox")
            {
                if (!base.player.playerData.isInHell()) 
                {
                    Damage(1);
                }
                
                base.player.EnemyJump();
            }
        }   
    }

    private WaitForSeconds blinkPause = new WaitForSeconds(.05f);

    IEnumerator Blink(float timer)
    {
        bool blinking = false;
        float blinkingTimer = timer;

        while (blinkingTimer > 0)
        {
            blinkingTimer -= .1f;

            blinking = !blinking;

            if (blinking)
            {
                playerMat.color = Color.red;
            }
            else
            {
                playerMat.color = playerColor;
            }

            yield return blinkPause;
        }


        playerMat.color = playerColor;
        blinking = false;
    }

}
