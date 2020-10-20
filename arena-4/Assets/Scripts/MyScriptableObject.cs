using UnityEngine;
namespace NoSuchStudio.ExtendingEditor {
    [CreateAssetMenu(fileName = "New My SO", menuName = "My Menu/My Scriptable Object")]
    class MyScriptableObject : ScriptableObject {
        [SerializeField] int dummyInt;
    }
}
