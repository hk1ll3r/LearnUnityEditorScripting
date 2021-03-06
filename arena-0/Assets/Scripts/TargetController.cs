﻿using UnityEngine;

namespace NoSuchStudio.ExtendingEditor {

    
    public class TargetController : MonoBehaviour {
        public static int enemies;

        public int score;

        private void Die() {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj) {
                playerObj.GetComponent<PlayerInfo>().AddScore(score);
            }
            Destroy(gameObject);
        }

        private void OnEnable() {
            enemies++;
        }

        private void OnDisable() {
            enemies--;
        }

        private void FixedUpdate() {
            if (transform.position.y < -2) {
                Die();
            }
        }
    }

}