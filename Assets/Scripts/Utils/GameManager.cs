﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Swordsmanship
{
	public class GameManager : MonoBehaviour {

		private float timeScale;

		private GameObject[] players;

        //public EnemyGenerator enemyGenerator;
        public Transform[] birthPoint;

        int MAX_RESPAWN_ENEMY_TIME = 1000;
        int respawnEnemyTime;


        // Use this for initialization
        void Start () {
			timeScale = Time.timeScale;
			StorePlayer ();
            respawnEnemyTime = MAX_RESPAWN_ENEMY_TIME;

        }

		void StorePlayer()
		{
			GameObject[] gos = GameObject.FindGameObjectsWithTag("Human") as GameObject[];
			GameObject[] gos2 = GameObject.FindGameObjectsWithTag ("Player") as GameObject[];

			var objList = new System.Collections.Generic.List<GameObject>();

			foreach (GameObject player in gos) 
			{
				objList.Add (player);
			}

			foreach (GameObject player in gos2) 
			{
				objList.Add (player);
			}

			players = objList.ToArray ();
		}

		void HandlePause()
		{
			if (Input.GetKeyDown (KeyCode.P)) 
			{
				//Time.timeScale = timeScale;
				if (Time.timeScale > 0) 
				{
					Time.timeScale = 0;
					//Cursor.lockState = CursorLockMode.None;

				} 
				else 
				{
					Time.timeScale = timeScale;
					//Cursor.lockState = CursorLockMode.Locked;
				}
			}
		}

		void GameFinish()
		{
			for (int i = 0; i < players.Length; i++) {
				if (players [i].GetComponent<SwordsmanCharacter>().isDead ()) {
					//if (players [i].layer == LayerMask.NameToLayer("EnemyLayer"))
					//	SceneManager.LoadScene ("WinScene");
					//else
					//	SceneManager.LoadScene ("LoseScene");
					
				}
			}
		}

		// Update is called once per frame
		void Update () {
            respawnEnemyTime -= 1;
            if(respawnEnemyTime <= 0)
            {
                GenerateEnemy();
                respawnEnemyTime = MAX_RESPAWN_ENEMY_TIME;
            }


			HandlePause ();
			GameFinish ();

		}



        GameObject GenerateEnemy()
        {
            int name_idx = 0;
            int birth_idx = 0;
            //TODO: random name

            //GameObject newEnemy = Instantiate(Resources.Load(enemyName[name_idx]),birthPoint[birth_idx].position, Quaternion.identity) as GameObject;
            GameObject newEnemy = Instantiate(Resources.Load("AI-Di"), birthPoint[birth_idx].position, Quaternion.identity) as GameObject;
            newEnemy.tag = "Human";
            newEnemy.layer = LayerMask.NameToLayer("EnemyLayer");


            return newEnemy;
        }
    }
}