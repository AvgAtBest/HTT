using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyLand
{
    public class ResetToSpawn : MonoBehaviour
    {
        public Vector3 originPos;
        private void Start()
        {
            originPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Hazard")
            {
                this.GetComponent<Player>().enabled = false;
                this.GetComponent<Animator>().SetBool("Hurt", true);
                Invoke("Spawn", 1.5f);

            }
        }
        void Spawn()
        {

            gameObject.transform.position = originPos;
            this.GetComponent<Player>().enabled = true;
            this.GetComponent<Animator>().SetBool("Hurt", false);
        }

    } 
}
