using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;

namespace NoSuchStudio.ExtendingEditor {

    public class GameManager : MonoBehaviour {

        [SerializeField] int score;
        [SerializeField, HideInInspector] string levelName;
        public string LevelName {
            get { return levelName; }
            set { 
                levelName = value;
            }
        }
        [SerializeField, TextArea(2,4)] string intro;

        [SerializeField] Text txtScore;
        [SerializeField] Text txtLevelName;
        [SerializeField] Text txtIntro;
        [SerializeField] Slider sliderCooldown;

        float startTime;

        private void Start() {
            startTime = Time.time;
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.white;
            Gizmos.DrawIcon(transform.position, "managerIcon.png", true /* allowScaling */);
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

        public string Validate() {
            StringBuilder sb = new StringBuilder();
            if (txtScore == null) {
                sb.AppendLine("score text not set.");
            }
            if (txtLevelName == null) {
                sb.AppendLine("level name text not set.");
            }
            if (txtIntro == null) {
                sb.AppendLine("intro text not set.");
            }
            if (sliderCooldown == null) {
                sb.AppendLine("cooldown slider not set.");
            }
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            GameObject arenaObj = GameObject.FindGameObjectWithTag("Arena");
            PlayerController pc = playerObj?.GetComponent<PlayerController>();
            if (playerObj == null) {
                sb.AppendLine("scene has no player object.");
            } else {
                if (pc == null) {
                    sb.AppendLine("player object has no PlayerController component.");
                }
            }
            if (arenaObj == null) {
                sb.AppendLine("scene has no arena object.");
            }

            return sb.ToString();
        }

    }
}