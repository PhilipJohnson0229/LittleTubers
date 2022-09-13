using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPlayer : MonoBehaviour, IDamageable
{

    private float gravity = -30f;

    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private float jumpForce = 5.0f;

    [SerializeField]
    private int jumpSound, enemyJumpSound;

    [SerializeField]
    private int health;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private Vector3 direction;

    [SerializeField]
    private CharacterController characterController;

    public UIManager uiManager;

    public PlayerData playerData;

    [SerializeField]
    int IDamageable.health { get; set; }

    void Start()
    {
        health = playerData.ReturnHealth();
        uiManager.UpdateLives(health);
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if (characterController.isGrounded) 
        {
            direction = new Vector3(0, 0, h) * _speed;
        }

        

        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
        {

            Jump();

        }

        direction.y += gravity * Time.deltaTime;

        characterController.Move(direction * Time.deltaTime);
    }

    public void Jump()
    {
        AudioManager.instance.PlaySoundEffects(jumpSound);
        direction.y = 0;
        direction.y += jumpForce;
        direction.x = 0;
    }

   

    public void Damage(int amount)
    {
        playerData.Damage(amount);
        health = playerData.ReturnHealth();
        
        uiManager.UpdateLives(health);

        int randNum = Random.Range(1, 3);

        AudioManager.instance.PlaySoundEffects(randNum);
        //anim.SetBool("MouthOpen", true);
        //anim.SetInteger("FaceAnim", randNum);
        if (health < 1)
        {
            playerData.SendToHell();
        }
    }

    public void SendToHell()
    {
        uiManager.LoadNextLevel(6);
    }


    public void Heal()
    {
        playerData.setHealth(6);
        health = playerData.ReturnHealth();
        AudioManager.instance.PlaySoundEffects(32);
        uiManager.UpdateLives(health);
    }

    private void OnEnable()
    {
        playerData.playerDied += SendToHell;

    }

    private void OnDisable()
    {
        playerData.playerDied -= SendToHell;

    }
}
