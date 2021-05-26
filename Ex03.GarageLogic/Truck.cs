using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private const float k_FuelCapacity = 120f;
        private const int k_NumberOfWheels = 16;
        private float m_CarryingWeight;
        private bool m_IsHavingDangerousMaterials;

        public Truck(string i_LicenseNumber, int i_IsElectricType)
            : base(i_LicenseNumber, i_IsElectricType)
        {
            AddWheels();
        }

        public override void AddWheels()
        {
            for (int i = 0; i < k_NumberOfWheels; i++)
            {
                Wheel wheel = new Wheel((float)Wheel.eMaxAirPressure.Truck);
                Wheels.Add(wheel);
            }
        }

        public override void SetEngineProperties()
        {
            if (VehicleEngine is FuelEngine)
            {
                (VehicleEngine as FuelEngine).FuelType = FuelEngine.eFuelType.Soler;
                VehicleEngine.EngineCapacity = k_FuelCapacity;
            }
            else
            {
                throw new ArgumentException("There is no electric truck!");
            }
        }

        public override List<string> GetSpecificVehicleDetails()
        {
            List<string> vehicleSpecificDetails = new List<string>();

            vehicleSpecificDetails.Add("The truck's max carrying weight is: ");
            vehicleSpecificDetails.Add("Does the truck carry dangerous materials? 0 - NO, 1 - YES: ");

            return vehicleSpecificDetails;
        }

        public override void SetSpecificDetails(List<string> i_VehicleDetails)
        {
            int dangerousMaterials = int.Parse(i_VehicleDetails[1]);

            if (!int.TryParse(i_VehicleDetails[1], out dangerousMaterials))
            {
                throw new FormatException("Wrong format type!");
            }
            else if (dangerousMaterials != 0 && dangerousMaterials != 1)
            {
                throw new ValueOutOfRangeException(0, 1);
            }

            if (i_VehicleDetails[0] == "0")
            {
                m_IsHavingDangerousMaterials = false;
            }
            else
            {
                m_IsHavingDangerousMaterials = true;
            }

            m_CarryingWeight = int.Parse(i_VehicleDetails[0]);
        }

        public override string ToString()
        {
            StringBuilder truckDetails = new StringBuilder();

            truckDetails.AppendFormat("Truck's maximum carrying weight is: {0}", m_CarryingWeight).AppendLine();

            if (m_IsHavingDangerousMaterials)
            {
                truckDetails.AppendLine("The truck is having a dangerous materials!");
            }
            else
            {
                truckDetails.AppendLine("The truck doesn't have a dangerous materials!");
            }

            return base.ToString() + truckDetails.ToString();
        }
    }
}