using System.Runtime.Serialization;
namespace Core.Entities
{
    public enum AppStatus
    {
        [EnumMember(Value = "Not Set")]
        NotSet,

        [EnumMember(Value = "Activated")]
        Activated,

        [EnumMember(Value = "Need Activation")]
        NeedActivation,

        [EnumMember(Value = "Defense Activated")]
        DefenseActivated
    }
}