using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace GolfHandicap
{
    class Program
    {





        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            var fileName = Path.Combine(directory.FullName, "Courses.json");
            
            int result = 0;
            while (result != 4)
            {

                Console.WriteLine("What would you like to do? Then Hit Enter" + "\n - 1 look at available course list" + "\n - 2 add another course" + "\n - 3 delete a course" + "\n - 4 exit");


                if (int.TryParse(Console.ReadLine(), out result))
                {
                    if (result == 1)
                    {
                        List<GolfCourse> golfCourses = GetCourses(fileName);
                        ViewCourses(golfCourses);
                    }
                    else if (result == 2)
                    {
                        GolfCourse course = new GolfCourse();

                        List<GolfCourse> golfCourses = GetCourses(fileName);

                        Console.WriteLine("Golf Course Name?");
                        string name = Console.ReadLine();


                        Console.WriteLine("Course Par?");
                        int par = Convert.ToInt32(Console.ReadLine());
                        

                        Console.WriteLine("Course Rating?");
                        double rating = Convert.ToDouble(Console.ReadLine());


                        Console.WriteLine("Course Slope?");
                        double slope = Convert.ToDouble(Console.ReadLine());


                        Console.WriteLine("Course Distance?");
                        int distance = Convert.ToInt32(Console.ReadLine());


                        course.Name = name;
                        course.Par = par;
                        course.Rating = rating;
                        course.Slope = slope;
                        course.Distance = distance;

                        AddCourse(course, golfCourses, fileName);
                    }

                    else if (result == 3)
                    {
                        Console.WriteLine("Golf Course Name?");
                        string name = Console.ReadLine();

                        DeleteCourse(name, fileName);
                    }
                    else if (result == 4)
                    {
                        Console.WriteLine("Head to the 19th green");
                    }
                    Console.WriteLine();
                }
           
            }


        }

        public static void DeleteCourse(string name, string fileName)
        {
            List<GolfCourse> golfCourses = GetCourses(fileName);
            
            foreach (GolfCourse gc in new List<GolfCourse>(golfCourses))
            {
                if (gc.Name == name)
                {
                    golfCourses.Remove(gc);
                }
            }

            Console.WriteLine("Deleted course: " + name);

            Save(golfCourses, fileName);
        }

       




        // delete individual courses            (firstordefault)  delete method
        
        // loop the menu til exit                 Main method
        public static void AddCourse(GolfCourse course, List<GolfCourse> golfCourses, string fileName)
        {
            golfCourses.Add(course);

            Save(golfCourses, fileName);
        }

        private static void Save(List<GolfCourse> golfCourses, string fileName)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(golfCourses);

                if (!string.IsNullOrEmpty(jsonData))
                {
                    File.WriteAllText(fileName, jsonData);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("SaveError",
                    $"An error occurred while trying to save the lsit.");
                throw;
            }
        }

        private static void ViewCourses(List<GolfCourse> golfCourses)
        {
            foreach (var course in golfCourses)
            {
                Console.WriteLine($"courseName: {course.Name}, coursePar: {course.Par}, courseRating: {course.Rating}, courseSlope: {course.Slope}, courseDistance: {course.Distance}");


            }



        }

        private static List<GolfCourse> GetCourses(string fileName)
        {
            //throw new NotImplementedException();
            List<GolfCourse> ReturnValue = new List<GolfCourse>();

            try
            {
                if (File.Exists(fileName))
                {
                    //read the file
                    string jsonData = File.ReadAllText(fileName);

                    if (!String.IsNullOrEmpty(jsonData))
                    {
                        //deserialize the file back into a list
                        ReturnValue = JsonConvert.DeserializeObject<List<GolfCourse>>(jsonData);
                    }
                }
                else
                {
                    Save(ReturnValue, fileName);
                }
            }
            catch (Exception ex)
            {
                ex.Data.Add("GetAllError",
                    $"An error occurred while trying to get golf courses.");
                throw;
            }

            return ReturnValue;
        }

        public static string ReadFile(string fileName)
            {
                using (var reader = new StreamReader(fileName))
                {
                return reader.ReadToEnd();
                }
            }
        
        public static List<string[]> ReadGolfCourseData(string fileName)
        {
            var golfCourseData = new List<string[]>();
            using(var reader = new StreamReader(fileName))
            {
                string line = "";
                while((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    golfCourseData.Add(values);
                }
            }
            return golfCourseData;
        }

        
    }
}
