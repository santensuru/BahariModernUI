using BahariModernUI.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BahariModernUI.Pages.Personality
{
    /// <summary>
    /// Interaction logic for Score.xaml
    /// </summary>
    public partial class Score : UserControl
    {
        public Score()
        {
            InitializeComponent();
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            try
            {
                SQLiteDatabase db = new SQLiteDatabase();
                DataTable users;
                String query = "SELECT NAME, SCORE ";
                query += "FROM USER ";
                query += "ORDER BY SCORE DESC;";

                users = db.GetDataTable(query);

                List<PeopleModel> _peoples = new List<PeopleModel>();

                // Or looped through for some other reason
                foreach (DataRow r in users.Rows)
                {
                    _peoples.Add(new PeopleModel { Name = r["NAME"].ToString(), Score = int.Parse(r["SCORE"].ToString()) });
                    //MessageBox.Show( _peoples.Count().ToString() );
                }

                tStack.ItemsSource = _peoples;
            }
            catch (Exception fail)
            {
                String error = "The following error has occurred:\n\n";
                error += fail.Message.ToString() + "\n\n";
                MessageBox.Show(error);
            }
        }
    }
}
