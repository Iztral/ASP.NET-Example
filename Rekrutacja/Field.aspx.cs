using Rekrutacja.Classes;
using System;

namespace Rekrutacja
{
    public partial class Pole : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(DropDownList1.SelectedIndex > 0)
            {
                Panel1.Visible = true;

                InputBox.Text = "";
                ResultLabel.Text = "";
                FormatLabel.Text = GetFigure().InputFormat;
            }
        }

        private double Calculate(Figures.Shape figure, string inputData)
        {
            string[] values = inputData.Split('\\');
            figure.sides = new double[values.Length];
            for(int i = 0; i <= values.Length-1; i++)
            {
                figure.sides[i] = Convert.ToDouble(values[i]);
            }
            
            return Math.Round(figure.Area(),2);
        }

        private Figures.Shape GetFigure()
        {
            return (Figures.Shape)Activator.CreateInstance(Type.GetType("Rekrutacja.Classes.Figures+" 
                + DropDownList1.SelectedValue)); ;
        }

        protected void CalculateButton_Click(object sender, EventArgs e)
        {
            try
            {
                ResultLabel.Text = "Pole powierzchni wybranej figury " + DropDownList1.SelectedItem.Text
                + " wynosi: " + Calculate(GetFigure(), InputBox.Text);
            }
            catch (Exception ex)
            {
                if (ex is IndexOutOfRangeException || ex is FormatException)
                {
                    string alert = "Błędne dane, proszę o ponowne sprawdzenie.";

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + alert + "');", true);
                }
            }
        }
    }
}