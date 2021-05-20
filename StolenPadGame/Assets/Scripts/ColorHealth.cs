using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ColorHealth : MonoBehaviour
{
    public enum ColorStages
    {
        StageOne = 0,
        StageTwo = 1,
        StageThree = 2,
    }

    public ColorStages colorStages;

    [Space]
    [Header("Colors Switch Stages")]
    [SerializeField] private Color colorStageOne;
    [SerializeField] private Color colorStageTwo;
    [SerializeField] private Color colorStageThree;

    [Space]
    [SerializeField] private float switchColorDuration = 0.5f;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();    
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            transform.GetComponent<MeshRenderer>().material.DOColor(colorStageTwo, 2f);
        }
    }

    public void SwitchColor(bool state)
    {
        if(state == true)
        {
            switch (colorStages)
            {
                case ColorStages.StageOne:
                    //Change Current Color to ColorStageTwo 
                    meshRenderer.material.DOColor(colorStageTwo, switchColorDuration);
                    break;
                case ColorStages.StageTwo:
                    //Change Current Color to ColorStageThree 
                    meshRenderer.material.DOColor(colorStageThree, switchColorDuration);
                    break;
                case ColorStages.StageThree:
                    //GameOver tell the GameManger that you lost

                    break;
                default:
                    break;
            }
        }
    }

    public  void ResetColors()
    {
        colorStages = ColorStages.StageOne;

    }
}
