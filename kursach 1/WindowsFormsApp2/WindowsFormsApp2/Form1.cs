using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        Viewer<Data> viewer;
        public Form1()
        {
            InitializeComponent();
            viewer = new Viewer<Data>();
            viewer.ViewerControl = listView1;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            List<Data> test = new List<Data>();
            test.Add(new Data { ColumnData1 = "1", ColumnData2 = "2", NotViewData= "error" });
            test.Add(new Data { ColumnData1 = "3", ColumnData2 = "4", NotViewData = "error" });

            // чтобы не перечислять колонки каждый раз сразу делаем список тегов
            List<string> tags = new List<string>();
            for (int i = 0; i < listView1.Columns.Count; i++)
                tags.Add(listView1.Columns[i].Tag.ToString());

            viewer.UniversalView(tags, test);
        }

       
    }

    public class Viewer<T>
    {
        public ListView ViewerControl;

        public void UniversalView(List<string> columns, List<T> array)
        {
            ViewerControl.Items.Clear();

            // получаем список свойств объекта
            var all_props = typeof(T).GetProperties();
            // выбираем свойства, помеченные атрибутом, формируем словарь из атрибутов
            var attributes = all_props
                .Where(s => s.CustomAttributes.Count() == 1)
                .Select(s =>new { Prop = s, ColumnName = ((ColumnViewerAttribute)Attribute.GetCustomAttribute(s, typeof(ColumnViewerAttribute))).ColumnName })
                .ToDictionary(s=>s.ColumnName);


            foreach (var cell in array)
            {
                ListViewItem item = new ListViewItem();
                // извлекаем из первой колонки Tag, по нему обращаемся к свойству класса и получаем значение
                item.Text = attributes[columns[0]].Prop.GetValue(cell).ToString();

                // аналогично заполняем остальные столбцы
                for (int i = 1; i < columns.Count; i++)
                    item.SubItems.Add(attributes[columns[i]].Prop.GetValue(cell).ToString());

                ViewerControl.Items.Add(item);
            }
            // получилось немного стремно из-за необходимости приведения object в string, но так уж работает рефлексия

        }
    }

    public class Data
    {
        // делаем специальный атрибут и помечаем им те свойства, значения которых нужно вывести в столбцы. Указываем тэг столбца
        [ColumnViewer("Column1")]
        public string ColumnData1 { get; set; }
        [ColumnViewer("Column2")]
        public string ColumnData2 { get; set; }

        public string NotViewData { get; set; }
    }
}
