using SQLiteORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyBoardTrainee
{

    // c#\lesson38\KeyBoardTrainee2 — копия\KeyBoardTrainee\KeyBoardTrainee\packages.config


    public class DB
    {
        private  SQLiteDBEngine dBEngine;
        private  string pathTofile = @" D:\c#\lesson38\result.db";
        public  int userLoggedId;

        public DB()
        {
            dBEngine = new SQLiteDBEngine(pathTofile, SQLIteMode.EXISTS);
        }

        public void CreateUser(string login, string password)//регистрация
        {      
            SQLiteTable users = dBEngine["users"];
            List<string> tmp = new List<string>();
            tmp.Add(login);
            tmp.Add(password);           
            users.AddOneRow(tmp);
            dBEngine.Async();
        }


        public void SaveResult(int result)
        {   
            SQLiteTable records = dBEngine["Records"];
            List<string> tmp = new List<string>();
            tmp.Add(userLoggedId.ToString());
            tmp.Add(result.ToString());
            records.AddOneRow(tmp);
            dBEngine.Async();
        }


       

        public List<int> GetAllResult()
        {           
            SQLiteTable records = dBEngine["Records"];
            List<int> results = new List<int>();           
            foreach (List<string> row in records.BodyRows.Values)
            {
                results.Add(Convert.ToInt32(row[1]));               
            }           
            return results;            
        }




        public bool CheckUser(string login, string password)//проверка правильности пароля
        {   
            SQLiteTable users = dBEngine["users"];
            List<KeyValuePair<string, string>> searchPattern = new List<KeyValuePair<string, string>>();
            searchPattern.Add(new KeyValuePair<string, string>("login", login));//
            searchPattern.Add(new KeyValuePair<string, string>("password", password));
            KeyValuePair<long, List<string>>? userInDb = users.GetOneRow(searchPattern);         

            if (userInDb != null)
            {
                userLoggedId = Convert.ToInt32(userInDb.Value.Key);
                
                return true;
            }
            return false;
        }



    }
}
