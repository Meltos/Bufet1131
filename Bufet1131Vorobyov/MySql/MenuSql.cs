using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bufet1131Vorobyov
{
    class MenuSql
    {
        DB dB;

        public MenuSql(DB dB)
        {
            this.dB = dB;
        }

        public ObservableCollection<Menu> GetData()
        {
            ObservableCollection<Menu> result = new ObservableCollection<Menu>();
            string sql = "SELECT id_menu, id_food, ISNULL(id_food) as foodnull, m.id as idmenu, m.name AS mname, m.status AS mstatus, f.status as fstatus, f.id as idfood, f.name as fname, count, price, img, description FROM idmenufood i JOIN food f ON i.id_food = f.id RIGHT JOIN menu m ON i.id_menu = m.id";
            Menu last = null;
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
                            if (dr.GetInt32("mstatus") > 0)
                            {
                                if (last != null && last.ID != dr.GetInt32("idmenu"))
                                    last = null;

                                if (last == null)
                                {
                                    last = new Menu();
                                    last.ID = dr.GetInt32("idmenu");

                                    last.Name = dr.GetString("mname");
                                    last.Status = dr.GetInt32("mstatus");
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
                                            foods[foodid].Menus.Add(last);
                                        }
                                        else
                                        {
                                            Food food = new Food { ID = foodid, Name = dr.GetString("fname"), PathIMG = dr.GetString("img"), Count = dr.GetInt32("count"), Description = dr.GetString("description"), Price = dr.GetInt32("price") };
                                            food.Menus.Add(last);
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

        internal ObservableCollection<Menu> GetPublicMenu()
        {
            ObservableCollection<Menu> result = new ObservableCollection<Menu>();
            ObservableCollection<Menu> menus = GetData();
            foreach (var menu in menus)
            {
                if (menu.Status == 2)
                {
                    result.Add(menu);
                }
            }
            return result;
        }

        internal int AddNewMenu(Menu newmenu)
        {
            int id = 0;
            string sql = $"start transaction; insert into menu (id, name) values(0, '{newmenu.Name}'); select LAST_INSERT_ID(); commit;";
            if (dB.OpenConnection())
            {
                using (var mc = new MySqlCommand(sql, dB.connection))
                {
                    using (var dr = mc.ExecuteReader())
                    {
                        while(dr.Read())
                        {
                            id = dr.GetInt32("LAST_INSERT_ID()");
                        }
                    }
                }
                dB.CloseConnection();
            }
            return id;
        }

        internal void AddFoodMenu(Food selectedFood, Menu selectedMenu)
        {
            string sql = $"insert into idmenufood (id, id_menu, id_food) values(0, '{selectedMenu.ID}', '{selectedFood.ID}')";
            if (dB.OpenConnection())
            {
                var mc = new MySqlCommand(sql, dB.connection);
                mc.ExecuteNonQuery();
                
                dB.CloseConnection();
            }

        }

        internal void PublicMenu(Menu menu)
        {
            string sql = $"update menu set status = '2' where id = '{menu.ID}'";
            if (dB.OpenConnection())
            {
                var mc = new MySqlCommand(sql, dB.connection);
                mc.ExecuteNonQuery();
                dB.CloseConnection();
            }
        }

        internal void NoPublicMenu(Menu menu)
        {
            string sql = $"update menu set status = '1' where id = '{menu.ID}'";
            if (dB.OpenConnection())
            {
                var mc = new MySqlCommand(sql, dB.connection);
                mc.ExecuteNonQuery();
                dB.CloseConnection();
            }
        }

        internal void RemoveFoodMenu(Food selectedFood, Menu selectedMenu)
        {
            string sql = $"DELETE from idmenufood WHERE id_menu = '{selectedMenu.ID}' and id_food = '{selectedFood.ID}'";
            if (dB.OpenConnection())
            {
                var mc = new MySqlCommand(sql, dB.connection);
                mc.ExecuteNonQuery();
                dB.CloseConnection();
            }
        }

        internal void EditMenu(Menu menu)
        {
            string sql = $"update menu set name = @name where id = '{menu.ID}'";
            if (dB.OpenConnection())
            {
                var mc = new MySqlCommand(sql, dB.connection);
                var p1 = new MySqlParameter("name", MySqlDbType.String);
                p1.Value = menu.Name;
                mc.Parameters.Add(p1);
                mc.ExecuteNonQuery();
                dB.CloseConnection();
            }
        }

        internal void RemoveMenu(Menu menu)
        {
            string sql = $"update menu set status = '0' where id = '{menu.ID}'";
            if (dB.OpenConnection())
            {
                var mc = new MySqlCommand(sql, dB.connection);
                mc.ExecuteNonQuery();
                dB.CloseConnection();
            }
        }

        internal ObservableCollection<Menu> GetNullMenuFoods()
        {
            ObservableCollection<Menu> result = new ObservableCollection<Menu>();
            Menu nullmenu = new Menu { ID = 0, Name = "" };
            Food nullfood = new Food { ID = 0, Name = "" };
            Provider nullprovider = new Provider { ID = 0, Name = "" };
            nullfood.Menus.Add(nullmenu);
            nullfood.Providers.Add(nullprovider);
            nullmenu.Foods.Add(nullfood);
            result.Add(nullmenu);
            string sql = "SELECT id_menu, id_food, m.id as idmenu, m.name AS mname, m.status AS mstatus, f.status as fstatus, f.id as idfood, f.name as fname, count, price, img, description FROM idmenufood i JOIN food f ON i.id_food = f.id JOIN menu m ON i.id_menu = m.id";
            Menu last = null;
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
                            if (last != null && last.ID != dr.GetInt32("idmenu"))
                                last = null;

                            if (last == null)
                            {
                                last = new Menu();
                                last.ID = dr.GetInt32("idmenu");
                                last.Name = dr.GetString("mname");
                                last.Status = dr.GetInt32("mstatus");
                                Food nullfoodmenu = new Food { ID = 0, Name = "" };
                                Provider nullproviderfood = new Provider { ID = 0, Name = "" };
                                nullfoodmenu.Providers.Add(nullproviderfood);
                                last.Foods.Add(nullfoodmenu);
                                result.Add(last);
                            }

                            foodid = dr.GetInt32("id_food");
                            if (foods.ContainsKey(foodid))
                            {
                                last.Foods.Add(foods[foodid]);
                                foods[foodid].Menus.Add(last);
                            }
                            else
                            {
                                Food food = new Food { ID = foodid, Name = dr.GetString("fname"), PathIMG = dr.GetString("img"), Count = dr.GetInt32("count"), Description = dr.GetString("description"), Price = dr.GetInt32("price") };
                                food.Menus.Add(last);
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
    }
}
