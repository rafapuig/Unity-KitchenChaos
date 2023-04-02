using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{   

    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private Image barImage;

    private void Start() {
        //cuttingCounter.OnProgressChanged.AddListener(n);
        barImage.fillAmount = 0f;
        Hide();
    }

    public void CuttingCounter_OnProgressChanged(float progressNormalized) { 
        barImage.fillAmount = progressNormalized;

        if (progressNormalized == 0f || progressNormalized == 1f) {
            Hide();
        } else {
            Show();
        }
            
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() { 
        gameObject.SetActive(false);
    }

}
