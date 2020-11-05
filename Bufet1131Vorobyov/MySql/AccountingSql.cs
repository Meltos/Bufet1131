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
                                    last = new Accounting();
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
            string sql = $"update accountingprovider set count = @count, date = @date, id_provider = @id_provider, id_food = @id_food where id = '{selectedAccounting.ID}'";
            if (dB.OpenConnection())
            {
                var mc = new MySqlCommand(sql, dB.connection);
                var p1 = new MySqlParameter("date", MySqlDbType.Int64);
                p1.Value = selectedAccounting.DateTime.ToBinary();
                mc.Parameters.Add(p1);
                var p2 = new MySqlParameter("id_provider", MySqlDbType.Int32);
                p2.Value = selectedAccounting.Provider.ID;
                mc.Parameters.Add(p2);
                var p3 = new MySqlParameter("count", MySqlDbType.Int32);
                p3.Value = selectedAccounting.Count;
                mc.Parameters.Add(p3);
                var p4 = new MySqlParameter("id_food", MySqlDbType.Int32);
                p4.Value = selectedAccounting.Food.ID;
                mc.Parameters.Add(p4);
                mc.ExecuteNonQuery();
                dB.CloseConnection();
            }
        }

        internal int AddNewAccounting(Accounting newaccounting)
        {
            long date = newaccounting.DateTime.ToBinary();
            int id = 0;
            string sql = $"start transaction; insert into accountingprovider values(0, '{newaccounting.Count}', '{date}', '{newaccounting.Provider.ID}', '{newaccounting.Food.ID}'); select LAST_INSERT_ID(); commit;";
            if (dB.OpenConnection())
            {
                using (var mc = new MySqlCommand(sql, dB.connection))
                {
                    using (var dr = mc.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            id = dr.GetInt32("LAST_INSERT_ID()");
                        }
                    }
                }
                dB.CloseConnection();
            }
            return id;
        }

        internal void RemoveAccounting(Accounting selectedAccounting)
        {
            string sql = $"DELETE from accountingprovider WHERE id = '{selectedAccounting.ID}'";
            if (dB.OpenConnection())
            {
                var mc = new MySqlCommand(sql, dB.connection);
                mc.ExecuteNonQuery();
                dB.CloseConnection();
            }
        }
    }
}