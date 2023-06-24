using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] private float destyorTime;

    void Start()
    {
        Destroy(gameObject, destyorTime);
    }

    void Update()
    {
        
    }
}
