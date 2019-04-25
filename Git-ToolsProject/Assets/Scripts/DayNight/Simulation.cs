using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulation : MonoBehaviour {

    public Hours hours;
    Light _light;
    float currentStepAmount = 0f;
    int index = 0;

    private void Awake()
    {
        _light = GetComponent<Light>();
    }

    // Use this for initialization
    void Start () {

        _light.color = hours.timeOfDays[index]._colorAtTime;
        _light.gameObject.transform.rotation = Quaternion.Euler(hours.timeOfDays[index]._timeAngle);

	}
	
	// Update is called once per frame
	void Update () {

        if (currentStepAmount >= hours.timeBetweenHours)
        {
            index++;
            if(index >= hours.timeOfDays.Capacity)
            {
                index = 0;
            }
            //_light.color = hours.timeOfDays[index]._colorAtTime;
            currentStepAmount = 0.1f;
        }

        currentStepAmount += Time.deltaTime;

        _light.color = (hours.timeOfDays[(index+1 >= hours.timeOfDays.Capacity)? 0 : index + 1]._colorAtTime - 
            hours.timeOfDays[index]._colorAtTime) *
            (currentStepAmount / hours.timeBetweenHours) +
            hours.timeOfDays[index]._colorAtTime;

        _light.gameObject.transform.rotation = Quaternion.Euler(
            (hours.timeOfDays[(index + 1 >= hours.timeOfDays.Capacity) ? 0 : index + 1]._timeAngle -
            hours.timeOfDays[index]._timeAngle) *
            (currentStepAmount / hours.timeBetweenHours) +
            hours.timeOfDays[index]._timeAngle);
    }
}
