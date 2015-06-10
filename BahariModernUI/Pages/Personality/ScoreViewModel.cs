using BahariModernUI.Model;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BahariModernUI.Pages.Personality
{
    public class ScoreViewModel
    {
        App app = (App)App.Current;
        public ObservableCollection<ScoreModel> Collection { get; set; }
        public ScoreViewModel()
        {
            Collection = new ObservableCollection<ScoreModel>();
            GenerateDatas();
        }
        private void GenerateDatas()
        {
            try
            {
                SQLiteDatabase db = new SQLiteDatabase();
                DataTable users;
                String query = "SELECT SCORE ";
                query += "FROM SCORE ";
                query += "WHERE USERNAME = '" + app.LoginUser + "';";

                users = db.GetDataTable(query);

                //ModernDialog.ShowMessage(app.LoginUser.ToString(), "", MessageBoxButton.OK);

                // Or looped through for some other reason
                int x = 0;
                foreach (DataRow r in users.Rows)
                {
                    double left = (double) x;
                    double right = double.Parse(r["SCORE"].ToString());
                    this.Collection.Add(new ScoreModel(left, right));
                    //ModernDialog.ShowMessage(Collection.ElementAt(0).Y.ToString(), "", MessageBoxButton.OK);
                    x++;
                }
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
