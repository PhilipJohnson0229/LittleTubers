using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField]
    private GameObject respawnPoint;

    [SerializeField]
    private UIManager UIManager;

    Player player;

    [SerializeField]
    Vector3 posSnapShot, goal, calculation;
   

    void Start() 
    {
        UIManager = GameObject.FindObjectOfType<UIManager>();
        player = FindObjectOfType<Player>();
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.TryGetComponent<Player>(out var player)) 
        {
            if (player != null)
            {
                goal = new Vector3(this.player.transform.position.x, this.player.transform.position.y + 5f, this.player.transform.position.z);

                //create a method to subtract a life
                if (player.playerData.isInHell() == false)
                {
                    player.PitFall();

                    StartCoroutine(PitFallCam(.7f));

                }
                else
                {
                    player.HellSpawn();

                    StartCoroutine(PitFallCam(.7f));
                }

            }
            
        }

        if (other.TryGetComponent<Blamo>(out var blamo))
        {
            if (blamo != null)
            {
                //create a method to subtract a life
                blamo.gameObject.SetActive(false);
            }
        }
    }

    public void TradeLifeForRespawn() 
    {
        
        if (player != null) 
        {
            
            player.transform.position = respawnPoint.transform.position;

            player.gameObject.SetActive(true);

            player.transform.parent = null;

            Blamo blamo = GameObject.FindObjectOfType<Blamo>();

            if (blamo != null) 
            {
                blamo.SetTrackedTarget(player.transform);
            }

            RushingBlamo rBlamo = GameObject.FindObjectOfType<RushingBlamo>();

            if (rBlamo != null) 
            {
                rBlamo.SetTrackedTarget(player.transform);
            }

            Berserker[] berserkers = GameObject.FindObjectsOfType<Berserker>();

            for (int i = 0; i < berserkers.Length; i++)
            {
                berserkers[i].HuntPlayer(player);
            }

            SharkTrap[] sTraps = GameObject.FindObjectsOfType<SharkTrap>();

            for (int j = 0; j < sTraps.Length; j++)
            {
                sTraps[j].ResetTrap();
            }


        }
        
    }

    public void SetSpawnPoint(GameObject newSpawnPoint)
    {
        respawnPoint = newSpawnPoint;
        if(UIManager != null) 
        {
            UIManager.Notification("Checkpoint");
        }
    }

    public void SetPlayer(Player thePlayer) 
    {
        player = thePlayer;
    }

  

    IEnumerator PitFallCam(float time)
    {
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            player.transform.position = Vector3.Lerp(player.transform.position, goal, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

    }
}
