using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected readonly List<Wheel> r_ListOfWheels = new List<Wheel>();
        protected eVehicleGarageStatus m_VehicleStatus = eVehicleGarageStatus.InRepair;
        protected Engine m_Engine;
        protected string m_ModelName;
        protected string m_LicenseNumber;
        protected float m_EnergyPercentage;
        protected Vehicle m_Vehicle;
        protected string m_OwnerName;
        protected string m_OwnerPhoneNumber;

        public enum eVehicleGarageStatus
        {
            InRepair = 1,
            Repaired,
            Paid
        }

        protected Vehicle(string i_LicenseNumber, int i_IsElectricType)
        {
            m_LicenseNumber = i_LicenseNumber;

            if (i_IsElectricType == 0)
            {
                m_Engine = new FuelEngine();
            }
            else
            {
                m_Engine = new ElectricEngine();
            }
        }

        public eVehicleGarageStatus VehicleStatus
        {
            get
            {
                return m_VehicleStatus;
            }

            set
            {
                m_VehicleStatus = value;
            }
        }

        public string ModelName
        {
            get
            {
                return m_ModelName;
            }

            set
            {
                m_ModelName = value;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }

            set
            {
                m_LicenseNumber = value;
            }
        }

        public string OwnerName
        {
            get
            {
                return m_OwnerName;
            }

            set
            {
                m_OwnerName = value;
            }
        }

        public string OwnerPhone
        {
            get
            {
                return m_OwnerPhoneNumber;
            }

            set
            {
                m_OwnerPhoneNumber = value;
            }
        }

        public float EnergyPercentage
        {
            get
            {
                return m_EnergyPercentage;
            }

            set
            {
                m_EnergyPercentage = value;
            }
        }

        public List<Wheel> Wheels
        {
            get
            {
                return r_ListOfWheels;
            }
        }

        public Engine VehicleEngine
        {
            get
            {
                return m_Engine;
            }
        }

        public void InflateWheel()
        {
            foreach (Wheel wheel in r_ListOfWheels)
            {
                wheel.InflateWheel(wheel.MaxAirPressure - wheel.CurrentAirPressure);
            }
        }

        public abstract void AddWheels();

        public abstract void SetEngineProperties();

        public abstract List<string> GetSpecificVehicleDetails();

        public abstract void SetSpecificDetails(List<string> i_VehicleDetails);

        public override string ToString()
        {
            int i = 1;
            StringBuilder detailes = new StringBuilder();

            string vehicleDetails = string.Format(
@"Vehicle Details are:
=================================
License Number: {0}
Status: {1}
Owner Name: {2}
Owner Phone Number: {3}
Model Name: {4}
{5}
Energy Percentage Remained: {6}%
=================================",
            m_LicenseNumber,
            m_VehicleStatus.ToString(),
            m_OwnerName,
            m_OwnerPhoneNumber,
            m_ModelName,
            m_Engine.ToString(),
            VehicleEngine.EnergyPercentage);

            detailes.AppendLine(vehicleDetails);

            foreach (Wheel wheel in r_ListOfWheels)
            {
                detailes.AppendFormat("Wheel number #{0}", i);
                detailes.AppendLine(wheel.ToString());
                i++;
            }

            return detailes.ToString();
        }
    }
}