<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ResultView.ascx.cs" Inherits="Controls_ResultView" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
    rel="stylesheet" type="text/css" />
<center>
    <table border="1" bgcolor="aliceblue" cellspacing="0" cellpadding="0" style="margin:none; border:1px solid #296488">
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
                <table id="tblResultView" border="1" cellspacing="0" bgcolor="#e0ffff" cellpadding="0" style="margin:none;border:1px solid green" runat="server">
                    <tr>
                        <td bgcolor="#b0e0e6" style="border:none; width:5px">
                        </td>
                        <td align="left" bgcolor="#b0e0e6" style="border:none;" valign="top">
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Result View For Exam:"></asp:Label></td>
                        <td bgcolor="#b0e0e6" style="border:none; width:10px">
                        </td>
                    </tr>
                    <tr>
                         <td bgcolor="#b0e0e6" style="border:none; width:5px">
                        </td>
                        <td align="left" bgcolor="#b0e0e6" style="border:none;" valign="top">
                            <asp:Label ID="lbl_ExamName" runat="server" Font-Bold="True" ForeColor="Desktop"></asp:Label>
                        </td>
                        <td bgcolor="#b0e0e6" style="border:none; width:10px">
                        </td>
                    </tr>
                    <tr>
                        <td style="border:none; width:5px">
                        
                        </td>
                        <td style="border:none; text-align:left">
                            <asp:Panel ID="pnl_Results" runat="server" ScrollBars="Auto" Width="1000px" Height="400px">
                                <asp:GridView ID="grid_Results" runat="server" CellPadding="4" ForeColor="#333333" GridLines="Vertical" AllowPaging="True" OnRowDataBound="grid_Results_RowDataBound" OnPageIndexChanging="grid_Results_PageIndexChanging" AutoGenerateColumns="true">
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                        <td style="border:none; width:10px">
                        
                        </td>
                    </tr>
                    <%--<tr>
                        <td style="border:none; width:5px">
                        
                        </td>
                        <td style="border:none; text-align:left">
                            <CR:CrystalReportViewer ID="crRVCandidateResults" runat="server" AutoDataBind="true" Width="350px" />
                        </td>
                        <td style="border:none; width:10px">
                        
                        </td>
                    </tr>--%>
                </table>
            </td>
            <td style="width:20px; border:none">
            </td>
        </tr>
        <tr>
            <td style="width: 20px; border:none; height: 20px">
            </td>
            <td style="border:none; height: 20px">
            </td>
            <td style="border: none;
                width: 20px; height: 20px">
            </td>
        </tr>
    </table>
</center>