using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp
{
    class TaskManager
    {
        List<Task> taskList;

        public TaskManager()
        {
            taskList = new List<Task>();
        }
        public Task GetTask(int index)
        {
            if (CheckIndex(index))
                return taskList[index];
            else
                return null;
        }

        public bool AddNewTask(Task newTask)
        {
            bool ok = true;
            if (newTask != null)
                taskList.Add(newTask);
            else
                ok = false;
            return ok;
        }

        public bool AddNewTask(DateTime newTime, string description, PriorityType priority)
        {
            bool ok = true;
            Task newTask = new Task(newTime, description, priority);
            if (newTask != null)
                taskList.Add(newTask);
            else
                ok = false;
            return ok;
        }

        public bool ChangeTaskAt(Task task, int index)
        {
            bool ok = true;

            if ((task != null) && CheckIndex(index))
                taskList[index] = task;
            else
                ok = false;
            return ok;
        }

        public bool DeleteTaskAt(int index)
        {
            bool ok = false;
            if((index >= 0 ) && (index < taskList.Count))
            {
                taskList.RemoveAt(index);
                ok = true;
            }
            return ok;
        }


        public bool CheckIndex(int index)
        {
            bool ok = false;
            if ((index >= 0) && (index < taskList.Count))
            {
                ok = true;
            } 
            return ok;
        }

        public string[] GetInfoStringList()
        {
            string[] infoStrings = new string[taskList.Count];
            for (int i = 0; i< infoStrings.Length; i++)
            {
                infoStrings[i] = taskList[i].ToString();
            }
            return infoStrings;
        }
        
        public bool WriteDataToFile(string fileName)
        {
            FileManager filemanager = new FileManager();
            return filemanager.SaveTaskListToFile(taskList, fileName);

        }

        public bool ReadDataFromFile(string fileName)
        {
            FileManager fileManager = new FileManager();

            return fileManager.ReadTaskListFromFile(taskList, fileName);

        }



    }
}
