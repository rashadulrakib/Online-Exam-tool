<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SystemUserEntry.ascx.cs" Inherits="Controls_SystemUserEntry" %>
<script type="text/javascript" language="javascript">
    function ClearErrorLebel(lblErrorID)
    {
        var olblErrorID = document.getElementById(lblErrorID);
        olblErrorID.innerHTML="";     
    }
</script>
<center>
    <table border="1" bgcolor="aliceblue" cellspacing="0" cellpadding="0" style="margin:none; border:1px solid #296488">
        <tr>
            <td style="width:20px; border:none">
            
            </td>
            <td style="border:none; text-align:left">
                <asp:ValidationSummary ID="vsSystemuserEntry" runat="server" HeaderText="Following fields missing or invalid" />
                
            </td>
            <td style="width:20px; border:none">
            
            </td>
        </tr>
        <tr>
            <td style="width:20px; height:20px; border:none">
            
            </td>
            <td style="height:20px; border:none; text-align:left">
                <asp:Label id="lbl_error" runat="server"></asp:Label>
            </td>
            <td style="width:20px; height:20px; border:none">
            
            </td>
        </tr>
        <tr>
            <td style="width:20px; border:none">
            
            </td>
            <td style=" border:none">
                <table border="1" cellspacing="0" cellpadding="0" bgcolor="#e0ffff" style="margin:0;border:1px solid green">
                    <tr>
                        <td style="border:none">
                            <table border="0" cellspacing="0" cellpadding="0" style="margin:0">
                                <tr>
                                    <td style="width: 5px" bgcolor="#b0e0e6">
                                    </td>
                                    <td style="text-align: left" bgcolor="#b0e0e6" valign="top" colspan="3">
                                        <asp:Label ID="Label8" runat="server" Text="System User Entry" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td style="width: 10px" bgcolor="#b0e0e6">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:5px">
                                    
                                    </td>
                                    <td style="text-align:left" valign="top">
                                        <asp:Label ID="Label1" runat="server" Text="User Name"></asp:Label>
                                    </td>
                                    <td style="width:5px">
                                    
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_UserName" runat="server" Width="250px" MaxLength="200"></asp:TextBox><span style="color:Red; vertical-align:top">*</span>
                                    </td>
                                    <td style="width:10px">
                                        <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txt_UserName"
                                            Display="None" ErrorMessage="User name required." SetFocusOnError="True"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                ID="revUserName" runat="server" ControlToValidate="txt_UserName" Display="None"
                                                ErrorMessage="character  '  and  -   is not allowed for user name" SetFocusOnError="True"
                                                ValidationExpression="([^'-]{1,200})"></asp:RegularExpressionValidator></td>
                                </tr>
                                <tr>
                                    <td style="width:5px">
                                    
                                    </td>
                                    <td style="text-align:left" valign="top">
                                        <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
                                    </td>
                                    <td style="width:5px">
                                    
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_Password" runat="server" TextMode="Password" Width="250px" MaxLength="200"></asp:TextBox><span style="color:Red; vertical-align:top">*</span>
                                    </td>
                                    <td style="width:10px">
                                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txt_Password"
                                            Display="None" ErrorMessage="Password required." SetFocusOnError="True"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                ID="revPassword" runat="server" ControlToValidate="txt_Password" Display="None"
                                                ErrorMessage="character  '  is not allowed for password" SetFocusOnError="True"
                                                ValidationExpression="([^']{1,200})"></asp:RegularExpressionValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align:left" valign="top">
                                        <asp:Label ID="Label3" runat="server" Text="Email"></asp:Label></td>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align:left;" valign="top">
                                        <asp:TextBox ID="txt_Email" runat="server" Width="250px"></asp:TextBox><span style="color:Red; vertical-align:top">*</span></td>
                                    <td style="width: 10px">
                                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txt_Email"
                                            Display="None" ErrorMessage="Email Address required." SetFocusOnError="True"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                ID="revEmail" runat="server" ControlToValidate="txt_Email" Display="None" ErrorMessage="Invalid Email Address"
                                                SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                                </tr>
                                <tr>
                                    <td style="width:5px">
                                    
                                    </td>
                                    <td>
                                        
                                    </td>
                                    <td style="width:5px">
                                    
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_CreateUser" runat="server" Text="User Entry" OnClick="btn_CreateUser_Click" />
                                    </td>
                                    <td style="width:10px">
                                    
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width:20px; border:none">
            </td>
        </tr>
        <tr>
            <td style="width:20px; height:20px; border:none">
            
            </td>
            <td style="height:20px; border:none">
                
            </td>
            <td style="width:20px; height:20px; border:none">
            </td>
        </tr>
    </table>
</center>

