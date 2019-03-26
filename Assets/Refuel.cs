using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refuel : SteeringBehaviour
{
    public GameObject baseGameObject;

    public Vector3 basePosition; 
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
        return boid.ArriveForce(basePosition, 7);
    }
}
