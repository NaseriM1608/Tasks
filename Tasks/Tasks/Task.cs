using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
namespace Tasks
{
    class Tasks
    {
        static int Auto_ID = 100;

        public int ID;
        public string Task_Description { get; set; }
        public bool Is_Done { get; set;}
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Tag { get; set; }
        
        public Tasks(string Tag, string Task_Description, DateTime Date, DateTime? Time, bool Is_Done)
        {
            this.Task_Description = Task_Description;
            this.Is_Done = Is_Done;
            this.Tag = Tag;
            this.Date = Date;
            ID = Auto_ID;
            if(Time != null)
            {
                this.Time = (DateTime)Time;
            }
            Auto_ID++;
        }

        #region Print by category
        public static void PrintByGroup(List<Tasks> tasksList)
        {
            var groupedTasks = tasksList.GroupBy(x => x.Tag);
            foreach (var group in groupedTasks)
            {
                Console.WriteLine($"{group.Key}:\n");
                foreach (var task in group)
                {
                    Console.WriteLine(task);
                }
                Console.WriteLine();
            }
        }
        #endregion

        #region Today's Tasks
        public static void PrintToday(List<Tasks> tasksList)
        {
            var dateGroup = tasksList.GroupBy(x => x.Date);
            foreach (var group in dateGroup)
            {
                if(group.Key.Day == DateTime.Now.Day)
                {
                    foreach (var item in group)
                    {
                        item.Print();
                    }
                }
            }
        }
        #endregion

        #region ToString Override
        public override string ToString()
        {
            if (Time.Hour != 0)
            {
                return String.Format($"ID: {ID}  Description: {Task_Description}  Date: {Date.Month}/{Date.Day}/{Date.Year}  Time: {Time.Hour}:{Time.Minute}  Completed: {Is_Done}");
            }
            else
            {
                return String.Format($"ID: {ID}  Description: {Task_Description}  Date: {Date.Month}/{Date.Day}/{Date.Year}  Time: -  Completed: {Is_Done}");
            }
        }
        #endregion

        #region Print Method
        public void Print()
        {
            if (Time.Hour != 0)
            {
                Console.WriteLine($"Tag: {Tag}  ID: {ID}  Description: {Task_Description}  Date: {Date.Month}/{Date.Day}/{Date.Year}  Time: {Time.Hour}:{Time.Minute}  Completed: {Is_Done}");
            }
            else
            {
                Console.WriteLine($"Tag: {Tag}  ID: {ID}  Description: {Task_Description}  Date: {Date.Month}/{Date.Day}/{Date.Year}  Time: -  Completed: {Is_Done}");
            }
        }
        #endregion

        #region Search Method
        public static List<Tasks> Search(List<Tasks> tasksList, string searchInput)
        {
            var matchingTasks = tasksList.Where(x => x.Tag == searchInput || x.Date.ToShortDateString() == searchInput || x.ID.ToString() == searchInput).ToList();
            return matchingTasks;
        }
        #endregion

        #region Delete Method
        public static void Delete(List<Tasks> tasksList, string[] searchInput)
        {
            for (int i = 0; i < searchInput.Length; i++)
            {
                List<Tasks> LoadedTasks = RestoreTasks();
                
                Tasks UpdatedTask = LoadedTasks.Find(x => x.ID.ToString() == searchInput[i]);

                if (UpdatedTask != null)
                {
                    LoadedTasks.Remove(UpdatedTask);
                    Console.WriteLine($"\nTask with ID {searchInput[i]} deleted successfully!");
                    SaveTasks(LoadedTasks);
                }
                else
                {
                    Console.WriteLine($"\nTask with ID {searchInput[i]} was not found.");
                }
            }
        }
        #endregion

        #region Save Tasks
        public static void SaveTasks(List<Tasks> tasksList)
        {
            string filePath = "tasks.json";
            string json = JsonConvert.SerializeObject(tasksList);
            File.WriteAllText(filePath, json);
        }
        #endregion

        #region Load Tasks
        public static List<Tasks> RestoreTasks()
        {
            string filePath = "tasks.json";
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                List<Tasks> tasksList = JsonConvert.DeserializeObject<List<Tasks>>(json);
                return tasksList;
            }

            return new List<Tasks>();
        }

        #endregion
    }
}
