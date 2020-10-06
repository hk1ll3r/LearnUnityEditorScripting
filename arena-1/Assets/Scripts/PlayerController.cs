using UnityEngine;
using System;

namespace NoSuchStudio.ExtendingEditor {

    public enum SpecialAbilityType {
        None,
        Dash,
        Bounce,
        Invisibility
    }

    [Serializable]
    public class SpecialAbility {
        public SpecialAbilityType type;
        public float duration;
        public float cooldown;
        public float power;
    }

    [DisallowMultipleComponent]
    public class PlayerController : MonoBehaviour {
        [Header("General")]
        [SerializeField] private float mass;
        [SerializeField] private float speed;
        [SerializeField] private float size;
        [Space(10)]
        [SerializeField] private SpecialAbility specialAbility;

        [Header("Visual")]
        [Tooltip("Color field takes effect at runtime")]
        [SerializeField] private Color color;
        [Tooltip("Texture field only takes effect at runtime")]
        [SerializeField] private Texture texture;

        float lastAbilityTime; // last time the player used the special ability.
        private void Start() {
            ApplyValues();
        }

        private void OnValidate() {
            ApplyValues();
        }

        private void ApplyValues() {
            GetComponent<Rigidbody>().mass = mass;
            transform.localScale = size * Vector3.one;
            if (Application.isPlaying) {
                GetComponent<MeshRenderer>().material.color = color;
                GetComponent<MeshRenderer>().material.mainTexture = texture;
            }
        }

        private void Die() {
            // TODO die animation
            Destroy(gameObject);
        }

        private void FixedUpdate() {
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");

            Vector3 av = speed * new Vector3(inputX, 0f, inputY);
            // Debug.Log($"input ({inputX}, {inputY})");

            GetComponent<Rigidbody>().AddForce(av, ForceMode.Acceleration);

            if (Input.GetButton("Fire1") && Time.time > lastAbilityTime + specialAbility?.cooldown) {
                // TODO perform special ability
                lastAbilityTime = Time.time;
                Debug.Log("action!");
            }

            if (transform.position.y < -2) {
                Die();
            }
        }
    }

}