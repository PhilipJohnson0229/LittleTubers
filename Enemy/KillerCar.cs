using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerCar : MonoBehaviour
{
    public Rigidbody rb;
    public int carHorn;
    public float speed;
    public GameObject[] carModels;
    private void OnEnable()
    {
        SetCar();
        AudioManager.instance.PlaySoundEffects(carHorn);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
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
        if (other.tag == "Player") 
        {
            Player player = other.GetComponent<Player>();

            if (player != null) 
            {
                player.Damage(1);
            }
        }
    }
}
