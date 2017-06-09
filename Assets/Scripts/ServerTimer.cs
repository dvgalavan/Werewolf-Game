using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Assets.Scripts;
namespace modules
{
    public class ServerTimer : NetworkBehaviour
    {
        private int ROUND_LENGTH = 10;
        public double myint;
        public ClientTimer timer;
        bool timerStart;
        bool firstTimer;
        public VillagerManager villagerManager;
        // Use this for initialization
        void Start()
        {
            timerStart = false;
            firstTimer = true;
            myint = 10;
        }

        // Update is called once per frame
        
        void Update()
        {
            if (timerStart)
            {
                myint -= Time.deltaTime;
                timer.value = Math.Ceiling(myint);
                if (myint <= 0)
                {
                    myint = ROUND_LENGTH;
                    string IncomingState = timer.ChangeStates();
                    Debug.Log("IncomingState = " + IncomingState);
                    if (IncomingState == "Nighttime") {
                        villagerManager.SortVillagers();
                    }
                    else if (IncomingState == "Daytime")
                    {
                        villagerManager.MoveVillagersToVillage();
                    }

                    if (firstTimer)
                    {
                        Debug.Log("Got here");
                        firstTimer = false;
                        villagerManager.AssignRoles();
                    }
                }
            }
        }

        public void StartTimer()
        {
            timerStart = !timerStart;
        }

    }
}
