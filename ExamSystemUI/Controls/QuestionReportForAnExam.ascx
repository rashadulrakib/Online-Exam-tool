<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuestionReportForAnExam.ascx.cs" Inherits="Controls_QuestionReportForAnExam" %>
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
                <table id="tblCanModification" border="1" cellspacing="0" cellpadding="0" style="margin:none;border:1px solid green" runat="server">
                    <tr>
                        <td bgcolor="#b0e0e6" style="border:none; width:5px">
                        </td>
                        <td align="left" bgcolor="#b0e0e6" style="border:none;" valign="top">
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Questions For Exam:"></asp:Label><asp:Label ID="lbl_ExamName" runat="server" Font-Bold="True" ForeColor="Desktop"></asp:Label></td>
                        <td bgcolor="#b0e0e6" style="border:none; width:10px">
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#b0e0e6" style="border:none; width:5px">
                        </td>
                        <td align="left" bgcolor="#b0e0e6" style="border:none;" valign="top">
                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Total number of questions:"></asp:Label>
                            <asp:Label ID="lblTotalQuestions" runat="server" Font-Bold="True" ForeColor="Desktop"></asp:Label></td>
                        <td bgcolor="#b0e0e6" style="border:none; width:10px">
                        </td>
                    </tr>
                    <tr>
                        <td style="border:none; width:5px">
                        
                        </td>
                        <td style="border:none">
                            <asp:Panel ID="pnl_AllQuestionsOfExam" runat="server" ScrollBars="Horizontal">
                                 <asp:GridView ID="Grid_AllQuestionsOfExam" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    CellPadding="4" ForeColor="#333333" GridLines="Horizontal" OnPageIndexChanging="Grid_AllQuestionsOfExam_PageIndexChanging"
                                    OnRowDataBound="Grid_AllQuestionsOfExam_RowDataBound" PageSize="5" Width="100%">
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Question">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <table cellpadding="0" cellspacing="0" style="border:none">
                                                    <tr align="left">
                                                        <td style="text-align:left; width:100px">
                                                            <asp:Label ID="lblDisplayQuestion" runat="server" Font-Bold="true" Text="Question: " ></asp:Label> 
                                                        </td>
                                                        <td style="text-align:left">
                                                            <asp:Label ID="lblQuestion" runat="server"></asp:Label>        
                                                        </td>
                                                    </tr>
                                                    <tr align="left">
                                                        <td colspan="2" style="height:5px">
                                                        </td>
                                                    </tr>
                                                    <tr align="left">
                                                        <td style="text-align:left; width:100px">
                                                            <asp:Label ID="lblDisplayObjective" runat="server" Font-Bold="true" Text="Choices: " ></asp:Label> 
                                                        </td>
                                                        <td style="text-align:left">
                                                        </td>
                                                    </tr>
                                                    <tr align="left">
                                                        <td style="text-align:left; width:100px">
                                                        </td>
                                                        <td>
                                                            <asp:Panel ID="pnl_ObjectiveAnswer" runat="server">
                                                                <table cellpadding="0" cellspacing="0" style="border:none">
                                                                    <tr>
                                                                        <td>
                                                                            
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
                        <td style="border:none; width:10px">
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
            <td style="height:20px; border:none; text-align:left">
            </td>
            <td style="width:20px; height:20px; border:none">
            
            </td>
        </tr>
    </table>
</center>
                        