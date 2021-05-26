using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private const float k_ElectricCapacity = 3.2f;
        private const float k_FuelCapacity = 45f;
        private const int k_NumberOfWheels = 4;
        private const int k_MinDoor = 2;
        private const int k_MaxDoor = 5;
        private const int k_MinColor = 1;
        private const int k_MaxColor = 4;
        private eCarColor m_CarColor;
        private eNumberOfDoors m_NumberOfDoors;        

        public enum eCarColor
        {
            Red = 1,
            Silver,
            White,
            Black
        }

        public enum eNumberOfDoors
        {
            TwoDoors = 2,
            ThreeDoors,
            FourDoors,
            FiveDoors
        }

        public Car(string i_LicenseNumber, int i_IsElectricType)
            : base(i_LicenseNumber, i_IsElectricType)
        {
            AddWheels();
        }

        public override void AddWheels()
        {
            for (int i = 0; i < k_NumberOfWheels; i++)
            {
                Wheel wheel = new Wheel((float)Wheel.eMaxAirPressure.Car);
                Wheels.Add(wheel);
            }
        }

        public override void SetEngineProperties()
        {
            if (VehicleEngine is FuelEngine)
            {
                (VehicleEngine as FuelEngine).FuelType = FuelEngine.eFuelType.Octan95;
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
            string colorsString = string.Format(
@"{0}Car's colors:
1. Red
2. Silver
3. White
4. Black{0}", 
            Environment.NewLine);

            vehicleSpecificDetails.Add(colorsString);
            vehicleSpecificDetails.Add(@"Number of Doors:
- 2 Doors
- 3 Doors
- 4 Doors
- 5 Doors");

            return vehicleSpecificDetails;
        }

        public override void SetSpecificDetails(List<string> i_VehicleDetails)
        {
            int carColor = int.Parse(i_VehicleDetails[0]);
            int carDoorsNumber = int.Parse(i_VehicleDetails[1]);

            if (!int.TryParse(i_VehicleDetails[0], out carColor) || !int.TryParse(i_VehicleDetails[1], out carDoorsNumber))
            {
                throw new FormatException("Wrong format type!");
            }
            else if (carColor < k_MinColor || carColor > k_MaxColor)
            {
                throw new ValueOutOfRangeException(k_MinColor, k_MaxColor);
            }
            else if (carDoorsNumber < k_MinDoor || carDoorsNumber > k_MaxDoor)
            {
                throw new ValueOutOfRangeException(k_MinDoor, k_MaxDoor);
            }

            m_CarColor = (eCarColor)carColor;
            m_NumberOfDoors = (eNumberOfDoors)carDoorsNumber;
        }

        public override string ToString()
        {
            string carDetails = string.Format(
@"Car's Color: {0}
Car's number of doors: {1}",
            Enum.GetName(typeof(eCarColor), m_CarColor),
            Enum.GetName(typeof(eNumberOfDoors), m_NumberOfDoors));

            return base.ToString() + carDetails;
        }
    }
}