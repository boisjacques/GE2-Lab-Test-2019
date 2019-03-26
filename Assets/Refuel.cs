using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refuel : SteeringBehaviour
{
    public GameObject BaseGameObject;

    public Vector3 BasePosition; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override Vector3 Calculate()
    {
        return boid.ArriveForce(BasePosition, 3);
    }
}
