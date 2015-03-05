<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CandidateExtend.ascx.cs" Inherits="Controls_CandidateExtend" %>
<script type="text/javascript" language="javascript">
    function check_uncheck(Val)
    {
      var ValChecked = Val.checked;
      var ValId = Val.id;
      var frm = document.forms[0];
            
      for (i = 0; i < frm.length; i++)
      {
                
        if (this != null)
        {
          if (ValId.indexOf('CheckAll') !=  - 1)
          {
            
            if (ValChecked)
            {  
              if(frm.elements[i].type.toString()=="checkbox")
              {
                frm.elements[i].checked = true;
              }
              
              
            }
            else
            {
              if(frm.elements[i].type.toString()=="checkbox")
              {
                frm.elements[i].checked = false;
              }
              
              
            }
          }
          else if (ValId.indexOf('deleteRec') !=  - 1)
          {
      
            if (frm.elements[i].checked == false)
              frm.elements[1].checked = false;
          }
        }
      } 
    } 

</script>
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
                        <td style="text-align:left; border:none" bgcolor="#b0e0e6" valign="top">
                            <asp:Label ID="Label8" runat="server" Text="Extend Candidate For Exam:" Font-Bold="True"></asp:Label>
                            <asp:Label ID="lbl_ExamName" runat="server" Font-Bold="True" ForeColor="Desktop"></asp:Label></td>
                        <td style="width:10px;border:none" bgcolor="#b0e0e6">
                        </td>
                    </tr>
                    <tr>
                        <td style="border:none; width:5px">
                            
                        </td>
                        <td style="border:none;" align="left" valign="top">
                            <asp:Label ID="Label1" runat="server" Text="Select Exam"></asp:Label>
                            <asp:DropDownList ID="dr_SelectExam" runat="server" Width="240px" AutoPostBack="True" OnSelectedIndexChanged="dr_SelectExam_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="border:none;width:10px">
                            
                        </td>
                    </tr>
                    <tr>
                        <td style="border:none; width:5px">
                            
                        </td>
                        <td style="border:none;">
                            <asp:GridView ID="grid_CandidatesOfExam" runat="server" AutoGenerateColumns="False"
                                CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" OnPageIndexChanging="grid_CandidatesOfExam_PageIndexChanging" OnRowDataBound="grid_CandidatesOfExam_RowDataBound" PageSize="10">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#EFF3FB" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                <asp:TemplateField>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="CheckAll" runat="server" onclick="return check_uncheck (this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="deleteRec" runat="server" onclick="return check_uncheck (this);" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Candidate ID">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCandidateID" runat="server" Text='<%# Eval("CandidateForExamCandidate.CandidateCompositeID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("CandidateForExamCandidate.CandidateName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Email">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("CandidateForExamCandidate.CandidateEmail") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            </asp:GridView>
                        </td>
                        <td style="border:none;width:10px">
                            
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5px; border:none">
                        </td>
                        <td style="border:none; text-align:left" valign="top">
                            <asp:RadioButtonList ID="rdoEmaiSendNotSend" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True">Dont Send</asp:ListItem>
                                <asp:ListItem>Send ID &amp; Password</asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td style="width: 10px; border:none">
                        </td>
                    </tr>
                     <tr>
                        <td style="border:none; width:5px">
                            
                        </td>
                        <td style="border:none;" align="left" valign="top">
                            <asp:Button ID="btn_AddCandidate" runat="server" OnClick="btn_AddCandidate_Click"
                                Text="Add Candidate" /></td>
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
