<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EvaluateCandidate.ascx.cs" Inherits="Controls_EvaluateCandidate" %>
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
                <table id="tbl_candidateLink" border="1" cellspacing="0" cellpadding="0" style="margin:none;border:1px solid green" runat="server">
                    <tr>
                        <td style="border:none; width:5px;text-align: left" bgcolor="#b0e0e6" valign="top">
                        
                        </td>
                        <td style="border:none;text-align: left" bgcolor="#b0e0e6" valign="top">
                            <asp:Label ID="Label8" runat="server" Text="Candidate List For Evaluation:" Font-Bold="True"></asp:Label> 
                        </td>
                        <td style="border:none; width:10px;text-align: left" bgcolor="#b0e0e6" valign="top">
                        
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#b0e0e6" style="border-right: medium none; border-top: medium none;
                            border-left: medium none; width: 5px; border-bottom: medium none; text-align: left"
                            valign="top">
                        </td>
                        <td bgcolor="#b0e0e6" style="border-right: medium none; border-top: medium none;
                            border-left: medium none; border-bottom: medium none; text-align: left" valign="top">
                            <asp:Label ID="lbl10" runat="server" Font-Bold="True" Text="For Exam:"></asp:Label>
                            <asp:Label ID="lbl_ExamName" runat="server" Font-Bold="True" ForeColor="Desktop"></asp:Label></td>
                        <td bgcolor="#b0e0e6" style="border-right: medium none; border-top: medium none;
                            border-left: medium none; width: 10px; border-bottom: medium none; text-align: left"
                            valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td style="border:none; width:5px">
                        
                        </td>
                        <td style="border:none; text-align:left">
                            <asp:Panel ID="pnlCandidates" runat="server">
                                <asp:GridView ID="gridCandidates" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="gridCandidates_RowDataBound" OnPageIndexChanging="gridCandidates_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <HeaderTemplate>
                                                <asp:Label ID="Label2" runat="server" Text="Candidate Name"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCandidateName" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <HeaderTemplate>
                                                <asp:Label ID="Label1" runat="server" Text="Candidate CompositeID"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table style="border:none" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td style="border:none; width:450px">
                                                            <asp:HyperLink ID="hyperCandidate" runat="server"></asp:HyperLink>             
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
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
            <td style="height:20px; border:none">
                
            </td>
            <td style="width:20px; height:20px; border:none">
            </td>
        </tr>
    </table>
</center>