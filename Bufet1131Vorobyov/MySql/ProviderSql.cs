using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Bufet1131Vorobyov
{
    class ProviderSql
    {
        DB dB;

        public ProviderSql(DB dB)
        {
            this.dB = dB;
        }

        internal ObservableCollection<Provider> GetData()
        {
            ObservableCollection<Provider> result = new ObservableCollection<Provider>();
            string sql = "SELECT id_provider, id_food, ISNULL(id_food) as foodnull, p.id as idprovider, p.name AS pname, p.phone AS pphone, p.address AS paddress, p.mail AS pmail, p.status AS pstatus, f.status as fstatus, f.id as idfood, f.name as fname, count, price, img, description FROM idproviderfood i JOIN food f ON i.id_food = f.id  RIGHT  JOIN provider p ON i.id_provider = p.id";
            Provider last = null;
            int foodid;
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
                                if (last != null && last.ID != dr.GetInt32("idprovider"))
                                    last = null;

                                if (last == null)
                                {
                                    last = new Provider();
                                    last.ID = dr.GetInt32("idprovider");
                                    last.Name = dr.GetString("pname");
                                    last.Phone = dr.GetString("pphone");
                                    last.Address = dr.GetString("paddress");
                                    last.Mail = dr.GetString("pmail");
                                    result.Add(last);
                                }

                                if (dr.GetInt32("foodnull") != 1)
                                {
                                    if (dr.GetInt32("fstatus") == 1)
                                    {
                                        foodid = dr.GetInt32("id_food");
                                        if (foods.ContainsKey(foodid))
                                        {
                                            last.Foods.Add(foods[foodid]);
                                            foods[foodid].Providers.Add(last);
                                        }
                                        else
                                        {
                                            Food food = new Food { ID = foodid, Name = dr.GetString("fname"), PathIMG = dr.GetString("img"), Count = dr.GetInt32("count"), Description = dr.GetString("description"), Price = dr.GetInt32("price") };
                                            food.Providers.Add(last);
                                            foods.Add(foodid, food);
                                            last.Foods.Add(foods[foodid]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                dB.CloseConnection();
            }
            return result;
        }

        internal ObservableCollection<Provider> GetProvidersFood()
        {
            ObservableCollection<Provider> result = new ObservableCollection<Provider>();
            string sql = "SELECT id_provider, id_food, p.id as idprovider, p.name AS pname, p.phone AS pphone, p.address AS paddress, p.mail AS pmail, p.status AS pstatus, f.status as fstatus, f.id as idfood, f.name as fname, count, price, img, description FROM idproviderfood i JOIN food f ON i.id_food = f.id JOIN provider p ON i.id_provider = p.id";
            Provider last = null;
            int foodid;
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
                                if (last != null && last.ID != dr.GetInt32("idprovider"))
                                    last = null;

                                if (last == null)
                                {
                                    last = new Provider();
                                    last.ID = dr.GetInt32("idprovider");
                                    last.Name = dr.GetString("pname");
                                    last.Phone = dr.GetString("pphone");
                                    last.Address = dr.GetString("paddress");
                                    last.Mail = dr.GetString("pmail");
                                    result.Add(last);
                                }

                                if (dr.GetInt32("fstatus") == 1)
                                {
                                    foodid = dr.GetInt32("id_food");
                                    if (foods.ContainsKey(foodid))
                                    {
                                        last.Foods.Add(foods[foodid]);
                                        foods[foodid].Providers.Add(last);
                                    }
                                    else
                                    {
                                        Food food = new Food { ID = foodid, Name = dr.GetString("fname"), PathIMG = dr.GetString("img"), Count = dr.GetInt32("count"), Description = dr.GetString("description"), Price = dr.GetInt32("price") };
                                        food.Providers.Add(last);
                                        foods.Add(foodid, food);
                                        last.Foods.Add(foods[foodid]);
                                    }
                                }
                                
                            }
                        }
                    }
                }
                dB.CloseConnection();
            }
            return result;
        }

        internal ObservableCollection<Provider> GetNullProviderFood()
        {
            ObservableCollection<Provider> result = new ObservableCollection<Provider>();
            Provider nullprovider = new Provider { Name="", ID=0};
            Food nullfood = new Food { Name = ""};
            nullfood.Providers.Add(nullprovider);
            nullprovider.Foods.Add(nullfood);
            result.Add(nullprovider);
            string sql = "SELECT id_provider, id_food, p.id as idprovider, p.name AS pname, p.phone AS pphone, p.address AS paddress, p.mail AS pmail, p.status AS pstatus, f.status as fstatus, f.id as idfood, f.name as fname, count, price, img, description FROM idproviderfood i JOIN food f ON i.id_food = f.id JOIN provider p ON i.id_provider = p.id";
            Provider last = null;
            int foodid;
            Dictionary<int, Food> foods = new Dictionary<int, Food>();
            if (dB.OpenConnection())
            {
                using (var mc = new MySqlCommand(sql, dB.connection))
                {
                    using (var dr = mc.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (last != null && last.ID != dr.GetInt32("idprovider"))
                                last = null;

                            if (last == null)
                            {
                                last = new Provider();
                                last.ID = dr.GetInt32("idprovider");
                                last.Name = dr.GetString("pname");
                                last.Phone = dr.GetString("pphone");
                                last.Address = dr.GetString("paddress");
                                last.Mail = dr.GetString("pmail");
                                Food nullfoodprovider = new Food { Name = "", ID=0 };
                                last.Foods.Add(nullfoodprovider);
                                result.Add(last);
                            }

                            foodid = dr.GetInt32("id_food");
                            if (foods.ContainsKey(foodid))
                            {
                                last.Foods.Add(foods[foodid]);
                                foods[foodid].Providers.Add(last);
                            }
                            else
                            {
                                Food food = new Food { ID = foodid, Name = dr.GetString("fname"), PathIMG = dr.GetString("img"), Count = dr.GetInt32("count"), Description = dr.GetString("description"), Price = dr.GetInt32("price") };
                                food.Providers.Add(last);
                                foods.Add(foodid, food);
                                last.Foods.Add(foods[foodid]);
                            }
                        }
                    }
                }
                dB.CloseConnection();
            }
            return result;
        }

        internal void EditProvider(Provider selectedProvider)
        {
            string sql = $"update provider set name = @name, phone = @phone, address = @address, mail = @mail where id = '{selectedProvider.ID}'";
            if (dB.OpenConnection())
            {
                var mc = new MySqlCommand(sql, dB.connection);
                var p1 = new MySqlParameter("phone", MySqlDbType.String);
                p1.Value = selectedProvider.Phone;
                mc.Parameters.Add(p1);
                var p2 = new MySqlParameter("name", MySqlDbType.String);
                p2.Value = selectedProvider.Name;
                mc.Parameters.Add(p2);
                var p3 = new MySqlParameter("address", MySqlDbType.String);
                p3.Value = selectedProvider.Address;
                mc.Parameters.Add(p3);
                var p4 = new MySqlParameter("mail", MySqlDbType.String);
                p4.Value = selectedProvider.Mail;
                mc.Parameters.Add(p4);
                mc.ExecuteNonQuery();
                dB.CloseConnection();
            }
        }

        internal int AddNewProvider(Provider newprovider)
        {
            int id = 0;
            string sql = $"start transaction; insert into provider values(0, '{newprovider.Name}', '{newprovider.Phone}', '{newprovider.Address}', '{newprovider.Mail}', '1'); select LAST_INSERT_ID(); commit;";
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

        internal void AddFoodProvider(Food selectedFood, Provider selectedProvider)
        {
            string sql = $"insert into idproviderfood values(0, '{selectedProvider.ID}', '{selectedFood.ID}')";
            if (dB.OpenConnection())
            {
                var mc = new MySqlCommand(sql, dB.connection);
                mc.ExecuteNonQuery();
                dB.CloseConnection();
            }
        }

        internal void RemoveProvider(Provider selectedProvider)
        {
            string sql = $"update provider set status = '0' where id = '{selectedProvider.ID}'";
            if (dB.OpenConnection())
            {
                var mc = new MySqlCommand(sql, dB.connection);
                mc.ExecuteNonQuery();
                dB.CloseConnection();
            }
        }

        internal void RemoveFoodProvider(Food selectedFood, Provider selectedProvider)
        {
            string sql = $"DELETE from idproviderfood WHERE id_provider = '{selectedProvider.ID}' and id_food = '{selectedFood.ID}'";
            if (dB.OpenConnection())
            {
                var mc = new MySqlCommand(sql, dB.connection);
                mc.ExecuteNonQuery();
                dB.CloseConnection();
            }
        }
    }
}