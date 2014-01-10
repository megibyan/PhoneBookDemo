using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace PhoneBookDemo
{
    public partial class PhoneBook : Form
    {
        public PhoneBook(){
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e){
        }

        [Serializable]

        public class Data{
            public string FirstName;
            public string LastName;
            public string PhoneNumber;
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e){
            DataVisualisationGrid.EndEdit();

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream output = new FileStream(saveFileDialog1.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                //saveFileDialog1.DefaultExt = "txt";
                //saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

                int n = DataVisualisationGrid.RowCount;
                Data[] Person = new Data[n - 1];
                for (int i = 0; i < n - 1; i++)
                {
                    Person[i] = new Data();
                    Person[i].FirstName = DataVisualisationGrid[0, i].Value.ToString();
                    Person[i].LastName = DataVisualisationGrid[1, i].Value.ToString();
                    Person[i].PhoneNumber = DataVisualisationGrid[2, i].Value.ToString();
                }

                formatter.Serialize(output, Person);

                output.Close();
            }
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e){
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                BinaryFormatter reader = new BinaryFormatter();
                FileStream input = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);

                Data[] Person = (Data[])reader.Deserialize(input);
                DataVisualisationGrid.Rows.Clear();
                for (int i = 0; i < Person.Length; i++)
                {
                    DataVisualisationGrid.Rows.Add();
                    DataVisualisationGrid[0, i].Value = Person[i].FirstName;
                    DataVisualisationGrid[1, i].Value = Person[i].LastName;
                    DataVisualisationGrid[2, i].Value = Person[i].PhoneNumber;
                }
            }
        }

        private void deleteToolStripMenuItem_Click_1(object sender, EventArgs e){
            foreach (DataGridViewRow dr in DataVisualisationGrid.SelectedRows)
            {

                if (dr.Cells[0].Value.ToString() != null)
                    DataVisualisationGrid.Rows.Remove(dr);
            }
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e){
            DataVisualisationGrid.Rows.Clear();
        }

        private void writeToAFileToolStripMenuItem_Click(object sender, EventArgs e){
            DataVisualisationGrid.EndEdit();

            SaveFileDialog saveFileDialog2 = new SaveFileDialog();
            saveFileDialog2.RestoreDirectory = true;

            if (saveFileDialog2.ShowDialog() == DialogResult.OK){
                FileStream output = new FileStream(saveFileDialog2.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                //saveFileDialog1.DefaultExt = "txt";
                //saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                output.Close();
                System.IO.File.WriteAllText(saveFileDialog2.FileName, string.Empty);
                int n = DataVisualisationGrid.RowCount;
                Data[] Person = new Data[n - 1];
                for (int i = 0; i < n - 1; i++)
                {
                    Person[i] = new Data();
                    Person[i].FirstName = DataVisualisationGrid[0, i].Value.ToString();
                    Person[i].LastName = DataVisualisationGrid[1, i].Value.ToString();
                    Person[i].PhoneNumber = DataVisualisationGrid[2, i].Value.ToString();
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(saveFileDialog2.FileName, true))
                        file.WriteLine(Person[i].FirstName + '/' + Person[i].LastName + '/' + Person[i].PhoneNumber);
                }

            }
        }

        private void readFromAFileToolStripMenuItem_Click(object sender, EventArgs e){

        }
    }
}

