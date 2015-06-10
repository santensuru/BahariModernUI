using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BahariModernUI.Model
{
    public class ScoreModel
    {
        public double X { get; set; }
        public double Y { get; set; }
       
        public ScoreModel(double x,double y)
        {
            X = x;
            Y = y;           
        }      
    }
}
