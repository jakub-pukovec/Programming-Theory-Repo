public interface ISwitchable
{
    bool IsSwitchedOn { get; }
    void SwitchOn();
    void SwitchOff();
}