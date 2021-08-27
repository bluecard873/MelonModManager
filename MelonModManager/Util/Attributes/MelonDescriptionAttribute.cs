using System;

namespace MelonModManager.Util.Attributes {
    [AttributeUsage(AttributeTargets.Assembly)]
    public class MelonDescriptionAttribute : Attribute {
        public string Description { get; set; }

        public MelonDescriptionAttribute(string description = null) {
            Description = description;
        }

    }
}