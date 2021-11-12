using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _yOffset = 1f;
    [SerializeField]
    private float _xOffset = 1f;
    [SerializeField]
    private float _smoothTime = 0.3F;
    // Start is called before the first frame update
    void Start()
    {
        _target = GameObject.Find("Player").transform;
        if (_target == null) 
        {
            Debug.LogError("No target found for camera");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 _targetPosition = new Vector3(transform.position.x + _xOffset, _target.position.y + _yOffset, _target.position.z);
        // Smoothly move the camera towards that target position
        transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _smoothTime);
    }
}
