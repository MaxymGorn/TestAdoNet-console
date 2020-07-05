using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestAdoNet
{
    class Program
    {
        static async Task Main(string[] args)
        {
            
            DataManager dataManager = new DataManager();
            await dataManager.LoadStudentsAsync();
            await dataManager.DisplayData();
            await dataManager.AddStudentsAsync(new Student("New St", 16, 20));
            await dataManager.DisplayData();
            dataManager.DeleteStudents("New St");
            await dataManager.DisplayData();
            Random random = new Random();
            await dataManager.ChangeRateAsync("St1",random.Next(0,100));          
            await dataManager.DisplayData();
            await Task.Delay(TimeSpan.FromSeconds(1));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("End program!!!");
        }
    }
}
