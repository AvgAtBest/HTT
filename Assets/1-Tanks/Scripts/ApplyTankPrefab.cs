using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks.Custom
{
    public class ApplyTankPrefab : MonoBehaviour
    {
        public Tank player1, player2;
        public Transform gun; // the transform of the gun and body
        public int[] partIndexPlayer1 = new int[3]; //index for customisable parts (body, gun and tracks)
        public int[] partIndexPlayer2 = new int[3]; //index for customisable parts (body, gun and tracks)

        private void Awake()
        {
            if(GameObject.Find("ButtonHandler") != null)
            {
                CreateCharacters handler = GameObject.Find("ButtonHandler").GetComponent<CreateCharacters>();
                partIndexPlayer1 = handler.partIndexPlayer1;
                partIndexPlayer2 = handler.partIndexPlayer2;

            }
        }
        void Start()
        {
            //Creates the bodypart of the tank as a gameobject and parents the guns transform to the tank body
            #region Spawn P1 Tank Parts
            if (player1)
            {
                gun = Instantiate(Resources.Load("Tank_Parts/Body_" + partIndexPlayer1[0]) as GameObject, player1.transform).transform;
                gun = gun.GetChild(0);

                GameObject turret = Instantiate(Resources.Load("Tank_Parts/Turret_" + partIndexPlayer1[1]) as GameObject, gun);
                Instantiate(Resources.Load("Tank_Parts/Tracks_" + partIndexPlayer1[2]) as GameObject, player1.transform);

                player1.gun = gun;
                player1.spawnPoint = turret.transform.GetChild(0).GetChild(0).transform;
            }
            #endregion
            #region Spawn P2 Tank Parts
            if (player2)
            {
                gun = Instantiate(Resources.Load("Tank_Parts/Body_" + partIndexPlayer2[0]) as GameObject, player2.transform).transform;
                gun = gun.GetChild(0);

                GameObject turret = Instantiate(Resources.Load("Tank_Parts/Turret_" + partIndexPlayer2[1]) as GameObject, gun);
                Instantiate(Resources.Load("Tank_Parts/Tracks_" + partIndexPlayer2[2]) as GameObject, player2.transform);

                player2.gun = gun;
                player2.spawnPoint = turret.transform.GetChild(0).GetChild(0).transform;
            }
            #endregion
        }

    }
}
