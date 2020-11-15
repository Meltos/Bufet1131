﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaginatorSample
{
    public class ListViewViewer
    {
        private readonly int columnCount;
        ListViewItem[] rows;
        ListView listView;
        public ListViewViewer(ListView listView1, int columnCount, int size)
        {
            listView = listView1;
            // следующие 2 строки это финт, позволяющий уменьшить мелькание при обновлении данных в списке
            var prop = listView.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            prop.SetValue(listView, true, null);
            this.columnCount = columnCount;
            Resize(size);
        }

        public void Resize(int size)
        {
            // создание буфера - списка строк в listview, с заготовленными колонками
            listView.Items.Clear();
            rows = new ListViewItem[size];
            for (int i = 0; i < size; i++)
            {
                var row = new ListViewItem();
                for (int c = 0; c < columnCount; c++)
                    row.SubItems.Add("");
                rows[i] = row;
            }
        }

        
        public void ViewData(IEnumerable<IViewRow> data)
        {
            int max = data.Count() <= rows.Length ? data.Count() : rows.Length;
            int i = 0;
            listView.BeginUpdate();
            listView.Items.Clear();
            // меняем данные в колонках для всех выводимых записей
            for (; i < max; i++)
            {
                var row = data.ElementAt(i);
                rows[i].Tag = row;
                for (int c = 0; c < columnCount; c++)
                    rows[i].SubItems[c].Text = row.GetColumn(c);
            }
            listView.Items.AddRange(rows.Take(max).ToArray());
            listView.EndUpdate();
        }
    }
}
