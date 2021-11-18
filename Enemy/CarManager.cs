using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    public Light trafficLight;

    public Transform[] spawnPoints;

    public GameObject carPrefab;

    public bool carIsActive = false;

    public float countDownTimer, countDownSetTime;

    

    public Quaternion carRot;
    private void OnEnable()
    {
        countDownTimer = countDownSetTime;

        SpawnCar(Random.Range(0, spawnPoints.Length - 1));
    }

    // Update is called once per frame
    void Update()
    {
        if (countDownTimer > 0 && !carIsActive) 
        {
            countDownTimer -= Time.deltaTime;

            if (countDownTimer <= 0)
            {
                countDownTimer = countDownSetTime;
                
                SpawnCar(Random.Range(0, spawnPoints.Length - 1));
            }
        }

        if (carIsActive)
        {
            trafficLight.color = Color.green;
        }
        else 
        {
            trafficLight.color = Color.red;
        }
    }

    void SpawnCar(int randomNumber) 
    {
        carIsActive = true;
        GameObject killeraCar = Instantiate(carPrefab, spawnPoints[randomNumber].position, carRot) as GameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "KillerCar")
        {
            Destroy(other.gameObject);
            carIsActive = false;
        }
    }
}
