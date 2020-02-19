using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class MyTimer : MonoBehaviour
{
    public float restTime = 300.0f;
    public float workTime = 1500.0f;

    public Slider slider;
    public GameObject Pin;
    public GameObject StartText;
    public GameObject PauseText;
    public GameObject ResetButton;

    public Text CurrentTime;
    public Text TotalTime;
    public Text TimeMessage;

    float CurrentTimeValue = 0.0f;
    float CurrentTimeHour = 0;
    float CurrentTimeMin = 0;
    float CurrentTimeSec = 0;

    float TotalTimeValue = 0.0f;
    float TotalTimeHour = 0;
    float TotalTimeMin = 0;
    float TotalTimeSec = 0;

    bool isStart;
    bool isWork;

    void RotatePin()
    {
        if (!isStart)
        {
            return;
        }
        Pin.transform.Rotate(0, 0, -1 * (360 / (restTime + workTime)) * Time.deltaTime);

    }

    public void StartPauseToggleTimer()
    {
        if (isStart)
        {
            //끄는 것
            isStart = false;
            ResetButton.SetActive(true);
            PauseText.SetActive(false);
            StartText.SetActive(true);
        }
        else
        {
            //켜는 것
            isStart = true;
            ResetButton.SetActive(false);
            PauseText.SetActive(true);
            StartText.SetActive(false);
        }
    }

    public void ResetTimer()
    {
        InitTimer();
    }


    void InitTimer()
    {
        isStart = false;
        isWork = true;
        PauseText.SetActive(false);
        ResetButton.SetActive(false);
        slider.value = workTime / (restTime + workTime);
        Pin.transform.rotation = Quaternion.Euler(0, 0, 0);
        CurrentTimeValue = workTime;
        TotalTimeValue = 0.0f;
        CalcTime();
    }

    void SetTime()
    {
        if (!isStart)
        {
            return;
        }
        if (CurrentTimeValue < TotalTimeValue)
        {
            if (isWork)
            {
                isWork = false;
                CurrentTimeValue += restTime;
                TimeMessage.text = "휴식시간";
            }
            else
            {
                isWork = true;
                CurrentTimeValue += workTime;
                TimeMessage.text = "집중할 시간!";
            }
        }
        CalcTime();

        TotalTimeValue += Time.deltaTime;
    }

    void CalcTime()
    {
        CurrentTimeHour = (CurrentTimeValue - TotalTimeValue) / 3600;
        CurrentTimeMin = ((CurrentTimeValue - TotalTimeValue) % 3600) / 60;
        CurrentTimeSec = ((CurrentTimeValue - TotalTimeValue) % 3600) % 60;

        TotalTimeHour = TotalTimeValue / 3600;
        TotalTimeMin = (TotalTimeValue % 3600) / 60;
        TotalTimeSec = (TotalTimeValue % 3600) % 60;

        CurrentTime.text = string.Format("{0:D2} : {1:D2} : {2:D2}", (int)CurrentTimeHour, (int)CurrentTimeMin, (int)CurrentTimeSec);
        TotalTime.text = string.Format("{0:D2} : {1:D2} : {2:D2}", (int)TotalTimeHour, (int)TotalTimeMin, (int)TotalTimeSec);
    }

    // Start is called before the first frame update
    void Start()
    {
        InitTimer();

    }
    // Update is called once per frame
    void Update()
    {
        RotatePin();
        
        SetTime();

    }
}
