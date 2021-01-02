using MySql.Data.MySqlClient;
using System;

namespace Wedding_Management_System{

    class User{
        private const string con = "server=localhost;database=weddingmanagementsystem;uid=root;pwd=Nipun@110396";
        private string email;
        private string pass;
        private long phoneNumber;
        public static int loginuserId;

        
        public int login(string email, string password) {
            int id = 0 ,role = 0;
            string email_id = "",pass = "";
            MySqlConnection logincon = new MySqlConnection(con);
            MySqlCommand command = new MySqlCommand("select * from users where email=@email and pass=@password;",logincon);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@password", password);
            try{
                logincon.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read()){
                    id = reader.GetInt32(0);
                    role = reader.GetInt32(1);
                    email_id = reader.GetString(2);
                    pass = reader.GetString(3);
                    if(email_id.Equals(email) && pass.Equals(password)) {
                        loginuserId = id;
                        logincon.Close();
                        return role;
                    }
                }
                return 0;
            }
            catch(Exception e){
                Console.WriteLine("Exception in login user = " + e.Message);
                return -1;
            }
        }

        public bool register(){
            int role = 2;
            Console.WriteLine("Enter Email:");
            this.email = Console.ReadLine();
            Console.WriteLine("Enter Password:");
            this.pass = Console.ReadLine();
            Console.WriteLine("Enter Phone Number:");
            this.phoneNumber = long.Parse(Console.ReadLine());
            String insertQuery = "INSERT INTO users (role, email, pass, phoneNumber) " +
                   "VALUES (@role, @email, @pass, @phoneNumber) ";
            MySqlConnection logincon = new MySqlConnection(con);
            try{
                MySqlCommand cmd = new MySqlCommand(insertQuery, logincon);
                cmd.Parameters.AddWithValue("@role", role);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@pass", pass);
                cmd.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                logincon.Open();
                cmd.ExecuteNonQuery();
                logincon.Close();
                return true;
            }catch(Exception e){
                Console.WriteLine("Exception in registeration of user:" + e.Message);
                return false;
            }
        }
    }
    
    interface crud{
        public bool add();
        public void update();
        public bool remove();
        public void search(string name);
        public void show();
    }

    class Planner:crud {
        private const string con = "server=localhost;database=weddingmanagementsystem;uid=root;pwd=Nipun@110396";
        private string email;
        private long phoneNumber;
        private string address;
        private string planner_name;
        private long price;
        public bool add(){
            Console.WriteLine("Enter Planner Email:");
            this.email = Console.ReadLine();
            Console.WriteLine("Enter Planner Name:");
            this.planner_name = Console.ReadLine();
            Console.WriteLine("Enter Planner Number:");
            this.phoneNumber = long.Parse(Console.ReadLine());
            Console.WriteLine("Enter Planner Address:");
            this.address = Console.ReadLine();
            Console.WriteLine("Enter Planner Price:");
            this.price = long.Parse(Console.ReadLine());
            String insertQuery = "INSERT INTO planner (email, phoneNumber, address, planner_name, price) " +
                   "VALUES (@email, @phoneNumber, @address, @planner_name, @price) ";
            MySqlConnection plannerCon = new MySqlConnection(con);
            try{
                MySqlCommand cmd = new MySqlCommand(insertQuery, plannerCon);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@planner_name", planner_name);
                cmd.Parameters.AddWithValue("@price", price);
                plannerCon.Open();
                cmd.ExecuteNonQuery();
                plannerCon.Close();
                return true;
            }
            catch(Exception e){
                Console.WriteLine("Exception in adding planner = " + e.Message);
                return false;
            }
        }

        public void update(){
            int choice = 0;
            show();
            do{
                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine("0. Go Back");
                Console.WriteLine("1. Update Planners Email");
                Console.WriteLine("2. Update Planners PhoneNumber");
                Console.WriteLine("3. Update Planners Address");
                Console.WriteLine("4. Update Planners Name");
                Console.WriteLine("5. Update Planner Price");
                Console.WriteLine("Enter your choice:");
                choice = int.Parse(Console.ReadLine());
                Console.WriteLine("------------------------------------------------------");
                switch (choice) {
                    case 0: break;
                    case 1: int id = 0;
                            string upateEmail = "";
                            string updatePlannerEmailQuery = "UPDATE planner set email = @email where planner_id = @id";
                            MySqlConnection emailConnection = new MySqlConnection(con);
                            try {
                                MySqlCommand cmd = new MySqlCommand(updatePlannerEmailQuery, emailConnection);
                                Console.WriteLine("Enter Planner Id:");
                                id = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter New Email:");
                                upateEmail = Console.ReadLine();
                                cmd.Parameters.AddWithValue("@email", upateEmail);
                                cmd.Parameters.AddWithValue("@id", id);
                                emailConnection.Open();
                                cmd.ExecuteNonQuery();
                                emailConnection.Close();
                                Console.WriteLine("Email Updated Successfully");
                            }catch(Exception e) {
                                Console.WriteLine("Exception in updating planners email = " + e.Message);
                            }
                            break;
                    case 2: int id1 = 0;
                            long upatePhoneNumber = 0;
                            string updatePlannerPhoneQuery = "UPDATE planner set phoneNumber = @number where planner_id = @id";
                            MySqlConnection phoneConnection = new MySqlConnection(con);
                            try{
                                MySqlCommand cmd = new MySqlCommand(updatePlannerPhoneQuery, phoneConnection);
                                Console.WriteLine("Enter Planner Id:");
                                id1 = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter New Phone Number:");
                                upatePhoneNumber = long.Parse(Console.ReadLine());
                                cmd.Parameters.AddWithValue("@number", upatePhoneNumber);
                                cmd.Parameters.AddWithValue("@id", id1);
                                phoneConnection.Open();
                                cmd.ExecuteNonQuery();
                                phoneConnection.Close();
                                Console.WriteLine("Phone Number Updated Successfully");
                            }
                            catch (Exception e){
                                Console.WriteLine("Exception in updating planners phone number = " + e.Message);
                            }
                            break;
                    case 3: int id2 = 0;
                            string upateAddress = "";
                            string updatePlannerAddressQuery = "UPDATE planner set address = @address where planner_id = @id";
                            MySqlConnection addressConnection = new MySqlConnection(con);
                            try{
                                MySqlCommand cmd = new MySqlCommand(updatePlannerAddressQuery, addressConnection);
                                Console.WriteLine("Enter Planner Id:");
                                id2 = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter New Address:");
                                upateAddress = Console.ReadLine();
                                cmd.Parameters.AddWithValue("@address", upateAddress);
                                cmd.Parameters.AddWithValue("@id", id2);
                                addressConnection.Open();
                                cmd.ExecuteNonQuery();
                                addressConnection.Close();
                                Console.WriteLine("Address Updated Successfully");
                            }
                            catch (Exception e){
                                Console.WriteLine("Exception in updating planners address = " + e.Message);
                            }
                            break;
                    case 4: int id3 = 0;
                            string upateName = "";
                            string updatePlannerNameQuery = "UPDATE planner set planner_name = @name where planner_id = @id";
                            MySqlConnection nameConnection = new MySqlConnection(con);
                            try{
                                MySqlCommand cmd = new MySqlCommand(updatePlannerNameQuery, nameConnection);
                                Console.WriteLine("Enter Planner Id:");
                                id3 = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter New Name:");
                                upateName = Console.ReadLine();
                                cmd.Parameters.AddWithValue("@name", upateName);
                                cmd.Parameters.AddWithValue("@id", id3);
                                nameConnection.Open();
                                cmd.ExecuteNonQuery();
                                nameConnection.Close();
                                Console.WriteLine("Name Updated Successfully");
                            }
                            catch (Exception e){
                                Console.WriteLine("Exception in updating planners name = " + e.Message);
                            }
                            break;
                    case 5: int id4 = 0;
                            long upatePrice = 0;
                            string updatePlannerPriceQuery = "UPDATE planner set price = @price where planner_id = @id";
                            MySqlConnection priceConnection = new MySqlConnection(con);
                            try{
                                MySqlCommand cmd = new MySqlCommand(updatePlannerPriceQuery, priceConnection);
                                Console.WriteLine("Enter Planner Id:");
                                id4 = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter New Price:");
                                upatePrice = long.Parse(Console.ReadLine());
                                cmd.Parameters.AddWithValue("@price", upatePrice);
                                cmd.Parameters.AddWithValue("@id", id4);
                                priceConnection.Open();
                                cmd.ExecuteNonQuery();
                                priceConnection.Close();
                                Console.WriteLine("Price Updated Successfully");
                            }
                            catch (Exception e){
                                Console.WriteLine("Exception in updating planners price = " + e.Message);
                            }
                            break;
                    default:Console.WriteLine("Invalid Input");break;
                }
            } while (choice != 0);
        }

        public bool remove() {
            int id = 0;
            show();
            string removeQuery = "Delete from planner where planner_id=@id";
            MySqlConnection removePlannerConnection = new MySqlConnection(con);
            try {
                MySqlCommand cmd = new MySqlCommand(removeQuery, removePlannerConnection);
                Console.WriteLine("Enter planner id:");
                id = int.Parse(Console.ReadLine());
                cmd.Parameters.AddWithValue("@id", id);
                removePlannerConnection.Open();
                cmd.ExecuteNonQuery();
                removePlannerConnection.Close();
                return true;
            }catch(Exception e) {
                Console.WriteLine("Exception in removing planner = " + e.Message);
                return false;
            }
        }

        public void search(string name) {
            int id = 0;
            string planner_name = "", address = "", email = "";
            long phone = 0,price=0;
            string searchQuery = "select * from planner where planner_name=@name";
            MySqlConnection showConnection = new MySqlConnection(con);
            try {
                MySqlCommand cmd = new MySqlCommand(searchQuery, showConnection);
                cmd.Parameters.AddWithValue("@name", name);
                showConnection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    id = reader.GetInt32(0);
                    email = reader.GetString(2);
                    phone = reader.GetInt64(3);
                    address = reader.GetString(4);
                    planner_name = reader.GetString(1);
                    price = reader.GetInt64(5);
                    Console.WriteLine("------------------------------------------------------");
                    Console.WriteLine("Planner Id = " + id + " , " + " Planner Email = " + email + " , "+" Planner Price = "+ price +" , " + " Planner PhoneNumber = " + phone + " , " + " Planner Address = " + address + " , " + " Planner  Name = " + planner_name);
                    Console.WriteLine("------------------------------------------------------");
                }
                showConnection.Close();
            }
            catch(Exception e) {
                Console.WriteLine("Exception in search planner = " + e.Message);
            }
        }

        public void show(){
            int planner_id = 0;
            string email = "", address = "", planner_name = "";
            long phoneNumber = 0, price=0;
            MySqlConnection showConnection = new MySqlConnection(con);
            MySqlCommand command = new MySqlCommand("select * from planner;", showConnection);
            try {
                showConnection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    planner_id = reader.GetInt32(0);
                    email = reader.GetString(2);
                    phoneNumber = reader.GetInt64(3);
                    address = reader.GetString(4);
                    planner_name = reader.GetString(1);
                    price = reader.GetInt64(5);
                    Console.WriteLine("------------------------------------------------------");
                    Console.WriteLine("Planner Id = "+planner_id+" , "+" Planner Email = "+email+" , "+" Planner Price = "+price+" , "+" Planner PhoneNumber = "+phoneNumber+" , "+" Planner Address = "+address+" , "+" Planner  Name = "+planner_name);
                    Console.WriteLine("------------------------------------------------------");
                }
                showConnection.Close();
            }
            catch(Exception e) {
                Console.WriteLine("Execption in show all planners = " + e.Message);
            }
        }
    }

    class Venue:crud {
        private const string con = "server=localhost;database=weddingmanagementsystem;uid=root;pwd=Nipun@110396";
        private string venue_type;
        private string venue_name;
        private string address;
        private string description;
        private long price;
        private long phoneNumber;

        public bool add(){
            Console.WriteLine("Enter Venue Type:");
            this.venue_type = Console.ReadLine();
            Console.WriteLine("Enter Venue Name:");
            this.venue_name = Console.ReadLine();
            Console.WriteLine("Enter Venue Address:");
            this.address = Console.ReadLine();
            Console.WriteLine("Enter Description:");
            this.description = Console.ReadLine();
            Console.WriteLine("Enter Venue Price:");
            this.price = long.Parse(Console.ReadLine());
            Console.WriteLine("Enter Venue Phone Number:");
            this.phoneNumber = long.Parse(Console.ReadLine());
            String insertQuery = "INSERT INTO venue (venue_type, venue_name, address, description, price, phoneNumber) " +
                   "VALUES (@type, @name, @address, @desc, @price, @phone) ";
            MySqlConnection venueCon = new MySqlConnection(con);
            try{
                MySqlCommand cmd = new MySqlCommand(insertQuery, venueCon);
                cmd.Parameters.AddWithValue("@type", venue_type);
                cmd.Parameters.AddWithValue("@name", venue_name);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@desc", description);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@phone", phoneNumber);
                venueCon.Open();
                cmd.ExecuteNonQuery();
                venueCon.Close();
                return true;
            }
            catch (Exception e){
                Console.WriteLine("Exception in adding venue = " + e.Message);
                return false;
            }
        }

        public void search(string name) {
            int id = 0;
            string venue_type="", venue_name = "", address = "", description = "";
            long phone = 0, price=0;
            string searchQuery = "select * from venue where venue_name=@name";
            MySqlConnection showConnection = new MySqlConnection(con);
            try{
                MySqlCommand cmd = new MySqlCommand(searchQuery, showConnection);
                cmd.Parameters.AddWithValue("@name", name);
                showConnection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()){
                    id = reader.GetInt32(0);
                    venue_type = reader.GetString(1);
                    venue_name = reader.GetString(2);
                    address = reader.GetString(3);
                    description = reader.GetString(4);
                    price = reader.GetInt64(5);
                    phone = reader.GetInt64(6);
                    Console.WriteLine("------------------------------------------------------");
                    Console.WriteLine("Venue Id = " + id + " , " + " Venue Type = " + venue_type + " , " + " Venue Name = " + venue_name + " , " + " Venue Address = " + address + " , " + " Venue Description = " + description + " , "+" Venue Price = "+ price + " , "+" Venue Phone Number = "+ phone);
                    Console.WriteLine("------------------------------------------------------");
                }
                showConnection.Close();
            }
            catch (Exception e){
                Console.WriteLine("Exception in search venue = " + e.Message);
            }
        }

        public bool remove() {
            int id = 0;
            show();
            string removeQuery = "Delete from venue where venue_id=@id";
            MySqlConnection removeVenueConnection = new MySqlConnection(con);
            try{
                MySqlCommand cmd = new MySqlCommand(removeQuery, removeVenueConnection);
                Console.WriteLine("Enter venue id:");
                id = int.Parse(Console.ReadLine());
                cmd.Parameters.AddWithValue("@id", id);
                removeVenueConnection.Open();
                cmd.ExecuteNonQuery();
                removeVenueConnection.Close();
                return true;
            }
            catch (Exception e){
                Console.WriteLine("Exception in removing venue = " + e.Message);
                return false;
            }
        }



        public void update(){
            int choice = 0;
            show();
            do{
                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine("0. Go Back");
                Console.WriteLine("1. Update Venue Type");
                Console.WriteLine("2. Update Venue Name");
                Console.WriteLine("3. Update Venue Address");
                Console.WriteLine("4. Update Venue Description");
                Console.WriteLine("5. Update Venue Price");
                Console.WriteLine("6. Update Venue Phone Number");
                Console.WriteLine("Enter your choice:");
                choice = int.Parse(Console.ReadLine());
                Console.WriteLine("------------------------------------------------------");
                switch (choice){
                    case 0: break;
                    case 1: int id = 0;
                            string upateType = "";
                            string updateVenueTypeQuery = "UPDATE venue set venue_type = @type where venue_id = @id";
                            MySqlConnection typeConnection = new MySqlConnection(con);
                            try{
                                MySqlCommand cmd = new MySqlCommand(updateVenueTypeQuery, typeConnection);
                                Console.WriteLine("Enter Venue Id:");
                                id = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter New Venue Type:");
                                upateType = Console.ReadLine();
                                cmd.Parameters.AddWithValue("@type", upateType);
                                cmd.Parameters.AddWithValue("@id", id);
                                typeConnection.Open();
                                cmd.ExecuteNonQuery();
                                typeConnection.Close();
                                Console.WriteLine("Venue Type Updated Successfully");
                            }
                            catch (Exception e){
                                Console.WriteLine("Exception in updating venue type = " + e.Message);
                            }
                            break;
                    case 2: int id1 = 0;
                            string upateName = "";
                            string updateVenueNameQuery = "UPDATE venue set venue_name = @name where venue_id = @id";
                            MySqlConnection nameConnection = new MySqlConnection(con);
                            try{
                                MySqlCommand cmd = new MySqlCommand(updateVenueNameQuery, nameConnection);
                                Console.WriteLine("Enter Venue Id:");
                                id1 = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter New Venue Name:");
                                upateName = Console.ReadLine();
                                cmd.Parameters.AddWithValue("@name", upateName);
                                cmd.Parameters.AddWithValue("@id", id1);
                                nameConnection.Open();
                                cmd.ExecuteNonQuery();
                                nameConnection.Close();
                                Console.WriteLine("Venue Name Updated Successfully");
                            }
                            catch (Exception e){
                                Console.WriteLine("Exception in updating venue name = " + e.Message);
                            }
                            break;
                    case 3: int id2 = 0;
                            string upateAddress = "";
                            string updateVenueAddressQuery = "UPDATE venue set address = @address where venue_id = @id";
                            MySqlConnection addressConnection = new MySqlConnection(con);
                            try{
                                MySqlCommand cmd = new MySqlCommand(updateVenueAddressQuery, addressConnection);
                                Console.WriteLine("Enter Venue Id:");
                                id2 = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter New Venue Address:");
                                upateAddress = Console.ReadLine();
                                cmd.Parameters.AddWithValue("@address", upateAddress);
                                cmd.Parameters.AddWithValue("@id", id2);
                                addressConnection.Open();
                                cmd.ExecuteNonQuery();
                                addressConnection.Close();
                                Console.WriteLine("Venue Address Updated Successfully");
                            }
                            catch (Exception e){
                                Console.WriteLine("Exception in updating venue address = " + e.Message);
                            }
                            break;
                    case 4: int id3 = 0;
                            string upateDesc = "";
                            string updateVenueDescQuery = "UPDATE venue set description = @desc where venue_id = @id";
                            MySqlConnection descConnection = new MySqlConnection(con);
                            try{
                                MySqlCommand cmd = new MySqlCommand(updateVenueDescQuery, descConnection);
                                Console.WriteLine("Enter Venue Id:");
                                id3 = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter New Venue Description:");
                                upateDesc = Console.ReadLine();
                                cmd.Parameters.AddWithValue("@desc", upateDesc);
                                cmd.Parameters.AddWithValue("@id", id3);
                                descConnection.Open();
                                cmd.ExecuteNonQuery();
                                descConnection.Close();
                                Console.WriteLine("Description Updated Successfully");
                            }
                            catch (Exception e){
                                Console.WriteLine("Exception in updating venue description = " + e.Message);
                            }
                            break;
                    case 5: int id4 = 0;
                            long updatePrice = 0;
                            string updateVenuePriceQuery = "UPDATE venue set price = @price where venue_id = @id";
                            MySqlConnection priceConnection = new MySqlConnection(con);
                            try{
                                MySqlCommand cmd = new MySqlCommand(updateVenuePriceQuery, priceConnection);
                                Console.WriteLine("Enter Venue Id:");
                                id4 = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter New Venue Price:");
                                updatePrice = long.Parse(Console.ReadLine());
                                cmd.Parameters.AddWithValue("@price", updatePrice);
                                cmd.Parameters.AddWithValue("@id", id4);
                                priceConnection.Open();
                                cmd.ExecuteNonQuery();
                                priceConnection.Close();
                                Console.WriteLine("Venue Price Updated Successfully");
                            }
                            catch (Exception e){
                                Console.WriteLine("Exception in updating venue price = " + e.Message);
                            }
                            break;
                    case 6: int id5 = 0;
                            long updatePhone = 0;
                            string updateVenuePhoneQuery = "UPDATE venue set phoneNumber = @phone where venue_id = @id";
                            MySqlConnection phoneConnection = new MySqlConnection(con);
                            try{
                                MySqlCommand cmd = new MySqlCommand(updateVenuePhoneQuery, phoneConnection);
                                Console.WriteLine("Enter Venue Id:");
                                id5 = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter New Venue Phone Number:");
                                updatePhone = long.Parse(Console.ReadLine());
                                cmd.Parameters.AddWithValue("@phone", updatePhone);
                                cmd.Parameters.AddWithValue("@id", id5);
                                phoneConnection.Open();
                                cmd.ExecuteNonQuery();
                                phoneConnection.Close();
                                Console.WriteLine("Venue Phone Number Updated Successfully");
                            }
                            catch (Exception e){
                                Console.WriteLine("Exception in updating venue phone number = " + e.Message);
                            }
                            break;
                    default:Console.WriteLine("Invalid Input"); break;
                }
            } while (choice != 0);
        }

        public void show(){
            int venue_id = 0;
            string venue_type = "", venue_name="", address = "", description="";
            long price=0,phoneNumber = 0;
            MySqlConnection showConnection = new MySqlConnection(con);
            MySqlCommand command = new MySqlCommand("select * from venue;", showConnection);
            try{
                showConnection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read()){
                    venue_id = reader.GetInt32(0);
                    venue_type = reader.GetString(1);
                    venue_name = reader.GetString(2);
                    address = reader.GetString(3);
                    description = reader.GetString(4);
                    price = reader.GetInt64(5);
                    phoneNumber = reader.GetInt64(6);
                    Console.WriteLine("------------------------------------------------------");
                    Console.WriteLine("Venue Id = " + venue_id + " , " + " Venue type = " + venue_type + " , " + " Venue Name = " + venue_name + " , " + " Venue Address = " + address + " , " + " Description = " + description + " , " + " Price = "+ price + " , "+" Venue Phone Number = " + phoneNumber);
                    Console.WriteLine("------------------------------------------------------");
                }
                showConnection.Close();
            }
            catch (Exception e){
                Console.WriteLine("Execption in show all venues = " + e.Message);
            }
        }
        
        
    }

    class Photographer:crud{
        private const string con = "server=localhost;database=weddingmanagementsystem;uid=root;pwd=Nipun@110396";
        private string photographer_name;
        private string email;
        private string address;
        private long price;
        private long phoneNumber;
        public bool add() {
            Console.WriteLine("Enter Photograoher Name:");
            this.photographer_name = Console.ReadLine();
            Console.WriteLine("Enter Photographer Email:");
            this.email = Console.ReadLine();
            Console.WriteLine("Enter Photographer Address:");
            this.address = Console.ReadLine();
            Console.WriteLine("Enter Photographer Price:");
            this.price = long.Parse(Console.ReadLine());
            Console.WriteLine("Enter Photographer Phone Number:");
            this.phoneNumber = long.Parse(Console.ReadLine());
            String insertQuery = "INSERT INTO photographer (photographer_name, phoneNumber, email, address, price) " +
                   "VALUES (@name, @phone, @email, @address, @price) ";
            MySqlConnection photographerCon = new MySqlConnection(con);
            try{
                MySqlCommand cmd = new MySqlCommand(insertQuery, photographerCon);
                cmd.Parameters.AddWithValue("@name", photographer_name);
                cmd.Parameters.AddWithValue("@phone", phoneNumber);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@price", price);
                photographerCon.Open();
                cmd.ExecuteNonQuery();
                photographerCon.Close();
                return true;
            }
            catch (Exception e){
                Console.WriteLine("Exception in adding photographer = " + e.Message);
                return false;
            }
        }
        public void update() {
            int choice = 0;
            show();
            do{
                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine("0. Go Back");
                Console.WriteLine("1. Update Photographer Name");
                Console.WriteLine("2. Update Photographer PhoneNumber");
                Console.WriteLine("3. Update Photographer Address");
                Console.WriteLine("4. Update Photographer Email");
                Console.WriteLine("5. Update Photographer Price");
                Console.WriteLine("Enter your choice:");
                choice = int.Parse(Console.ReadLine());
                Console.WriteLine("------------------------------------------------------");
                switch (choice){
                    case 0: break;
                    case 1: int id = 0;
                            string updateName = "";
                            string updatePhotographerNameQuery = "UPDATE photographer set photographer_name = @name where photographer_id = @id";
                            MySqlConnection nameConnection = new MySqlConnection(con);
                            try{
                                MySqlCommand cmd = new MySqlCommand(updatePhotographerNameQuery, nameConnection);
                                Console.WriteLine("Enter Photographer Id:");
                                id = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter New Name:");
                                updateName = Console.ReadLine();
                                cmd.Parameters.AddWithValue("@name", updateName);
                                cmd.Parameters.AddWithValue("@id", id);
                                nameConnection.Open();
                                cmd.ExecuteNonQuery();
                                nameConnection.Close();
                                Console.WriteLine("Name Updated Successfully");
                            }
                            catch (Exception e){
                                Console.WriteLine("Exception in updating photographer name = " + e.Message);
                            }
                            break;
                    case 2: int id1 = 0;
                            long upatePhoneNumber = 0;
                            string updatePhotographerPhoneQuery = "UPDATE photographer set phoneNumber = @number where photographer_id = @id";
                            MySqlConnection phoneConnection = new MySqlConnection(con);
                            try{
                                MySqlCommand cmd = new MySqlCommand(updatePhotographerPhoneQuery, phoneConnection);
                                Console.WriteLine("Enter Photographer Id:");
                                id1 = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter New Phone Number:");
                                upatePhoneNumber = long.Parse(Console.ReadLine());
                                cmd.Parameters.AddWithValue("@number", upatePhoneNumber);
                                cmd.Parameters.AddWithValue("@id", id1);
                                phoneConnection.Open();
                                cmd.ExecuteNonQuery();
                                phoneConnection.Close();
                                Console.WriteLine("Phone Number Updated Successfully");
                            }
                            catch (Exception e){
                                Console.WriteLine("Exception in updating photographer phone number = " + e.Message);
                            }
                            break;
                    case 3: int id2 = 0;
                            string upateAddress = "";
                            string updatePhotographerAddressQuery = "UPDATE photographer set address = @address where photographer_id = @id";
                            MySqlConnection addressConnection = new MySqlConnection(con);
                            try{
                                MySqlCommand cmd = new MySqlCommand(updatePhotographerAddressQuery, addressConnection);
                                Console.WriteLine("Enter Photographer Id:");
                                id2 = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter New Address:");
                                upateAddress = Console.ReadLine();
                                cmd.Parameters.AddWithValue("@address", upateAddress);
                                cmd.Parameters.AddWithValue("@id", id2);
                                addressConnection.Open();
                                cmd.ExecuteNonQuery();
                                addressConnection.Close();
                                Console.WriteLine("Address Updated Successfully");
                            }
                            catch (Exception e){
                                Console.WriteLine("Exception in updating photographer address = " + e.Message);
                            }
                            break;
                    case 4: int id3 = 0;
                            string upateEmail = "";
                            string updatePhotographerEmailQuery = "UPDATE photographer set email = @email where photographer_id = @id";
                            MySqlConnection emailConnection = new MySqlConnection(con);
                            try{
                                MySqlCommand cmd = new MySqlCommand(updatePhotographerEmailQuery, emailConnection);
                                Console.WriteLine("Enter Photographer Id:");
                                id3 = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter New Email:");
                                upateEmail = Console.ReadLine();
                                cmd.Parameters.AddWithValue("@email", upateEmail);
                                cmd.Parameters.AddWithValue("@id", id3);
                                emailConnection.Open();
                                cmd.ExecuteNonQuery();
                                emailConnection.Close();
                                Console.WriteLine("Email Updated Successfully");
                            }
                            catch (Exception e){
                                Console.WriteLine("Exception in updating photographer email = " + e.Message);
                            }
                            break;
                    case 5: int id4 = 0;
                            long upatePrice = 0;
                            string updatePhotographerPriceQuery = "UPDATE photographer set price = @price where photographer_id = @id";
                            MySqlConnection priceConnection = new MySqlConnection(con);
                            try{
                                MySqlCommand cmd = new MySqlCommand(updatePhotographerPriceQuery, priceConnection);
                                Console.WriteLine("Enter Photographer Id:");
                                id4 = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter New Price:");
                                upatePrice = long.Parse(Console.ReadLine());
                                cmd.Parameters.AddWithValue("@price", upatePrice);
                                cmd.Parameters.AddWithValue("@id", id4);
                                priceConnection.Open();
                                cmd.ExecuteNonQuery();
                                priceConnection.Close();
                                Console.WriteLine("Price Updated Successfully");
                            }
                            catch (Exception e){
                                Console.WriteLine("Exception in updating photographer price = " + e.Message);
                            }
                            break;
                    default: Console.WriteLine("Invalid Input"); break;
                }
            } while (choice != 0);
        }
        public bool remove() {
            int id = 0;
            show();
            string removeQuery = "Delete from photographer where photographer_id=@id";
            MySqlConnection removePhotographerConnection = new MySqlConnection(con);
            try{
                MySqlCommand cmd = new MySqlCommand(removeQuery, removePhotographerConnection);
                Console.WriteLine("Enter photographer id:");
                id = int.Parse(Console.ReadLine());
                cmd.Parameters.AddWithValue("@id", id);
                removePhotographerConnection.Open();
                cmd.ExecuteNonQuery();
                removePhotographerConnection.Close();
                return true;
            }
            catch (Exception e){
                Console.WriteLine("Exception in removing photographer = " + e.Message);
                return false;
            }
        }
        public void search(string name) {
            int id = 0;
            string photographer_name = "", address = "", email = "";
            long phone = 0, price = 0;
            string searchQuery = "select * from photographer where photographer_name=@name";
            MySqlConnection showConnection = new MySqlConnection(con);
            try{
                MySqlCommand cmd = new MySqlCommand(searchQuery, showConnection);
                cmd.Parameters.AddWithValue("@name", name);
                showConnection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()){
                    id = reader.GetInt32(0);
                    photographer_name = reader.GetString(1);
                    phone = reader.GetInt64(2);
                    email = reader.GetString(3);
                    address = reader.GetString(4);
                    price = reader.GetInt64(5);
                    Console.WriteLine("------------------------------------------------------");
                    Console.WriteLine("Photographer Id = " + id + " , " + " Photographer Name = " + photographer_name + " , " + " Photographer Phone Number = " + phone + " , " + " Photographer Email = " + email + " , " + " Photographer Address = " + address + " , " + " Photographer Price = " + price);
                    Console.WriteLine("------------------------------------------------------");
                }
                showConnection.Close();
            }
            catch (Exception e){
                Console.WriteLine("Exception in search photographer = " + e.Message);
            }
        }
        public void show(){
            int id = 0;
            string photographer_name = "", address = "", email = "";
            long phone = 0, price = 0;
            MySqlConnection showConnection = new MySqlConnection(con);
            MySqlCommand command = new MySqlCommand("select * from photographer;", showConnection);
            try{
                showConnection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read()){
                    id = reader.GetInt32(0);
                    photographer_name = reader.GetString(1);
                    phone = reader.GetInt64(2);
                    email = reader.GetString(3);
                    address = reader.GetString(4);
                    price = reader.GetInt64(5);
                    Console.WriteLine("------------------------------------------------------");
                    Console.WriteLine("Photographer Id = " + id + " , " + " Photographer Name = " + photographer_name + " , " + " Photographer Phone Number = " + phone + " , " + " Photographer Email = " + email + " , " + " Photographer Address = " + address + " , " + " Photographer Price = " + price);
                    Console.WriteLine("------------------------------------------------------");
                }
                showConnection.Close();
            }
            catch (Exception e){
                Console.WriteLine("Execption in show all photographers = " + e.Message);
            }
        }
    }

    class Decorator : crud {
        private const string con = "server=localhost;database=weddingmanagementsystem;uid=root;pwd=Nipun@110396";
        private string decorator_name;
        private string email;
        private string address;
        private long price;
        private long phoneNumber;
        public bool add() {
            Console.WriteLine("Enter Decorator Name:");
            this.decorator_name = Console.ReadLine();
            Console.WriteLine("Enter Decorator Email:");
            this.email = Console.ReadLine();
            Console.WriteLine("Enter Decorator Address:");
            this.address = Console.ReadLine();
            Console.WriteLine("Enter Decorator Price:");
            this.price = long.Parse(Console.ReadLine());
            Console.WriteLine("Enter Decorator Phone Number:");
            this.phoneNumber = long.Parse(Console.ReadLine());
            String insertQuery = "INSERT INTO decorator (decorator_name, phoneNumber, email, address, price) " +
                   "VALUES (@name, @phone, @email, @address, @price) ";
            MySqlConnection decoratorCon = new MySqlConnection(con);
            try{
                MySqlCommand cmd = new MySqlCommand(insertQuery, decoratorCon);
                cmd.Parameters.AddWithValue("@name", decorator_name);
                cmd.Parameters.AddWithValue("@phone", phoneNumber);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@price", price);
                decoratorCon.Open();
                cmd.ExecuteNonQuery();
                decoratorCon.Close();
                return true;
            }
            catch (Exception e){
                Console.WriteLine("Exception in adding decorator = " + e.Message);
                return false;
            }
        }
        public void update() {
            int choice = 0;
            show();
            do{
                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine("0. Go Back");
                Console.WriteLine("1. Update Decorator Name");
                Console.WriteLine("2. Update Decorator PhoneNumber");
                Console.WriteLine("3. Update Decorator Address");
                Console.WriteLine("4. Update Decorator Email");
                Console.WriteLine("5. Update Decorator Price");
                Console.WriteLine("Enter your choice:");
                choice = int.Parse(Console.ReadLine());
                Console.WriteLine("------------------------------------------------------");
                switch (choice){
                    case 0: break;
                    case 1: int id = 0;
                            string updateName = "";
                            string updateDecoratorNameQuery = "UPDATE decorator set decorator_name = @name where decorator_id = @id";
                            MySqlConnection nameConnection = new MySqlConnection(con);
                            try{
                                MySqlCommand cmd = new MySqlCommand(updateDecoratorNameQuery, nameConnection);
                                Console.WriteLine("Enter Decorator Id:");
                                id = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter New Name:");
                                updateName = Console.ReadLine();
                                cmd.Parameters.AddWithValue("@name", updateName);
                                cmd.Parameters.AddWithValue("@id", id);
                                nameConnection.Open();
                                cmd.ExecuteNonQuery();
                                nameConnection.Close();
                                Console.WriteLine("Name Updated Successfully");
                            }
                            catch (Exception e){
                                Console.WriteLine("Exception in updating decorator name = " + e.Message);
                            }
                            break;
                    case 2: int id1 = 0;
                            long upatePhoneNumber = 0;
                            string updateDecoratorPhoneQuery = "UPDATE decorator set phoneNumber = @number where decorator_id = @id";
                            MySqlConnection phoneConnection = new MySqlConnection(con);
                            try{
                                MySqlCommand cmd = new MySqlCommand(updateDecoratorPhoneQuery, phoneConnection);
                                Console.WriteLine("Enter Decorator Id:");
                                id1 = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter New Phone Number:");
                                upatePhoneNumber = long.Parse(Console.ReadLine());
                                cmd.Parameters.AddWithValue("@number", upatePhoneNumber);
                                cmd.Parameters.AddWithValue("@id", id1);
                                phoneConnection.Open();
                                cmd.ExecuteNonQuery();
                                phoneConnection.Close();
                                Console.WriteLine("Phone Number Updated Successfully");
                            }
                            catch (Exception e){
                                Console.WriteLine("Exception in updating decorator phone number = " + e.Message);
                            }
                            break;
                    case 3: int id2 = 0;
                            string upateAddress = "";
                            string updateDecoratorAddressQuery = "UPDATE decorator set address = @address where decorator_id = @id";
                            MySqlConnection addressConnection = new MySqlConnection(con);
                            try{
                                MySqlCommand cmd = new MySqlCommand(updateDecoratorAddressQuery, addressConnection);
                                Console.WriteLine("Enter Decorator Id:");
                                id2 = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter New Address:");
                                upateAddress = Console.ReadLine();
                                cmd.Parameters.AddWithValue("@address", upateAddress);
                                cmd.Parameters.AddWithValue("@id", id2);
                                addressConnection.Open();
                                cmd.ExecuteNonQuery();
                                addressConnection.Close();
                                Console.WriteLine("Address Updated Successfully");
                            }
                            catch (Exception e){
                                Console.WriteLine("Exception in updating decorator address = " + e.Message);
                            }
                            break;
                    case 4: int id3 = 0;
                            string upateEmail = "";
                            string updateDecoratorEmailQuery = "UPDATE decorator set email = @email where decorator_id = @id";
                            MySqlConnection emailConnection = new MySqlConnection(con);
                            try{
                                MySqlCommand cmd = new MySqlCommand(updateDecoratorEmailQuery, emailConnection);
                                Console.WriteLine("Enter Decorator Id:");
                                id3 = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter New Email:");
                                upateEmail = Console.ReadLine();
                                cmd.Parameters.AddWithValue("@email", upateEmail);
                                cmd.Parameters.AddWithValue("@id", id3);
                                emailConnection.Open();
                                cmd.ExecuteNonQuery();
                                emailConnection.Close();
                                Console.WriteLine("Email Updated Successfully");
                            }
                            catch (Exception e){
                                Console.WriteLine("Exception in updating decorator email = " + e.Message);
                            }
                            break;
                    case 5: int id4 = 0;
                            long upatePrice = 0;
                            string updateDecoratorPriceQuery = "UPDATE decorator set price = @price where decorator_id = @id";
                            MySqlConnection priceConnection = new MySqlConnection(con);
                            try{
                                MySqlCommand cmd = new MySqlCommand(updateDecoratorPriceQuery, priceConnection);
                                Console.WriteLine("Enter Decorator Id:");
                                id4 = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter New Price:");
                                upatePrice = long.Parse(Console.ReadLine());
                                cmd.Parameters.AddWithValue("@price", upatePrice);
                                cmd.Parameters.AddWithValue("@id", id4);
                                priceConnection.Open();
                                cmd.ExecuteNonQuery();
                                priceConnection.Close();
                                Console.WriteLine("Price Updated Successfully");
                            }
                            catch (Exception e){
                                Console.WriteLine("Exception in updating decorator price = " + e.Message);
                            }
                            break;
                    default: Console.WriteLine("Invalid Input"); break;
                }
            } while (choice != 0);
        }
        public bool remove() {
            int id = 0;
            show();
            string removeQuery = "Delete from decorator where decorator_id=@id";
            MySqlConnection removeDecoratorConnection = new MySqlConnection(con);
            try{
                MySqlCommand cmd = new MySqlCommand(removeQuery, removeDecoratorConnection);
                Console.WriteLine("Enter decorator id:");
                id = int.Parse(Console.ReadLine());
                cmd.Parameters.AddWithValue("@id", id);
                removeDecoratorConnection.Open();
                cmd.ExecuteNonQuery();
                removeDecoratorConnection.Close();
                return true;
            }
            catch (Exception e){
                Console.WriteLine("Exception in removing decorator = " + e.Message);
                return false;
            }
        }
        public void search(string name) {
            int id = 0;
            string decorator_name = "", address = "", email = "";
            long phone = 0, price = 0;
            string searchQuery = "select * from decorator where decorator_name=@name";
            MySqlConnection showConnection = new MySqlConnection(con);
            try{
                MySqlCommand cmd = new MySqlCommand(searchQuery, showConnection);
                cmd.Parameters.AddWithValue("@name", name);
                showConnection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()){
                    id = reader.GetInt32(0);
                    decorator_name = reader.GetString(1);
                    phone = reader.GetInt64(2);
                    email = reader.GetString(3);
                    address = reader.GetString(4);
                    price = reader.GetInt64(5);
                    Console.WriteLine("------------------------------------------------------");
                    Console.WriteLine("Decorator Id = " + id + " , " + " Decorator Name = " + decorator_name + " , " + " Decorator Phone Number = " + phone + " , " + " Decorator Email = " + email + " , " + " Decorator Address = " + address + " , " + " Decorator Price = " + price);
                    Console.WriteLine("------------------------------------------------------");
                }
                showConnection.Close();
            }
            catch (Exception e){
                Console.WriteLine("Exception in search decorator = " + e.Message);
            }
        }
        public void show() {
            int id = 0;
            string decorator_name = "", address = "", email = "";
            long phone = 0, price = 0;
            MySqlConnection showConnection = new MySqlConnection(con);
            MySqlCommand command = new MySqlCommand("select * from decorator;", showConnection);
            try{
                showConnection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read()){
                    id = reader.GetInt32(0);
                    decorator_name = reader.GetString(1);
                    phone = reader.GetInt64(2);
                    email = reader.GetString(3);
                    address = reader.GetString(4);
                    price = reader.GetInt64(5);
                    Console.WriteLine("------------------------------------------------------");
                    Console.WriteLine("Decorator Id = " + id + " , " + " Decorator Name = " + decorator_name + " , " + " Decorator Phone Number = " + phone + " , " + " Decorator Email = " + email + " , " + " Decorator Address = " + address + " , " + " Decorator Price = " + price);
                    Console.WriteLine("------------------------------------------------------");
                }
                showConnection.Close();
            }
            catch (Exception e){
                Console.WriteLine("Execption in show all decorators = " + e.Message);
            }
        }
    }

    class Caterer : crud{
        private const string con = "server=localhost;database=weddingmanagementsystem;uid=root;pwd=Nipun@110396";
        private string caterer_name;
        private string email;
        private string address;
        private long price;
        private long phoneNumber;
        public bool add(){
            Console.WriteLine("Enter Caterer Name:");
            this.caterer_name = Console.ReadLine();
            Console.WriteLine("Enter Caterer Email:");
            this.email = Console.ReadLine();
            Console.WriteLine("Enter Caterer Address:");
            this.address = Console.ReadLine();
            Console.WriteLine("Enter Caterer Price:");
            this.price = long.Parse(Console.ReadLine());
            Console.WriteLine("Enter Caterer Phone Number:");
            this.phoneNumber = long.Parse(Console.ReadLine());
            String insertQuery = "INSERT INTO caterer (caterer_name, phoneNumber, email, address, price) " +
                   "VALUES (@name, @phone, @email, @address, @price) ";
            MySqlConnection catererCon = new MySqlConnection(con);
            try{
                MySqlCommand cmd = new MySqlCommand(insertQuery, catererCon);
                cmd.Parameters.AddWithValue("@name", caterer_name);
                cmd.Parameters.AddWithValue("@phone", phoneNumber);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@price", price);
                catererCon.Open();
                cmd.ExecuteNonQuery();
                catererCon.Close();
                return true;
            }
            catch (Exception e){
                Console.WriteLine("Exception in adding caterer = " + e.Message);
                return false;
            }
        }

        public bool remove(){
            int id = 0;
            show();
            string removeQuery = "Delete from caterer where caterer_id=@id";
            MySqlConnection removeCatererConnection = new MySqlConnection(con);
            try{
                MySqlCommand cmd = new MySqlCommand(removeQuery, removeCatererConnection);
                Console.WriteLine("Enter caterer id:");
                id = int.Parse(Console.ReadLine());
                cmd.Parameters.AddWithValue("@id", id);
                removeCatererConnection.Open();
                cmd.ExecuteNonQuery();
                removeCatererConnection.Close();
                return true;
            }
            catch (Exception e){
                Console.WriteLine("Exception in removing caterer = " + e.Message);
                return false;
            }
        }

        public void search(string name){
            int id = 0;
            string caterer_name = "", address = "", email = "";
            long phone = 0, price = 0;
            string searchQuery = "select * from caterer where caterer_name=@name";
            MySqlConnection showConnection = new MySqlConnection(con);
            try{
                MySqlCommand cmd = new MySqlCommand(searchQuery, showConnection);
                cmd.Parameters.AddWithValue("@name", name);
                showConnection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()){
                    id = reader.GetInt32(0);
                    caterer_name = reader.GetString(1);
                    phone = reader.GetInt64(2);
                    email = reader.GetString(3);
                    address = reader.GetString(4);
                    price = reader.GetInt64(5);
                    Console.WriteLine("------------------------------------------------------");
                    Console.WriteLine("Caterer Id = " + id + " , " + " Caterer Name = " + caterer_name + " , " + " Caterer Phone Number = " + phone + " , " + " Caterer Email = " + email + " , " + " Caterer Address = " + address + " , " + " Caterer Price = " + price);
                    Console.WriteLine("------------------------------------------------------");
                }
                showConnection.Close();
            }
            catch (Exception e){
                Console.WriteLine("Exception in search caterer = " + e.Message);
            }
        }

        public void show(){
            int id = 0;
            string caterer_name = "", address = "", email = "";
            long phone = 0, price = 0;
            MySqlConnection showConnection = new MySqlConnection(con);
            MySqlCommand command = new MySqlCommand("select * from caterer;", showConnection);
            try{
                showConnection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read()){
                    id = reader.GetInt32(0);
                    caterer_name = reader.GetString(1);
                    phone = reader.GetInt64(2);
                    email = reader.GetString(3);
                    address = reader.GetString(4);
                    price = reader.GetInt64(5);
                    Console.WriteLine("------------------------------------------------------");
                    Console.WriteLine("Caterer Id = " + id + " , " + " Caterer Name = " + caterer_name + " , " + " Caterer Phone Number = " + phone + " , " + " Caterer Email = " + email + " , " + " Caterer Address = " + address + " , " + " Caterer Price = " + price);
                    Console.WriteLine("------------------------------------------------------");
                }
                showConnection.Close();
            }
            catch (Exception e){
                Console.WriteLine("Execption in show all caterer = " + e.Message);
            }
        }

        public void update(){
            int choice = 0;
            show();
            do{
                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine("0. Go Back");
                Console.WriteLine("1. Update Caterer Name");
                Console.WriteLine("2. Update Caterer PhoneNumber");
                Console.WriteLine("3. Update Caterer Address");
                Console.WriteLine("4. Update Caterer Email");
                Console.WriteLine("5. Update Caterer Price");
                Console.WriteLine("Enter your choice:");
                choice = int.Parse(Console.ReadLine());
                Console.WriteLine("------------------------------------------------------");
                switch (choice){
                    case 0: break;
                    case 1: int id = 0;
                            string updateName = "";
                            string updateCatererNameQuery = "UPDATE caterer set caterer_name = @name where caterer_id = @id";
                            MySqlConnection nameConnection = new MySqlConnection(con);
                            try{
                                MySqlCommand cmd = new MySqlCommand(updateCatererNameQuery, nameConnection);
                                Console.WriteLine("Enter Caterer Id:");
                                id = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter New Name:");
                                updateName = Console.ReadLine();
                                cmd.Parameters.AddWithValue("@name", updateName);
                                cmd.Parameters.AddWithValue("@id", id);
                                nameConnection.Open();
                                cmd.ExecuteNonQuery();
                                nameConnection.Close();
                                Console.WriteLine("Name Updated Successfully");
                            }
                            catch (Exception e){
                                Console.WriteLine("Exception in updating caterer name = " + e.Message);
                            }
                            break;
                    case 2: int id1 = 0;
                            long upatePhoneNumber = 0;
                            string updateCatererPhoneQuery = "UPDATE caterer set phoneNumber = @number where caterer_id = @id";
                            MySqlConnection phoneConnection = new MySqlConnection(con);
                            try{
                                MySqlCommand cmd = new MySqlCommand(updateCatererPhoneQuery, phoneConnection);
                                Console.WriteLine("Enter Caterer Id:");
                                id1 = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter New Phone Number:");
                                upatePhoneNumber = long.Parse(Console.ReadLine());
                                cmd.Parameters.AddWithValue("@number", upatePhoneNumber);
                                cmd.Parameters.AddWithValue("@id", id1);
                                phoneConnection.Open();
                                cmd.ExecuteNonQuery();
                                phoneConnection.Close();
                                Console.WriteLine("Phone Number Updated Successfully");
                            }
                            catch (Exception e){
                                Console.WriteLine("Exception in updating caterer phone number = " + e.Message);
                            }
                            break;
                    case 3: int id2 = 0;
                            string upateAddress = "";
                            string updateCatererAddressQuery = "UPDATE caterer set address = @address where caterer_id = @id";
                            MySqlConnection addressConnection = new MySqlConnection(con);
                            try{
                                MySqlCommand cmd = new MySqlCommand(updateCatererAddressQuery, addressConnection);
                                Console.WriteLine("Enter Caterer Id:");
                                id2 = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter New Address:");
                                upateAddress = Console.ReadLine();
                                cmd.Parameters.AddWithValue("@address", upateAddress);
                                cmd.Parameters.AddWithValue("@id", id2);
                                addressConnection.Open();
                                cmd.ExecuteNonQuery();
                                addressConnection.Close();
                                Console.WriteLine("Address Updated Successfully");
                            }
                            catch (Exception e){
                                Console.WriteLine("Exception in updating Caterer address = " + e.Message);
                            }
                            break;
                    case 4: int id3 = 0;
                            string upateEmail = "";
                            string updateCatererEmailQuery = "UPDATE caterer set email = @email where caterer_id = @id";
                            MySqlConnection emailConnection = new MySqlConnection(con);
                            try{
                                MySqlCommand cmd = new MySqlCommand(updateCatererEmailQuery, emailConnection);
                                Console.WriteLine("Enter Caterer Id:");
                                id3 = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter New Email:");
                                upateEmail = Console.ReadLine();
                                cmd.Parameters.AddWithValue("@email", upateEmail);
                                cmd.Parameters.AddWithValue("@id", id3);
                                emailConnection.Open();
                                cmd.ExecuteNonQuery();
                                emailConnection.Close();
                                Console.WriteLine("Email Updated Successfully");
                            }
                            catch (Exception e){
                                Console.WriteLine("Exception in updating Caterer email = " + e.Message);
                            }
                            break;
                    case 5: int id4 = 0;
                            long upatePrice = 0;
                            string updateCatererPriceQuery = "UPDATE caterer set price = @price where caterer_id = @id";
                            MySqlConnection priceConnection = new MySqlConnection(con);
                            try{
                                MySqlCommand cmd = new MySqlCommand(updateCatererPriceQuery, priceConnection);
                                Console.WriteLine("Enter Caterer Id:");
                                id4 = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter New Price:");
                                upatePrice = long.Parse(Console.ReadLine());
                                cmd.Parameters.AddWithValue("@price", upatePrice);
                                cmd.Parameters.AddWithValue("@id", id4);
                                priceConnection.Open();
                                cmd.ExecuteNonQuery();
                                priceConnection.Close();
                                Console.WriteLine("Price Updated Successfully");
                            }
                            catch (Exception e){
                                Console.WriteLine("Exception in updating Caterer price = " + e.Message);
                            }
                            break;
                    default: Console.WriteLine("Invalid Input"); break;
                }
            } while (choice != 0);
        }
    }

    class Booking {
        private const string con = "server=localhost;database=weddingmanagementsystem;uid=root;pwd=Nipun@110396";
        public void bookPlanner() {
            int id = 0, bookId=0, user_id=0;
            long price = 0;
            string date = "", status="";
            Planner p = new Planner();
            p.show();
            Console.WriteLine("Enter Planner Id to book:");
            id = int.Parse(Console.ReadLine());
            string query = "select * from planner where planner_id = @id";
            MySqlConnection bookPlannerConnection = new MySqlConnection(con);
            try {
                MySqlCommand cmd = new MySqlCommand(query, bookPlannerConnection);
                cmd.Parameters.AddWithValue("@id", id);
                bookPlannerConnection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows) {
                    while (reader.Read()) { 
                        Console.WriteLine("Enter Booking Date:");
                        date = Console.ReadLine();
                        status = "open";
                        bookId = id;
                        price = reader.GetInt64(5);
                        user_id = User.loginuserId;
                    }
                    reader.Close();
                    string insertquery = "INSERT INTO booking (booking_date, amount, user_id,planner_id, booking_status)" + "VALUES(@date, @amount,@user_id, @planner_id, @status);";
                    MySqlCommand cmd1 = new MySqlCommand(insertquery, bookPlannerConnection);
                    cmd1.Parameters.AddWithValue("@date", date);
                    cmd1.Parameters.AddWithValue("@amount", price);
                    cmd1.Parameters.AddWithValue("@user_id", user_id);
                    cmd1.Parameters.AddWithValue("@planner_id", bookId);
                    cmd1.Parameters.AddWithValue("@status", status);
                    cmd1.ExecuteNonQuery();
                    Console.WriteLine("Congratulations your planner has booked for " + date);
                }else {
                    Console.WriteLine("Please enter valid planner id to book");
                }
                bookPlannerConnection.Close();
            }catch(Exception e) {
                Console.WriteLine("Exception in finding planner at the time of booking planner = " + e.Message);
            }
        }

        public void createWedding() {
            int choice = 0;
            int venue_id=0, photographer_id=0, decorator_id=0, caterer_id=0, user_id=0;
            long total_amount = 0,venue_price=0, photographer_price=0, decorator_price=0, caterer_price=0;
            string date="", status="";
            do{
                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine("0. Go Back");
                Console.WriteLine("1. Book Venue.");
                Console.WriteLine("2. Book Photographer.");
                Console.WriteLine("3. Book Decorator.");
                Console.WriteLine("4. Book Caterer.");
                Console.WriteLine("5. Done with booking.");
                Console.WriteLine("Enter your choice:");
                choice = int.Parse(Console.ReadLine());
                Console.WriteLine("------------------------------------------------------");
                switch (choice) {
                    case 0:break;
                    case 1: Venue v = new Venue();
                            v.show();
                            Console.WriteLine("Enter Venue Id to reserve.");
                            venue_id = int.Parse(Console.ReadLine());
                            string query = "select * from venue where venue_id = @id";
                            MySqlConnection venueConnect = new MySqlConnection(con);
                            try {
                                MySqlCommand command = new MySqlCommand(query, venueConnect);
                                command.Parameters.AddWithValue("@id", venue_id);
                                venueConnect.Open();
                                MySqlDataReader reader = command.ExecuteReader();
                                if (reader.HasRows) {
                                    while (reader.Read()) {
                                        venue_price = reader.GetInt64(5);
                                    }
                                    Console.WriteLine("Congratulation your venue is reserved.");
                                    Console.WriteLine("Please continue with other bookings or press 5 to confirm your booking.");
                                }
                                else {
                                    venue_id = 0;
                                    venue_price = 0;
                                    Console.WriteLine("Please enter valid venue id to reserve");
                                }
                                reader.Close();
                                venueConnect.Close();
                            }catch(Exception e) {
                                Console.WriteLine("Exception in Venue Booking = " + e.Message);
                            }
                            break;
                    case 2: Photographer p = new Photographer();
                            p.show();
                            Console.WriteLine("Enter Photographer Id to reserve.");
                            photographer_id = int.Parse(Console.ReadLine());
                            string query1 = "select * from photographer where photographer_id = @id";
                            MySqlConnection photographerConnect = new MySqlConnection(con);
                            try{
                                MySqlCommand command = new MySqlCommand(query1, photographerConnect);
                                command.Parameters.AddWithValue("@id", photographer_id);
                                photographerConnect.Open();
                                MySqlDataReader reader = command.ExecuteReader();
                                if (reader.HasRows){
                                    while (reader.Read()) {
                                        photographer_price = reader.GetInt64(5);
                                    }
                                    Console.WriteLine("Congratulation your photographer is reserved.");
                                    Console.WriteLine("Please continue with other bookings or press 5 to confirm your booking.");
                                }
                                else{
                                    photographer_id = 0;
                                    photographer_price = 0;
                                    Console.WriteLine("Please enter valid photographer id to reserve");
                                }
                                reader.Close();
                                photographerConnect.Close();
                            }catch (Exception e){
                                Console.WriteLine("Exception in Photographer Booking = " + e.Message);
                            }
                            break;
                    case 3: Decorator d = new Decorator();
                            d.show();
                            Console.WriteLine("Enter Decorator Id to reserve.");
                            decorator_id = int.Parse(Console.ReadLine());
                            string query2 = "select * from decorator where decorator_id = @id";
                            MySqlConnection decoratorConnect = new MySqlConnection(con);
                            try{
                                MySqlCommand command = new MySqlCommand(query2, decoratorConnect);
                                command.Parameters.AddWithValue("@id", decorator_id);
                                decoratorConnect.Open();
                                MySqlDataReader reader = command.ExecuteReader();
                                if (reader.HasRows){
                                    while (reader.Read()) {
                                        decorator_price = reader.GetInt64(5);
                                    }
                                    Console.WriteLine("Congratulation your decorator is reserved.");
                                    Console.WriteLine("Please continue with other bookings or press 5 to confirm your booking.");
                                }
                                else{
                                    decorator_id = 0;
                                    decorator_price = 0;
                                    Console.WriteLine("Please enter valid decorator id to reserve");
                                }
                                reader.Close();
                                decoratorConnect.Close();
                            }catch (Exception e){
                                Console.WriteLine("Exception in Decorator Booking = " + e.Message);
                            }
                            break;
                    case 4: Caterer c = new Caterer();
                            c.show();
                            Console.WriteLine("Enter Caterer Id to reserve.");
                            caterer_id = int.Parse(Console.ReadLine());
                            string query3 = "select * from caterer where caterer_id = @id";
                            MySqlConnection catererConnect = new MySqlConnection(con);
                            try{
                                MySqlCommand command = new MySqlCommand(query3, catererConnect);
                                command.Parameters.AddWithValue("@id", caterer_id);
                                catererConnect.Open();
                                MySqlDataReader reader = command.ExecuteReader();
                                if (reader.HasRows){
                                    while (reader.Read()) {
                                        caterer_price = reader.GetInt64(5);
                                    }
                                    Console.WriteLine("Congratulation your caterer is reserved.");
                                    Console.WriteLine("Please continue with other bookings or press 5 to confirm your booking.");
                                }else{
                                    caterer_id = 0;
                                    caterer_price = 0;
                                    Console.WriteLine("Please enter valid caterer id to reserve");
                                }
                                reader.Close();
                                catererConnect.Close();
                            }catch (Exception e){
                                Console.WriteLine("Exception in Caterer Booking = " + e.Message);
                            }
                            break;
                    case 5: Console.WriteLine("Enter Date of booking:");
                            date = Console.ReadLine();
                            string insertquery = "INSERT INTO booking (booking_date, amount, user_id, venue_id, photographer_id, decorator_id, caterer_id,booking_status)" + "VALUES(@date, @amount,@user_id, @venue_id, @photographer_id, @decorator_id, @caterer_id,@status);";
                            string insertWithoutVenue = "INSERT INTO booking (booking_date, amount, user_id, photographer_id, decorator_id, caterer_id,booking_status)" + "VALUES(@date, @amount,@user_id, @photographer_id, @decorator_id, @caterer_id,@status);";
                            string insertWithoutVenueAndPhotographer = "INSERT INTO booking (booking_date, amount, user_id, decorator_id, caterer_id,booking_status)" + "VALUES(@date, @amount,@user_id, @decorator_id, @caterer_id,@status);";
                            string insertWithoutVenueAndPhotographerAndDecorator = "INSERT INTO booking (booking_date, amount, user_id, caterer_id,booking_status)" + "VALUES(@date, @amount,@user_id, @caterer_id,@status);";
                            string insertWithoutPhotographer = "INSERT INTO booking (booking_date, amount, user_id, venue_id, decorator_id, caterer_id,booking_status)" + "VALUES(@date, @amount,@user_id, @venue_id, @decorator_id, @caterer_id,@status);";
                            string insertWithoutPhotographerAndDecorator = "INSERT INTO booking (booking_date, amount, user_id, venue_id, caterer_id,booking_status)" + "VALUES(@date, @amount,@user_id, @venue_id, @caterer_id,@status);";
                            string insertWithoutPhotographerAndDecoratorAndCaterer = "INSERT INTO booking (booking_date, amount, user_id, venue_id, booking_status)" + "VALUES(@date, @amount,@user_id, @venue_id,@status);";
                            string insertWithoutDecorator = "INSERT INTO booking (booking_date, amount, user_id, venue_id, photographer_id, caterer_id,booking_status)" + "VALUES(@date, @amount,@user_id, @venue_id, @photographer_id, @caterer_id,@status);";
                            string insertWithoutDecoratorAndCaterer = "INSERT INTO booking (booking_date, amount, user_id, venue_id, photographer_id,booking_status)" + "VALUES(@date, @amount,@user_id, @venue_id, @photographer_id,@status);";
                            string insertWithoutDecoratorAndCatererAndVenue = "INSERT INTO booking (booking_date, amount, user_id, photographer_id, booking_status)" + "VALUES(@date, @amount,@user_id, @photographer_id,@status);";
                            string insertWithoutCaterer = "INSERT INTO booking (booking_date, amount, user_id, venue_id, photographer_id, decorator_id, booking_status)" + "VALUES(@date, @amount,@user_id, @venue_id, @photographer_id, @decorator_id,@status);";
                            string insertWithoutCatererAndVenue = "INSERT INTO booking (booking_date, amount, user_id, photographer_id, decorator_id, booking_status)" + "VALUES(@date, @amount,@user_id, @photographer_id, @decorator_id,@status);";
                            string insertWithoutCatererAndVenueAndPhotographer = "INSERT INTO booking (booking_date, amount, user_id, decorator_id, booking_status)" + "VALUES(@date, @amount,@user_id, @decorator_id,@status)";
                            MySqlConnection bookConnection = new MySqlConnection(con);
                            if (venue_id == 0){
                                MySqlCommand cmdWithoutVenue = new MySqlCommand(insertWithoutVenue, bookConnection);
                                try{
                                    total_amount = photographer_price + decorator_price + caterer_price;
                                    user_id = User.loginuserId;
                                    status = "open";
                                    cmdWithoutVenue.Parameters.AddWithValue("@date", date);
                                    cmdWithoutVenue.Parameters.AddWithValue("@amount", total_amount);
                                    cmdWithoutVenue.Parameters.AddWithValue("@user_id", user_id);
                                    cmdWithoutVenue.Parameters.AddWithValue("@photographer_id", photographer_id);
                                    cmdWithoutVenue.Parameters.AddWithValue("@decorator_id", decorator_id);
                                    cmdWithoutVenue.Parameters.AddWithValue("@caterer_id", caterer_id);
                                    cmdWithoutVenue.Parameters.AddWithValue("@status", status);
                                    bookConnection.Open();
                                    cmdWithoutVenue.ExecuteNonQuery();
                                    bookConnection.Close();
                                    Console.WriteLine("Congratulations your booking is done for " + date);
                                    Console.WriteLine("Total amount to pay = " + total_amount);
                                }
                                catch (Exception e){
                                    Console.WriteLine("Exception in booking without venue = " + e.Message);
                                }
                            }
                            else if(venue_id == 0 && photographer_id == 0) {
                                MySqlCommand cmdWithoutVenueAndPhotographer = new MySqlCommand(insertWithoutVenueAndPhotographer, bookConnection);
                                try{
                                    total_amount = decorator_price + caterer_price;
                                    user_id = User.loginuserId;
                                    status = "open";
                                    cmdWithoutVenueAndPhotographer.Parameters.AddWithValue("@date", date);
                                    cmdWithoutVenueAndPhotographer.Parameters.AddWithValue("@amount", total_amount);
                                    cmdWithoutVenueAndPhotographer.Parameters.AddWithValue("@user_id", user_id);
                                    cmdWithoutVenueAndPhotographer.Parameters.AddWithValue("@decorator_id", decorator_id);
                                    cmdWithoutVenueAndPhotographer.Parameters.AddWithValue("@caterer_id", caterer_id);
                                    cmdWithoutVenueAndPhotographer.Parameters.AddWithValue("@status", status);
                                    bookConnection.Open();
                                    cmdWithoutVenueAndPhotographer.ExecuteNonQuery();
                                    bookConnection.Close();
                                    Console.WriteLine("Congratulations your booking is done for " + date);
                                    Console.WriteLine("Total amount to pay = " + total_amount);
                                }
                                catch (Exception e){
                                    Console.WriteLine("Exception in booking without venue and photographer = " + e.Message);
                                }
                            }
                            else if(venue_id==0 && photographer_id==0 && decorator_id == 0) {
                                MySqlCommand cmdWithoutVenueAndPhotographerAndDecorator = new MySqlCommand(insertWithoutVenueAndPhotographerAndDecorator, bookConnection);
                                try{
                                    total_amount = caterer_price;
                                    user_id = User.loginuserId;
                                    status = "open";
                                    cmdWithoutVenueAndPhotographerAndDecorator.Parameters.AddWithValue("@date", date);
                                    cmdWithoutVenueAndPhotographerAndDecorator.Parameters.AddWithValue("@amount", total_amount);
                                    cmdWithoutVenueAndPhotographerAndDecorator.Parameters.AddWithValue("@user_id", user_id);
                                    cmdWithoutVenueAndPhotographerAndDecorator.Parameters.AddWithValue("@decorator_id", decorator_id);
                                    cmdWithoutVenueAndPhotographerAndDecorator.Parameters.AddWithValue("@caterer_id", caterer_id);
                                    cmdWithoutVenueAndPhotographerAndDecorator.Parameters.AddWithValue("@status", status);
                                    bookConnection.Open();
                                    cmdWithoutVenueAndPhotographerAndDecorator.ExecuteNonQuery();
                                    bookConnection.Close();
                                    Console.WriteLine("Congratulations your booking is done for " + date);
                                    Console.WriteLine("Total amount to pay = " + total_amount);
                                }
                                catch (Exception e){
                                    Console.WriteLine("Exception in booking without venue and photographer and decorator= " + e.Message);
                                }
                            }
                            else if(photographer_id == 0) {
                                MySqlCommand cmdWithoutPhotographer = new MySqlCommand(insertWithoutPhotographer, bookConnection);
                                try{
                                    total_amount = venue_price + decorator_price + caterer_price;
                                    user_id = User.loginuserId;
                                    status = "open";
                                    cmdWithoutPhotographer.Parameters.AddWithValue("@date", date);
                                    cmdWithoutPhotographer.Parameters.AddWithValue("@amount", total_amount);
                                    cmdWithoutPhotographer.Parameters.AddWithValue("@user_id", user_id);
                                    cmdWithoutPhotographer.Parameters.AddWithValue("@venue_id", venue_id);
                                    cmdWithoutPhotographer.Parameters.AddWithValue("@decorator_id", decorator_id);
                                    cmdWithoutPhotographer.Parameters.AddWithValue("@caterer_id", caterer_id);
                                    cmdWithoutPhotographer.Parameters.AddWithValue("@status", status);
                                    bookConnection.Open();
                                    cmdWithoutPhotographer.ExecuteNonQuery();
                                    bookConnection.Close();
                                    Console.WriteLine("Congratulations your booking is done for " + date);
                                    Console.WriteLine("Total amount to pay = " + total_amount);
                                }
                                catch (Exception e){
                                    Console.WriteLine("Exception in booking without photographer = " + e.Message);
                                }
                            }
                            else if(photographer_id==0 && decorator_id == 0) {
                                MySqlCommand cmdWithoutPhotographerAndDecorator = new MySqlCommand(insertWithoutPhotographerAndDecorator, bookConnection);
                                try{
                                    total_amount = venue_price + caterer_price;
                                    user_id = User.loginuserId;
                                    status = "open";
                                    cmdWithoutPhotographerAndDecorator.Parameters.AddWithValue("@date", date);
                                    cmdWithoutPhotographerAndDecorator.Parameters.AddWithValue("@amount", total_amount);
                                    cmdWithoutPhotographerAndDecorator.Parameters.AddWithValue("@user_id", user_id);
                                    cmdWithoutPhotographerAndDecorator.Parameters.AddWithValue("@venue_id", venue_id);
                                    cmdWithoutPhotographerAndDecorator.Parameters.AddWithValue("@caterer_id", caterer_id);
                                    cmdWithoutPhotographerAndDecorator.Parameters.AddWithValue("@status", status);
                                    bookConnection.Open();
                                    cmdWithoutPhotographerAndDecorator.ExecuteNonQuery();
                                    bookConnection.Close();
                                    Console.WriteLine("Congratulations your booking is done for " + date);
                                    Console.WriteLine("Total amount to pay = " + total_amount);
                                }
                                catch (Exception e){
                                    Console.WriteLine("Exception in booking without photographer and decorator = " + e.Message);
                                }
                            }
                            else if(photographer_id==0 && decorator_id==0 && caterer_id == 0) {
                                MySqlCommand cmdWithoutPhotographerAndDecoratorAndCaterer = new MySqlCommand(insertWithoutPhotographerAndDecoratorAndCaterer, bookConnection);
                                try{
                                    total_amount = venue_price;
                                    user_id = User.loginuserId;
                                    status = "open";
                                    cmdWithoutPhotographerAndDecoratorAndCaterer.Parameters.AddWithValue("@date", date);
                                    cmdWithoutPhotographerAndDecoratorAndCaterer.Parameters.AddWithValue("@amount", total_amount);
                                    cmdWithoutPhotographerAndDecoratorAndCaterer.Parameters.AddWithValue("@user_id", user_id);
                                    cmdWithoutPhotographerAndDecoratorAndCaterer.Parameters.AddWithValue("@venue_id", venue_id);
                                    cmdWithoutPhotographerAndDecoratorAndCaterer.Parameters.AddWithValue("@status", status);
                                    bookConnection.Open();
                                    cmdWithoutPhotographerAndDecoratorAndCaterer.ExecuteNonQuery();
                                    bookConnection.Close();
                                    Console.WriteLine("Congratulations your booking is done for " + date);
                                    Console.WriteLine("Total amount to pay = " + total_amount);
                                }
                                catch (Exception e){
                                    Console.WriteLine("Exception in booking without photographer and decorator and caterer = " + e.Message);
                                }
                            }
                            else if (decorator_id == 0) {
                                MySqlCommand cmdWithoutDecorator = new MySqlCommand(insertWithoutDecorator, bookConnection);
                                try{
                                    total_amount = venue_price + photographer_price + caterer_price;
                                    user_id = User.loginuserId;
                                    status = "open";
                                    cmdWithoutDecorator.Parameters.AddWithValue("@date", date);
                                    cmdWithoutDecorator.Parameters.AddWithValue("@amount", total_amount);
                                    cmdWithoutDecorator.Parameters.AddWithValue("@user_id", user_id);
                                    cmdWithoutDecorator.Parameters.AddWithValue("@venue_id", venue_id);
                                    cmdWithoutDecorator.Parameters.AddWithValue("@photographer_id", photographer_id);
                                    cmdWithoutDecorator.Parameters.AddWithValue("@caterer_id", caterer_id);
                                    cmdWithoutDecorator.Parameters.AddWithValue("@status", status);
                                    bookConnection.Open();
                                    cmdWithoutDecorator.ExecuteNonQuery();
                                    bookConnection.Close();
                                    Console.WriteLine("Congratulations your booking is done for " + date);
                                    Console.WriteLine("Total amount to pay = " + total_amount);
                                }
                                catch (Exception e){
                                    Console.WriteLine("Exception in booking without decorator = " + e.Message);
                                }
                            }
                            else if(decorator_id==0 && caterer_id == 0){
                                MySqlCommand cmdWithoutDecoratorAndCaterer = new MySqlCommand(insertWithoutDecoratorAndCaterer, bookConnection);
                                try{
                                    total_amount = venue_price + photographer_price;
                                    user_id = User.loginuserId;
                                    status = "open";
                                    cmdWithoutDecoratorAndCaterer.Parameters.AddWithValue("@date", date);
                                    cmdWithoutDecoratorAndCaterer.Parameters.AddWithValue("@amount", total_amount);
                                    cmdWithoutDecoratorAndCaterer.Parameters.AddWithValue("@user_id", user_id);
                                    cmdWithoutDecoratorAndCaterer.Parameters.AddWithValue("@venue_id", venue_id);
                                    cmdWithoutDecoratorAndCaterer.Parameters.AddWithValue("@photographer_id", photographer_id);
                                    cmdWithoutDecoratorAndCaterer.Parameters.AddWithValue("@status", status);
                                    bookConnection.Open();
                                    cmdWithoutDecoratorAndCaterer.ExecuteNonQuery();
                                    bookConnection.Close();
                                    Console.WriteLine("Congratulations your booking is done for " + date);
                                    Console.WriteLine("Total amount to pay = " + total_amount);
                                }
                                catch (Exception e){
                                    Console.WriteLine("Exception in booking without decorator and caterer = " + e.Message);
                                }
                            }
                            else if(decorator_id==0 && caterer_id==0 && venue_id == 0) {
                                MySqlCommand cmdWithoutDecoratorAndCatererAndVenue = new MySqlCommand(insertWithoutDecoratorAndCatererAndVenue, bookConnection);
                                try{
                                    total_amount = photographer_price;
                                    user_id = User.loginuserId;
                                    status = "open";
                                    cmdWithoutDecoratorAndCatererAndVenue.Parameters.AddWithValue("@date", date);
                                    cmdWithoutDecoratorAndCatererAndVenue.Parameters.AddWithValue("@amount", total_amount);
                                    cmdWithoutDecoratorAndCatererAndVenue.Parameters.AddWithValue("@user_id", user_id);
                                    cmdWithoutDecoratorAndCatererAndVenue.Parameters.AddWithValue("@photographer_id", photographer_id);
                                    cmdWithoutDecoratorAndCatererAndVenue.Parameters.AddWithValue("@status", status);
                                    bookConnection.Open();
                                    cmdWithoutDecoratorAndCatererAndVenue.ExecuteNonQuery();
                                    bookConnection.Close();
                                    Console.WriteLine("Congratulations your booking is done for " + date);
                                    Console.WriteLine("Total amount to pay = " + total_amount);
                                }
                                catch (Exception e){
                                    Console.WriteLine("Exception in booking without decorator and caterer and venue= " + e.Message);
                                }
                            }
                            else if (caterer_id == 0) {
                                MySqlCommand cmdWithoutCaterer = new MySqlCommand(insertWithoutCaterer, bookConnection);
                                try{
                                    total_amount = venue_price + photographer_price + decorator_price;
                                    user_id = User.loginuserId;
                                    status = "open";
                                    cmdWithoutCaterer.Parameters.AddWithValue("@date", date);
                                    cmdWithoutCaterer.Parameters.AddWithValue("@amount", total_amount);
                                    cmdWithoutCaterer.Parameters.AddWithValue("@user_id", user_id);
                                    cmdWithoutCaterer.Parameters.AddWithValue("@venue_id", venue_id);
                                    cmdWithoutCaterer.Parameters.AddWithValue("@photographer_id", photographer_id);
                                    cmdWithoutCaterer.Parameters.AddWithValue("@decorator_id", decorator_id);
                                    cmdWithoutCaterer.Parameters.AddWithValue("@status", status);
                                    bookConnection.Open();
                                    cmdWithoutCaterer.ExecuteNonQuery();
                                    bookConnection.Close();
                                    Console.WriteLine("Congratulations your booking is done for " + date);
                                    Console.WriteLine("Total amount to pay = " + total_amount);
                                }
                                catch (Exception e){
                                    Console.WriteLine("Exception in booking without caterer = " + e.Message);
                                }
                            }
                            else if(caterer_id==0 && venue_id == 0) {
                                MySqlCommand cmdWithoutCatererAndVenue = new MySqlCommand(insertWithoutCatererAndVenue, bookConnection);
                                try{
                                    total_amount = photographer_price + decorator_price;
                                    user_id = User.loginuserId;
                                    status = "open";
                                    cmdWithoutCatererAndVenue.Parameters.AddWithValue("@date", date);
                                    cmdWithoutCatererAndVenue.Parameters.AddWithValue("@amount", total_amount);
                                    cmdWithoutCatererAndVenue.Parameters.AddWithValue("@user_id", user_id);
                                    cmdWithoutCatererAndVenue.Parameters.AddWithValue("@photographer_id", photographer_id);
                                    cmdWithoutCatererAndVenue.Parameters.AddWithValue("@decorator_id", decorator_id);
                                    cmdWithoutCatererAndVenue.Parameters.AddWithValue("@status", status);
                                    bookConnection.Open();
                                    cmdWithoutCatererAndVenue.ExecuteNonQuery();
                                    bookConnection.Close();
                                    Console.WriteLine("Congratulations your booking is done for " + date);
                                    Console.WriteLine("Total amount to pay = " + total_amount);
                                }
                                catch (Exception e){
                                    Console.WriteLine("Exception in booking without caterer and venue = " + e.Message);
                                }
                            }
                            else if(caterer_id==0 && venue_id==0 && photographer_id == 0) {
                                MySqlCommand cmdWithoutCatererAndVenueAndPhotographer = new MySqlCommand(insertWithoutCatererAndVenueAndPhotographer, bookConnection);
                                try{
                                    total_amount = decorator_price;
                                    user_id = User.loginuserId;
                                    status = "open";
                                    cmdWithoutCatererAndVenueAndPhotographer.Parameters.AddWithValue("@date", date);
                                    cmdWithoutCatererAndVenueAndPhotographer.Parameters.AddWithValue("@amount", total_amount);
                                    cmdWithoutCatererAndVenueAndPhotographer.Parameters.AddWithValue("@user_id", user_id);
                                    cmdWithoutCatererAndVenueAndPhotographer.Parameters.AddWithValue("@decorator_id", decorator_id);
                                    cmdWithoutCatererAndVenueAndPhotographer.Parameters.AddWithValue("@status", status);
                                    bookConnection.Open();
                                    cmdWithoutCatererAndVenueAndPhotographer.ExecuteNonQuery();
                                    bookConnection.Close();
                                    Console.WriteLine("Congratulations your booking is done for " + date);
                                    Console.WriteLine("Total amount to pay = " + total_amount);
                                }
                                catch (Exception e){
                                    Console.WriteLine("Exception in booking without caterer and venue and photographer = " + e.Message);
                                }
                            }
                            else{
                                MySqlCommand cmd1 = new MySqlCommand(insertquery, bookConnection);
                                try{
                                    total_amount = venue_price + photographer_price + decorator_price + caterer_price;
                                    user_id = User.loginuserId;
                                    status = "open";
                                    cmd1.Parameters.AddWithValue("@date", date);
                                    cmd1.Parameters.AddWithValue("@amount", total_amount);
                                    cmd1.Parameters.AddWithValue("@user_id", user_id);
                                    cmd1.Parameters.AddWithValue("@venue_id", venue_id);
                                    cmd1.Parameters.AddWithValue("@photographer_id", photographer_id);
                                    cmd1.Parameters.AddWithValue("@decorator_id", decorator_id);
                                    cmd1.Parameters.AddWithValue("@caterer_id", caterer_id);
                                    cmd1.Parameters.AddWithValue("@status", status);
                                    bookConnection.Open();
                                    cmd1.ExecuteNonQuery();
                                    bookConnection.Close();
                                    Console.WriteLine("Congratulations your booking is done for " + date);
                                    Console.WriteLine("Total amount to pay = " + total_amount);
                                }
                                catch (Exception e){
                                    Console.WriteLine("Exception in booking = " + e.Message);
                                }
                            }
                            break;
                    default:Console.WriteLine("Invalid input");break;
                }
            } while (choice != 0);
        }

        public void showBooking(int userId) {
            int booking_id=0,user_id=0,venue_id=0,planner_id=0,photographer_id=0,decorator_id=0,caterer_id=0;
            string date="", status="";
            long amount=0;
            string query = "select * from booking where user_id = @id";
            MySqlConnection showConnection = new MySqlConnection(con);
            try{
                MySqlCommand cmd = new MySqlCommand(query, showConnection);
                cmd.Parameters.AddWithValue("@id", userId);
                showConnection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows){
                    while (reader.Read()) {
                        booking_id = reader.GetInt32(0);
                        date = reader.GetString(1);
                        amount = reader.GetInt64(2);
                        user_id = reader.GetInt32(3);
                        venue_id = reader.GetInt32(4);
                        planner_id = reader.GetInt32(5);
                        photographer_id = reader.GetInt32(6);
                        decorator_id = reader.GetInt32(7);
                        caterer_id = reader.GetInt32(8);
                        status = reader.GetString(9);
                        Console.WriteLine("------------------------------------------------------");
                        Console.WriteLine("Booking id = " + booking_id);
                        Console.WriteLine("Booking date = " + date);
                        Console.WriteLine("Booking amount = " + amount);
                        Console.WriteLine("Venue = " + venue_id);
                        Console.WriteLine("Planner = " + planner_id);
                        Console.WriteLine("Photographer = " + photographer_id);
                        Console.WriteLine("Decorator = " + decorator_id);
                        Console.WriteLine("Caterer = " + caterer_id);
                        Console.WriteLine("Booking status = " + status);
                        Console.WriteLine("------------------------------------------------------");
                    }
                }
                else {
                    Console.WriteLine("You have no bookings.");
                }
                showConnection.Close();
            }catch(Exception e) {
                Console.WriteLine("Exception in show bookings = " + e.Message);
            }
        }

        public void cancelBooking(){

        }
    }

    class Program{
        
        static void Main(string[] args){
            int choice = 0, choice1=0, choice2=0;
            int choicePlanner=0, choiceVenue=0, choiceDecorator=0, choicePhotographer=0, choiceCaterer=0, choicePackage=0;
            do{
                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("Enter your choice:");
                choice = int.Parse(Console.ReadLine());
                Console.WriteLine("------------------------------------------------------");
                switch (choice) {
                    case 0: System.Environment.Exit(0);break;
                    case 1: string email = " ";
                            string password = " ";
                            User u = new User();
                            Console.WriteLine("Enter Email:");
                            email = Console.ReadLine();
                            Console.WriteLine("Enter Password:");
                            password = Console.ReadLine();
                            int role = u.login(email, password);
                            if(role == 0){
                                Console.WriteLine("Something is wrong with login");
                            }else {
                                if(role == 1) {
                                    do{
                                        Console.WriteLine("------------------------------------------------------");
                                        Console.WriteLine("0. Logout");
                                        Console.WriteLine("1. Planner");
                                        Console.WriteLine("2. Venue");
                                        Console.WriteLine("3. Photographer");
                                        Console.WriteLine("4. Decorator");
                                        Console.WriteLine("5. Caterer");
                                        Console.WriteLine("Enter your choice:");
                                        choice1 = int.Parse(Console.ReadLine());
                                        Console.WriteLine("------------------------------------------------------");
                                        switch (choice1){
                                            case 0: User.loginuserId = 0;
                                                    break;
                                            case 1: do{
                                                        Console.WriteLine("------------------------------------------------------");
                                                        Console.WriteLine("0. Go Back");
                                                        Console.WriteLine("1. Add Planner");
                                                        Console.WriteLine("2. Update Planner");
                                                        Console.WriteLine("3. Delete Planner");
                                                        Console.WriteLine("4. Search Planner");
                                                        Console.WriteLine("5. View All Planners");
                                                        Console.WriteLine("Enter your choice:");
                                                        choicePlanner = int.Parse(Console.ReadLine());
                                                        Console.WriteLine("------------------------------------------------------");
                                                        switch (choicePlanner){
                                                            case 0: break;
                                                            case 1: Planner p = new Planner();
                                                                    bool b1 = p.add();
                                                                    if (b1) {
                                                                        Console.WriteLine("New Planner Added");
                                                                    }
                                                                    break;
                                                            case 2: Planner p1 = new Planner();
                                                                    p1.update();   
                                                                    break;
                                                            case 3: Planner p2 = new Planner();
                                                                    bool b2 = p2.remove();
                                                                    if (b2) {
                                                                        Console.WriteLine("Planner Removed Successfully");
                                                                    }
                                                                    break;
                                                            case 4: Planner p3 = new Planner();
                                                                    string plannerName = "";
                                                                    Console.WriteLine("Enter Planner Name:");
                                                                    plannerName = Console.ReadLine();
                                                                    p3.search(plannerName);
                                                                    break;
                                                            case 5: Planner p4 = new Planner();
                                                                    p4.show();
                                                                    break;
                                                            default: Console.WriteLine("Invalid input");break;
                                                        }    
                                                    } while (choicePlanner != 0);
                                                    break;
                                            case 2:do{
                                                        Console.WriteLine("------------------------------------------------------");
                                                        Console.WriteLine("0. Go Back");
                                                        Console.WriteLine("1. Add Venue");
                                                        Console.WriteLine("2. Update Venue");
                                                        Console.WriteLine("3. Delete Venue");
                                                        Console.WriteLine("4. Search Venue");
                                                        Console.WriteLine("5. View All Venues");
                                                        Console.WriteLine("Enter your choice:");
                                                        choiceVenue = int.Parse(Console.ReadLine());
                                                        Console.WriteLine("------------------------------------------------------");
                                                        switch (choiceVenue){
                                                            case 0: break;
                                                            case 1: Venue v = new Venue();
                                                                    bool vb1 = v.add();
                                                                    if (vb1){
                                                                        Console.WriteLine("New Venue Added");
                                                                    }
                                                                    break;
                                                            case 2: Venue v1 = new Venue();
                                                                    v1.update();
                                                                    break;
                                                            case 3: Venue v2 = new Venue();
                                                                    bool vb2 = v2.remove();
                                                                    if (vb2){
                                                                        Console.WriteLine("Venue Removed Successfully");
                                                                    }
                                                                    break;
                                                            case 4: Venue v3 = new Venue();
                                                                    string venueName = "";
                                                                    Console.WriteLine("Enter Venue Name:");
                                                                    venueName = Console.ReadLine();
                                                                    v3.search(venueName);
                                                                    break;
                                                            case 5: Venue v4 = new Venue();
                                                                    v4.show();
                                                                    break;
                                                            default: Console.WriteLine("Invalid input"); break;
                                                        }
                                                    } while (choiceVenue != 0);
                                                    break;
                                            case 3:do{
                                                        Console.WriteLine("------------------------------------------------------");
                                                        Console.WriteLine("0. Go Back");
                                                        Console.WriteLine("1. Add Photographer");
                                                        Console.WriteLine("2. Update Photographer");
                                                        Console.WriteLine("3. Delete Photographer");
                                                        Console.WriteLine("4. Search Photographer");
                                                        Console.WriteLine("5. View All Photographer");
                                                        Console.WriteLine("Enter your choice:");
                                                        choicePhotographer = int.Parse(Console.ReadLine());
                                                        Console.WriteLine("------------------------------------------------------");
                                                        switch (choicePhotographer){
                                                            case 0: break;
                                                            case 1: Photographer p = new Photographer();
                                                                    bool pb1 = p.add();
                                                                    if (pb1){
                                                                        Console.WriteLine("New Photographer Added");
                                                                    }
                                                                    break;
                                                            case 2: Photographer p1 = new Photographer();
                                                                    p1.update();
                                                                    break;
                                                            case 3: Photographer p2 = new Photographer();
                                                                    bool pb2 = p2.remove();
                                                                    if (pb2){
                                                                        Console.WriteLine("Photographer Removed Successfully");
                                                                    }
                                                                    break;
                                                            case 4: Photographer p3 = new Photographer();
                                                                    string photographerName = "";
                                                                    Console.WriteLine("Enter Photographer Name:");
                                                                    photographerName = Console.ReadLine();
                                                                    p3.search(photographerName);
                                                                    break;
                                                            case 5: Photographer p4 = new Photographer();
                                                                    p4.show();
                                                                    break;
                                                            default: Console.WriteLine("Invalid input"); break;
                                                        }
                                                    } while (choicePhotographer != 0);
                                                    break;
                                            case 4:do{
                                                        Console.WriteLine("------------------------------------------------------");
                                                        Console.WriteLine("0. Go Back");
                                                        Console.WriteLine("1. Add Decorator");
                                                        Console.WriteLine("2. Update Decorator");
                                                        Console.WriteLine("3. Delete Decorator");
                                                        Console.WriteLine("4. Search Decorator");
                                                        Console.WriteLine("5. View All Decorators");
                                                        Console.WriteLine("Enter your choice:");
                                                        choiceDecorator = int.Parse(Console.ReadLine());
                                                        Console.WriteLine("------------------------------------------------------");
                                                        switch (choiceDecorator){
                                                            case 0: break;
                                                            case 1: Decorator d = new Decorator();
                                                                    bool db1 = d.add();
                                                                    if (db1){
                                                                        Console.WriteLine("New Decorator Added");
                                                                    }
                                                                    break;
                                                            case 2: Decorator d1 = new Decorator();
                                                                    d1.update();
                                                                    break;
                                                            case 3: Decorator d2 = new Decorator();
                                                                    bool db2 = d2.remove();
                                                                    if (db2){
                                                                        Console.WriteLine("Decorator Removed Successfully");
                                                                    }
                                                                    break;
                                                            case 4: Decorator d3 = new Decorator();
                                                                    string decoratorName = "";
                                                                    Console.WriteLine("Enter Decorator Name:");
                                                                    decoratorName = Console.ReadLine();
                                                                    d3.search(decoratorName);
                                                                    break;
                                                            case 5: Decorator d4 = new Decorator();
                                                                    d4.show();
                                                                    break;
                                                            default:Console.WriteLine("Invalid input"); break;
                                                        }
                                                    } while (choiceDecorator != 0);
                                                    break;
                                            case 5:do{
                                                        Console.WriteLine("------------------------------------------------------");
                                                        Console.WriteLine("0. Go Back");
                                                        Console.WriteLine("1. Add Caterer");
                                                        Console.WriteLine("2. Update Caterer");
                                                        Console.WriteLine("3. Delete Caterer");
                                                        Console.WriteLine("4. Search Caterer");
                                                        Console.WriteLine("5. View All Caterers");
                                                        Console.WriteLine("Enter your choice:");
                                                        choiceCaterer = int.Parse(Console.ReadLine());
                                                        Console.WriteLine("------------------------------------------------------");
                                                        switch (choiceCaterer){
                                                            case 0: break;
                                                            case 1: Caterer c = new Caterer();
                                                                    bool cb1 = c.add();
                                                                    if (cb1){
                                                                        Console.WriteLine("New Caterer Added");
                                                                    }
                                                                    break;
                                                            case 2: Caterer c1 = new Caterer();
                                                                    c1.update();
                                                                    break;
                                                            case 3: Caterer c2 = new Caterer();
                                                                    bool cb2 = c2.remove();
                                                                    if (cb2){
                                                                        Console.WriteLine("Caterer Removed Successfully");
                                                                    }
                                                                    break;
                                                            case 4: Caterer c3 = new Caterer();
                                                                    string catererName = "";
                                                                    Console.WriteLine("Enter Caterer Name:");
                                                                    catererName = Console.ReadLine();
                                                                    c3.search(catererName);
                                                                    break;
                                                            case 5: Caterer c4 = new Caterer();
                                                                    c4.show();
                                                                    break;
                                                            default: Console.WriteLine("Invalid input"); break;
                                                        }
                                                    } while (choiceCaterer != 0);
                                                    break;
                                            default:Console.WriteLine("Invalid choice");break;
                                        }
                                    } while (choice1 != 0);
                                }
                                if(role == 2) {
                                    do{
                                        Console.WriteLine("------------------------------------------------------");
                                        Console.WriteLine("0. Logout");
                                        Console.WriteLine("1. Book Wedding Planner.");
                                        Console.WriteLine("2. Create your own wedding.");
                                        Console.WriteLine("3. View your bookings.");
                                        Console.WriteLine("4. Cancel your booking.");
                                        Console.WriteLine("Enter your choice:");
                                        choice2 = int.Parse(Console.ReadLine());
                                        Console.WriteLine("------------------------------------------------------");
                                        switch (choice2) {
                                            case 0: User.loginuserId = 0;
                                                    break;
                                            case 1: Booking book = new Booking();
                                                    book.bookPlanner();
                                                    break;
                                            case 2: Booking book1 = new Booking();
                                                    book1.createWedding();
                                                    break;
                                            case 3: Booking book2 = new Booking();
                                                    book2.showBooking(User.loginuserId);
                                                    break;
                                            case 4: break; 
                                            default: Console.WriteLine("Invalid input");break;
                                        }
                                    } while (choice2 != 0);
                                }
                            }
                            break;
                    case 2: User user = new User();
                            bool b = user.register();
                            if (b){
                                Console.WriteLine("New User Added");
                            }
                            else{
                                Console.WriteLine("Something is wrong");
                            }
                            break;
                    default:Console.WriteLine("Invalid Input");break;
                }
            } while (choice != 0);
        }
    }
}
