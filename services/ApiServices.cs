﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using PrinterApp.Admin.Models;
using PrinterApp.Tables.Register;
using Newtonsoft.Json;

namespace PrinterApp.Serivces
{
    public class ApiServices
    {
        private JsonSerializer _serializer = new JsonSerializer();

        private static ApiServices _ServiceClientInstance;



        public static ApiServices ServiceClientInstance
        {
            get
            {
                if (_ServiceClientInstance == null)
                    _ServiceClientInstance = new ApiServices();
                return _ServiceClientInstance;
            }
        }

        FirebaseClient firebase;
        public ApiServices()
        {
            //replace this with your firebase realtimedatabase end point.
            firebase = new FirebaseClient("https://printerapp-9ae65.firebaseio.com/");
        }


        #region ClientSection


        //[Pushing Single table to the Database]
        public async Task<bool> RegisterUser(string uname, string passwd)
        {
            var result = await firebase
                .Child("RegisterEmployeeTable")
                .PostAsync(new RegisterTable() { UserId = Guid.NewGuid(), UserName = uname, Password = passwd });

            if (result.Object != null)
            {
                return true;
            }
            else
            {
                return false;

            }
        }


        //Login with clients credentials. 
        public async Task<RegisterTable> LoginUser(string uname, string passwd)
        {
            var GetPerson = (await firebase
              .Child("RegisterEmployeeTable")
              .OnceAsync<RegisterTable>()).Where(a => a.Object.UserName == uname).Where(b => b.Object.Password == passwd).FirstOrDefault();

            if (GetPerson != null)
            {

                var content = GetPerson.Object as RegisterTable;
                return content;

            }
            else
            {
                return null;
            }
        }
        //Get all the task of the clients
        public async Task<List<TasksModel>> GetClientTasks(string userId)
        {
            var GetTasks = (await firebase
              .Child("EmployeeTaskTable")
              .OnceAsync<TasksModel>()).Where(a => a.Object.ClientID.ToString() == userId).Select(item => new TasksModel
              {
                  ClientID = item.Object.ClientID,
                  TaskTitle = item.Object.TaskTitle,
                  TaskId = item.Object.TaskId,
                  clientTasks = item.Object.clientTasks
              }).ToList(); ;

            if (GetTasks != null)
            {
                return new List<TasksModel>(GetTasks);
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region AdminSection

        //Get All the registered Client Users

        public async Task<List<RegisterTable>> GetRegisteredUsers()
        {
            var GetClients = (await firebase
               .Child("RegisterEmployeeTable")
               .OnceAsync<RegisterTable>()).Select(item => new RegisterTable
               {
                   UserName = item.Object.UserName,
                   Password = item.Object.Password,
                   UserId = item.Object.UserId
               }).ToList();
            return GetClients;
        }


        // [Pushing Nested table to the Database]
        public async Task AssignTaskToClient(TasksModel tasks)
        {
            var result = await firebase
            .Child("EmployeeTaskTable")
            .PostAsync(new TasksModel()
            {
                ClientID = tasks.ClientID,
                TaskId = tasks.TaskId,
                TaskTitle = tasks.TaskTitle,
                clientTasks = tasks.clientTasks
            });
        }


        //Update the database
        public async Task<bool> DeleteDatabaseConetent(TasksModel tasksModel)
        {
            var DeleteUserDb = (await firebase
             .Child("EmployeeTaskTable")
             .OnceAsync<TasksModel>()).Where(a => a.Object.TaskId == tasksModel.TaskId).FirstOrDefault();
            await firebase.Child("EmployeeTaskTable").Child(DeleteUserDb.Key).DeleteAsync();


            if (DeleteUserDb.Object != null)
            {
                return true;
            }
            else
            {
                return false;

            }
        }



        //Register Admin User
        public async Task<bool> RegisterAdminUser(string userName, string password)
        {
            var result = await firebase
           .Child("RegisterAdminTable")
           .PostAsync(new RegisterTable() { UserId = Guid.NewGuid(), UserName = userName, Password = password });

            if (result.Object != null)
            {
                return true;
            }
            else
            {
                return false;

            }
        }


        //Login AdminUser
        public async Task<RegisterTable> LoginAdminUser(string username, string password)
        {
            var GetPerson = (await firebase
              .Child("RegisterAdminTable")
              .OnceAsync<RegisterTable>()).Where(a => a.Object.UserName == username).Where(b => b.Object.Password == password).FirstOrDefault();

            if (GetPerson != null)
            {

                var content = GetPerson.Object as RegisterTable;
                return content;

            }
            else
            {
                return null;
            }
        }

        #endregion

    }
}