using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace NoSuchStudio.ExtendingEditor {

    public class GameManager : MonoBehaviour {

        public int score;
        public string levelName;
        public string intro;

        public Text txtScore;
        public Text txtLevelName;
        public Text txtIntro;

        float startTime;

        private void Start() {
            startTime = Time.time;
        }

        private void Update() {
            if (Time.time - startTime > 3f && txtIntro.gameObject.activeSelf) txtIntro.gameObject.SetActive(false);
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (!playerObj) {
                Debug.Log("LOST");
                enabled = false;
            } else if (TargetController.enemies == 0) {
                // won!
                Debug.Log("WON");
                enabled = false;
            }
        }
    }

}