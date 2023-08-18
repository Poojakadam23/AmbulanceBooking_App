using Microsoft.Data.SqlClient;
using System.Data;

namespace AmbulanceBooking_App
{
    internal class Program
    {


        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("          $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$          ");
            Console.WriteLine("          $$$                                                 $$$          ");
            Console.WriteLine("          $$$           AMBULANCE BOOKING SYSTEM              $$$          ");
            Console.WriteLine("          $$$                                                 $$$          ");
            Console.WriteLine("          $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$          ");
            Console.WriteLine();

           // Connect();

            while (true)
            {
               try
                {
                Console.WriteLine("1. Register as User");
                Console.WriteLine("2. Login as User");
                Console.WriteLine("3. Login as Admin");
                Console.WriteLine("4. Exit");

                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

               
                    switch (choice)
                    {
                        case 1:
                            UserRegister();
                            break;
                        case 2:
                            UserLogin();
                            break;
                        case 3:
                            AdminLogin();
                            break;
                        case 4:
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;

                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer choice.");
                }
            }
        }

        static void Connect()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSqlLocalDb;Initial Catalog=Ambulance_Bookings;Integrated Security=true";
            cn.Open();

            Console.WriteLine("open");
           cn.Close();
        }

        static void DisplayBookingTable(List<Booking> bookings)
        {
            Console.WriteLine("--------------------------------------------------------------------------------------------------------");
            Console.WriteLine("| BookingID | PatientName | PickupLocation | DropLocation | PickupTime          | Status | AmbulanceId  |");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------");
            foreach (Booking bk in bookings)
            {
                Console.WriteLine($"| {bk.BookingId,-9} | {bk.PatientName,-11} | {bk.PickupLocation,-15} | {bk.DropLocation,-11} | {bk.PickupTime,-20}| {bk.Status,-7}| {bk.AmbulanceId,-12} |");
            }
            Console.WriteLine("--------------------------------------------------------------------------------------------------------");
        }

        static void DisplayAmbulanceTable(List<Ambulance> ambulances)
        {
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("| AmbulanceId | AmbulanceName | DriverName | ContactNumber | Capacity |");
            Console.WriteLine("-----------------------------------------------------------------------");
            foreach (Ambulance ab in ambulances)
            {
                Console.WriteLine($"| {ab.AmbulanceId,-11} | {ab.AmbulanceName,-13} | {ab.DriverName,-10} | {ab.ContactNumber,-13} | {ab.Capacity,-8} |");
            }
            Console.WriteLine("------------------------------------------------------------------------");
        }

       
        static void AdminLogin()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSqlLocalDb;Initial Catalog=Ambulance_Bookings;Integrated Security=true";
            cn.Open();
            


            Console.WriteLine("Enter Admin name:");
            string AdminName = Console.ReadLine();

            Console.WriteLine("Enter Admin Password:");
            string AdminPassword = Console.ReadLine();



