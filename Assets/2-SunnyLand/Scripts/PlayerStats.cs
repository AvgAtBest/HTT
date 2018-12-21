using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyNamespace
{
    public class PlayerStats : MonoBehaviour
    {
        public static int lives = 3, score = 0;

        public Transform heartContainer;

        public GameObject damageIcon, healIcon, coinIcon;
        public GameObject heartPrefab;

        public Text coinCount;

        private void Start()
        {
            for (int i = 0; i < lives; i++)
            {
                Instantiate(heartPrefab, heartContainer);
            }

            coinCount.text = "x " + score;

        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Hazard")
            {
                if(lives > 0)
                {
                    Destroy(heartContainer.GetChild(0).gameObject);
                    lives--;
                    GameObject clone = Instantiate(damageIcon, this.transform);
                    Destroy(clone, 1.5f);
                    if (lives == 0)
                    {

                    }
                }
            }
            if (collision.gameObject.tag == "Coin")
            {
                score++;
                Destroy(collision.gameObject);
                coinCount.text = "x " + score;
                GameObject clone = Instantiate(coinIcon, this.transform);
                Destroy(clone, 1.5f);

            }
            if (collision.gameObject.tag == "Heart")
            {
                lives++;
                Instantiate(heartPrefab, heartContainer);
                Destroy(collision.gameObject);
                GameObject clone = Instantiate(healIcon, this.transform);
                Destroy(clone, 1.5f);
            }
        }
    } 
}
