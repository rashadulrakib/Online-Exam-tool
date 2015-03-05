<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SystemUserMainPage.ascx.cs" Inherits="Controls_SystemUserMainPage" %>
<center>
    <table style="border:1px solid #296488;margin:none" border="1" bgcolor="aliceblue" cellspacing="0" cellpadding="0">
        <tr>
            <td style="width:20px; height:20px; border:none">
            
            </td>
            <td style="height:20px; border:none; text-align:left">
                <asp:Label ID="lbl_error" runat="server"></asp:Label>
            </td>
            <td style="width:20px; height:20px; border:none">
            
            </td>
        </tr>
        <tr>
            <td style="width:20px; border:none">
            
            </td>
            <td style=" border:none">
                <table border="1" cellspacing="0" cellpadding="0" style="margin:none;border:1px solid green" bgcolor="#e0ffff" align="center">
                    <tr>
                        <td style="width:5px; border:none" bgcolor="#b0e0e6">
                        </td>
                        <td style="text-align:left; border:none" bgcolor="#b0e0e6" valign="top" colspan="3">
                            <asp:Label ID="Label8" runat="server" Text="System User Home Page" Font-Bold="True"></asp:Label>
                        </td>
                        <td style="width:10px;border:none" bgcolor="#b0e0e6">
                        </td>
                    </tr>
                    <tr>
                        <td style="border:none; width:5px">
                            
                        </td>
                        <td style="border:none;" align="left" valign="top">
                            <asp:Label ID="Label1" runat="server" Text="Select Exam"></asp:Label>
                        </td>
                        <td style="border:none;width:5px">
                            
                        </td>
                        <td style="border:none" align="left" valign="top">
                            <asp:DropDownList ID="dr_SelectExam" runat="server" Width="350px">
                            </asp:DropDownList>
                        </td>
                        <td style="border:none;width:10px">
                            
                        </td>
                    </tr>
                    <tr>
                        <td style="border:none; width:5px">
                            
                        </td>
                        <td style="border:none;">
                          
                        </td>
                        <td style="border:none;width:5px">
                            
                        </td>
                        <td style="border:none" align="left" valign="top">
                            <asp:Button ID="btn_Go" runat="server" Text="Go" Width="100px" OnClick="btn_Go_Click" />
                        </td>
                        <td style="border:none;width:10px">
                            
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
