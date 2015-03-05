<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryModification.ascx.cs" Inherits="Controls_CategoryModification" %>
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
           return confirm('Are you sure you want to delete these Categories?');
        }
    } 

</script>
<center>
    <table border="1" bgcolor="aliceblue" cellspacing="0" cellpadding="0" style="margin:none; border:1px solid #296488">
        <tr>
            <td style="width:20px; height:20px; border:none">
            
            </td>
            <td style="height:20px; border:none; text-align:left; vertical-align:top">
                <asp:Label ID="lbl_error" runat="server"></asp:Label>
            </td>
            <td style="width:20px; height:20px; border:none">
            
            </td>
        </tr>
        <tr>
            <td style="width:20px; border:none">
            </td>
            <td style=" border:none">
                <table id="tblCategory" border="1" cellspacing="0" cellpadding="0" style="margin:none;border:1px solid green" bgcolor="#e0ffff" runat="server">
                     <tr>
                        <td bgcolor="#b0e0e6" colspan="1" rowspan="1" style="width:5px;border:none;">
                        </td>
                        <td colspan="3" valign="top" style="text-align: left;border:none;" bgcolor="#b0e0e6">
                            <asp:Label ID="Label2" runat="server" Text="Category Modification" Font-Bold="true"></asp:Label>
                        </td>
                        <td bgcolor="#b0e0e6" colspan="1" rowspan="1" style="width:10px;border:none;">
                        </td>
                    </tr>
                    <tr>
                        <td style="border:none; width:5px">
                        </td>
                        <td style="border:none">
                            <asp:Panel ID="pnl_Categories" runat="server">
                                <asp:GridView ID="Grid_Categories" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="None" AllowPaging="True" OnPageIndexChanging="Grid_Categories_PageIndexChanging" OnRowDataBound="Grid_Categories_RowDataBound" PageSize="10">
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
                                        <asp:TemplateField HeaderText="Category Name">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCategoryName" runat="server" Width="170px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="New Name">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCategoryName" runat="server" Text="" Width="250px" MaxLength="50"></asp:TextBox>
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
                            <asp:Button ID="btn_Delete" runat="server" Text="Delete" OnClick="btn_Delete_Click" OnClientClick="return confirmMsg(this.form)"/>
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
            <td style="height:20px; border:none" align=center>
                
            </td>
            <td style="width:20px; height:20px; border:none">
            
            </td>
        </tr>
    </table>
</center>