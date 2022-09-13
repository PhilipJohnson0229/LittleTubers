using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[CreateAssetMenu(fileName = "PlayerData.asset", menuName = "Data/Player")]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    private int health, maxHealth = 6;

    [SerializeField]
    private int coins;
    
    [SerializeField]
    private bool inHell, died, hasKey;
    
    [SerializeField]
    private float speed = 5.0f, gravity = 15.0f, jumpForce = 5.0f, girneyTime = 40.0f;

    public int jumpSound, enemyJumpSound;

    public GameObject hellSplat;

    public event Action changed;
    public event Action playerDied;
    public event Action destinationReached;
    public event Action ressurrected;

    public void SendToHell()
    {
        playerDied?.Invoke();
        this.setDied(true);
    }

    public int ReturnHealth()
    {
        if (this.isInHell() == true) 
        {
            this.setHealth(0);
        }
        return health;
    }

    public void Damage(int amount) 
    {
        setHealth(ReturnHealth() - amount);

        if (ReturnHealth() < 0) 
        {
            setHealth(0);
        }
    }

    public void CountDown() 
    {
        setGirneyTime(getGirneyTime() - Time.deltaTime);

        if (getGirneyTime() <= 0) 
        {
            destinationReached?.Invoke();
            setGirneyTime(0);
        }
    }

    public void PayToll() 
    {
        ressurrected?.Invoke();

        this.setCoins(0);

        this.setInHell(false);

        this.setHealth(6);
    }
    
    public void SplatEffect(Vector3 position) 
    {
        Instantiate(hellSplat, position, Quaternion.identity);
    }


    public void setHealth(int health)
    {
        this.health = health;
    }


    public int getMaxHealth()
    {
        return maxHealth;
    }


    public void setMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
    }


    public int getCoins()
    {
        return coins;
    }


    public void setCoins(int coins)
    {
        this.coins = coins;
    }


    public int getJumpSound()
    {
        return jumpSound;
    }


    public void setJumpSound(int jumpSound)
    {
        this.jumpSound = jumpSound;
    }


    public int getEnemyJumpSound()
    {
        return enemyJumpSound;
    }


    public void setEnemyJumpSound(int enemyJumpSound)
    {
        this.enemyJumpSound = enemyJumpSound;
    }


    public bool isInHell()
    {
        return inHell;
    }


    public void setInHell(bool inHell)
    {
        this.inHell = inHell;
    }


    public bool isDied()
    {
        return died;
    }


    public void setDied(bool died)
    {
        this.died = died;
    }


    public bool isHasKey()
    {
        return hasKey;
    }


    public void setHasKey(bool hasKey)
    {
        this.hasKey = hasKey;
    }


    public float getSpeed()
    {
        return speed;
    }


    public void setSpeed(float speed)
    {
        this.speed = speed;
    }


    public float getGravity()
    {
        return gravity;
    }


    public void setGravity(float gravity)
    {
        this.gravity = gravity;
    }


    public float getJumpForce()
    {
        return jumpForce;
    }


    public void setJumpForce(float jumpForce)
    {
        this.jumpForce = jumpForce;
    }


    public float getGirneyTime()
    {
        return girneyTime;
    }


    public void setGirneyTime(float girneyTime)
    {
        this.girneyTime = girneyTime;
    }

}
