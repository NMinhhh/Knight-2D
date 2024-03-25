using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private StatsBar healthBar;
    [SerializeField] private StatsBar exBar;
    private float target;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.image.fillAmount = 1;
        exBar.image.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        target = currentHealth / maxHealth;
        StartCoroutine(UpdateBar(healthBar.image, target));
    }

    public void SetValueEx()
    {
        exBar.image.fillAmount = 0;
    }

    public void UpdateEx(float currentEx)
    {
        target = currentEx / GameManager.Instance.maxEx;
        exBar.image.fillAmount = target;
        
    }

    IEnumerator UpdateBar(Image image, float target)
    {
        float time = 0;
        float decreaseTime = 0.25f;
        while(time < decreaseTime)
        {
            time += Time.deltaTime;
            image.fillAmount = Mathf.Lerp(image.fillAmount, target, time / decreaseTime);
            yield return null;
        }
    }

   
}
