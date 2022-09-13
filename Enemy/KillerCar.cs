using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerCar : Enemy
{
    private Rigidbody rb;
    public int carHorn;
    public float speed;
    public GameObject[] carModels;
    [SerializeField]
    private float playerDistance;
    bool honkedHorn = false;
    public bool isOpenRoad = false;
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        SetCar();
        
    }
   

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (player != null)
        {
            playerDistance = transform.position.z - player.transform.position.z;
        }
        
        if (!honkedHorn) 
        {
            if (Mathf.Abs(playerDistance) < 2)
            {
                AudioManager.instance.PlaySoundEffects(carHorn);
                honkedHorn = true;
            }
        }
        
    }

    void SetCar() 
    {
        foreach (GameObject car in carModels) 
        {
            car.SetActive(false);
        }

        int randNum = Random.Range(0, carModels.Length - 1);

        carModels[randNum].SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player)) 
        {

            if (player != null) 
            {
                player.Damage(1);
            }
        }

        if (other.TryGetComponent<GPlayer>(out var gPlayer))
        {

            if (gPlayer != null)
            {
                gPlayer.Damage(1);
            }
        }

        if (other.TryGetComponent<Blamo>(out var blamo))
        {

            if (blamo != null)
            {
                
                CarManager.carDefeated += blamo.Defeat;
                CarManager.carDefeated();

                Destroy(gameObject);
            }
        }
    }

  
}
