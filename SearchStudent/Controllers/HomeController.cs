using System.Data.OleDb;
using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using SearchStudent.Models;

namespace SearchStudent.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        string cs = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=D:\\C#\\Students.accdb";
        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }
        [HttpPost]
       
        public string Search(double minAverage)
        {
            StringBuilder result=new StringBuilder();
            using (OleDbConnection c=new OleDbConnection(cs))
            {
                c.Open();
                string query = "SELECT Name, Surname, Average FROM Students WHERE Average>?";
                OleDbCommand cmd=new OleDbCommand(query,c);
                cmd.Parameters.AddWithValue("@avg", minAverage);
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader["Name"].ToString();
                    string surname = reader["Surname"].ToString();
                    string avg= reader["Average"].ToString();
                    result.AppendLine(name + " " + surname + " " + "Ortalama: " + avg + ")");
                }
            }
            return result.ToString();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
