using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp_UI : MonoBehaviour
{
    private Entity entity;
    private RectTransform rectTransform;
    private Slider slider;

    private void Start()
    {
        entity =GetComponentInParent<Entity>();
        rectTransform = GetComponent<RectTransform>();
        slider = GetComponentInChildren<Slider>();

        entity.OnFlip += flip;
        entity.stats.OnHealthChange += UpdateHp;

        UpdateHp();
    }



    public void UpdateHp()
    {
        slider.maxValue = entity.stats.GetTotalHealth();
        slider.value = entity.stats.currentHealth;
    }
    public void flip()
    {
        rectTransform.Rotate(0,180,0);
    }

    private void OnDisable()
    {
        entity.OnFlip -= flip;
        entity.stats.OnHealthChange-= UpdateHp;
    }
}
