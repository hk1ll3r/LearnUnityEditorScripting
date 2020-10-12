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
        [SerializeField] Slider sliderCooldown;

        float startTime;

        private void Start() {
            startTime = Time.time;
        }

        public void SetScore(int score) {
            txtScore.text = $"{score}";
        }

        private void Update() {
            if (Time.time - startTime > 3f && txtIntro.gameObject.activeSelf) txtIntro.gameObject.SetActive(false);
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (!playerObj) {
                Debug.Log("LOST");
                enabled = false;
            } else {
                if (TargetController.enemies == 0) {
                    // won!
                    Debug.Log("WON");
                    enabled = false;
                }
                PlayerController pc = playerObj.GetComponent<PlayerController>();
                sliderCooldown.value = Mathf.Clamp01((Time.time - pc.LastAbilityTime) / pc.SpecialAbility.cooldown);
            }
        }
    }
}