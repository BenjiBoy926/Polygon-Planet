public class EnergyEventData
{
    public EnergySource source { get; private set; }
    public EnergySocket socket { get; private set; }
    public int energy { get; private set; }

    public EnergyEventData(EnergySource src, EnergySocket dest, int amt)
    {
        source = src;
        socket = dest;
        energy = amt;
    }
}
