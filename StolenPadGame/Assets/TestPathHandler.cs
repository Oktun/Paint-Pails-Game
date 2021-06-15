using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.DemiLib;
using DG.Tweening;

public class TestPathHandler : MonoBehaviour
{
    public DOTweenPath path;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKey(KeyCode.X))
        {
            path.DOPlay();
        }
    }
}
