using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TodoApp
{
    class FileManager
    {
        private const string fileVersionToken = "ToDoRe_1";

        private const double fileVersion = 1.0;

        public bool SaveTaskListToFile(List<Task> taskList, string filename)
        {
            bool ok = true;
            // trying to save all the information to file
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(filename);
                writer.WriteLine(fileVersionToken);
                writer.WriteLine(fileVersion);
                writer.WriteLine(taskList.Count);

                for (int i = 0; i < taskList.Count; i++)
                {
                    writer.WriteLine(taskList[i].Description);
                    writer.WriteLine(taskList[i].Priority.ToString());
                    writer.WriteLine(taskList[i].TaskDate.Year);
                    writer.WriteLine(taskList[i].TaskDate.Month);
                    writer.WriteLine(taskList[i].TaskDate.Day);
                    writer.WriteLine(taskList[i].TaskDate.Hour);
                    writer.WriteLine(taskList[i].TaskDate.Minute);
                    writer.WriteLine(taskList[i].TaskDate.Second);

                }
            }
            catch
            {
                ok = false;
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
            return ok;
        }

        public bool ReadTaskListFromFile(List<Task> taskList, string filename)
        {
            bool ok = true;
            StreamReader reader = null;

            try
            {
                if (taskList != null)
                    taskList.Clear();
                else
                    taskList = new List<Task>();

                reader = new StreamReader(filename);

                string versionTest = reader.ReadLine();

                double version = double.Parse(reader.ReadLine());

                if ((versionTest == fileVersionToken) && (version == fileVersion))
                {
                    int count = int.Parse(reader.ReadLine());
                    for (int i = 0; i < count; i++)
                    {
                        Task task = new Task();
                        task.Description = reader.ReadLine();
                        task.Priority = (PriorityType)Enum.Parse(typeof(PriorityType), reader.ReadLine());

                        int year = 0, month = 0, day = 0;
                        int hour = 0, minute = 0, second = 0;

                        year = int.Parse(reader.ReadLine());
                        month = int.Parse(reader.ReadLine());
                        day = int.Parse(reader.ReadLine());
                        hour = int.Parse(reader.ReadLine());
                        minute = int.Parse(reader.ReadLine());
                        second = int.Parse(reader.ReadLine());

                        task.TaskDate = new DateTime(year, month, day, hour, minute, second);
                        taskList.Add(task);
                    }
                }
                else
                {
                    ok = false;
                }
            }
            catch
            {
                ok = false;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return ok;
        }


    }
}
