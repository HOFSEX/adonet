using DBConnect.Data;
using DBConnect.Models;

namespace DBConnect
{
    class Program
    {
        static string connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;" +
            "Initial Catalog=College_DB;" +
            "Integrated Security=True;" +
            "TrustServerCertificate=True;";

        static StudentRepository studentRepo = new StudentRepository(connectionString);
        static GroupRepository groupRepo = new GroupRepository(connectionString);

        static void Main()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== Журнал (Dapper) ===");
                Console.WriteLine("1. Показать всех студентов");
                Console.WriteLine("2. Показать все группы");
                Console.WriteLine("3. Добавить студента");
                Console.WriteLine("0. Выход");
                Console.Write("Выбор: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        ShowAllStudents();
                        break;
                    case "2":
                        ShowAllGroups();
                        break;
                    case "3":
                        AddStudent();
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nНажми любую клавишу...");
                    Console.ReadKey();
                }
            }
        }

        static void ShowAllStudents()
        {
            var students = studentRepo.GetAll();
            if (students.Count == 0)
            {
                Console.WriteLine("Список пуст");
                return;
            }

            foreach (var s in students)
                Console.WriteLine(s);
        }

        static void ShowAllGroups()
        {
            var groups = groupRepo.GetAll();
            if (groups.Count == 0)
            {
                Console.WriteLine("Список пуст");
                return;
            }

            foreach (var g in groups)
                Console.WriteLine(g);
        }

        static void AddStudent()
        {
            Console.Write("Имя: ");
            string firstName = Console.ReadLine() ?? "";
            Console.Write("Фамилия: ");
            string lastName = Console.ReadLine() ?? "";
            Console.Write("Возраст: ");
            int age = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("GroupId: ");
            int groupId = int.Parse(Console.ReadLine() ?? "0");

            var student = new Student
            {
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                GroupId = groupId
            };

            int newId = studentRepo.CreateStudent(student);
            Console.WriteLine($"Студент добавлен, id = {newId}");
        }
    }
}
