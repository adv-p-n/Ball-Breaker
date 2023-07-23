using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Bricks : MonoBehaviour
{
    [SerializeField] int row = 1;
    TextMeshPro brickText;
   
    void Awake()
    {
        brickText= GetComponentInChildren<TextMeshPro>();
    }
    void Start()
    {
        
    }

   
    void Update()
    {
        brickText.text = row.ToString();
    }
}
