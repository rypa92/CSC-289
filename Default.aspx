<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BLMP.IT Schedule Applet</title>
</head>
<body>
    <h1>Schedule Applet</h1>
    <!--Top box-->
    <div style="width:100%">
        <form runat="server">
        <!--Top Box-->
        <div style="background-color:gray;">
            <h4>Current Employees --</h4>
            <table style="width:100%">
                <tr>
                    <td style="width:50%" colspan="2"><asp:ListBox ID="curEmpListBox" runat="server" style="width:100%" rows="1"/></td>
                    <td style="width:10%">Log ID: </td>
                    <td style="width:auto"><asp:TextBox ID="timePunchTextBox" runat="server" Width="100%" TextMode="Password" /></td>
                </tr>
                <tr>
                    <td style="width:25%"><asp:Button ID="btnRefresh" runat="server" Text="Refresh List" OnClick="btnRefresh_Click" Width="100%" /></td>
                    <td style="width:25%"><asp:Button ID="btnDelEmp" runat="server" Text="Remove Employee" OnClientClick="return confirm('Delete the selected employee?');" OnClick="btnDelEmp_Click" Width="100%" /></td>
                    <td style="width:50%" colspan="2"><asp:Button ID="btnLogTime" runat="server" Text="Clock In/Out" OnClientClick="return logIDFunction();" Width="100%" OnClick="btnLogTime_Click" /></td>
                </tr>
            </table>
        </div>
            <!--Add New Emplyee Box-->
            <div style="background-color:gray;">
                <h4>New Employee Information --</h4>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 33%">Name: </td>
                        <td>First: <asp:TextBox ID="firsTextBox" runat="server" Width="75%"></asp:TextBox></td>
                        <td>Last: <asp:TextBox ID="lastTextBox" runat="server" Width="75%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><!--Empty cell--></td>
                        <td><asp:RequiredFieldValidator ID="firstnameValidator" runat="server" ErrorMessage="First Name is required." ValidationGroup="NewEmp" ControlToValidate="firsTextBox"></asp:RequiredFieldValidator></td>
                        <td><asp:RequiredFieldValidator ID="lastnameValidator" runat="server" ErrorMessage="Last Name is required." ValidationGroup="NewEmp" ControlToValidate="lastTextBox"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>Employee ID: </td>
                        <td colspan="2"><asp:TextBox ID="idTextBox" runat="server" Width="100%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><asp:RangeValidator ID="idValidator" runat="server" ErrorMessage="ID is required." MaximumValue="99999999" MinimumValue="0" ValidationGroup="NewEmp" ControlToValidate="idTextBox"></asp:RangeValidator></td>
                    </tr>
                    
                    <tr>
                        <td>Pay Rate: </td>
                        <td colspan="2"><asp:TextBox ID="payrateTextbox" runat="server" Width="100%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><asp:RequiredFieldValidator ID="payrateValidator" runat="server" ErrorMessage="Pay Rate is required." ValidationGroup="NewEmp" ControlToValidate="payrateTextBox"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>Phone Number: </td>
                        <td colspan="2"><asp:TextBox ID="phonTextBox" runat="server" Width="100%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:RegularExpressionValidator ID="phonValidator" runat="server" ErrorMessage="Phone format. ###-###-####" ValidationExpression="^[2-9]\d{2}-\d{3}-\d{4}$" ValidationGroup="NewEmp" ControlToValidate="phonTextBox"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Email: </td>
                        <td colspan="2"><asp:TextBox ID="emaiTextBox" runat="server" Width="100%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:RegularExpressionValidator ID="emaiValidator" runat="server" ErrorMessage="Email format: name@server.com" ControlToValidate="emaiTextBox" ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" ValidationGroup="NewEmp"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Birthday: </td>
                        <td colspan="2"><asp:TextBox ID="bdayTextBox" runat="server" Width="100%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:RegularExpressionValidator ID="birthdayValidator" runat="server" ErrorMessage="Birthday format: DD/MM/YYYY" ControlToValidate="bdayTextBox" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$" ValidationGroup="NewEmp"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Hire Date: </td>
                        <td colspan="2"><asp:TextBox ID="hireTextBox" runat="server" Width="100%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Gender: </td>
                        <td colspan="2"><asp:TextBox ID="gendTextBox" runat="server" Width="100%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><asp:RequiredFieldValidator ID="genderValidator" runat="server" ErrorMessage="Gender is required." ValidationGroup="NewEmp" ControlToValidate="gendTextBox"></asp:RequiredFieldValidator></td>
                    </tr>
                </table>
                <h4>New Employee Address --</h4>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 33%">Address ID: </td>
                        <td><asp:TextBox ID="adIDTextBox" runat="server" Width="100%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><asp:RequiredFieldValidator ID="AddressIDValidator" runat="server" ErrorMessage="Address ID is required." ValidationGroup="NewEmp" ControlToValidate="adIDTextBox"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>Address (Line 1): </td>
                        <td><asp:TextBox ID="add1TextBox" runat="server" Width="100%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><asp:RequiredFieldValidator ID="add1Validator" runat="server" ErrorMessage="Address Line 1 is required." ValidationGroup="NewEmp" ControlToValidate="add1TextBox"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>City: </td>
                        <td><asp:TextBox ID="cityTextBox" runat="server" Width="100%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><asp:RequiredFieldValidator ID="cityValidator" runat="server" ErrorMessage="City is required." ValidationGroup="NewEmp" ControlToValidate="cityTextBox"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>State: </td>
                        <td><asp:RadioButtonList ID="rbState" runat="server">
                                <asp:ListItem>North Carolina</asp:ListItem>
                                <asp:ListItem>Other</asp:ListItem>
                        </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RequiredFieldValidator ID="stateValidator" runat="server" ErrorMessage="State is required." ValidationGroup="NewEmp" ControlToValidate="rbState"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Zip Code: </td>
                        <td><asp:TextBox ID="zipTextBox" runat="server" Width="100%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:RegularExpressionValidator ID="zipValidator" runat="server" ErrorMessage="Zipcode format. #####(-####)" ValidationExpression="^[0-9]{5}(?:-[0-9]{4})?$" ValidationGroup="NewEmp" ControlToValidate="zipTextBox"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Country: </td>
                        <td><asp:RadioButtonList ID="rbCountry" runat="server">
                                <asp:ListItem>United States</asp:ListItem>
                                <asp:ListItem>Other</asp:ListItem>
                        </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RequiredFieldValidator ID="countryValidator" runat="server" ErrorMessage="Country is required." ValidationGroup="NewEmp" ControlToValidate="rbCountry"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                <h4>New Employee Title --</h4>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 33%">Title ID:</td>
                        <td><asp:TextBox ID="titIDTextBox" runat="server" Width="100%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><asp:RequiredFieldValidator ID="titIDValidator" runat="server" ErrorMessage="Title ID is required." ValidationGroup="NewEmp" ControlToValidate="titIDTextBox"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>Title: </td>
                        <td><asp:TextBox ID="titTitleTextBox" runat="server" Width="100%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><asp:RequiredFieldValidator ID="titTitleValidator" runat="server" ErrorMessage="Title is required." ValidationGroup="NewEmp" ControlToValidate="titTitleTextBox"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>Employment Type: </td>
                        <td><asp:RadioButtonList ID="rbType" runat="server">
                                    <asp:ListItem>Part Time</asp:ListItem>
                                    <asp:ListItem>Full Time</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td><asp:RequiredFieldValidator ID="statusTypeValidator" runat="server" ErrorMessage="Status is required." ValidationGroup="NewEmp" ControlToValidate="rbType"></asp:RequiredFieldValidator></td>
                    </tr>
                </table>
                <h4>New Employee Login --</h4>
                <table style="width:100%">
                    <tr>
                        <td style="width:33%">Login ID:</td>
                        <td><asp:TextBox ID="logIDTB" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><asp:RequiredFieldValidator ID="logIDValidator" runat="server" ErrorMessage="Login ID is required." ValidationGroup="NewEmp" ControlToValidate="logIDTB"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td><asp:Button ID="newEmpButton" runat="server" Text="Add New Employee" OnClick="newEmpButton_Click" Width="100%" ValidationGroup="NewEmp" /></td>
                        <td><asp:Label ID="newEmpLabel" runat="server" Width="100%"></asp:Label></td>
                    </tr>
                </table>
                <h4>Generate Time Punch Report --</h4>
                <table style="width:100%">
                    <tr>
                        <td style="width:50%">Start Date: </td>
                        <td style="width:50%"><asp:TextBox ID="tbReportStartDate" runat="server" Width="100%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width:50%">End Date: </td>
                        <td style="width:50%"><asp:TextBox ID="tbReportEndDate" runat="server" Width="100%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width:100%" colspan="2"><asp:Button ID="generateTimesButton" runat="server" Text="Generate Log Report" Width="100%" OnClick="generateTimesButton_Click" /></td>
                    </tr>
                </table>
            </div>
        </form>
    </div>
</body>
</html>
