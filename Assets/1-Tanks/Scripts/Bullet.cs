using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tanks
{
    public class Bullet : MonoBehaviour
    {
        private Rigidbody2D rigid;
        public float explosionRadius = 10f;
        public float damage = 10f;
        public ParticleSystem particle;
        // Use this for initialization
        void Start()
        {
            particle = GetComponentInChildren<ParticleSystem>();
            rigid = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            RotateToVelocity();
            
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
        private void OnDestroy()
        {
            if (GameManager.Instance)
            {
                GameManager.Instance.NextTank();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {

            if (other.tag == "Ground")
            {
                //DetachParticle();
                Destroy(gameObject);
                Explode();
                if (gameObject.tag == "FireBullet")
                {
                    DetachParticle();
                }
            }
            else
            {
                Explode();
                Destroy(gameObject);
                //DetachParticle();
                if (gameObject.tag == "FireBullet")
                {
                    DetachParticle();
                }
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
        void Explode()
        {
            //grabs all the 2d colliders in the circle where it hits
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

            //for each collider that it hits in the circle
            foreach (var hit in hits)
            {
                //gets the health component from the hit target
                Health health = hit.GetComponent<Health>();
                //if it has health
                if (health)
                {
                    
                    //take damage
                    health.TakeDamage(damage, transform.position);
                }
                Tilemap tilemap = hit.GetComponent<Tilemap>();
                if (tilemap)
                {
                    DestroyTiles(tilemap, transform.position, (int)explosionRadius);
                }
            }

        }
        void DestroyTiles(Tilemap tilemap, Vector3 point, int radius)
        {
            //if tilemap exists
            if (tilemap)
            {
                for (int x = -radius; x <= radius; x++)
                {
                    for (int y = -radius; y <= radius; y++)
                    {
                        Vector3 hitPoint = point + new Vector3(x, y) * .5f;
                        float distance = Vector3.Distance(transform.position, hitPoint);
                        if (distance <= explosionRadius * .5f)
                        {
                            //Convert point of the tilemap in the world via vector3 integer
                            Vector3Int hitPos = tilemap.WorldToCell(hitPoint);
                            //if there is a tile at that position
                            if (tilemap.GetTile(hitPos) != null)
                            {
                                //remove that tile
                                tilemap.SetTile(hitPos, null);
                            }
                        }
                    }
                }

            }
        }
        void DetachParticle()
        {
            //detaches particle system from parent
            particle.transform.SetParent(null);
            //if its the fire particle effect from the fire bullet
            if(particle.tag == "FireBullet")
            {
                //destroy it after 4 seconds
                Destroy(particle, 4);
            }
            else
            {
                //if its anything else, just destroy the particle on collision
                Destroy(particle);
            }

        }
    }

}