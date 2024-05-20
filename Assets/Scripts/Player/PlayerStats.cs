using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        StartCoroutine(UpdateBar(healthBar.image, target));
        healthBar.text.text = $"{target * 100}%";
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
