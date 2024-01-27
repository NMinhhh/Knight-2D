using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private StatsBar healthBar;
    private float target;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.image.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        target = currentHealth / maxHealth;
        StartCoroutine(UpdateBar());
    }

    IEnumerator UpdateBar()
    {
        float time = 0;
        float decreaseTime = 0.25f;
        while(time < decreaseTime)
        {
            time += Time.deltaTime;
            healthBar.image.fillAmount = Mathf.Lerp(healthBar.image.fillAmount, target, time / decreaseTime);
            yield return null;
        }
    }
}
