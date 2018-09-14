using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2.Roles
{
	public partial class Roles : System.Web.UI.Page
	{
		TextBox txbId = new TextBox();
		TextBox txbName = new TextBox();
		public Button btnAjout = new Button();

		protected void Page_PreInit(object sender, EventArgs e)
		{

		}

		protected void Page_Load(object sender, EventArgs e)
		{
			//btnAjout.Click += btnAjout_Click;
			btnAjout.CommandName = "Insert";
			if (GridView1.FooterRow != null)
			{
				GridView1.FooterRow.Visible = true;
				GridView1.FooterRow.BackColor = Color.Red;
			}
			txbId.Width = 50;
			txbId.CssClass = "colId";
		}

		protected void btnAjout_Click(object sender, EventArgs e)
		{
			int result = 0;
			bool isnum = int.TryParse(txbId.Text, out result);
			if (!isnum || result == 0) return;

			ConnectionStringSettings setting =
				ConfigurationManager.ConnectionStrings["AuthConnectionString"];
			using (SqlConnection conn = new SqlConnection(setting.ConnectionString))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand(
					"INSERT INTO AspNetRoles(Id, Name)" +
					"VALUES(@Id, @Name)", conn);
				cmd.Parameters.Add(
					new SqlParameter("Id", txbId.Text));
				cmd.Parameters.Add(
					new SqlParameter("Name", txbName.Text));
				try
				{
					cmd.ExecuteNonQuery();
				}
				catch(Exception ex)
				{
					lblErr.Text = ex.Message;
				}
				cmd.Dispose();
				GridView1.DataBind();
				txbName.Text = "";
			}

		}

		protected void AjoutControles(GridViewRow row)
		{
			ConnectionStringSettings setting =
				ConfigurationManager.ConnectionStrings["AuthConnectionString"];
			int num = 0;
			using (SqlConnection conn = new SqlConnection(setting.ConnectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(
					"SELECT Max(Id) FROM AspNetRoles", conn))
				{
					SqlDataReader reader = cmd.ExecuteReader();
					if (reader.HasRows)
					{
						reader.Read();
						num = int.Parse(reader.GetString(0)) + 1;
						reader.Close();
					}
					else
					{
						num = 1;
					}
				}
			}
			txbId.Text = num.ToString();
			lblErr.Text = "_";
			row.Cells[1].Controls.Add(txbId);
			row.Cells[2].Controls.Add(txbName);
			txbName.CssClass = "colName";
			txbId.TextChanged += txbId_TextChanged;
			btnAjout.Text = "+";
			btnAjout.ID = "btnAjout";
			btnAjout.BackColor = Color.Red;
			//btnAjout.CausesValidation = false;
			row.Cells[3].Controls.Add(btnAjout);
			//txbName.BackColor = Color.GreenYellow;
			//row.Cells[0].BackColor = Color.Beige;
			//row.Cells[2].BackColor = Color.Azure;
			//row.Cells[3].BackColor = Color.Green;
		}

		protected void txbId_TextChanged(object sender, EventArgs e)
		{
			string Value = ((TextBox)sender).Text;
			int result = 0;
			bool isnum = int.TryParse(Value, out result);
			if (isnum == false) lblErr.Text = "La clef doit être numérique.";
			else
				if (result == 0) lblErr.Text = "Prévoir une clef numérique non nulle.";
			else
				lblErr.Text = "_";
		}

		protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
		{
		}


		protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.Footer)
			{
				btnAjout.Click += btnAjout_Click;
				//e.Row.Cells[3].Controls.Add(btnAjout);
				AjoutControles(e.Row);
			}

		}

		protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
		{

		}

		protected void GridView1_DataBound(object sender, EventArgs e)
		{
			GridView1.FooterRow.Visible = true;
			if (GridView1.FooterRow.Cells[2].Controls.Count == 0)
			{
				AjoutControles(GridView1.FooterRow);
			}
		}

		protected void btnAnnulerSel_Click(object sender, EventArgs e)
		{
			GridView1.SelectedIndex = -1;
			GridView1.Focus();
			pnlDelete.Visible = false;
		}

		protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(GridView1.SelectedIndex > -1)
			{
				pnlDelete.Visible = true;
			}
		}

		protected void btnSupprimer_Click(object sender, EventArgs e)
		{
			ConnectionStringSettings setting = ConfigurationManager.ConnectionStrings["AuthConnectionString"];
			using (SqlConnection conn = new SqlConnection(setting.ConnectionString))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand(
					"DELETE FROM AspNetRoles WHERE Id=@Id",
					conn);
				cmd.Parameters.Add(new SqlParameter("Id", GridView1.SelectedValue));
				try
				{
					cmd.ExecuteNonQuery();
				}
				catch (Exception ex)
				{
					lblErr.Text = ex.Message;
				}
			}
			GridView1.DataBind();
			btnAnnulerSel_Click(GridView1, new EventArgs());
		}
	}
}