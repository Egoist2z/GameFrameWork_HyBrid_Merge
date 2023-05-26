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
        if (group.alpha == 0)
        {
            group.DOFade(1, 1f);
        }
        text.text = str;
    }    
    public void SetProgressBar(float value,string text)
    {        
        slider.value = value;
        sliderText.text = (value * 100).ToString();
        if (!slider.IsActive())
        {
            slider.gameObject.SetActive(true);
        }        
    }
}
