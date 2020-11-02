using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Bufet1131Vorobyov
{
    internal class AccountingSql
    {
        private DB dB;

        public AccountingSql(DB dB)
        {
            this.dB = dB;
        }

        internal ObservableCollection<Accounting> GetData()
        {
            ObservableCollection<Accounting> result = new ObservableCollection<Accounting>();
            string sql = "SELECT a.id as aid, a.date as adate, a.count as acount, id_provider, id_food, p.id as idprovider, p.name AS pname, p.status AS pstatus, f.status as fstatus, f.id as idfood, f.name as fname, f.count AS fcount FROM accountingprovider a JOIN food f ON a.id_food = f.id JOIN provider p ON a.id_provider = p.id";
            Accounting last = null;
            Dictionary<int, Food> foods = new Dictionary<int, Food>();
            if (dB.OpenConnection())
            {
                using (var mc = new MySqlCommand(sql, dB.connection))
                {
                    using (var dr = mc.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (dr.GetInt32("pstatus") > 0)
                            {
                                if (last != null && last.ID != dr.GetInt32("aid"))
                                    last = null;

                                if (last == null)
                                {
                                    last.ID = dr.GetInt32("aid");
                                    last.DateTime = DateTime.FromBinary(dr.GetInt64("adate"));
                                    last.Count = dr.GetInt32("acount");
                                    Provider provider = new Provider();
                                    provider.ID = dr.GetInt32("idprovider");
                                    provider.Name = dr.GetString("pname");
                                    last.Provider = provider;
                                    Food food = new Food();
                                    food.ID = dr.GetInt32("idfood");
                                    food.Name = dr.GetString("fname");
                                    food.Count = dr.GetInt32("fcount");
                                    last.Food = food;
                                    result.Add(last);
                                }
                            }
                        }
                    }
                }
                dB.CloseConnection();
            }
            return result;
        }

        internal void EditAccounting(Accounting selectedAccounting)
        {

        }
    }
}