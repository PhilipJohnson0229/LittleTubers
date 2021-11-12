using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField]
    private GameObject _respawnPoint;

    [SerializeField]
    private bool _isRespawning;

    [SerializeField]
    private UIManager _UIManager;

    void Start() 
    {
        _UIManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player") 
        {
            Player _player = other.GetComponent<Player>();

            if (_player != null) 
            {
                //create a method to subtract a life
                _player.Damage(10);
            }

            CharacterController _cController = other.GetComponent<CharacterController>();

            if (_cController != null)
            {
                _cController.enabled = false;
            }

            other.transform.position = _respawnPoint.transform.position;

            //destroy player and force respawn
            StartCoroutine(Respawn(_cController));
        }
    }


    IEnumerator Respawn(CharacterController _controller) 
    {
        yield return new WaitForSeconds(0.5f);
        _controller.enabled = true;
    }

    public void SetSpawnPoint(GameObject newSpawnPoint)
    {
        _respawnPoint = newSpawnPoint;
        if(_UIManager != null) 
        {
            _UIManager.Notification("Checkpoint set at: " + newSpawnPoint.transform.position.ToString());
        }
    }


}
