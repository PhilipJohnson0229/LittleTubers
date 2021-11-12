using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField]
    private Vector3  _fromTopPosition, _fromPolarPosition, _standPos, _stepOffPos, _airPosition;
    [SerializeField]
    private GameObject _bottomExit, _topExit, _cover;

    void Start() 
    {
        CloseExits();
    }
    void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            //try and grab the character controller
            var _player = _other.GetComponent<Player>();

            if (_player != null)
            {
                _player.LadderCheck(this, true, false);
            }
        }
    }
    void OnTriggerExit(Collider _other) 
    {
        if (_other.CompareTag("Player"))
        {
            //try and grab the character controller
            var _player = _other.GetComponent<Player>();

            if (_player != null)
            {
                _player.LadderCheck(this, false, false);
            }
        }
    }

    public Vector3 EnterFromTopPos() 
    {
        return _fromTopPosition;
    }

    public Vector3 GetStandPos()
    {
        return _standPos;
    }

    public Vector3 GetStepDownPos()
    {
        return _stepOffPos;
    }

    public void SetAirPosition(float _x, float _y)
    {
        _airPosition.x = _x;
        _airPosition.y = _y;
        _airPosition.z = _fromPolarPosition.z;

    }

    public Vector3 MountLadderPos(bool _fromBase, bool _fromAir)
    {
        Vector3 _targetPosition = new Vector3(0, 0, 0);

        if (_fromBase && !_fromAir)
        {
            _targetPosition = _fromPolarPosition;
            _targetPosition.y += 0.5f;
        }
        else if (!_fromBase && _fromAir)
        {
            _targetPosition = _airPosition;
        }
       
        return _targetPosition;
    }

    public void OpenExits() 
    {
        _bottomExit.SetActive(true);
        _topExit.SetActive(true);
        _cover.SetActive(false);
    }

    public void CloseExits()
    {
        _bottomExit.SetActive(false);
        _topExit.SetActive(false);
        _cover.SetActive(true);
    }
}
