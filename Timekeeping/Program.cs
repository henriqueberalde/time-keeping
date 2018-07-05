using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Timekeeping
{
    public class Program
    {
        private static Dictionary<string, string> dick = new Dictionary<string, string> {
                { "entry", "Entry" },
                { "exit", "Exit" },
                { "log", "Log" },
                { "help", "Help" },
                { "clear", "Clear" },
        };

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Enter a command: ");
                var command = Console.ReadLine();

                if (string.IsNullOrEmpty(command.Trim()))
                    continue;

                var programClass = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.Name.ToUpper() == "PROGRAM").FirstOrDefault();
                var curInsance = Activator.CreateInstance(programClass);

                try
                {
                    var methodName = "";

                    if (dick.ContainsKey(command))
                    {
                        methodName = dick[command].ToString();
                        curInsance.GetType().GetMethod(methodName).Invoke(curInsance, null);
                    }
                    else
                        throw new CommandInvalidException();

                    //else
                    //{
                    //    var cmds = command.Split(' ');

                        //    if (!cmds.Any() || cmds.Count() <= 1)
                        //        throw new CommandInvalidException();

                        //    if (!dick.ContainsKey(cmds.FirstOrDefault()))
                        //        throw new CommandInvalidException();

                        //    methodName = dick[cmds.FirstOrDefault()];

                        //    int pInt;

                        //    if (int.TryParse(cmds[1], out pInt))
                        //    {
                        //        curInsance.GetType().GetMethod(methodName).Invoke(curInsance, new object[] { pInt });
                        //    }

                        //    if (cmds.Count() <= 2)
                        //        throw new CommandInvalidException();

                        //    DateTime pDateTime;

                        //    if (DateTime.TryParse(cmds[1] + " " + cmds[2], out pDateTime))
                        //        curInsance.GetType().GetMethod(methodName).Invoke(curInsance, new object[] { pDateTime });
                        //}
                }
                catch (CommandInvalidException)
                {
                    Console.WriteLine("Invalid Command");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error executing action");
                    Console.WriteLine(ex);
                }

                Console.WriteLine();
                Console.WriteLine();
            }
        }

        public static void Entry()
        {
            RegisterTimeRecord(TimeRecordType.Entry);
        }

        public static void Exit()
        {
            RegisterTimeRecord(TimeRecordType.Exit);
        }

        public static void RegisterTimeRecord(TimeRecordType type, DateTime? date = null)
        {
            using (var db = new TimeRecordContext())
            {
                var dateRecord = DateTime.Now;

                if (date.HasValue)
                    dateRecord = date.Value;
                else
                    dateRecord = DateTime.Now;

                var timeRecord = new TimeRecord(dateRecord, type);
                db.TimeRecords.Add(timeRecord);
                db.SaveChanges();
                Console.Write($"{type.ToString()} Registered");
            }
        }

        public static void Log()
        {
            using (var db = new TimeRecordContext())
            {
                var query = db.TimeRecords.OrderByDescending((t) => t.Time);

                Console.WriteLine("All TimeRecords from data base:\n");
                foreach (var item in query)
                {
                    ConsoleColor color;// = ConsoleColor.Red;
                    if (item.Type == TimeRecordType.Entry)
                        color = ConsoleColor.Green;
                    else
                        color = ConsoleColor.Red;

                    Console.ForegroundColor = color;
                    Console.WriteLine($"{item.Id} {item.Type.ToString()} - {item.Time}");
                    Console.ResetColor();
                }
            }
        }

        public static void Help()
        {
            Console.WriteLine("All commands:\n");
            foreach (var item in dick)
            {
                Console.WriteLine($"{item.Key}");
            }
        }

        public static void Clear()
        {
            using (var db = new TimeRecordContext())
            {
                db.Database.ExecuteSqlCommand("DELETE FROM [TimeRecords]");   
            }
            Console.WriteLine("All records was cleaned up");
        }
    }
}
