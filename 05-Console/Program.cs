using Microsoft.Data.SqlClient;

class Program
{
    static string connectionString =
        "Data Source=(localdb)\\MSSQLLocalDB;" +
        "Initial Catalog=College_DB;" +
        "Integrated Security=True;" +
        "TrustServerCertificate=True;";

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Журнал студентов ===");
            Console.WriteLine("1. Показать всех студентов");
            Console.WriteLine("2. Найти студента по имени");
            Console.WriteLine("0. Выход");
            Console.Write("Выбор: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ShowAllStudents();
                    break;
                case "2":
                    Console.Write("Введите имя или фамилию: ");
                    string name = Console.ReadLine();
                    FindStudentByName(name);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный выбор");
                    break;
            }

            Console.WriteLine("\nНажми любую клавишу...");
            Console.ReadKey();
        }
    }

    static void ShowAllStudents()
    {
        using var conn = new SqlConnection(connectionString);
        conn.Open();

        var cmd = new SqlCommand(
            "SELECT StudentId, FirstName, LastName, Age, GroupId FROM Students", conn);

        using var reader = cmd.ExecuteReader();

        Console.WriteLine("\n--- Все студенты ---");
        while (reader.Read())
        {
            Console.WriteLine(
                $"{reader["StudentId"]} | {reader["FirstName"]} {reader["LastName"]} | " +
                $"Age: {reader["Age"]} | GroupId: {reader["GroupId"]}");
        }
    }

    static void FindStudentByName(string name)
    {
        using var conn = new SqlConnection(connectionString);
        conn.Open();

        var cmd = new SqlCommand(
            "SELECT StudentId, FirstName, LastName, Age FROM Students " +
            "WHERE FirstName LIKE @name OR LastName LIKE @name", conn);

        cmd.Parameters.AddWithValue("@name", "%" + name + "%");

        using var reader = cmd.ExecuteReader();

        Console.WriteLine("\n--- Результаты поиска ---");
        bool found = false;
        while (reader.Read())
        {
            found = true;
            Console.WriteLine(
                $"{reader["StudentId"]} | {reader["FirstName"]} {reader["LastName"]} | Age: {reader["Age"]}");
        }

        if (!found)
            Console.WriteLine("Ничего не найдено");
    }
}
