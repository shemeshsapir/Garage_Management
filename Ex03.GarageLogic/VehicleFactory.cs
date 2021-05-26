namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {
        public enum eVehicleType
        {
            Car = 1,
            Motorcycle,
            Truck
        }

        public static Vehicle CreatVehicle(string i_LicenseNumber, int i_VehicleType, int i_IsElectricType)
        {
            Vehicle vehicleToCreate = null;
            eVehicleType vehicleType = (eVehicleType)i_VehicleType;

            switch (vehicleType)
            {
                case eVehicleType.Car:
                    vehicleToCreate = new Car(i_LicenseNumber, i_IsElectricType);
                    break;
                case eVehicleType.Motorcycle:
                    vehicleToCreate = new Motorcycle(i_LicenseNumber, i_IsElectricType);
                    break;
                case eVehicleType.Truck:
                    vehicleToCreate = new Truck(i_LicenseNumber, i_IsElectricType);
                    break;
            }

            vehicleToCreate.SetEngineProperties();

            return vehicleToCreate;
        }
    }
}