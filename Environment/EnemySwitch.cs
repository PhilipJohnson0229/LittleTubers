using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwitch : MonoBehaviour
{
    public GameObject topLevel, bottomLevel, currentLevel;

    public Player player;

    [SerializeField]
    private bool topLevelActive = true;

    [SerializeField]
    private List<GameObject> ragdolls;

    [SerializeField]
    private Vector3 relativePosition;

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    private void Update()
    {
        relativePosition = transform.position - player.transform.position;
        Switch();      
    }

    void Switch()
    {
        if (relativePosition.y > 1)
        {
            topLevelActive = false;
        }
        else if (relativePosition.y < -1) 
        {
            topLevelActive = true;
        }
        

        if (topLevelActive)
        {
            topLevel.SetActive(true);
            bottomLevel.SetActive(false);
            currentLevel = topLevel;
        }
        else 
        {
            topLevel.SetActive(false);
            bottomLevel.SetActive(true);
            currentLevel = bottomLevel;
        }
    }

    public void AddRagdolls() 
    {
        ragdolls.Clear();
        

        GameObject[] activeRagdolls = GameObject.FindGameObjectsWithTag("EventItem");

        foreach (GameObject ragdollObject in activeRagdolls) 
        {
            ragdolls.Add(ragdollObject);
        }

        SetRagdollParents();
    }

    void SetRagdollParents() 
    {
        foreach (GameObject ragdollObject in ragdolls) 
        {
            if (ragdollObject != null) 
            {
                ragdollObject.transform.parent = currentLevel.transform;
            }
        }
    }
}
