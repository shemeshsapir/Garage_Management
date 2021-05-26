using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private const float k_FuelCapacity = 6f;
        private const float k_ElectricCapacity = 1.8f;
        private const int k_NumberOfWheels = 2;
        private const int k_MinLicense = 1;
        private const int k_MaxLicense = 4;
        private float m_EngineCapacity;
        private eLicenseType m_LicenseType;

        public enum eLicenseType
        {
            A = 1, 
            B1,
            AA,
            BB
        }

        public Motorcycle(string i_LicenseNumber, int i_IsElectricType)
            : base(i_LicenseNumber, i_IsElectricType)
        {
            AddWheels();
        }

        public override void AddWheels()
        {
            for (int i = 0; i < k_NumberOfWheels; i++)
            {
                Wheel wheel = new Wheel((float)Wheel.eMaxAirPressure.Motorcycle);
                Wheels.Add(wheel);
            }
        }

        public override void SetEngineProperties()
        {
            if (VehicleEngine is FuelEngine)
            {
                (VehicleEngine as FuelEngine).FuelType = FuelEngine.eFuelType.Octan98;
                VehicleEngine.EngineCapacity = k_FuelCapacity;
            }
            else
            {
                VehicleEngine.EngineCapacity = k_ElectricCapacity;
            }
        }

        public override List<string> GetSpecificVehicleDetails()
        {
            List<string> vehicleSpecificDetails = new List<string>();
            string licenseTypes = string.Format(
@"{0}Motorcycle's license types are:
1. A
2. B1
3. AA
4. BB{0}", 
            Environment.NewLine);

            vehicleSpecificDetails.Add(licenseTypes);
            vehicleSpecificDetails.Add("Motorcycle engine capacity:");

            return vehicleSpecificDetails;
        }

        public override void SetSpecificDetails(List<string> i_VehicleDetails)
        {
            int licenseType = int.Parse(i_VehicleDetails[0]);

            if (!int.TryParse(i_VehicleDetails[0], out licenseType))
            {
                throw new FormatException("Wrong format type!");
            }
            else if (licenseType < k_MinLicense || licenseType > k_MaxLicense)
            {
                throw new ValueOutOfRangeException(k_MinLicense, k_MaxLicense);
            }

            m_LicenseType = (eLicenseType)licenseType;
            m_EngineCapacity = int.Parse(i_VehicleDetails[1]);
        }

        public override string ToString()
        {
            string motorcycleDetails = string.Format(
@"Motorcycle's License type: {0}
Motorcycle's engine capacity: {1}",
            Enum.GetName(typeof(eLicenseType), m_LicenseType),
            m_EngineCapacity);

            return base.ToString() + motorcycleDetails;
        }
    }
}