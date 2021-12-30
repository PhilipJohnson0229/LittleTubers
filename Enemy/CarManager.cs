using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    public Light trafficLight;

    public Transform[] spawnPoints;

    public GameObject carPrefab, defeatedPrefab;

    private bool carIsActive = false;

    private bool carHasCrashed;

    [SerializeField]
    private bool playerIsCloseEnough;

    [SerializeField]
    private float playerDistance;

    public float countDownTimer, countDownSetTime;
    
    [SerializeField]
    private Player thePlayer;

    public delegate void onDefeated();
    public static onDefeated carDefeated;




    public Quaternion carRot;
    private void OnEnable()
    {
        countDownTimer = countDownSetTime;
        carDefeated += ClearPath;
        SpawnCar(Random.Range(0, spawnPoints.Length - 1));
    }

    private void Start()
    {
        thePlayer = GameObject.FindObjectOfType<Player>();    


    }

    // Update is called once per frame
    void Update()
    {

     
        if (!carHasCrashed || thePlayer != null) 
        {
            playerDistance = Mathf.Abs(this.transform.position.z - thePlayer.transform.position.z);


            if (playerDistance <= 6 && playerDistance < 7)
            {
                playerIsCloseEnough = true;
            }
            else { playerIsCloseEnough = false; }


            if (playerIsCloseEnough && countDownTimer > 0 && !carIsActive)
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

    public void ClearPath() 
    {
        defeatedPrefab.SetActive(true);
        carHasCrashed = true;
    }
}
