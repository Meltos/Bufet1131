using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;

namespace Bufet1131Vorobyov
{
    internal class OrderSql
    {
        private DB dB;

        public OrderSql(DB dB)
        {
            this.dB = dB;
        }

        internal ObservableCollection<Order> GetData()
        {
            ObservableCollection<Order> result = new ObservableCollection<Order>();
            string sql = "SELECT o.id as oid, o.date as odate, o.count as ocount, o.cost as ocost, id_provider, id_food, o.id_menu, p.id as idprovider, p.name AS pname, f.id as idfood, f.name as fname, f.count AS fcount, f.price as fprice, m.id AS idmenu, m.name AS mname FROM orderfood o JOIN food f ON o.id_food = f.id  JOIN provider p ON o.id_provider = p.id JOIN menu m ON o.id_menu = m.id";
            Order last = null;
            if (dB.OpenConnection())
            {
                using (var mc = new MySqlCommand(sql, dB.connection))
                {
                    using (var dr = mc.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (last != null && last.ID != dr.GetInt32("oid"))
                                last = null;

                            if (last == null)
                            {
                                last = new Order();
                                last.ID = dr.GetInt32("oid");
                                last.DateTime = DateTime.FromBinary(dr.GetInt64("odate"));
                                last.Count = dr.GetInt32("ocount");
                                last.Cost = dr.GetInt32("ocost");
                                Provider provider = new Provider();
                                provider.ID = dr.GetInt32("idprovider");
                                provider.Name = dr.GetString("pname");
                                last.Provider = provider;
                                Food food = new Food();
                                food.ID = dr.GetInt32("idfood");
                                food.Name = dr.GetString("fname");
                                food.Count = dr.GetInt32("fcount");
                                food.Price = dr.GetInt32("fprice");
                                last.Food = food;
                                Menu menu = new Menu();
                                menu.ID = dr.GetInt32("idmenu");
                                menu.Name = dr.GetString("mname");
                                last.Menu = menu;
                                result.Add(last);
                            }
                        }
                    }
                }
                dB.CloseConnection();
            }
            return result;
        }

        internal void EditOrder(Order selectedOrder)
        {
            string sql = $"update orderfood set date = @date, count = @count, cost = @cost, id_provider = @id_provider, id_food = @id_food, id_menu = @id_menu where id = '{selectedOrder.ID}'";
            if (dB.OpenConnection())
            {
                var mc = new MySqlCommand(sql, dB.connection);
                var p1 = new MySqlParameter("date", MySqlDbType.Int64);
                p1.Value = selectedOrder.DateTime.ToBinary();
                mc.Parameters.Add(p1);
                var p2 = new MySqlParameter("id_provider", MySqlDbType.Int32);
                p2.Value = selectedOrder.Provider.ID;
                mc.Parameters.Add(p2);
                var p3 = new MySqlParameter("count", MySqlDbType.Int32);
                p3.Value = selectedOrder.Count;
                mc.Parameters.Add(p3);
                var p4 = new MySqlParameter("id_food", MySqlDbType.Int32);
                p4.Value = selectedOrder.Food.ID;
                mc.Parameters.Add(p4);
                var p5 = new MySqlParameter("id_menu", MySqlDbType.Int32);
                p5.Value = selectedOrder.Menu.ID;
                mc.Parameters.Add(p5);
                var p6 = new MySqlParameter("cost", MySqlDbType.Int32);
                p6.Value = selectedOrder.Cost;
                mc.Parameters.Add(p6);
                mc.ExecuteNonQuery();
                dB.CloseConnection();
            }
        }

        internal int AddNewOrder(Order newOrder)
        {
            int id = 0;
            string sql = $"start transaction; insert into orderfood values(0, '{newOrder.DateTime.ToBinary()}', '{newOrder.Count}', '{newOrder.Cost}', '{newOrder.Provider.ID}', '{newOrder.Food.ID}', '{newOrder.Menu.ID}'); select LAST_INSERT_ID(); commit;";
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

        internal void RemoveOrder(Order selectedOrder)
        {
            string sql = $"DELETE from orderfood WHERE id = '{selectedOrder.ID}'";
            if (dB.OpenConnection())
            {
                var mc = new MySqlCommand(sql, dB.connection);
                mc.ExecuteNonQuery();
                dB.CloseConnection();
            }
        }
    }
}