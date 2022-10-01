using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    [SerializeField] private Rigidbody2D thisRigidbody;
    [SerializeField] private float speed;
    private Vector2 direction;
    
    void FixedUpdate()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        direction.Normalize();

        if (direction.magnitude > 0) {
            thisRigidbody.velocity = direction * (speed * Time.deltaTime);
        }
    }
}
