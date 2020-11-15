using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Бюро_находок_и_забытых_вещей
{
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
                .Select(s => new { Prop = s, ColumnName = ((ColumnViewerAttribute)Attribute.GetCustomAttribute(s, typeof(ColumnViewerAttribute))).ColumnName })
                .ToDictionary(s => s.ColumnName);


            foreach (var cell in array)
            {
                ListViewItem item = new ListViewItem();
                // извлекаем из первой колонки Tag, по нему обращаемся к свойству класса и получаем значение
                item.Text = attributes[columns[0]].Prop.GetValue(cell).ToString();

                // аналогично заполняем остальные столбцы
                for (int i = 1; i < columns.Count; i++)
                    item.SubItems.Add(attributes[columns[i]].Prop.GetValue(cell).ToString());
                item.Tag = cell;
                ViewerControl.Items.Add(item);
            }
            // получилось немного стремно из-за необходимости приведения object в string, но так уж работает рефлексия

        }
    }
}
