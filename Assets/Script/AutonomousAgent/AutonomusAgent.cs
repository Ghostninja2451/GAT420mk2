using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutonomusAgent : Agent
{
    [SerializeField] Perception perception;
    [SerializeField] Perception flockperception;
    [SerializeField] ObsticlePerception obsticlePerception;
    [SerializeField] Steering steering;
    [SerializeField] AutomonousAgentData agentData;

    // Update is called once per frame
    void Update()
    {
        GameObject[] gameObjects = perception.GetGameObjects();

        //seek / flee
        if(gameObjects.Length !=0 )
        {

            //Vector3 force = gameObjects[0].transform.position - transform.position;
            movement.ApplyForce(steering.Seek(this, gameObjects[0]) * agentData.seekWeight);

            movement.ApplyForce(steering.Flee(this, gameObjects[0]) * agentData.fleeWeight);
           
        }
        // wander
        if (movement.acceleration.sqrMagnitude <= movement.maxForce * 0.1f)
        {
            movement.ApplyForce(steering.Wander(this)); 
        }
        //flocking
        gameObjects = flockperception.GetGameObjects();
        if(gameObjects.Length != 0)
        {
            movement.ApplyForce(steering.Cohesion(this, gameObjects) * agentData.cohesionWeight);
            movement.ApplyForce(steering.Seperation(this, gameObjects, agentData.separationRadius) * agentData.separationWeight);
            movement.ApplyForce(steering.Alignment(this, gameObjects) * agentData.alignmentWeight);
        }
        // obstacle avoidance
        if (obsticlePerception.IsObstacleInFront())
        {
            Vector3 direction = obsticlePerception.GetOpenDirection();
            movement.ApplyForce(steering.CalculateSteering(this, direction) * agentData.obstacleWeight);
        }

    }
}
