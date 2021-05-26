using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private readonly List<Vehicle> r_VehicleInGarage = new List<Vehicle>();

        public List<Vehicle> VehicleInGarage
        {
            get
            {
                return r_VehicleInGarage;
            }
        }

        public Vehicle FindVehicleByLicenseNumber(string i_LicenseNumber)
        {
            Vehicle searchedVehicle = null;

            foreach (Vehicle vehicle in r_VehicleInGarage)
            {
                if (vehicle.LicenseNumber.Equals(i_LicenseNumber))
                {
                    searchedVehicle = vehicle;
                    break;
                }
            }

            return searchedVehicle;
        }

        public void AddVehicleToGarage(Vehicle i_NewVehicle)
        {
            r_VehicleInGarage.Add(i_NewVehicle);
        }

        public void ChangeVehicleStatus(Vehicle i_CurrentVehicle, Vehicle.eVehicleGarageStatus i_NewStatus)
        {
            i_CurrentVehicle.VehicleStatus = i_NewStatus;
        }

        public List<string> FilterVehiclesByStatus(int i_VehicleStatus)
        {
            List<string> filteredByStatus = new List<string>();

            foreach (Vehicle vehicle in r_VehicleInGarage)
            {
                if (vehicle.VehicleStatus == (Vehicle.eVehicleGarageStatus)i_VehicleStatus)
                {
                    filteredByStatus.Add(vehicle.LicenseNumber);
                }
            }

            return filteredByStatus;
        }
    }
}