using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private float lenghtTrace;
    private float minimum = 0;
    public float current;
    public Image mask;
    public Image fill;
    public Text percentText;
    public Color color;
    private float playerPoint;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    
    void Update()
    {

        GetCurrentFill();
        
    }

    void GetCurrentFill()
    {

        float currentOffset = playerPoint - minimum;
        float maximumOffset = lenghtTrace;
        float fillAmount = currentOffset / maximumOffset;
        mask.fillAmount = fillAmount;
        float percent = (currentOffset / lenghtTrace) * 100;
        int per = (int) percent;
        
        

        if(per > 100)
        {
            string PercentFormatted = 100 + "%";
            percentText.text = PercentFormatted;
        }

        else if (per < 0)
        {
            string PercentFormatted = 0 + "%";
            percentText.text = PercentFormatted;
        }
        
        else
        {
            string PercentFormatted = per + "%";
            percentText.text = PercentFormatted;
        }

        fill.color = color;
    } 
}
