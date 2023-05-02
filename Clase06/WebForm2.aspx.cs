using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clase06 {
    public partial class WebForm2 : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e) {
            if(e.Row.RowType == DataControlRowType.DataRow) {
                decimal precio = (decimal) DataBinder.Eval(e.Row.DataItem, "precio");
                e.Row.ForeColor = System.Drawing.Color.DarkRed;
                if(precio > 2) {
                    e.Row.ForeColor = System.Drawing.Color.Green;
                }
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e) {
            SelectLabel.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[1].Text;
        }
    }
}