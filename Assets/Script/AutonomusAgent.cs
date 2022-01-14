using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutonomusAgent : Agent
{
    [SerializeField] Perception perception;
    [SerializeField] Perception flockperception;
    [SerializeField] Steering steering;
    [SerializeField] AutomonousAgentData agentData;

    public float maxSpeed { get { return agentData.maxSpeed; } }
    public float maxForce { get { return agentData.maxForce; } }

    public Vector3 velocity { get; set; } = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        Vector3 acceleration = Vector3.zero;

        GameObject[] gameObjects = perception.GetGameObjects();

        //seek / flee
        if(gameObjects.Length !=0 )
        {

            //Vector3 force = gameObjects[0].transform.position - transform.position;
            acceleration = steering.Seek(this, gameObjects[0]) * agentData.seekWeight;
            acceleration = steering.Flee(this, gameObjects[0]) * agentData.fleeWeight;
            

        }
        else
        {
            acceleration += steering.Wander(this);
        }
        //flocking
        gameObjects = flockperception.GetGameObjects();
        if(gameObjects.Length != 0)
        {
            acceleration += steering.Cohesion(this, gameObjects) * agentData.cohesionWeight;
            acceleration += steering.Seperation(this, gameObjects, agentData.separationRadius) * agentData.separationWeight;
            acceleration += steering.Alignment(this, gameObjects) * agentData.alignmentWeight;
        }
        
        velocity += acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        transform.position += velocity * Time.deltaTime;
        if(velocity.sqrMagnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(velocity);
        }

        transform.position = Utility.Wrap(transform.position, new Vector3(-10, -10, -10), new Vector3(10, 10, 10));

    }
}
