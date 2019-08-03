using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Default2 : System.Web.UI.Page
{
    System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection();
    List<Employee> EmployeeList
    {
        get { return (List<Employee>)Session["EmployeeList"]; }
        set { Session["EmployeeList"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        con.ConnectionString = "Server=192.169.197.238,1433;Database=ByrumM0141;User id=pater0296;Password=1Password!";
        con.Open();
        try
        {
            string selInfoString = @"SELECT Employee_Information.Employee_Lastname, Employee.Information.Employee_Firstname
                                     Employee_Log.Log_In, Employee_Log.Log_Date FROM Employee_Information JOIN Employee_Log
                                     ON Employee_Information.Log_ID = Employee_Log.Log_ID WHERE NOT (Employee_Log.Log_Date > '" +
                                     Session["EndDate"].ToString() + "' OR Employee_Log.Log_Date < '" + Session["StartDate"].ToString() +
                                     "' ORDER BY Employee_Log.Log_Date THEN BY Employee_Log.Log_Time;";
            SqlCommand selLogData = new SqlCommand(selInfoString, con);
            SqlDataReader infoReader = selLogData.ExecuteReader();
            while (infoReader.Read())
            {
                weeklyReportLabel.Text += @"<tr><td>Name: " + infoReader.GetValue(0).ToString() + ", " + infoReader.GetValue(1).ToString() +
                                          " | Date: " + infoReader.GetValue(3).ToString() + " | Time: " + infoReader.GetValue(2).ToString() +
                                          " | </td></tr>";
            }
            infoReader.Close();
        }
        catch (Exception ex)
        {

        }
        finally
        {
            con.Close();
        }
    }
}