using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GhostMovement : MonoBehaviour {
    
    //Components needed to be set in editor
    [SerializeField] private Rigidbody2D thisRigidbody;
    
    //Game balance Variables
    [SerializeField] private float visionRange;
    [SerializeField] private float speed;
    [SerializeField] private float MoveTimeBase = 1.0f; //how long before changing to other type of movement
    [SerializeField] private float MoveTimeReal;
    //Process and status variables
    private bool chasing; //being in vision range of the player means ghost will chase
    private Vector2 moveDirection;
    private float timer;
    private Transform player;

    private void Start() {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update() {
        //As a first step the ghost enemy is idle until the player gets inside the vision range
        if(!chasing && (player.position - transform.position).magnitude < visionRange) {
            chasing = true;
            ChooseDirection(); //A direction is chosen in relation to the player
            timer = 0;
        }
        //Timer is always running, but the reset when a new direction is chosen allows to check
        //enough time has passed before starting a new move in a new direction
        timer += Time.deltaTime;
        if (timer >= MoveTimeReal) {
            chasing = false;
        }
    }

    private void FixedUpdate() {
        if (chasing) { //As long as the chasing variable is true, the movement will continue.
            thisRigidbody.velocity = moveDirection.normalized * (speed * Time.deltaTime);    
        }
    }

    private void ChooseDirection() {
        int rand = Math.Clamp(Random.Range(0, 5), 1, 5); //A random direction is chosen
        //We start with a default movement, towards the player
        moveDirection = player.position - transform.position;
        Quaternion rot;
        switch (rand) {
            case 1:
                //default is kept
                MoveTimeReal = MoveTimeBase; 
                break;
            case 2:
                moveDirection *= -1; //Inversion of the default (back)
                MoveTimeReal = MoveTimeBase / 2;
                break;
            case 3:
                //Here we use a quaternion function to rotate the forward direction 90 degrees
                //making the enemy strafe to their left(or right im not sure)
                rot = Quaternion.AngleAxis(90, Vector3.back); 
                moveDirection = rot * moveDirection; 
                MoveTimeReal = MoveTimeBase / 3;
                break;
            case 4:
                //Same as previous but the other side (either left or right)
                rot = Quaternion.AngleAxis(-90, Vector3.back); 
                moveDirection = rot * moveDirection; 
                MoveTimeReal = MoveTimeBase / 3;
                break;
        }
        
    }
}
