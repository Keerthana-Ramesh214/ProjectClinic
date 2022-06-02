
using System;
using System.Data.SqlClient;

namespace ADO.NET_HospitalProject
{
    public class Hospital
    {
        public static SqlConnection con;
        public static SqlCommand cmd;
        public static int DocId;
        public static int PatientId;
        public static int Count;
        public static string Slot;
        public static DateTime DOA;
        private static SqlConnection connect()
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=ClinicalManagementDb;Integrated Security=true");
            con.Open();
            return con;
        }
        public static SqlDataReader SelectDataforUserPass()
        {
            con = connect();
            cmd = new SqlCommand("select * from UserPassword");
            cmd.Connection = con;
            SqlDataReader dr = cmd.ExecuteReader();     
            return dr;
        }

        public static void SelectDoctors()
        {
            try
            {
                con = connect();
                cmd = new SqlCommand("select * from Doctors");
                cmd.Connection = con;
                SqlDataReader dr1 = cmd.ExecuteReader();
                while (dr1.Read())
                {
                    for (int i = 0; i < dr1.FieldCount; i++)
                    {
                        if (i == 0)
                        {
                            Console.WriteLine("Doctor Id:"+dr1[0]);
                        }
                        else if (i == 1)
                        {
                            Console.WriteLine("First_Name:" + dr1[1]);
                        }
                        else if (i == 2)
                        {
                            Console.WriteLine("Last_Name:" + dr1[2]);
                        }
                        else if (i == 3)
                        {
                            Console.WriteLine("Sex:" + dr1[3]);
                        }
                        else if (i == 4)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Specialization:" + dr1[4]);
                            Console.ForegroundColor=ConsoleColor.White;
                        }
                        else if (i == 5)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Visiting_Hours:" + dr1[5]);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("************************************************************************");
                        }
                       
                    }
                    
                }
                dr1.Close();
            }
            catch (SqlException s)
            {
                Console.WriteLine(s.Message);
            }

        }
        public static void AddPatient(string First_Name, string Last_Name, string Sex, int Age)
        {
            try
            {
                con = connect();
                cmd = new SqlCommand("insert into Patient values (@First_Name,@Last_Name,@Sex,@Age)", con);
                cmd.Parameters.AddWithValue("@First_Name", First_Name);
                cmd.Parameters.AddWithValue("@Last_Name", Last_Name);
                cmd.Parameters.AddWithValue("@Sex", Sex);
                cmd.Parameters.AddWithValue("@Age", Age);
                int i = cmd.ExecuteNonQuery();
                Console.WriteLine(i + " Row(s) affected");
            }
            catch (SqlException s)
            {
                Console.WriteLine(s.Message);
            }
        }

        public static int GetP_Id(string PatientName)
        {
            cmd = new SqlCommand("SelectPatientId", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Fst_Name", PatientName);
            SqlDataReader dr2 = cmd.ExecuteReader();
            while (dr2.Read())
                for (int i = 0; i < dr2.FieldCount; i++)
                {
                    Console.WriteLine("Patient Id :" + dr2[i]);
                    PatientId = Convert.ToInt32(dr2[i]);
                }
            dr2.Close();
            return PatientId;
        }

        public static int ViewDoc(string Spec)
        {
            cmd = new SqlCommand("SelectDoctor", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Spec", Spec);
            SqlDataReader dr3 = cmd.ExecuteReader();
            while (dr3.Read())
                for (int i = 0; i < dr3.FieldCount; i++)
                {
                    Console.WriteLine("Doctor Id :" + dr3[i]);
                    DocId = Convert.ToInt32(dr3[i]);
                }
            dr3.Close();
            return DocId;
        }

        public static int AvailabilityCheck(int D_Id)
        {
            Console.WriteLine("Enter Date of AppointMent:");
            DOA = DateTime.ParseExact(Console.ReadLine(), "d/M/yyyy", null);
            cmd = new SqlCommand("AvailabilityOfDoctor", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Doc_Id", D_Id);
            cmd.Parameters.AddWithValue("@DOA", DOA);
            SqlDataReader dr4 = cmd.ExecuteReader();
            while (dr4.Read())
            {
                for (int i = 0; i < dr4.FieldCount; i++)
                {
                    Count = Convert.ToInt32(dr4[i]);
                }
            }
            dr4.Close();
            return Count;

        }

        public static void SelectAllAppointMent(int DocId)
        {
            cmd = new SqlCommand("AvailabilityOfDoc", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Doc_Id", DocId);
            cmd.Parameters.AddWithValue("@DOA", DOA);
            SqlDataReader dr5 = cmd.ExecuteReader();
            while (dr5.Read())
            {
                for (int i = 0; i < dr5.FieldCount; i++)
                {
                    if (i == 0)
                    {
                        Console.WriteLine("AppointMent_Id:" + dr5[0]);
                        continue;
                    }
                    else if (i == 1)
                    {
                        Console.WriteLine("Doctor_Id:" + dr5[1]);
                        continue;
                    }
                    else if (i == 2)
                    {
                        DateTime date = Convert.ToDateTime(dr5[2]);
                        Console.WriteLine("Date OF AppointMent:{0}" , date.ToShortDateString());
                        continue;
                    }
                    else if (i == 3)
                    {
                        Console.WriteLine("Slot:" + dr5[3]);
                        Slot = Convert.ToString(dr5[3]);
                        continue;
                    }
                    else if (i == 4)
                    {
                        Console.WriteLine("Patient Id:" + dr5[4]);
                        break;
                    }


                }
            }
            dr5.Close();
        }
        public static void AppointMentBooking(int Cnt)
        {
            Console.WriteLine("Enter the Patient Name:");
            string patientName = Console.ReadLine();
            PatientId = Hospital.GetP_Id(patientName);

            if (Cnt == 1)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Only One Slot available.....Hurry");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("*******************************************************");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Already Booked slot of the Day:");
                Hospital.SelectAllAppointMent(DocId);
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("*******************************************************");

                if (Slot == "From 1Pm To 2Pm")
                {
                    Console.WriteLine("Enter 1:To choose AppointMent From 2pm to 3pm");
                    int slt=Convert.ToInt32(Console.ReadLine());
                    Slot = "From 2Pm To 3Pm";
                }
                else
                {
                    Console.WriteLine("Enter 1:To choose AppointMent From 1pm to 2pm");
                    int slt = Convert.ToInt32(Console.ReadLine());
                    Slot = "From 1Pm To 2Pm";
                }
                
                cmd = new SqlCommand("insert into AppointMent values (@DocId,@DateOfAppoint,@Slot,@PatientId)", con);
                cmd.Parameters.AddWithValue("@DocId", DocId);
                cmd.Parameters.AddWithValue("@DateOfAppoint", DOA);
                cmd.Parameters.AddWithValue("@Slot", Slot);
                cmd.Parameters.AddWithValue("@PatientId", PatientId);
                cmd.ExecuteNonQuery();
                Console.WriteLine("AppointMent Fixed :)");

            }
            else if (Cnt == 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Sorry No slots available try Another Day :(");
            }
            else
            {
                Console.WriteLine("Both slots are free");
                Console.WriteLine("Enter the Slot :\n Press 1 : From 1pm to 2pm \n Press 2: From 2pm to 3pm");
                int slotNum = Convert.ToInt32(Console.ReadLine());
                if (slotNum == 1)
                {
                    Slot = "From 1Pm To 2Pm";
                }
                else if (slotNum == 2)
                {
                    Slot = "From 2Pm To 3Pm";
                }
                cmd = new SqlCommand("insert into AppointMent values (@DocId,@DateOfAppoint,@Slot,@PatientId)", con);
                cmd.Parameters.AddWithValue("@DocId", DocId);
                cmd.Parameters.AddWithValue("@DateOfAppoint", DOA);
                cmd.Parameters.AddWithValue("@Slot", Slot);
                cmd.Parameters.AddWithValue("@PatientId", PatientId);
                cmd.ExecuteNonQuery();
                Console.WriteLine("AppointMent Fixed :)");
            }
        }

        public static void Cancel_AppointMent()
        {
            Console.WriteLine("Enter Patient Name:");
            string FN = Console.ReadLine();
            PatientId = Hospital.GetP_Id(FN);

            cmd = new SqlCommand("CancelAppointMent", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PtntId", PatientId);
            SqlDataReader dr7 = cmd.ExecuteReader();
            Console.WriteLine("AppointMent Cancelled");
            dr7.Close();
        }
    }
}