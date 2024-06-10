using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BouncyMushroom : MonoBehaviour
{
    WasOnBeatTester beatTester;
    void Awake()
    {
        beatTester = GetComponent<WasOnBeatTester>();
    }
    // Start is called before the first frame update
    void Start()
    {
        beatTester.OnSuccess += turnOnAntiGravity; 
        beatTester.OnFailure += failureFunction; 

    }
    void OnDestroy()
    {
        beatTester.OnSuccess -= turnOnAntiGravity;
        beatTester.OnFailure -= failureFunction;
    }

    void failureFunction()
    {
        print("failure");
    }

    void turnOnAntiGravity()
    {
        print("Gravity turned off");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        beatTester.Interact();
        
    }
    
}
