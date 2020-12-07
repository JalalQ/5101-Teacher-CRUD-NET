using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//After installing MySQL.Data. Connects wit MySQL. Keep MAMP open and running.
using MySql.Data.MySqlClient;

// Most of the following code has been taken from:
//https://github.com/christinebittle/BlogProject_5/blob/master/BlogProject/Models/BlogDbContext.cs

namespace TeacherCrud.Models
{
    public class SchoolDbContext
    {

        //Values set based on my own local MySQL database
        private static string User { get { return "root"; } }
        private static string Password { get { return "root"; } }
        private static string Database { get { return "school"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }

        //ConnectionString is a series of credentials used to connect to the database.
        protected static string ConnectionString
        {
            get
            {
                //convert zero datetime is a db connection setting which returns NULL if the date is 0000-00-00
                //this can allow C# to have an easier interpretation of the date (no date instead of 0 BCE)
                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password
                    + "; convert zero datetime = True";
            }
        }
        //This is the method we actually use to get the database!
        /// <summary>
        /// Returns a connection to the teachers database.
        /// </summary>
        /// <example>
        /// private TeacherDbContext Teacher = new TeacherDbContext();
        /// MySqlConnection Conn = Teacher.AccessDatabase();
        /// </example>
        /// <returns>A MySqlConnection Object</returns>
        public MySqlConnection AccessDatabase()
        {
            //We are instantiating the MySqlConnection Class to create an object
            //the object is a specific connection to our Teacher database on port 3306 of localhost
            return new MySqlConnection(ConnectionString);
        }
    }

}