using System;
using System.Data.SqlClient;
using ADO.NET_HospitalProject;

namespace BusinessLogic_dll
{
    public class BusinessLogic
    {

        public static string UserName;
        public static string PassWord;
        public static bool Flag=false;
        public static bool UserPass(string UserName, String PassWord)
        {
            SqlDataReader dr = Hospital.SelectDataforUserPass();
            while (dr.Read())
            {
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    if (i == 2)
                    {
                        string UName = Convert.ToString(dr[i]);
                        string Pass = Convert.ToString(dr[i + 1]);
                        if ((UserName == UName) && (PassWord == Pass))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Login Successfull");
                            Console.ForegroundColor = ConsoleColor.White;
                            Flag = true;
                            Console.WriteLine("********************************************Home_Page*********************************************");
                            return true;
                        }
                        else if((UserName != UName) && (PassWord != Pass))
                        {
                            break;
                        }
                        
                    }
                   
                }
            }
            if (Flag == false)
            {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You’ve entered an incorrect user name or password");
                    Console.ForegroundColor = ConsoleColor.White;
                    return false;
            }

            dr.Close();
            return true;
        }
           
        

        public static void SelectDoctor()
        {
            Hospital.SelectDoctors();
        }

        public static void AddPatients()
        {
            Console.WriteLine("Enter Patient First Name:");
            String First_Name = Console.ReadLine();
            Console.WriteLine("Enter Patient Last Name:");
            String Last_Name = Console.ReadLine();
            Console.WriteLine("Enter the Gender of Patient:");
            String Sex = Console.ReadLine();
            Console.WriteLine("Enter the Patient DOB:");
            DateTime DOB = DateTime.Parse(Console.ReadLine());
            int Age = DateTime.Now.Year - DOB.Year;
            Hospital.AddPatient(First_Name, Last_Name, Sex, Age);
        }

        public static void GetPatientId()
        {
            Console.WriteLine("Enter the Patient Name:");
            string PatientName = Console.ReadLine();
            Hospital.GetP_Id(PatientName);
        }

        public static void ViewDoctorofParticularSpec()
        {
            Console.WriteLine("Specialization Available:\n  1.General\n  2.Internal Medicine\n  3.Pediatrics\n  4.Orthopedics\n  5.Ophthalmology\n  ");
            Console.WriteLine("What Specialization you are Looking for?");
            string Spec = Console.ReadLine();
            int D_Id = Hospital.ViewDoc(Spec);
            int Cnt = Hospital.AvailabilityCheck(D_Id);
            Hospital.AppointMentBooking(Cnt);
        }
        public static void CancAppointMent()
        {
            Hospital.Cancel_AppointMent();
        }
    }
}