using System;
using BusinessLogic_dll;
using ADO.NET_HospitalProject;
namespace HospitalClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("********************************************************Welcome*********************************************************");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Enter UserName:");
            string UserName = Console.ReadLine();
            Console.WriteLine("Enter your Password:");
            string PassWord = Console.ReadLine();
            bool flag=BusinessLogic.UserPass(UserName,PassWord);
            if (flag)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("1.View Doctors");
                Console.WriteLine("2.Add Patient");
                Console.WriteLine("3.Existing Patient? Get Patient_Id");
                Console.WriteLine("4.Schedule Appointment");
                Console.WriteLine("5.Cancel Appointment");
                Console.WriteLine("6.Logout");
                bool Flag = Convert.ToBoolean(true);
                while (Flag)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Enter Option:");
                    int m = Convert.ToInt32(Console.ReadLine());
                    switch (m)
                    {
                        case 1:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("********************************************List of Doctors in Our Hospital********************************************");
                            Console.ForegroundColor=ConsoleColor.White;
                            BusinessLogic.SelectDoctor();
                            break;
                        case 2:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("********************************************New_Patient_Entry********************************************");
                            Console.ForegroundColor = ConsoleColor.White;
                            BusinessLogic.AddPatients();
                            break;
                        case 3:
                            Console.WriteLine("************************************************************************************************************");
                            BusinessLogic.GetPatientId();
                            break;
                        case 4:
                            Console.WriteLine("************************************************************************************************************");
                            BusinessLogic.ViewDoctorofParticularSpec();
                            break;
                        case 5:
                            Console.WriteLine("************************************************************************************************************");
                            BusinessLogic.CancAppointMent();
                            break;
                        case 6:
                            Flag = false;
                            break;
                    }
                }

            }
        }
    }
}

