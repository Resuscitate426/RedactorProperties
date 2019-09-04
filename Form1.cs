using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Redactor
{
    public partial class Form1 : Form
    {
        SimpleClass EditableObject = new SimpleClass(); //Для смены объекта
        PropertyInfo[] allProperties;
        Type myType = typeof(SimpleClass);
        

        public Form1()
        {
            InitializeComponent();
            ComboFilling();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            EditorProperties(Convert.ToInt64(numericUpDown1.Value), comboBox1);
        }


        void EditorProperties(object first, ComboBox box) //Запись новых значений
        {
            try
            {
                PropertyInfo fi = myType.GetProperty(box.Text);
                fi.SetValue(EditableObject, Convert.ChangeType(first, fi.PropertyType), null);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Исключение: " + ex.Message);
            }
        }

        void SearchFields() //Поиск всех свойств объекта 
        {

            listBox1.Items.Clear();

            PropertyFilling();


            for (int i = 0; i < allProperties.Length; i++)
            {
                if (allProperties[i].PropertyType == typeof(long))
                    listBox1.Items.Add(allProperties[i].Name + " = " + allProperties[i].GetValue(EditableObject));

                else
                {
                    listBox1.Items.Add(allProperties[i].Name);
                    listBox1.Items.Add(allProperties[i].GetValue(EditableObject) + "");
                }
            }


        }

        private void Button2_Click(object sender, EventArgs e)
        {
            SearchFields();
        }


        void PropertyFilling() //Рефлексия
        {
            allProperties = myType.GetProperties(BindingFlags.NonPublic
                | BindingFlags.Instance
                | BindingFlags.Public);
        }

        void ComboFilling() //Заполнение comboBox-ов
        {
            PropertyFilling();


            foreach (var tt in allProperties)
            {
                if (tt.PropertyType == typeof(long))
                    comboBox1.Items.Add(tt.Name);
                else
                    comboBox2.Items.Add(tt.Name);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            EditorProperties(textBox2.Text, comboBox2);
        }
    }
}
