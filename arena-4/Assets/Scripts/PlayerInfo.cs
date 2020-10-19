using UnityEngine;

namespace NoSuchStudio.ExtendingEditor {

    public class PlayerInfo : MonoBehaviour {
        [SerializeField] string playerName;
        [SerializeField] int score;

        public void AddScore(int s) {
            score += s;
            GameObject managerObj = GameObject.FindGameObjectWithTag("GameController");
            if (managerObj) {
                managerObj.GetComponent<GameManager>().SetScore(score);
            }
        }
    }
}