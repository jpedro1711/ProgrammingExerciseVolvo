namespace FleetManager.Domain.Entities.Owned
{
    public class ChassisId(string chassisSeries, uint chassisNumber)
    {
        public string ChassisSeries { get; } = chassisSeries;
        public uint ChassisNumber { get; } = chassisNumber;

        public override string ToString() => $"{ChassisSeries}-{ChassisNumber}";
    }
}
