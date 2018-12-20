using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
    public class BurnDamage : MonoBehaviour
    {
        public float burnDamage = 4f;
        public float burnRadius = 2f;
        public float burnDuration = 4f;
        public float burnInterval = .5f;


        private void OnTriggerEnter2D(Collider2D other)
        {
            StartCoroutine(BurnAOE());
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            StartCoroutine(BurnAOE());
        }

        public IEnumerator Burn()
        {
             
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, burnRadius);

            Debug.Log("Burn tick");

            foreach (var hit in hits)
            {

                Health health = hit.GetComponent<Health>();
                if (health)
                {
                    health.TakeDamage(burnDamage, transform.position);
                }
            }

            yield return new WaitForSeconds(burnInterval);

            StartCoroutine(Burn());
        }

        public IEnumerator BurnAOE()
        {
            StartCoroutine(Burn());

            yield return new WaitForSeconds(burnDuration);

            StopAllCoroutines();

            // Destroy Self
            Destroy(gameObject, 6);
        }

    } 
}
