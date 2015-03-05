<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExamineProcess.ascx.cs" Inherits="Controls_ExamineProcess" %>
<table cellpadding="0" cellspacing="0" border="0" style="margin:none;">
    <tr>
        <td style="border:none; height:20px; text-align:left" valign="top" >
            <asp:Label ID="lbl_error" runat="server" ></asp:Label>
        </td>
    </tr>
</table>
<table cellpadding="0" cellspacing="0" border="0" style="margin:none; width:100%;">
    <tr align="left" valign="top">
        <td style="border:none" colspan="2">
            <table cellpadding="0" cellspacing="0" border="0" style="margin:none;">
                <tr align="left">
                    <td style="border:none;" align="left" valign="top"> 
                        <asp:Label ID="Label4" runat="server" Text="Candidate Name: " Font-Bold="True" Font-Italic="True" Font-Size="Medium" ForeColor="Teal"></asp:Label><asp:Label ID="lbl_CandidateName" runat="server" Font-Italic="True" Font-Size="Medium" ForeColor="Green"></asp:Label>
                    </td>
                </tr>
                <tr align="left">
                    <td style="border:none;" align="left" valign="top">
                        <asp:Label ID="Label3" runat="server" Text="Exam Name: " Font-Bold="True" Font-Italic="True" Font-Size="Medium" ForeColor="Peru"></asp:Label><asp:Label ID="lbl_ExamName" runat="server" Font-Italic="True" Font-Size="Medium" ForeColor="Desktop"></asp:Label>
                        <asp:Label ID="Label7" runat="server" Text="Exam Constraint: " Font-Bold="True" Font-Italic="True" Font-Size="Medium" ForeColor="Peru"></asp:Label><asp:Label ID="lbl_ExamConstraint" runat="server" Font-Italic="True" Font-Size="Medium" ForeColor="Desktop"></asp:Label>
                    </td>
                </tr>
                <tr align="left">
                    <td style="border:none;" align="left" valign="top">
                        <asp:Label ID="Label5" runat="server" Text="Exam Total: " Font-Bold="True" Font-Italic="True" Font-Size="Medium" ForeColor="Peru"></asp:Label><asp:Label ID="lbl_ExamTotal" runat="server" Font-Italic="True" Font-Size="Medium" ForeColor="Desktop"></asp:Label>
                        <asp:Label ID="Label6" runat="server" Text="Exam StartTime: " Font-Bold="True" Font-Italic="True" Font-Size="Medium" ForeColor="Peru"></asp:Label><asp:Label ID="lbl_ExamStartTime" runat="server" Font-Italic="True" Font-Size="Medium" ForeColor="Desktop"></asp:Label>
                    </td>
                </tr>
            </table>        
        </td>
    </tr>
    <tr>
        <td style="border:none; height:10px" colspan="2">
        </td>
    </tr>
    <tr style="height:450px">
        <td style="border:none; width:10px"align="left" valign="top">
        </td>
        <td style="border:none" align="left" valign="top">
            <asp:Panel ID="pnl_Answers" runat="server">
                <asp:GridView ID="Grid_Answers" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="None" AllowPaging="true" PageSize="1" Width="100%" OnRowDataBound="Grid_Answers_RowDataBound" OnPageIndexChanging="Grid_Answers_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Questions">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <table style="margin:none; width:100%" cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td style="border:none" colspan="3">
                                            <asp:Label ID="lblQuestion" runat="server" ></asp:Label>                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border:none; height:20px" colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border:none" colspan="3" align="left" valign="top">
                                            <asp:Panel ID="pnl_DescriptiveOrObjective" runat="server">
                                                <table style="margin:none;" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td style="border:none;" align="left" valign="top">
                                                            <asp:Label ID="Label1" runat="server" Text="Obtain Mark: "></asp:Label>
                                                        </td>
                                                        <td style="border:none" align="left" valign="top">
                                                           <asp:TextBox ID="txtObtainMark" runat="server" TextMode="SingleLine" Width="50px" EnableViewState="true"></asp:TextBox>                                                       
                                                        </td>
                                                        <td style="border:none; width:5px">
                                                        
                                                        </td>
                                                        <td style="border:none;" align="left" valign="top">
                                                            <asp:Label ID="Label2" runat="server" Text="Set Up Mark: "></asp:Label>
                                                        </td>
                                                        <td style="border:none" align="left" valign="top">
                                                           <asp:Label ID="lblSetupMark" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table style="margin:none;" cellpadding="0" cellspacing="0" border="0">
                                                    <tr align="left" valign="top">
                                                        <td style="border:none" align="left" valign="top">
                                                            <asp:Label ID="lbl_AnswerAttachFile" runat="server" Text="Attached Answer File"> </asp:Label> <asp:HyperLink ID="hyperAnswerFilePath" runat="server"></asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                    <tr align="left" valign="top">
                                                        <td style="border:none" align="left" valign="top">
                                                            <asp:TextBox ID="txtTempDescriptive" runat="server" ReadOnly="true" TextMode="MultiLine" Height="200px" Width="800px" EnableViewState="true"></asp:TextBox>                                                       
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle BackColor="#EFF3FB" />
                    <EditRowStyle BackColor="#2461BF" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>    
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td style="height:20px" colspan="2">
        
        </td>
    </tr>
    <tr>
        <td align="right" colspan="2">
            <asp:Button ID="btn_Evaluation" runat="server" Text="Finish Evaluation" OnClick="btn_Evaluation_Click"/>
        </td>
    </tr>
</table>