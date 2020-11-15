﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Бюро_находок_и_забытых_вещей
{
    public partial class CountryForm : Form
    {
        List<string> Tags;
        Paginator<CountryDB, Country> paginator;
        Viewer<Country> viewer;
        CountryDB dB;
        public CountryForm()
        {
            InitializeComponent();
            dB = new CountryDB();
            // создаем экземпляр пагинатора для отображения 10 записей на странице. Число 10 можно сделать переменной и вынести в настройки
            paginator = new Paginator<CountryDB, Country>(dB, 15);
            // для отображения данных в листвью я сделал отдельный класс
            // в нем кэшируются строки
            viewer = new Viewer<Country>();
            viewer.ViewerControl = listView1;
            var prop = listView1.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            prop.SetValue(listView1, true, null);
            List<string> tags = new List<string>();
            for (int i = 0; i < listView1.Columns.Count; i++)
                tags.Add(listView1.Columns[i].Tag.ToString());
            Tags = tags;
            viewer.UniversalView(tags, dB.GetList());
            // подписываемся на событие изменения выводимых записей
            paginator.ShowRowsChanges += Paginator_ShowRowsChanges;
            // подписываемся на изменение кол-ва страниц
            paginator.CountChanged += Paginator_CountChanged;
            // подписываемся на изменение текущего индекса
            paginator.CurrentIndexChanged += Paginator_CurrentIndexChanged;

            // вызываем обновление всех данных и событий
            // за счет того, что данный метод вызывается ПОСЛЕ создания пагинатора интерфейс успевает подписаться на события пагинатора и нормально отобразить все данные
            dB.Save();

        }

        private void Paginator_CurrentIndexChanged(object sender, EventArgs e)
        {
            hScrollBar1.Value = paginator.CurrentIndex;
        }

        private void Paginator_CountChanged(object sender, EventArgs e)
        {
            hScrollBar1.Maximum = paginator.MaxIndex;
            hScrollBar1.Value = paginator.CurrentIndex;
        }

        private void Paginator_ShowRowsChanges(object sender, EventArgs e)
        {
            ShowData(paginator.ShowRows);
        }

        private void ShowData(List<Country> rows)
        {
            viewer.UniversalView(Tags, rows);
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paginator.ShowRowsChanges -= Paginator_ShowRowsChanges;
            AddCountryForm addCountryForm = new AddCountryForm(dB.Add(), dB);
            addCountryForm.ShowDialog();
            paginator.ShowRowsChanges += Paginator_ShowRowsChanges;
            dB.Save();
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0)
                return;
            Country country = (Country)listView1.SelectedItems[0].Tag;
            AddCountryForm addCountryForm = new AddCountryForm(country, dB);
            addCountryForm.ShowDialog();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0)
                return;
            Country country = (Country)listView1.SelectedItems[0].Tag;
            if (country.Cities.Count > 0)
            {
                MessageBox.Show("Нельзя удалить страну, если у неё остались города!");
                return;
            }
            if (MessageBox.Show("Точно удалить страну?", "Предупреждение!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                dB.Remove(country);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            paginator.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            paginator.End();
        }

        private void hScrollBar1_Scroll_1(object sender, ScrollEventArgs e)
        {
            if (e.NewValue > e.OldValue)
                paginator.Right();
            else if (e.NewValue < e.OldValue)
                paginator.Left();
        }
    }
}
