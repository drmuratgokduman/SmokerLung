using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class compareLungController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("UI")]
    [SerializeField] private Slider slider;
    [Header("Lung")]
    [SerializeField] private GameObject solLob;
    [SerializeField] private GameObject hastaSolLob;
    [SerializeField] private GameObject akciger;
    [Header("Animator")]
    [SerializeField] private Animator animator;

    private Renderer solLobRenderer;

    private float sliderValue;
    // Start is called before the first frame update
    void Start()
    {
        if (solLob != null && slider != null)
        {
            solLobRenderer = solLob.GetComponent<Renderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(slider.value);
        if (slider.value < 50)
        {
            if (solLobRenderer != null)
            {
                float normalizedValue = (slider.value) / 100.0f;

                Color newColor = Color.Lerp(Color.white, Color.black, normalizedValue);
                solLobRenderer.material.SetColor("_Color", newColor);
                solLob.transform.localScale = new Vector3(1, 1, 1);
            }
            Invoke("SliderValueChanged", 0.5f);
        }
        if (slider.value > 50)
        {
            float scaleCalculate = (slider.value - 50) / 200;
            solLob.transform.localScale = new Vector3(1 - scaleCalculate, 1 - scaleCalculate, 1 - scaleCalculate);
            Invoke("SliderValueChanged", 0.5f);
        }
        if (animator != null)
        {
            float normalizedValue = (slider.value) / 100.0f;
            animator.SetFloat("AnimSpeed", (1.5f - normalizedValue) / 1.5f);
        }
    }
    void SliderValueChanged()
    {
        sliderValue = slider.value;
    }
}
