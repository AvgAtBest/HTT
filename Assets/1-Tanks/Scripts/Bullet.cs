using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
    public class Bullet : MonoBehaviour
    {
        private Rigidbody2D rigid;

        // Use this for initialization
        void Start()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            RotateToVelocity();
            
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.tag == "Ground")
            {
                Destroy(gameObject);
            }   
        }

        void RotateToVelocity()
        {
            // Get Velocity
            Vector3 vel = rigid.velocity;
            // Get Angle from Velocity
            float angle = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;
            // Rotate bullet in that angle
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }

}