<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.ascx.cs" Inherits="Controls_ChangePassword" %>
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
                <asp:ValidationSummary ID="vsChangePassword" runat="server" HeaderText="Following fields missing or invalid" />
                
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
                                        <asp:Label ID="Label8" runat="server" Text="Change Password" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td style="width: 10px" bgcolor="#b0e0e6">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:5px">
                                    
                                    </td>
                                    <td style="text-align:left" valign="top">
                                        <asp:Label ID="Label1" runat="server" Text="Old Password"></asp:Label>
                                    </td>
                                    <td style="width:5px">
                                    
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_OldPassword" runat="server" Width="230px" MaxLength="200" TextMode="Password"></asp:TextBox><span style="color:Red; vertical-align:top">*</span>
                                    </td>
                                    <td style="width:10px">
                                        <asp:RequiredFieldValidator ID="rfvOldPassword" runat="server" ControlToValidate="txt_OldPassword"
                                            Display="None" ErrorMessage="Old Password required." SetFocusOnError="True"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                ID="revOldPassword" runat="server" ControlToValidate="txt_OldPassword" Display="None"
                                                ErrorMessage="character  '   is not allowed for Old Password" SetFocusOnError="True"
                                                ValidationExpression="([^']{1,200})"></asp:RegularExpressionValidator></td>
                                </tr>
                                <tr>
                                    <td style="width:5px">
                                    
                                    </td>
                                    <td style="text-align:left" valign="top">
                                        <asp:Label ID="Label2" runat="server" Text="New Password"></asp:Label>
                                    </td>
                                    <td style="width:5px">
                                    
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_NewPassword" runat="server" TextMode="Password" Width="230px" MaxLength="200"></asp:TextBox><span style="color:Red; vertical-align:top">*</span>
                                    </td>
                                    <td style="width:10px">
                                        <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ControlToValidate="txt_NewPassword"
                                            Display="None" ErrorMessage="New Password required." SetFocusOnError="True"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                ID="revNewPassword" runat="server" ControlToValidate="txt_NewPassword" Display="None"
                                                ErrorMessage="character  '   is not allowed for New Password" SetFocusOnError="True"
                                                ValidationExpression="([^']{1,200})"></asp:RegularExpressionValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align:left" valign="top">
                                        <asp:Label ID="Label3" runat="server" Text="Confirm Password"></asp:Label></td>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align:left;" valign="top">
                                        <asp:TextBox ID="txt_ConfirmPassword" runat="server" Width="230px" TextMode="Password"></asp:TextBox><span style="color:Red; vertical-align:top">*</span></td>
                                    <td style="width: 10px">
                                        <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ControlToValidate="txt_ConfirmPassword"
                                            Display="None" ErrorMessage="Confirm Password required." SetFocusOnError="True"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                ID="revConfirmPassword" runat="server" ControlToValidate="txt_ConfirmPassword"
                                                Display="None" ErrorMessage="character  '   is not allowed for Confirm Password"
                                                SetFocusOnError="True" ValidationExpression="([^']{1,200})"></asp:RegularExpressionValidator></td>
                                </tr>
                                <tr>
                                    <td style="width:5px">
                                    
                                    </td>
                                    <td>
                                        
                                    </td>
                                    <td style="width:5px">
                                    
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_ChangePassword" runat="server" Text="Change Password" OnClick="btn_ChangePassword_Click" />
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

