﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tanks.Custom
{
    public class CreateCharacters : MonoBehaviour
    {
        public int[] partIndexPlayer1 = new int[3];
        public int[] partIndexPlayer2 = new int[3];
        public int[] partIndexMax = new int[3];
        public Image[] sprites = new Image[3];
        public Image[] spritesPlayer2 = new Image[3];
        public List<Sprite> body;
        public List<Sprite> turret;
        public List<Sprite> tracks;

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
        private void Start()
        {
            for (int i = 0; i < partIndexMax[0]; i++)
            {
                GameObject temp = Resources.Load("Tank_Parts/Body_" + i) as GameObject;
                body.Add(temp.GetComponent<SpriteRenderer>().sprite);
            }
            for (int i = 0; i < partIndexMax[1]; i++)
            {
                GameObject temp = Resources.Load("Tank_Parts/Turret_" + i) as GameObject;
                turret.Add(temp.GetComponentInChildren<SpriteRenderer>().sprite);
            }
            for (int i = 0; i < partIndexMax[2]; i++)
            {
                GameObject temp = Resources.Load("Tank_Parts/Tracks_" + i) as GameObject;
                tracks.Add(temp.GetComponentInChildren<SpriteRenderer>().sprite);
            }
            
        }
        #region Change P1
        public void ChangeBodyPlayer1(int direction)
        {
            partIndexPlayer1[0] += direction;
            if(partIndexPlayer1[0] < 0)
            {
                partIndexPlayer1[0] = partIndexMax[0] - 1;
            }
            if (partIndexPlayer1[0] > partIndexMax[0] - 1)
            {
                partIndexPlayer1[0] = 0;
            }
            sprites[0].sprite = body[partIndexPlayer1[0]];
        }
        public void ChangeTurretPlayer1(int direction)
        {
            partIndexPlayer1[1] += direction;
            if (partIndexPlayer1[1] < 0)
            {
                partIndexPlayer1[1] = partIndexMax[1] - 1;
            }
            if (partIndexPlayer1[1] > partIndexMax[1] - 1)
            {
                partIndexPlayer1[1] = 0;
            }
            sprites[1].sprite = turret[partIndexPlayer1[1]];
        }
        public void ChangeTracksPlayer1(int direction)
        {
            partIndexPlayer1[2] += direction;
            if (partIndexPlayer1[2] < 0)
            {
                partIndexPlayer1[2] = partIndexMax[2] - 1;
            }
            if (partIndexPlayer1[2] > partIndexMax[2] - 1)
            {
                partIndexPlayer1[2] = 0;
            }
            sprites[2].sprite = tracks[partIndexPlayer1[2]];
        }
        #endregion
        #region Change P2
        public void ChangeBodyPlayer2(int direction)
        {
            partIndexPlayer2[0] += direction;
            if (partIndexPlayer2[0] < 0)
            {
                partIndexPlayer2[0] = partIndexMax[0] - 1;
            }
            if (partIndexPlayer2[0] > partIndexMax[0] - 1)
            {
                partIndexPlayer2[0] = 0;
            }
            spritesPlayer2[0].sprite = body[partIndexPlayer2[0]];
        }
        public void ChangeTurretPlayer2(int direction)
        {
            partIndexPlayer2[1] += direction;
            if (partIndexPlayer2[1] < 0)
            {
                partIndexPlayer2[1] = partIndexMax[1] - 1;
            }
            if (partIndexPlayer2[1] > partIndexMax[1] - 1)
            {
                partIndexPlayer2[1] = 0;
            }
            spritesPlayer2[1].sprite = turret[partIndexPlayer2[1]];
        }
        public void ChangeTracksPlayer2(int direction)
        {
            partIndexPlayer2[2] += direction;
            if (partIndexPlayer2[2] < 0)
            {
                partIndexPlayer2[2] = partIndexMax[2] - 1;
            }
            if (partIndexPlayer2[2] > partIndexMax[2] - 1)
            {
                partIndexPlayer2[2] = 0;
            }
            spritesPlayer2[2].sprite = tracks[partIndexPlayer2[2]];
        }
        #endregion
    }
}
