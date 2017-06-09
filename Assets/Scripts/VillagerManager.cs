using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    public class VillagerManager : NetworkBehaviour
    {
        //List of villagers in each house to get used between the functions
        public List<Villager> house1;
        public List<Villager> house2;
        public List<Villager> house3;
        public List<Villager> house4;


        public void AssignRoles()
        {
            //Get the players and the number of players

            List<GameObject> ListOfPlayers = new List<GameObject> { };
            int numPlayers = ListOfPlayers.Count;

            foreach (GameObject villager in GameObject.FindGameObjectsWithTag("Player"))
            {
                ListOfPlayers.Add(villager);
            }

            // could try to swap with   
            // Villager[] villagers = FindObjectsOfType(typeof(Villager)) as Villager[];

            int werewolf1 = -1;
            int werewolf2 = -1;
            int werewolf3 = -1;
            int werewolf4 = -1;

            //Get random indexes of werewolves
            // Resistance ratio:
            //  total: 5 = 3 villagers, 2 werewolves
            //  total: 6 = 4 villagers, 2 werewolves
            //  total: 7 = 4 villagers, 3 werewolves
            //  total: 8 = 5 villagers, 3 werewolves
            //  total: 9 = 6 villagers, 3 werewolves
            //  total:10 = 6 villagers, 4 werewolves


            // Determine Roles and Assign them!
            werewolf1 = UnityEngine.Random.Range(1, numPlayers);
            if (numPlayers >= 5)
            {
                while (werewolf2 == -1 || werewolf2 == werewolf1)
                {
                    werewolf2 = UnityEngine.Random.Range(0, numPlayers);
                }
            }

            if (numPlayers >= 7)
            {
                while (werewolf3 == -1 || werewolf3 == werewolf1 || werewolf3 == werewolf2)
                {
                    werewolf3 = UnityEngine.Random.Range(0, numPlayers);
                }
            }


            if (numPlayers == 10)
            {
                while (werewolf4 == -1 || werewolf4 == werewolf1 || werewolf4 == werewolf2 || werewolf4 == werewolf3)
                {
                    werewolf4 = UnityEngine.Random.Range(0, numPlayers);
                }
            }
            List<int> werewolves = new List<int> {werewolf1,werewolf2,werewolf3,werewolf4};
            

            foreach (GameObject player in ListOfPlayers){
                int index = ListOfPlayers.IndexOf(player);
                if (werewolves.Contains(index)){
                    player.SendMessage("CmdSetAsWerewolf");
                } 
                else{
                    player.SendMessage("CmdSetAsHuman");
                }

            }
        }

        public void SortVillagers()
        {
            //Clear the lists
            house1.Clear();
            house2.Clear();
            house3.Clear();
            house4.Clear();


            //Grab the Villagers and check if they are alive.
            List<Villager> ListOfAlivePlayers = new List<Villager> { };
            Villager[] villagers = FindObjectsOfType(typeof(Villager)) as Villager[];

            int housePop1 = 0;
            int housePop2 = 0;
            int housePop3 = 0;
            int housePop4 = 0;


            foreach (Villager villager in villagers)
            {
                if (villager.checkStatus() == "Dead")
                {
                    continue;
                }
                else
                {
                    ListOfAlivePlayers.Add(villager);
                }

            }

            // Depending on number of players left, determine the number of houses required
            // (1-3 players left will probably end the game but it's here for testing)

            int maxHouse = 0;
            int aliveCount = ListOfAlivePlayers.Count;
            Debug.Log(aliveCount);
            switch (aliveCount)
            {
                case 1:
                    maxHouse = 1;
                    break;
                case 2:
                    maxHouse = 1;
                    break;
                case 3:
                    //House 1: Max 2
                    //House 2: Max 1 (for testing)
                    maxHouse = 2;
                    break;
                case 4:
                    //House 1: Max 2
                    //House 2: Max 2
                    maxHouse = 2;
                    break;
                case 5:
                    //House 1: Max 3
                    //House 2: Max 2
                    maxHouse = 2;
                    break;
                case 6:
                    //House 1: Max 2
                    //House 2: Max 2
                    //House 3: Max 2
                    maxHouse = 3;
                    break;
                case 7:
                    //House 1: Max 3
                    //House 2: Max 2
                    //House 3: Max 2
                    maxHouse = 3;
                    break;
                case 8:
                    //House 1: Max 3
                    //House 2: Max 3
                    //House 3: Max 2
                    maxHouse = 3;
                    break;
                case 9:
                    //House 1: Max 3
                    //House 2: Max 2
                    //House 3: Max 2
                    //House 4: Max 2
                    maxHouse = 4;
                    break;
                case 10:
                    //House 1: Max 3
                    //House 2: Max 3
                    //House 3: Max 2
                    //House 4: Max 2
                    maxHouse = 4;
                    break;
            }

            Debug.Log(maxHouse);
            // Assign each player to the houses. (houses 1 and 2 have a flex max of 2/3) 

            foreach(Villager aliveVillager in ListOfAlivePlayers)
            {
                bool playerAssigned = false;

                while (playerAssigned == false)
                {
                    int assignment = UnityEngine.Random.Range(1, maxHouse+1);

                    switch (assignment)
                    {
                        case 1:
                            if (aliveCount == 4 || aliveCount == 6 || aliveCount == 3)
                            {
                                if (housePop1 == 2)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                if (housePop1 == 3)
                                {
                                    break;
                                }
                            }
                            house1.Add(aliveVillager);
                            housePop1++;
                            playerAssigned = true;
                            break;
                        case 2:
                            if (aliveCount == 8 || aliveCount == 10)
                            {
                                if (housePop2 == 3)
                                {
                                    break;
                                }
                            }else{
                                if (housePop2 == 2)
                                {
                                    break;
                                }
                            }
                            house2.Add(aliveVillager);
                            housePop2++;
                            playerAssigned = true;
                            break;
                        case 3:
                            if (housePop3 == 2)
                            {
                                break;
                            }
                            house3.Add(aliveVillager);
                            housePop3++;
                            playerAssigned = true;
                            break;
                        case 4:
                            if (housePop4 == 2)
                            {
                                break;
                            }
                            house4.Add(aliveVillager);
                            housePop4++;
                            playerAssigned = true;
                            break;
                    }
                }

                //To implement:
                MoveVillagersToHouses();

            }
        }

        private void MoveVillagersToHouses()
        {
            Debug.Log("Villages moved to houses");

            foreach (Villager villager in house1)
            {
                //Move to house 1 into open spot and set chat channel
                villager.CmdSetLocation("house1");
            }

            foreach (Villager villager in house2)
            {
                //Move to house 2 into open spot and set chat channel
                villager.CmdSetLocation("house2");
            }

            foreach (Villager villager in house3)
            {
                //Move to house 3 into open spot and set chat channel
                villager.CmdSetLocation("house3");
            }

            foreach (Villager villager in house4)
            {
                //Move to house 4 into open spot and set chat channel
                villager.CmdSetLocation("house4");
            }
        }

        public void MoveVillagersToVillage()
        {
            Villager[] villagers = FindObjectsOfType(typeof(Villager)) as Villager[];

            foreach (Villager villager in villagers)
            {
                //Move to main village area and set to all chat
                villager.CmdSetLocation("the village");
            }
        }

    }
}
