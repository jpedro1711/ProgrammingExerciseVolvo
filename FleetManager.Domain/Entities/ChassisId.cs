

namespace FleetManager.Domain.Entities
{
    public class ChassisId(string chassisSeries, int chassisNumber)
    {
        public string ChassisSeries { get; } = chassisSeries;
        public int ChassisNumber { get; } = chassisNumber;

        public override string ToString() => $"{ChassisSeries}-{ChassisNumber}";
    }
}
