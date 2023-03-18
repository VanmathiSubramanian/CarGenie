using Microsoft.Data.Sqlite;

namespace OrderBot
{
    public class User : ISQLModel
    {
        private int _userId = -1;
        private string _name = string.Empty;
        private string _email = string.Empty;

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public string Name
        { 
            get { return _name; } 
            set { _name = value; } 
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public void Save()
        {
            if (_userId == -1)
            {
                // create query to insert data
                string insertQuery = "INSERT INTO User (Name, Email) VALUES (@Name, @Email);";

                // create connection object
                using (var connection = new SqliteConnection(DB.GetConnectionString()))
                {
                    var commandInsert = connection.CreateCommand();
                    commandInsert.CommandText = insertQuery;
                    commandInsert.Parameters.AddWithValue("@Name", Name);
                    commandInsert.Parameters.AddWithValue("@Email", Email);
                    connection.Open();
                    commandInsert.ExecuteNonQuery();
                }
            }
            else
            {
                // create query to insert data
                string updateQuery = "UPDATE User SET Name = @Name, Email = @Email WHERE UserId = @UserId)";

                // create connection object
                using (var connection = new SqliteConnection(DB.GetConnectionString()))
                {
                    var commandInsert = connection.CreateCommand();
                    commandInsert.CommandText = updateQuery;
                    commandInsert.Parameters.AddWithValue("@Name", Name);
                    commandInsert.Parameters.AddWithValue("@Email", Email);
                    commandInsert.Parameters.AddWithValue("@UserId", UserId);
                    connection.Open();
                    commandInsert.ExecuteNonQuery();
                }
            }
        }

    }    
    public class SalesPerson : ISQLModel
    {
        private int _salesPersonId = -1;
        private string _salesPersonName = string.Empty;
        private string _salesPersonEmail = string.Empty;

        public int SalesPersonId
        {
            get { return _salesPersonId; }
            set { _salesPersonId = value; }
        }

        public string SalesPersonName
        {
            get { return _salesPersonName; }
            set { _salesPersonName = value; }
        }

        public string SalesPersonEmail
        {
            get { return _salesPersonEmail; }
            set { _salesPersonEmail = value; }
        }
        
        public void Save()
        {
            if (_salesPersonId == -1)
            {
                // create query to insert data
                string insertQuery = "INSERT INTO SalesPerson (SalesPersonName, SalesPersonEmail) VALUES (@Name, @Email);";

                // create connection object
                using (var connection = new SqliteConnection(DB.GetConnectionString()))
                {
                    var commandInsert = connection.CreateCommand();
                    commandInsert.CommandText = insertQuery;
                    commandInsert.Parameters.AddWithValue("@Name", SalesPersonName);
                    commandInsert.Parameters.AddWithValue("@Email", SalesPersonEmail);
                    connection.Open();
                    commandInsert.ExecuteNonQuery();
                }
            }
            else
            {
                // create query to update data
                string updateQuery = "UPDATE SalesPerson SET SalesPersonName = @Name, SalesPersonEmail = @Email WHERE SalesPersonId = @SalesPersonId)";

                // create connection object
                using (var connection = new SqliteConnection(DB.GetConnectionString()))
                {
                    var commandInsert = connection.CreateCommand();
                    commandInsert.CommandText = updateQuery;
                    commandInsert.Parameters.AddWithValue("@Name", SalesPersonName);
                    commandInsert.Parameters.AddWithValue("@Email", SalesPersonEmail);
                    commandInsert.Parameters.AddWithValue("@SalesPersonId", SalesPersonId);
                    connection.Open();
                    commandInsert.ExecuteNonQuery();
                }
            }
        }
    }
    public class Appointment : ISQLModel
    {
        private int _appointmentId = -1;
        private int _salesPersonId = 0;
        private int _userId = 0;
        private DateTime _appointmentDate = DateTime.MinValue;
        private string _availabilityStatus = String.Empty;
        private string _appointmentType = String.Empty;

        public int AppointmentId
        {
            get { return _appointmentId; }
            set { _appointmentId = value; }
        }

