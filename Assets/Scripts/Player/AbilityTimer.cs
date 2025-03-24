using UnityEngine;
using UnityEngine.UI;

public class AbilityTimer : MonoBehaviour
{
    public Image image;

    public bool recharging = true;

    public float TimerActual { get; set; } = 30f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        recharging = !(image.fillAmount >= 1.0f);
        if (recharging == false)
        {
            image.fillAmount = 1.0f;
            image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else
        {
            image.fillAmount += Time.deltaTime / TimerActual;
            image.color = new Color(0.6f, 0.6f, 0.6f, 1.0f);
        }
    }

    public void TriggerAbility()
    {
        recharging = true;
        image.fillAmount = 0f;
    }
}