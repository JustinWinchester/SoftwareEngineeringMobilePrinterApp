using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace PrinterApp.Model
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
