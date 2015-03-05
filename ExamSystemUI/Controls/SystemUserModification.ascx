<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SystemUserModification.ascx.cs" Inherits="Controls_SystemUserModification" %>
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
    
    function confirmMsg(frm)
    {
        var flag=false;
        var iIndexOfaRow=-1;
        var i=0;
        
        for (i = 0; i < frm.length; i++)
        {
            if (frm.elements[i].name.indexOf("deleteRec") !=  - 1)
            {
                iIndexOfaRow=iIndexOfaRow+1;
                if (frm.elements[i].checked)
                {    
                    flag=true;
                }
            }
        }
        
        if(flag)
        {
            return confirm('Are you sure you want to remove these systemusers?');
        }
    }

</script>
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
            <td style="border:none">
                <table border="1" cellspacing="0" cellpadding="0" bgcolor="#e0ffff" style="margin:0;border:1px solid green">
                    <tr>
                        <td style="border:none; width:5px" bgcolor="#b0e0e6">
                        
                        </td>
                        <td style="text-align: left; border:none" bgcolor="#b0e0e6" valign="top">
                            <asp:Label ID="Label8" runat="server" Text="System User Modification" Font-Bold="True"></asp:Label>
                        </td>
                        <td style="border:none; width:10px" bgcolor="#b0e0e6">
                        
                        </td>
                    </tr>
                    <tr>
                        <td style="border:none; width:5px">
                        
                        </td>
                        <td style="border:none">
                            <asp:Panel ID="pnl_SystemUsers" runat="server">
                                <asp:GridView ID="Grid_SystemUsers" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="None" OnRowDataBound="Grid_Questions_RowDataBound" AllowPaging="True" OnPageIndexChanging="Grid_SystemUsers_PageIndexChanging" PageSize="10">
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
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
                                        <%--<asp:TemplateField HeaderText="Name">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblUserName" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="New Name">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtUserName" runat="server" Text="" Width="150px" MaxLength="200"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <%-- <asp:TemplateField HeaderText="Password">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblPassword" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="New Password">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPassword" runat="server" Text="" Width="150px" TextMode="Password" MaxLength="200"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="New Email">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEmail" runat="server" Text="" Width="150px" MaxLength="100"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
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
                    <tr>
                        <td style="border:none; width:5px">
                        
                        </td>
                        <td style="border:none">
                            <asp:Button ID="btn_Delete" runat="server" Text="Delete" OnClick="btn_Delete_Click" OnClientClick="return confirmMsg(this.form)" />
                            <asp:Button ID="btn_Update" runat="server" Text="Update" OnClick="btn_Update_Click" />
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