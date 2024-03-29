using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Web;


namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class AnnouController : ControllerBase
    {


        private readonly ILogger<AnnouController> _logger;

        HttpContext _requestContext;
        public AnnouController(ILogger<AnnouController> logger)
        {
            _logger = logger;
        }

        public ArrayList list_of_annou = new ArrayList();
        [HttpGet]
        public IEnumerable Get([FromQuery] string category)
        {
            string connection = "Data Source=.\\sqlexpress;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string sql = "select * from announcment where category='" + category + "'";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            var annou = new Announcement();
                            annou.id = reader.GetInt32(0);
                            annou.category = reader.GetString(1);
                            annou.price = reader.GetDouble(2);
                            annou.title = reader.GetString(3);
                            annou.description = reader.GetString(4);
                            annou.author = reader.GetString(5);
                            annou.photo = reader.GetString(6);
                            list_of_annou.Add(annou);

                        }
                    }


                }

            }
            return list_of_annou;


        }



        [HttpGet("photo")]
        public FileStream Get([FromQuery] int id)
        {

            var sciezka = "";
            string connection = "Data Source=.\\sqlexpress;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string sql = "select photo from announcment where id=" + id;
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sciezka = reader.GetString(0);
                        }



                    }

                }
            }
            var file = new FileStream(sciezka, FileMode.Open);

            return file;
        }



        [HttpPost]
        public async Task<IActionResult> AddAnnou(IFormFile file, [FromQuery] string category, [FromQuery] double price, [FromQuery] string title, [FromQuery] string description, [FromQuery] string author)
        {



            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads\\", file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            




            string connection = "Data Source=.\\sqlexpress;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                var sql = $"INSERT INTO announcment (category, price, title, description, author, photo) values ('{category}',{price},'{title}','{description}','{author}','{filePath}')";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                        }

                    }
                }
            }

            
       
                    
        

                    return Ok("Ogłoszenie dodane prawidłowe");
        }

        [HttpPost("dd")]
        public async Task<IActionResult> Upload(IFormFile file,[FromQuery] string title)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is not selected");
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(),"uploads\\", file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok("File is uploaded successfully"+title);
        }


        [HttpGet("help")]
        public Announcement get2()
        {
            var result = new Announcement();
            string connection = "Data Source=.\\sqlexpress;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string sql = "select * from help";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            var annou = new Announcement();
                            annou.id = reader.GetInt32(0);
                            annou.category = reader.GetString(1);
                            annou.price = reader.GetDouble(2);
                            annou.title = reader.GetString(3);
                            annou.description = reader.GetString(4);
                            annou.author = reader.GetString(5);
                            annou.photo = reader.GetString(6);
                            result = annou;

                        }
                    }


                }

            }
            return result;


        }
        [HttpPost("help")]

        public async Task<IActionResult> Addhelp(int id, string file, [FromQuery] string category, [FromQuery] double price, [FromQuery] string title, [FromQuery] string description, [FromQuery] string author)
        {



            //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads\\", file.FileName);

            //using (var stream = new FileStream(filePath, FileMode.Create))
            //{
            //    await file.CopyToAsync(stream);
            //}






            string connection = "Data Source=.\\sqlexpress;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                var sql = $"UPDATE help SET  id={id}, category='{category}',price={price},title='{title}',description='{description}',author='{author}',photo='{file}'";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                        }

                    }
                }
            }






            return Ok("Ogłoszenie dodane prawidłowe");
        }

    }
}