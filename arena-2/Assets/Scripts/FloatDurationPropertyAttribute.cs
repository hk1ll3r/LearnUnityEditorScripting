using UnityEngine;

namespace NoSuchStudio.ExtendingEditor {
    public enum FloatDurationUnitsMode {
        Flexible,
        Seconds,
        Minutes,
        Hours
    }

    public static class FloatDurationUnitsModeExts {
        public static string ToDisplayString(this FloatDurationUnitsMode unitsMode) {
            switch (unitsMode) {
                case FloatDurationUnitsMode.Seconds:
                    return "seconds";
                case FloatDurationUnitsMode.Minutes:
                    return "minutes";
                case FloatDurationUnitsMode.Hours:
                    return "hours";
                default:
                    return "???";
            }
        }

        public static FloatDurationUnitsMode FromDisplayString(string str) {
            switch (str) {
                case "seconds":
                    return FloatDurationUnitsMode.Seconds;
                case "minutes":
                    return FloatDurationUnitsMode.Minutes;
                case "hours":
                    return FloatDurationUnitsMode.Hours;
                default:
                    return FloatDurationUnitsMode.Flexible;
            }
        }
    }

    public class FloatDurationPropertyAttribute : PropertyAttribute {
        FloatDurationUnitsMode _unitsMode;
        public FloatDurationUnitsMode unitsMode {
            get { return _unitsMode; }
        }

        public FloatDurationPropertyAttribute(FloatDurationUnitsMode unitsMode = FloatDurationUnitsMode.Flexible) {
            _unitsMode = unitsMode;
        }
    }
}
