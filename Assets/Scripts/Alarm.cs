using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BoxCollider2D))]
public class Alarm : MonoBehaviour
{
    [Range(0.1f, 1f)][SerializeField] private float _minVolume;
    [Range(0.1f, 1f)][SerializeField] private float _maxVolume;
    [SerializeField] private float _alarmVolumeSwingPeriod;
    
    private AudioSource _audio;
    private Coroutine _alarm;
    
    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Thief>(out var thief))
        {
            _alarm = StartCoroutine(ChangeVolumeCoroutine(_alarmVolumeSwingPeriod));
            
            _audio.Play();
        }
        
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.TryGetComponent<Thief>(out var thief))
        { 
            StopCoroutine(_alarm);
            
            _audio.Stop();
        }

       
    }

    private IEnumerator ChangeVolumeCoroutine(float time)
    {
        _audio.volume = _maxVolume;
        var isSwingMiddlePassed = false;

        while (true)
        {
            if (_audio.volume == _minVolume)
            {
                isSwingMiddlePassed = true;
            }
            if (_audio.volume == _maxVolume)
            {
                isSwingMiddlePassed = false;
            }
            _audio.volume = Mathf.Clamp(_audio.volume + (isSwingMiddlePassed ? Time.deltaTime : -Time.deltaTime) * (_maxVolume - _minVolume) / time, _minVolume,
                _maxVolume);
            yield return new WaitForEndOfFrame();
        }
    }
}

