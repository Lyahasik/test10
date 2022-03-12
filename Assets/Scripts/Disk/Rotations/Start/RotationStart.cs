using System.Collections;
using UnityEngine;

[RequireComponent(typeof(DiskProperties))]
[RequireComponent(typeof(IDiskRotation))]
public abstract class RotationStart : MonoBehaviour, IRotationStart
{
    protected IDiskRotation _iDiskRotation;
    
    protected float _duration;
    protected float _finalSpeed;
    protected float _currentSpeed;
    
    protected bool _isActive;
    protected float _delta;
    protected float _timeNext;

    private void Awake()
    {
        _isActive = false;
    }

    private void Start()
    {
        _iDiskRotation = (IDiskRotation) GetComponent(typeof(IDiskRotation));
        
        _duration = GetComponent<DiskProperties>().GetDurationStart();
        _finalSpeed = GetComponent<DiskProperties>().GetSpeedRotation();
    }

    void Update()
    {
        Rotation();
    }

    public void Switch()
    {
        _isActive = !_isActive;
        
        _currentSpeed = 0.0f;
        _timeNext = Time.time + _duration;
    }

    protected virtual void Rotation()
    {
        if (_isActive)
        {
            _delta = 1 - (_timeNext - Time.time) / _duration;

            _currentSpeed = Mathf.Lerp(0, _finalSpeed, _delta);

            if (_currentSpeed >= _finalSpeed)
            {
                transform.Rotate(Vector3.forward * _finalSpeed * Time.deltaTime);

                _isActive = false;
                _iDiskRotation.Switch();
                
                return;
            }

            transform.Rotate(Vector3.forward * _currentSpeed * Time.deltaTime);
        }
    }
}