using SQLite;
using System;
namespace PrinterApp.Tables.Register
{
    
    public class RegisterTable
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