            try
            {


                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select COUNT(*) from  [Admin]  where AdminName=@AdminName AND AdminPassword=@AdminPassword";


                cmd.Parameters.AddWithValue("@AdminName", AdminName);
                cmd.Parameters.AddWithValue("@AdminPassword", AdminPassword);


                int count = (int)cmd.ExecuteScalar();
                if (count > 0)
                {
                    Console.WriteLine("Login successful!");
                    AdminMenu();
                }
                else
                {
                    Console.WriteLine("Login failed!");
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                cn.Close();
            }

        }
        static void UserRegister()
        {
            
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSqlLocalDb;Initial Catalog=Ambulance_Bookings;Integrated Security=true";
            cn.Open();

            Console.WriteLine("Enter User Id:");
            int UserId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter User name:");
            string UserName = Console.ReadLine();

            Console.WriteLine("Enter User Password:");
            string Password = Console.ReadLine();


            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into [User](UserId,UserName,Password) values(@UserId,@UserName,@Password)";


                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@Password", Password);

                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                    Console.WriteLine("Registration successful!");
                else
                    Console.WriteLine("Registration failed!");





            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        static void UserLogin()
        {
            
           SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSqlLocalDb;Initial Catalog=Ambulance_Bookings;Integrated Security=true";
            cn.Open();



            Console.WriteLine("Enter User name:");
            string UserName = Console.ReadLine();

            Console.WriteLine("Enter User Password:");
            string Password = Console.ReadLine();



            try
            {


                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select COUNT(*) from  [User]  where UserName=@UserName AND Password=@Password";


                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@Password", Password);


                int count = (int)cmd.ExecuteScalar();
                if (count > 0)
                {
                    Console.WriteLine("Login successful!");
                    UserMenu();
                }
                else
                {
                    Console.WriteLine("Login failed!");
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }
        static void AdminMenu()
        {
            while (true)
            {
                try
                {
                    // Console.WriteLine("Ambulance Booking System");

                Console.WriteLine("\n------------- Admin Menu --------------");

                Console.WriteLine("1. Add  Ambulance");
                Console.WriteLine("2. View Ambulance List");
                Console.WriteLine("3. Update Ambulance");
                Console.WriteLine("4. Delete Ambulance");
                Console.WriteLine("5. Logout");

                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());
                
                    switch (choice)
                    {
                        case 1:
                            AddAmbulance();
                            break;
                        case 2:
                            List<Ambulance> lstAmbs = viewAmbulance();
                            DisplayAmbulanceTable(lstAmbs);
                            break;
                        case 3:
                            UpdateAmbulance();
                            break;
                        case 4:
                            DeleteAmbulance();
                            break;
                        case 5:
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer choice.");
                }
            }


        }

        static void UserMenu()
        {
            while (true)
            {
                Console.WriteLine("\n------------- User Menu --------------");
                try
                {

                Console.WriteLine("1. Make a booking");
                Console.WriteLine("2. View bookings");
                Console.WriteLine("3. Update booking");
                Console.WriteLine("4. Delete booking");
                Console.WriteLine("5. Logout");

                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());
               
                    switch (choice)
                    {

                        case 1:
                            Insert();
                            break;
                        case 2:
                            List<Booking> lstEmps = viewBooking();
                            DisplayBookingTable(lstEmps);
                            break;
                        case 3:
                            UpdateBooking();
                            break;
                        case 4:
                            DeleteBooking();
                            break;
                        case 5:
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer choice.");
                }

            }

        }
        static void Insert()
        {
            Console.WriteLine("\n------------- Adding Booking Record --------------");
            
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSqlLocalDb;Initial Catalog=Ambulance_Bookings;Integrated Security=true";
            cn.Open();

            Console.WriteLine("Enter Booking Id:");
            int BookingId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter patient name:");
            string PatientName = Console.ReadLine();

            Console.WriteLine("Enter pickup location:");
            string PickupLocation = Console.ReadLine();

            Console.WriteLine("Enter destination:");
            string DropLocation = Console.ReadLine();

            Console.WriteLine("Enter date and time:");
            DateTime PickupTime = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Enter AmbulanceId:");
            int AmbulanceId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter status:");
            string Status = Console.ReadLine();

            try
            {


                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into Booking values(@BookingID,@PatientName,@PickupLocation,@DropLocation,@PickupTime,@Status,@AmbulanceId)";

                cmd.Parameters.AddWithValue("@BookingID", BookingId);
                cmd.Parameters.AddWithValue("@PatientName", PatientName);
                cmd.Parameters.AddWithValue("@PickupLocation", PickupLocation);
                cmd.Parameters.AddWithValue("@DropLocation ", DropLocation);
                cmd.Parameters.AddWithValue("@PickupTime", PickupTime);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@AmbulanceId", AmbulanceId);




                cmd.ExecuteNonQuery();

                Console.WriteLine("Booking Added");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        static List<Booking> viewBooking()
        {
            Console.WriteLine("\n---------------------- Displaying Booking Details ----------------------");
            Console.WriteLine();
            List<Booking> lstEmps = new List<Booking>();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSqlLocalDb;Initial Catalog=Ambulance_Bookings;Integrated Security=true";
            cn.Open();
           

            try
            {
                //SqlCommand cmd = cn.CreateCommand();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Booking";


                SqlDataReader dr = cmd.ExecuteReader();
                Booking obj;

                while (dr.Read())
                {
                    obj = new Booking();
                    obj.BookingId = dr.GetInt32("BookingId"); ;
                    obj.PatientName = dr.GetString("PatientName");
                    obj.PickupLocation = dr.GetString("PickupLocation");
                    obj.DropLocation = dr.GetString("DropLocation");
                    obj.PickupTime = dr.GetDateTime("PickupTime");
                    obj.Status = dr.GetString("Status");
                    obj.AmbulanceId = dr.GetInt32("AmbulanceId");
                    lstEmps.Add(obj);
                }

                dr.Close();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                cn.Close();
            }
            return lstEmps;


        }
        public static void UpdateBooking()
        {
            Console.WriteLine("\n------------- Updating Booking Record --------------");


            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSqlLocalDb;Initial Catalog=Ambulance_Bookings;Integrated Security=true";



            Console.WriteLine("Enter Booking Id:");
            int BookingId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter patient name:");
            string PatientName = Console.ReadLine();

            Console.WriteLine("Enter pickup location:");
            string PickupLocation = Console.ReadLine();

            Console.WriteLine("Enter destination:");
            string DropLocation = Console.ReadLine();

            Console.WriteLine("Enter date and time:");
            DateTime PickupTime = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Enter AmbulanceId:");
            int AmbulanceId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter status:");
            string Status = Console.ReadLine();
            try
            {
                cn.Open();

                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.CommandType = System.Data.CommandType.Text;
                cmdInsert.CommandText = "update Booking set PatientName =@PatientName,PickupLocation=@PickupLocation,DropLocation=@DropLocation,PickupTime=@PickupTime,Status=@Status,AmbulanceId=@AmbulanceId where BookingId =@BookingID;";




                cmdInsert.Parameters.AddWithValue("@BookingID", BookingId);


                cmdInsert.Parameters.AddWithValue("@PatientName", PatientName);
                cmdInsert.Parameters.AddWithValue("@PickupLocation", PickupLocation);
                cmdInsert.Parameters.AddWithValue("@DropLocation", DropLocation);
                cmdInsert.Parameters.AddWithValue("@PickupTime", PickupTime);
                cmdInsert.Parameters.AddWithValue("@Status", Status);
                cmdInsert.Parameters.AddWithValue("@AmbulanceId", AmbulanceId);

                cmdInsert.ExecuteNonQuery();
                Console.WriteLine("Booking Updated");



            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cn.Close();
            }

        }

        public static void DeleteBooking()
        {
            Console.WriteLine("\n------------- Deleting Booking Record --------------");

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSqlLocalDb;Initial Catalog=Ambulance_Bookings;Integrated Security=true";


            Console.WriteLine("Enter Booking Id:");
            int BookingId = int.Parse(Console.ReadLine());


            try
            {
                cn.Open();
                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.CommandType = System.Data.CommandType.Text;
                cmdInsert.CommandText = "delete from Booking where BookingID =@BookingId";




                cmdInsert.Parameters.AddWithValue("@BookingId", BookingId);
                cmdInsert.ExecuteNonQuery();



            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        static void AddAmbulance()
        {
            Console.WriteLine("\n------------- Adding Ambulance Record --------------");

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSqlLocalDb;Initial Catalog=Ambulance_Bookings;Integrated Security=true";
            cn.Open();

            Console.WriteLine("Enter Ambulance Id:");
            int AmbulanceId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Ambulnce Name:");
            string AmbulanceName = Console.ReadLine();

            Console.WriteLine("Enter Driver Name:");
            string DriverName = Console.ReadLine();

            Console.WriteLine("Enter Contact Number:");
            string ContactNumber = Console.ReadLine();


            Console.WriteLine("Enter Capacity:");
            int Capacity = int.Parse(Console.ReadLine());



            try
            {


                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into Ambulance values(@AmbulanceId,@AmbulanceName,@DriverName,@ContactNumber,@Capacity)";

                cmd.Parameters.AddWithValue("@AmbulanceId", AmbulanceId);
                cmd.Parameters.AddWithValue("@AmbulanceName", AmbulanceName);
                cmd.Parameters.AddWithValue("@DriverName", DriverName);
                cmd.Parameters.AddWithValue("@ContactNumber ", ContactNumber);
                cmd.Parameters.AddWithValue("@Capacity", Capacity);





                cmd.ExecuteNonQuery();

                Console.WriteLine("Ambulance Added");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        static List<Ambulance> viewAmbulance()
        {
            Console.WriteLine("\n------------- Displaying Ambulance Details --------------");
            List<Ambulance> lstAmbs = new List<Ambulance>();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSqlLocalDb;Initial Catalog=Ambulance_Bookings;Integrated Security=true";
            cn.Open();

            try
            {
                //SqlCommand cmd = cn.CreateCommand();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Ambulance";


                SqlDataReader dr = cmd.ExecuteReader();
                Ambulance obj;

                while (dr.Read())
                {
                    obj = new Ambulance();
                    obj.AmbulanceId = dr.GetInt32("AmbulanceId"); ;
                    obj.AmbulanceName = dr.GetString("AmbulanceName");
                    obj.DriverName = dr.GetString("DriverName");
                    obj.ContactNumber = dr.GetString("ContactNumber");
                    obj.Capacity = dr.GetInt32("Capacity");
                    lstAmbs.Add(obj);
                }

                dr.Close();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                cn.Close();
            }
            return lstAmbs;


        }

        public static void UpdateAmbulance()
        {
            Console.WriteLine("\n------------- Updating Ambulance Record --------------");

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSqlLocalDb;Initial Catalog=Ambulance_Bookings;Integrated Security=true";



            Console.WriteLine("Enter Ambulance Id:");
            int AmbulanceId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Ambulance name:");
            string AmbulanceName = Console.ReadLine();

            Console.WriteLine("Enter Driver Name:");
            string DriverName = Console.ReadLine();

            Console.WriteLine("Enter Contact Number:");
            string ContactNumber = Console.ReadLine();

            Console.WriteLine("Enter Ambulance Capacity:");
            int Capacity = int.Parse(Console.ReadLine());






            try
            {
                cn.Open();

                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.CommandType = System.Data.CommandType.Text;
                cmdInsert.CommandText = "update Ambulance set AmbulanceName =@AmbulanceName,DriverName=@DriverName,ContactNumber=@ContactNumber,Capacity=@Capacity where AmbulanceId =@AmbulanceId;";

                cmdInsert.Parameters.AddWithValue("@AmbulanceId", AmbulanceId);
                cmdInsert.Parameters.AddWithValue("@AmbulanceName", AmbulanceName);
                cmdInsert.Parameters.AddWithValue("@DriverName", DriverName);
                cmdInsert.Parameters.AddWithValue("@ContactNumber", ContactNumber);
                cmdInsert.Parameters.AddWithValue("@Capacity", Capacity);


                cmdInsert.ExecuteNonQuery();
                Console.WriteLine("Ambulance Record Updated");



            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cn.Close();
            }

        }

        public static void DeleteAmbulance()
        {
            Console.WriteLine("\n------------- Deleting Ambulance Record --------------");
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSqlLocalDb;Initial Catalog=Ambulance_Bookings;Integrated Security=true";


            Console.WriteLine("Enter Ambulance Id:");
            int AmbulanceId = int.Parse(Console.ReadLine());


            try
            {
                cn.Open();
                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.CommandType = System.Data.CommandType.Text;
                cmdInsert.CommandText = "delete from Ambulance where AmbulanceId =@AmbulanceId";




                cmdInsert.Parameters.AddWithValue("@AmbulanceId", AmbulanceId);
                cmdInsert.ExecuteNonQuery();



            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

    }

    class Booking
    {
        public int BookingId { get; set; }

        public string PatientName { get; set; }


        public string PickupLocation { get; set; }

        public string DropLocation { get; set; }

        public DateTime PickupTime { get; set; }

        public string Status { get; set; }

        public int AmbulanceId { get; set; }
    }

    class Ambulance
    {
        public int AmbulanceId { get; set; }

        public string AmbulanceName { get; set; }

        public string DriverName { get; set; }

        public string ContactNumber { get; set; }

        public int Capacity { get; set; }

    }

    class User
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }

    class Admin
    {
        public int AdminId { get; set; }

        public string AdminName { get; set; }

        public string AdminPassword { get; set; }
    }
}