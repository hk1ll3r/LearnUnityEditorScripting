﻿using UnityEngine;

namespace NoSuchStudio.ExtendingEditor {

    
    public class TargetController : MonoBehaviour {
        public static int enemies;

        [SerializeField] int score;

        private void Die() {
            // TODO die animation
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