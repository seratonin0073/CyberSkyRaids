using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class FPSCounter : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    float FPS = 0;

    // Update is called once per frame
    private void Awake()
    {
        InvokeRepeating("Setewf", 0, 0.08f);
    }

    void Update()
    {
        FPS = Mathf.Ceil(1f / Time.deltaTime);
               
    }

    void Setewf()
    {
        text.text = "FPS: " + FPS;
    }
}
