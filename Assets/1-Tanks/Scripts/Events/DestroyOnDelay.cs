using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tanks
{
    public class DestroyOnDelay : MonoBehaviour
    {
        public int destroyTime;
        // Use this for initialization
        void Start()
        {
            Destroy(gameObject, destroyTime);
        }

        // Update is called once per frame
        void Update()
        {

        }
    } 
}
