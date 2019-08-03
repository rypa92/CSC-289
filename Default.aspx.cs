using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection();
    List<Employee> EmployeeList
    {
        get { return (List<Employee>) Session["EmployeeList"]; }
        set { Session["EmployeeList"] = value; }
    }
    List<Information> InformationList
    {
        get { return (List<Information>) Session["InformationList"]; }
        set { Session["InformationList"] = value; }
    }
    List<Address> AddressList
    {
        get { return (List<Address>) Session["AddressList"]; }
        set { Session["AddressList"] = value; }
    }
    List<Title> TitleList
    {
        get { return (List<Title>) Session["TitleList"]; }
        set { Session["TitleList"] = value; }
    }
    List<Log> LogList
    {
        get { return (List<Log>) Session["LogList"]; }
        set { Session["LogList"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (EmployeeList == null)
        {
            EmployeeList = new List<Employee>();
        }
        if (InformationList == null)
        {
            InformationList = new List<Information>();
        }
        if (AddressList == null)
        {
            AddressList = new List<Address>();
        }
        if (TitleList == null)
        {
            TitleList = new List<Title>();
        }
        if (LogList == null)
        {
            LogList = new List<Log>();
        }
    }

    protected void newEmpButton_Click(object sender, EventArgs e)
    {
        try
        {
            //Gather all info from the form
            ////Employee_Information Table
            int id = int.Parse(idTextBox.Text);
            string firs = firsTextBox.Text;
            string last = lastTextBox.Text;
            string hire = hireTextBox.Text;
            string phon = phonTextBox.Text;
            string bday = bdayTextBox.Text;
            string emai = emaiTextBox.Text;
            string gend = gendTextBox.Text;
            double salray = double.Parse(payrateTextbox.Text);
            ////Employee_Address Table
            int adID = int.Parse(adIDTextBox.Text);
            string add1 = add1TextBox.Text;
            string city = cityTextBox.Text;
            string state = rbState.SelectedItem.Value.ToString();
            string country = rbCountry.SelectedItem.Value.ToString();
            string zip = zipTextBox.Text;
            ////Employee_Title Table
            int titID = int.Parse(titIDTextBox.Text);
            string title = titTitleTextBox.Text;
            string type = rbType.SelectedItem.Value.ToString();
            ////Employee_Log Table
            int logID = int.Parse(logIDTB.Text);

            //Create a temporary employee
            Information tempInfo = new Information(id, firs, last, hire, bday, gend, phon, emai);
            Address tempAdd = new Address(adID, add1, city, state, country, zip);
            Title tempTit = new Title(titID, title, "Employeed", type);
            Log tempLog = new Log(logID);
            Employee tempEmp = new Employee(tempInfo, tempAdd, tempTit, tempLog);
            EmployeeList.Add(tempEmp);

            //Add employee to database
            con.ConnectionString = "Server=192.169.197.238,1433;Database=ByrumM0141;User id=pater0296;Password=1Password!";
            con.Open();

            string newEmpString = @"INSERT INTO Employee_Information(Employee_ID, Address_ID, Title_ID, Employee_Firstname, Employee_Lastname, 
                                    Employee_Hiredate, Employee_Phone, Employee_Birthdate, Employee_Email, Employee_Sex) VALUES('" +
                                    id + "', '" + adID + "', '" + titID + "', '" + firs + "', '" + last + "', '" + hire + "', '" + phon + "', '" +
                                    bday + "', '" + emai + "', '" + gend + "');";
            string newAddString = @"INSERT INTO Employee_Address(Address_ID, Employee_Address, Employee_City, Employee_State, Employee_Country, Employee_Postalcode)
                                    VALUES('" + adID + "', '" + add1 + "', '" + city + "', '" + state + "', '" + country + "', '" + zip + "');";
            string newTitString = @"INSERT INTO Employee_Title(Title_ID, Employee_Title, Employee_Status, Employee_Type)
                                    VALUES('" + titID + "', '" + title + "', '" + "Employeed" + "', '" + type + "');";
            string newLogString = @"INSERT INTO Employee_Log(Log_ID, Log_In, Log_Out, Log_Date) VALUES('" + logID + "', '" + null + "', '" + null + "', '" + null + "');";

            //Attempting SQL Commands against the server.
            //Each command inside its own try/catch to
            //give specific error messages based on which
            //step it gets to before a problem occurs.
            try
            {
                SqlCommand newLog = new SqlCommand(newLogString, con);
                newLog.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Problem with Login ID. Is this ID already taken?");
            }
            try
            {
                SqlCommand newTit = new SqlCommand(newTitString, con);
                newTit.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Problem with Title. Is the Title ID already taken?");
            }
            try
            {
                SqlCommand newAdd = new SqlCommand(newAddString, con);
                newAdd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Problem with Address. Is the Address ID already taken?");
            }
            try
            {
                SqlCommand newEmp = new SqlCommand(newEmpString, con);
                newEmp.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Problem with Employee Information. Is the ID already taken?");
            }

            //Add the new employee to the current box
            curEmpListBox.Items.Add("ID: " + id + " | Name: " + last + ", " + firs);
            newEmpLabel.Text = "Employee Added Successfully";
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message.ToString() + "');</script>");
        }
        finally
        {
            con.Close();
        }
    }
    
    protected void btnDelEmp_Click(object sender, EventArgs e)
    {
        try
        {
            newEmpLabel.Text = EmployeeList.Count.ToString();
            con.ConnectionString = "Server=192.169.197.238,1433;Database=ByrumM0141;User id=pater0296;Password=1Password!";
            con.Open();
            string delEmp = @"DELETE FROM Employee_Information WHERE Employee_ID='" + EmployeeList.ElementAt(curEmpListBox.SelectedIndex).getInfo().getEmpID() + @"';";
            SqlCommand delEmpList = new SqlCommand(delEmp, con);
            delEmpList.ExecuteNonQuery();
            string delAdd = @"DELETE FROM Employee_Address WHERE Address_ID='" + EmployeeList.ElementAt(curEmpListBox.SelectedIndex).getAddress().getAddID() + @"';";
            SqlCommand delAddList = new SqlCommand(delAdd, con);
            delAddList.ExecuteNonQuery();
            string delTit = @"UPDATE Employee_Title SET Employee_Status = 'Terminated' WHERE Title_ID = '" + EmployeeList.ElementAt(curEmpListBox.SelectedIndex).getTitle().getTitleID() + @"';";
            SqlCommand delTitList = new SqlCommand(delTit, con);
            delTitList.ExecuteNonQuery();
            EmployeeList.RemoveAt(curEmpListBox.SelectedIndex);
            AddressList.RemoveAt(curEmpListBox.SelectedIndex);
            TitleList.RemoveAt(curEmpListBox.SelectedIndex);
            curEmpListBox.Items.RemoveAt(curEmpListBox.SelectedIndex);
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "');</script>");
        }
        finally
        {
            con.Close();
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            con.ConnectionString = "Server=192.169.197.238,1433;Database=ByrumM0141;User id=pater0296;Password=1Password!";
            con.Open();

            /************* DELETE TEST DATA CODE *****************
            string delEmp = @"DELETE FROM Employee_Information WHERE Employee_ID='1000016';";
            SqlCommand delEmpList = new SqlCommand(delEmp, con);
            delEmpList.ExecuteNonQuery();
            string delAdd = @"DELETE FROM Employee_Address WHERE Address_ID='2000016';";
            SqlCommand delAddList = new SqlCommand(delAdd, con);
            delAddList.ExecuteNonQuery();
            string delTit = @"DELETE FROM Employee_Title WHERE Title_ID = '3000016';";
            SqlCommand delTitList = new SqlCommand(delTit, con);
            delTitList.ExecuteNonQuery();
            string delLog = @"DELETE FROM Employee_Log WHERE Log_ID = '4000016';";
            SqlCommand delLogList = new SqlCommand(delLog, con);
            delLogList.ExecuteNonQuery();
            /************* DELETE TEST DATA CODE *****************/

            //Get Employee_Info
            try
            {
                InformationList.Clear();
                if (InformationList.Count < 1)
                {
                    string selInfoString = @"SELECT Employee_ID, Employee_Firstname, Employee_Lastname, Employee_Hiredate, Employee_Birthdate, 
                                    Employee_Sex, Employee_Phone, Employee_Email FROM Employee_Information;";
                    SqlCommand fillCurInfoList = new SqlCommand(selInfoString, con);
                    SqlDataReader infoReader = fillCurInfoList.ExecuteReader();
                    while (infoReader.Read())
                    {
                        Information temp = new Information(infoReader.GetInt32(0), infoReader.GetValue(1).ToString(), infoReader.GetValue(2).ToString(),
                                                            infoReader.GetValue(3).ToString(), infoReader.GetValue(4).ToString(), infoReader.GetValue(5).ToString(),
                                                            infoReader.GetValue(6).ToString(), infoReader.GetValue(7).ToString());
                        InformationList.Add(temp);
                    }
                    infoReader.Close();
                }
            }
            catch (Exception ex)
            {

            }

            //Get Employee_Address
            try
            {
                AddressList.Clear();
                if (AddressList.Count < 1)
                {
                    string selAddString = @"SELECT Address_ID, Employee_Address, Employee_City, Employee_State, Employee_Country, Employee_Postalcode
                                    FROM Employee_Address;";
                    SqlCommand fillCurAddList = new SqlCommand(selAddString, con);
                    SqlDataReader addReader = fillCurAddList.ExecuteReader();
                    while (addReader.Read())
                    {
                        Address temp = new Address(addReader.GetInt32(0), addReader.GetValue(1).ToString(), addReader.GetValue(2).ToString(),
                                                   addReader.GetValue(3).ToString(), addReader.GetValue(4).ToString(), addReader.GetValue(5).ToString());
                        AddressList.Add(temp);
                    }
                    addReader.Close();
                }
            }
            catch (Exception ex)
            {

            }

            //Get Employee_Title
            try
            {
                TitleList.Clear();
                if (TitleList.Count < 1)
                {
                    string selTitString = @"SELECT Title_ID, Employee_Title, Employee_Status, Employee_Type FROM Employee_Title;";
                    SqlCommand fillCurTitList = new SqlCommand(selTitString, con);
                    SqlDataReader titReader = fillCurTitList.ExecuteReader();
                    while (titReader.Read())
                    {
                        Title temp = new Title(titReader.GetInt32(0), titReader.GetValue(1).ToString(), titReader.GetValue(2).ToString(), titReader.GetValue(3).ToString());
                        TitleList.Add(temp);
                    }
                    titReader.Close();
                }
            }
            catch (Exception ex) { }

            //Get Employee_Log
            try
            {
                LogList.Clear();
                if (LogList.Count < 1)
                {
                    string selLogString = @"SELECT Log_ID, Log_In, Log_Date FROM Employee_Log;";
                    SqlCommand fillCurLogList = new SqlCommand(selLogString, con);
                    SqlDataReader logReader = fillCurLogList.ExecuteReader();
                    while (logReader.Read())
                    {
                        Log temp = new Log(logReader.GetInt32(0), logReader.GetValue(1).ToString(), logReader.GetValue(2).ToString());
                        LogList.Add(temp);
                    }
                    logReader.Close();
                }
            }
            catch (Exception ex) { }

            //Build Employee List
            EmployeeList.Clear();
            if (EmployeeList.Count < 1)
            {
                for (int x = 0; x < InformationList.Count; x++)
                {
                    Employee temp = new Employee(InformationList[x], AddressList[x], TitleList[x], LogList[x]);
                    EmployeeList.Add(temp);
                }
            }

            //Clear the list box and then fill with current information
            curEmpListBox.Items.Clear();
            for (int x = 0; x < InformationList.Count(); x++)
            {
                curEmpListBox.Items.Add(InformationList[x].getNameInfo());
            }
        }
        catch(Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "');</script>");
        }
        finally
        {
            con.Close();
        }
    }

    protected void btnLogTime_Click(object sender, EventArgs e)
    {
        try
        {
            int entered_id = int.Parse(timePunchTextBox.Text);
            int stored_id = EmployeeList.ElementAt(curEmpListBox.SelectedIndex).getLog().getLogID();
            if (entered_id == stored_id)
            {
                con.ConnectionString = "Server=192.169.197.238,1433;Database=ByrumM0141;User id=pater0296;Password=1Password!";
                con.Open();
                string newTimeString = @"INSERT INTO Employee_Log(Log_ID, Log_In, Log_Out, Log_Date)
                                         VALUES('" + stored_id + "', '" + System.DateTime.Now.ToShortTimeString() + "', '" +
                                         System.DateTime.Now.ToShortTimeString() + "', '" + System.DateTime.Today.ToShortDateString() + "');";
                SqlCommand newTime = new SqlCommand(newTimeString, con);
                newTime.ExecuteNonQuery();
            }
            else
            {
                throw new Exception("Invalid Login ID for the selected employee.");
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "');</script>");
        }
        finally
        {
            con.Close();
        }
    }

    protected void generateTimesButton_Click(object sender, EventArgs e)
    {
        Session["StartDate"] = tbReportStartDate.Text;
        Session["EndDate"] = tbReportEndDate.Text;
        Response.Redirect("TimeReportPage.aspx");
    }
}