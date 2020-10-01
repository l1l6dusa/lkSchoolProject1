using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{

    [SerializeField] private float _alarmVolumeSwingPeriod;
    [Range(0.1f, 1f)][SerializeField] private float _maxVolume;
    [Range(0.1f, 1f)][SerializeField] private float _minVolume;
    private AudioSource audio;
    private Coroutine alarm;
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.volume = _maxVolume;
    }
    
    void Update() { }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Thief>(out var thief))
        {
            alarm = StartCoroutine(ChangeVolumeCoroutine(_alarmVolumeSwingPeriod));
            
            audio.Play();
        }
        
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.TryGetComponent<Thief>(out var thief))
        { 
            StopCoroutine(alarm);
            
            audio.Stop();
        }

       
    }

    private IEnumerator ChangeVolumeCoroutine(float time)
    {
       
        var isSwingMiddlePassed = false;
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            if (isSwingMiddlePassed == false)
            {
                if (timer < time)
                {
                    audio.volume = Mathf.Clamp(audio.volume - Time.deltaTime * (_maxVolume - _minVolume), _minVolume,
                        _maxVolume);
                    yield return new WaitForEndOfFrame();
                }

                if (audio.volume == _minVolume)
                {
                    isSwingMiddlePassed = true;
                    timer = 0;
                }
            }
            else
            {
                timer += Time.deltaTime;
                if (isSwingMiddlePassed == true)
                {
                    if (timer < time)
                    {
                        audio.volume = Mathf.Clamp(audio.volume + Time.deltaTime * (_maxVolume - _minVolume), _minVolume,
                            _maxVolume);
                        yield return new WaitForEndOfFrame();
                    }

                    if (audio.volume == _maxVolume)
                    {
                        isSwingMiddlePassed = false;
                        timer = 0;
                    }
                }
            }
        }
    }
}
