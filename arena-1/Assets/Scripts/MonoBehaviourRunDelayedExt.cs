using System;
using System.Collections;

using UnityEngine;

namespace NoSuchStudio.Common {


    /// <summary>
    /// Utility class for UnityEngine.Object subclasses (MonoBehaviour, Component, Editor, etc.) that want to use the extended logging capabilities below:
    /// <ul>
    /// <li>Option to log ThreadId, class name, object name, game time or other common info to log messages.</li>
    /// <li>Configure the info PER CLASS. Useful for debugging specific classes.</li>
    /// </ul>
    /// </summary>
    /// <remarks>
    /// This class keeps track of all types that use it and creates a <see cref="UnityEngine.Logger"/> for each. 
    /// Any messages logged through the extension methods will have the info based on the LoggerConfig for that type prepended to the message.
    /// <code>
    /// MyClass myObj = new MyClass(); // MyClass extends UnityEngine.Object (i.e. MonoBehaviour, Editor, Component, ...)
    /// myObj.LogLog("Hello World!"); 
    /// // will print "[1][4.56](MyClass)(myObjName) Hello World!"
    /// </code>
    /// Using sample code like below, you can filter your logs by class.
    /// <code>
    /// UnityObjectLoggerExt.GetLoggerByType&lt;MyClass&gt;().logger.filterLogType = LogType.Error;
    /// </code>
    /// Using sample code like below, you can change the logging config for each class.
    /// <code>
    /// UnityObjectLoggerExt.GetLoggerByType&lt;MyClass&gt;().loggerConfig.logGameTime = false;
    /// </code>
    /// </remarks>
    public static class MonoBehaviourRunDelayedExt {

        public static IEnumerator DelayedCoroutine(float delay, Action a) {
            yield return new WaitForSeconds(delay);
            a();
        }

        public static IEnumerator DelayedCoroutineRealtime(float delay, Action a) {
            yield return new WaitForSecondsRealtime(delay);
            a();
        }

        public static Coroutine RunDelayed(this MonoBehaviour mono, float delay, Action a) {
            return mono.StartCoroutine(DelayedCoroutine(delay, a));
        }

        public static Coroutine RunDelayedRealtime(this MonoBehaviour mono, float delay, Action a) {
            return mono.StartCoroutine(DelayedCoroutineRealtime(delay, a));
        }
    }
}