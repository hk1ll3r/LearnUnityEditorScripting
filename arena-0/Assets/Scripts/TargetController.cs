using UnityEngine;

namespace NoSuchStudio.ExtendingEditor {

    
    public class TargetController : MonoBehaviour {
        public static int enemies;
        private void Die() {
            // TODO die animation
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