using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCircle : MonoBehaviour
{
    public static DayNightCircle Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    [SerializeField] private Light2D globalLight;
    [SerializeField] private Vector2 intensity;
    [Header("Day")]
    [SerializeField] private Vector2 dayTimer;
    private float currentDayTimer;

    [Header("Night")]
    [SerializeField] private Vector2 nightTimer;
    private float currentNightTimer;

    [Header("Rain")]
    [SerializeField] private ParticleSystem rainSystem;
    [SerializeField] private Vector2 rainDropTimer;
    [SerializeField] private Vector2 rainDropDelayTimer;
    private float currentRainDropTimer;
    private float currentRainDropDelayTimer;
    private bool isRain;
    public bool isNight {  get; private set; }
    public bool isChange;

    void Start()
    {
        currentDayTimer = Random.Range(dayTimer.x, dayTimer.y);
        currentNightTimer = Random.Range(nightTimer.x, nightTimer.y);
        currentRainDropDelayTimer = Random.Range(rainDropDelayTimer.x, rainDropDelayTimer.y);
        rainSystem.Stop();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isChange)
        {
            ChangeLight();
        }
        else
        {
            DayNight();
        }
        Rain();
       

        
    }

    void Rain()
    {
        if (isRain)
        {
            currentRainDropTimer -= Time.deltaTime;
            if (currentRainDropTimer <= 0)
            {
                var main = rainSystem.main;
                main.loop = false;
                currentRainDropDelayTimer = Random.Range(rainDropDelayTimer.x, rainDropDelayTimer.y);
                isRain = false;
            }
        }
        else
        {
            if (currentRainDropDelayTimer > 0)
            {
                currentRainDropDelayTimer -= Time.deltaTime;
            }
            else
            {
                var main = rainSystem.main;
                main.loop = true;
                rainSystem.Play();
                currentRainDropTimer = Random.Range(rainDropTimer.x, rainDropTimer.y);
                isRain = true;
            }
        }
        

    }

    void DayNight()
    {
        if (!isNight)
        {
            currentDayTimer -= Time.deltaTime;
            if (currentDayTimer <= 0)
            {
                currentDayTimer = Random.Range(dayTimer.x, dayTimer.y);
                isNight = true;
                isChange = true;
            }
        }
        else
        {
            currentNightTimer -= Time.deltaTime;
            if (currentNightTimer <= 0)
            {
                currentNightTimer = Random.Range(nightTimer.x, nightTimer.y);
                isNight = false;
                isChange = true;
            }
        }
    }

    void ChangeLight()
    {
        if (!isNight)
        {
            globalLight.intensity += Time.deltaTime;
            if(globalLight.intensity >= intensity.y)
            {
                globalLight.intensity = intensity.y;
                isChange = false;
            }
        }
        else
        {
            globalLight.intensity -= Time.deltaTime;
            if (globalLight.intensity <= intensity.x)
            {
                globalLight.intensity = intensity.x;
                isChange = false;
            }
        }
    }
    
}
