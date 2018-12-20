using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance = null;

        private void Awake()
        {
            Instance = this;
        }
        private void OnDestroy()
        {
            Instance = null;

        }

        public List<Tank> tanks;
        public int currentTank;

        // Use this for initialization
        void Start()
        {
            tanks = new List<Tank>(FindObjectsOfType<Tank>());
            SetTank(currentTank);
        }
        public void RemoveTank(Tank tankToRemove)
        {
            tanks.Remove(tankToRemove);
            SetTank(currentTank);
        }
        void SetTank(int current)
        {
            for (int i = 0; i < tanks.Count; i++)
            {
                Tank tank = tanks[i];
                tank.IsPlaying = false;

                if(i == current)
                {
                    tank.IsPlaying = true;
                }
            }
        }
        public void NextTank()
        {
            currentTank++;
            if(currentTank >= tanks.Count)
            {
                currentTank = 0;
            }
            SetTank(currentTank);
        }
    }
}