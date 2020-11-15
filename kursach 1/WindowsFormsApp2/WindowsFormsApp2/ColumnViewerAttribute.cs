using System;

namespace WindowsFormsApp2
{
    internal class ColumnViewerAttribute : Attribute
    {
        public string ColumnName;

        public ColumnViewerAttribute(string v)
        {
            this.ColumnName = v;
        }
    }
}