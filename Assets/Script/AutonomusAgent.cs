using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutonomusAgent : Agent
{
    [SerializeField] Perception perception;
    public Vector3 velocity { get; set; } = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        Vector3 acceleration = Vector3.zero;

        GameObject[] gameObjects = perception.GetGameObjects();
        if(gameObjects.Length !=0 )
        {
            Debug.DrawLine(transform.position, gameObjects[0].transform.position);

            //Vector3 force = gameObjects[0].transform.position - transform.position;
            Vector3 force =  transform.position - gameObjects[0].transform.position ;
            acceleration += force.normalized * 3;
        }

        velocity += acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
    }
}
