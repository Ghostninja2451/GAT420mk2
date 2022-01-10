using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutonomusAgent : Agent
{
    [SerializeField] Perception perception;
    [SerializeField] Steering steering;

    public float maxSpeed;
    public float maxForce;

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
            Vector3 force = steering.Flee(this, gameObjects[0]);
            acceleration += force;
        }

        velocity += acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        transform.position += velocity * Time.deltaTime;
        if(velocity.sqrMagnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(velocity);
        }
    }
}
