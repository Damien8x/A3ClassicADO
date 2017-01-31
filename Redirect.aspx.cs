using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Redirect : System.Web.UI.Page
{
    RedirectClass tb = new RedirectClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["personKey"] != null)
        {
          if(!IsPostBack)
            {
                DataTable table = tb.GetGrantTypes();
                DropDownList1.DataSource = table;
                DropDownList1.DataTextField = "GrantTypeKey";
                DropDownList1.DataValueField = "GrantTypeKey";
                DropDownList1.DataBind();
                ListItem item = new ListItem("Choose Grant Type");
                DropDownList1.Items.Insert(0, item);
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
        

    }
    protected void FillGrid()
    {
        if (!DropDownList1.SelectedItem.Text.Equals("Choose Grant Type"))
        {
            int key = int.Parse(DropDownList1.SelectedValue.ToString());
            DataTable tbl = tb.GetRequests(key);
            GridView1.DataSource = tbl;
            GridView1.DataBind();
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
}