        public int SalesPersonId
        {
            get { return _salesPersonId; }
            set { _salesPersonId = value; }
        }

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public DateTime AppointmentDate
        {
            get { return _appointmentDate; }
            set { _appointmentDate = value; }
        }

        public string AvailabilityStatus
        {
            get { return _availabilityStatus; }
            set { _availabilityStatus = value; }
        }

        public string AppointmentType
        {
            get { return _appointmentType; }
            set { _appointmentType = value; }
        }

        public void Save()
        {
            // create query to update data
            string updateQuery = "UPDATE Appointment SET UserId = @UserId WHERE AppointmentId = @AppointmentId)";

            // create connection object
            using (var connection = new SqliteConnection(DB.GetConnectionString()))
            {
                var commandInsert = connection.CreateCommand();
                commandInsert.CommandText = updateQuery;
                commandInsert.Parameters.AddWithValue("@UserId", UserId);
                commandInsert.Parameters.AddWithValue("@AppointmentId", AppointmentId);
                connection.Open();
                commandInsert.ExecuteNonQuery();
            }
        }
    }
    public class Car : ISQLModel
    {
        private int _carId;
        private string _modelName;
        private int _modelNumber;
        private string _availabilityStatus;
        private int _modelYear;

        public int CarId
        {
            get { return _carId; }
            set { _carId = value; }
        }

        public string ModelName
        {
            get { return _modelName; }
            set { _modelName = value; }
        }

        public int ModelNumber
        {
            get { return _modelNumber; }
            set { _modelNumber = value; }
        }

        public string AvailabilityStatus
        {
            get { return _availabilityStatus; }
            set { _availabilityStatus = value; }
        }

        public int ModelYear
        {
            get { return _modelYear; }
            set { _modelYear = value; }
        }

        public Car(int carId, string modelName, int modelNumber, string availabilityStatus, int modelYear)
        {
            _carId = carId;
            _modelName = modelName;
            _modelNumber = modelNumber;
            _availabilityStatus = availabilityStatus;
            _modelYear = modelYear;
        }

        public void Save()
        {
            throw new NotImplementedException("User Cannot Save or Update a CAR. It is inventory data");
        }
    }
    public class TestDrive : ISQLModel
    {
        private int _testDriveId;
        private DateTime _testDriveDate;
        private int _carId;

        public int TestDriveId
        {
            get { return _testDriveId; }
            set { _testDriveId = value; }
        }

        public DateTime TestDriveDate
        {
            get { return _testDriveDate; }
            set { _testDriveDate = value; }
        }

        public int CarId
        {
            get { return _carId; }
            set { _carId = value; }
        }

        public TestDrive(int testDriveId, DateTime testDriveDate, int carId)
        {
            _testDriveId = testDriveId;
            _testDriveDate = testDriveDate;
            _carId = carId;
        }

        public void Save()
        {
            string insertQuery = "INSERT INTO TestDrive (TestDriveDate, CarId) VALUES (@TestDriveDate, @CarId);";

            // create connection object
            using (var connection = new SqliteConnection(DB.GetConnectionString()))
            {
                var commandInsert = connection.CreateCommand();
                commandInsert.CommandText = insertQuery;
                commandInsert.Parameters.AddWithValue("@TestDriveDate", TestDriveDate);
                commandInsert.Parameters.AddWithValue("@CarId", CarId);
                connection.Open();
                commandInsert.ExecuteNonQuery();
            }
        }
    }
    public class Order : ISQLModel
    {
        private int _orderId;
        private int _carId;
        private int _salesPersonId;
        private int _userId;
        private DateTime _deliveryDate;
        private string _orderStatus;

        public int OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }

        public int CarId
        {
            get { return _carId; }
            set { _carId = value; }
        }

        public int SalesPersonId
        {
            get { return _salesPersonId; }
            set { _salesPersonId = value; }
        }

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public DateTime DeliveryDate
        {
            get { return _deliveryDate; }
            set { _deliveryDate = value; }
        }
        public string OrderStatus
        {
            get { return _orderStatus; }
            set { _orderStatus = value; }
        }

        public void Save()
        {
            throw new NotImplementedException("There is no use case for the user to insert or update order data. User can only query status for existing orders");
        }
    }
}
