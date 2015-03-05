<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryEntry.ascx.cs" Inherits="Controls_CategoryEntry" %>
<script type="text/javascript" language="javascript">
    function ClearErrorLebel(lblErrorID)
    {
        var olblErrorID = document.getElementById(lblErrorID);
        olblErrorID.innerHTML="";     
    }
</script>
<center>
    <table id="tblMain" border="1" style="margin:none;border:1px solid #296488" bgcolor="aliceblue" cellspacing="0" cellpadding="0">
        <tr>
            <td style="width:20px; border:none">
            
            </td>
            <td style="border:none; text-align:left" >
                <asp:ValidationSummary ID="vsCategoryEntry" runat="server" HeaderText="Following fields missing or invalid" />
                
            </td>
            <td style="width:20px; border:none">
            
            </td>
        </tr>
        <tr>
            <td style="width:20px; height:20px; border:none">
            
            </td>
            <td style="height:20px; border:none; text-align:left" >
                <asp:Label ID="lbl_error" runat="server" ></asp:Label>   
            </td>
            <td style="width:20px; height:20px; border:none">
            
            </td>
        </tr>
        <tr>
            <td style="width:20px; border:none">
            
            </td>
            <td style=" border:none">
                <table id="tblSCategory" border="1" cellspacing="0" cellpadding="0" style="margin:none;border:1px solid green" bgcolor="#e0ffff">
                    <tr>
                        <td id="tdSCategory" valign="top" style=" border:none">
                            <table border="0" cellpadding="0" cellspacing="0" style="margin:0">
                                <tr>
                                    <td bgcolor="#b0e0e6" colspan="1" rowspan="1" style="width:5px">
                                    </td>
                                    <td colspan="3" valign="top" style="text-align: left;" bgcolor="#b0e0e6" rowspan="">
                                        <asp:Label ID="Label2" runat="server" Text="Save Category" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td bgcolor="#b0e0e6" colspan="1" rowspan="1" style="width:10px;">
                                    </td>
                                </tr>
                                <tr>
                                    
                                    <td style="width:5px;">
                                    </td>
                                    <td valign="top" style="text-align: left;">
                                        <asp:Label ID="Label1" runat="server" Text="Category Name" ></asp:Label>
                                    </td>
                                    <td style="width:5px">
                                    </td>
                                    <td valign="top" style="text-align: left;">
                                        <asp:TextBox ID="txt_CategoryName" runat="server" MaxLength="50" Width="250px"></asp:TextBox><span style="color: #ff0000">*</span></td>
                                    <td style="text-align: left;width:10px;">
                                        <asp:RequiredFieldValidator ID="rfvCategoryName" runat="server" ControlToValidate="txt_CategoryName"
                                            Display="None" ErrorMessage="Category Name required." SetFocusOnError="True"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                ID="revCategoryName" runat="server" ControlToValidate="txt_CategoryName" Display="None"
                                                ErrorMessage="character  '  and  -   is not allowed for Category name" SetFocusOnError="True"
                                                ValidationExpression="([^'-]{1,50})"></asp:RegularExpressionValidator></td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; width:5px;">
                                    </td>
                                    <td style="text-align: left;">
                                    </td>
                                    <td style="text-align: left;width:5px; ">
                                    </td>
                                    <td valign="top" style="text-align: left; ">
                                        <asp:Button ID="btn_Save" runat="server" Text="Save" OnClick="btn_Save_Click" /></td>
                                    <td style="text-align: left; width:10px; ">
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
            <td style="height:20px; border:none" align=center>
                
            </td>
            <td style="width:20px; height:20px; border:none">
            
            </td>
        </tr>
    </table>
</center>