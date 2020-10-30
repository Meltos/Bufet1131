﻿using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Bufet1131Vorobyov
{
    class FoodSql
    {
        DB dB;

        public FoodSql(DB dB)
        {
            this.dB = dB;
        }

        public ObservableCollection<Food> GetData()
        {
            ObservableCollection<Food> result = new ObservableCollection<Food>();
            string sql = "SELECT ISNULL(id_menu) as menunull,id_menu, id_food, m.id as idmenu, m.name as mname, m.status as mstatus, f.status as fstatus, f.id as idfood, f.name as fname, count, price, img, description FROM idmenufood i JOIN menu m ON i.id_menu = m.id right JOIN food f ON i.id_food = f.id";
            Food last = null;
            int menuid;
            Dictionary<int, Menu> menus = new Dictionary<int, Menu>();
            if (dB.OpenConnection())
            {
                using (var mc = new MySqlCommand(sql, dB.connection))
                {
                    using (var dr = mc.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (dr.GetInt32("fstatus") == 1)
                            {
                                if (last != null && last.ID != dr.GetInt32("idfood"))
                                    last = null;

                                if (last == null)
                                {
                                    last = new Food { ID = dr.GetInt32("idfood"), Name = dr.GetString("fname"), PathIMG = dr.GetString("img"), Count = dr.GetInt32("count"), Description = dr.GetString("description"), Price = dr.GetInt32("price") }; ;
                                    result.Add(last);
                                }

                                if (dr.GetInt32("menunull") != 1)
                                {
                                    if (dr.GetInt32("mstatus") == 1)
                                    {
                                        menuid = dr.GetInt32("id_menu");
                                        if (menuid != 0)
                                        {
                                            if (menus.ContainsKey(menuid))
                                            {
                                                last.Menus.Add(menus[menuid]);
                                                menus[menuid].Foods.Add(last);
                                            }
                                            else
                                            {
                                                Menu menu = new Menu { ID = menuid, Name = dr.GetString("mname") };
                                                menu.Foods.Add(last);
                                                menus.Add(menuid, menu);
                                                last.Menus.Add(menus[menuid]);
                                            }
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

        internal ObservableCollection<Food> GetNoProviderFoods(Provider value)
        {
            var except = value.Foods.Select(s => s.ID);
            ObservableCollection<Food> foods = GetData();
            return new ObservableCollection<Food>(foods.Where(s => !except.Contains(s.ID)));
        }

        internal ObservableCollection<Food> GetNoMenuFoods(Menu value)
        {
            var except = value.Foods.Select(s=>s.ID);
            ObservableCollection<Food> foods = GetData();
            return new ObservableCollection<Food>(foods.Where(s => !except.Contains(s.ID)));
        }

        internal void EditFood(Food selectedFood)
        {
            string sql = $"update food set name = @name, count = @count, price = @price, img = @img, description = @description where id = '{selectedFood.ID}'";
            if (dB.OpenConnection())
            {
                var mc = new MySqlCommand(sql, dB.connection);
                var p1 = new MySqlParameter("img", MySqlDbType.String);
                p1.Value = selectedFood.PathIMG;
                mc.Parameters.Add(p1);
                var p2 = new MySqlParameter("name", MySqlDbType.String);
                p2.Value = selectedFood.Name;
                mc.Parameters.Add(p2);
                var p3 = new MySqlParameter("count", MySqlDbType.Int32);
                p3.Value = selectedFood.Count;
                mc.Parameters.Add(p3);
                var p4 = new MySqlParameter("price", MySqlDbType.Int32);
                p4.Value = selectedFood.Price;
                mc.Parameters.Add(p4);
                var p5 = new MySqlParameter("description", MySqlDbType.String);
                p5.Value = selectedFood.Description;
                mc.Parameters.Add(p5);
                mc.ExecuteNonQuery();
                dB.CloseConnection();
            }
        }

        internal int AddNewFood(Food newfood)
        {
            int id = 0;
            string sql = $"start transaction; insert into food values(0, '{newfood.Name}', 'Нет в наличии', '0', '{newfood.PathIMG}', '', '1'); select LAST_INSERT_ID(); commit;";
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

        internal void RemoveFood(Food food)
        {
            string sql = $"update food set status = '0' where id = '{food.ID}'";
            if (dB.OpenConnection())
            {
                var mc = new MySqlCommand(sql, dB.connection);
                mc.ExecuteNonQuery();
                dB.CloseConnection();
            }
        }
    }

}
