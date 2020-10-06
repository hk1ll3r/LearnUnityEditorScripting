using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace NoSuchStudio.ExtendingEditor {

    public class GameManager : MonoBehaviour {

        [SerializeField] int score;
        [SerializeField] string levelName;
        [SerializeField, TextArea(2,4)] string intro;

        [SerializeField] Text txtScore;
        [SerializeField] Text txtLevelName;
        [SerializeField] Text txtIntro;

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