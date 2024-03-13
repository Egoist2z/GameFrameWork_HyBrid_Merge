using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LodingForm : MonoBehaviour
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Text sliderText;

    [SerializeField]
    private CanvasGroup group;


    private void Start()
    {
        slider.maxValue = 1;
        slider.value = 0;
    }

    public void ResetForm()
    {
        group.alpha = 0;
        slider.gameObject.SetActive(false);
    }
    
    public void SetLodingState(string str)
    {
        group.alpha = 1;
        text.text = str;
    }    
    public void SetProgressBar(float value,string text)
    {        
        slider.value = value;
        sliderText.text = text;
        if (!slider.IsActive())
        {
            slider.gameObject.SetActive(true);
        }        
    }
}
