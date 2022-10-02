using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DoorBehavior : MonoBehaviour {
    //Needed components - Set in inspector
    [SerializeField] private SpriteRenderer leftDoor;
    [SerializeField] private SpriteRenderer rightDoor;
    [SerializeField] private Sprite openLeft;
    [SerializeField] private Sprite openRight;
    [SerializeField] private Sprite closedLeft;
    [SerializeField] private Sprite closedRight;
    [SerializeField] private BoxCollider2D thisCollider;
    
    //process variables
    private float timer = 0;
    
    public void SetStatus(bool open) {
        if (open) {
            leftDoor.sprite = openLeft;
            rightDoor.sprite = openRight;
            thisCollider.isTrigger = true;
        }
        else {
            leftDoor.sprite = closedLeft;
            rightDoor.sprite = closedRight;
            thisCollider.isTrigger = false;
        }
    }

    public void Update() {
        timer += Time.deltaTime;
        if (timer >= 10) {
            SetStatus(Random.Range(0, 2) == 0);
            timer = 0;
        }
    }
}
