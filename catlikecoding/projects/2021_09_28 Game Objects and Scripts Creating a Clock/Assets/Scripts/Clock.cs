using System;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] private Transform hourPivot, minutesPivot, secondsPivot;

    private const float hourToDegress = -30f, minutesToDegress = -6f, secondsToDegress = -6f;

    private void Update()
    {
        TimeSpan time = DateTime.Now.TimeOfDay;
        hourPivot.localRotation = Quaternion.Euler(0f, 0f, hourToDegress * (float)time.TotalHours);
        minutesPivot.localRotation = Quaternion.Euler(0f, 0f, minutesToDegress * (float)time.TotalMinutes);
        secondsPivot.localRotation = Quaternion.Euler(0f, 0f, secondsToDegress * (float)time.TotalSeconds);
    }
}