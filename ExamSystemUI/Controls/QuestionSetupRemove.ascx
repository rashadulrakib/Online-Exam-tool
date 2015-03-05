<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuestionSetupRemove.ascx.cs" Inherits="Controls_QuestionSetupRemove" %>
<script type="text/javascript" language="javascript">
    
    function ClearErrorLebel(lblErrorID)
    {
        var olblErrorID = document.getElementById(lblErrorID);
        olblErrorID.innerHTML="";     
    }
    
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
    
    function RemoveQuestion(frm,lblErrorID)
    {
        ClearErrorLebel(lblErrorID);
        
        var flag=false;
        var iIndexOfaRow=-1;
        var ob=document.getElementById('ctl00_CPH_Main_ctl00_HiddenFieldForStoreChkBoxIndex');
        var i=0;
        
        ob.value="";
        
        for (i = 0; i < frm.length; i++)
        {
            if (frm.elements[i].name.indexOf("deleteRec") !=  - 1)
            {
                iIndexOfaRow=iIndexOfaRow+1;
                if (frm.elements[i].checked)
                {    
                    ob.value=ob.value+iIndexOfaRow+":";
                    flag=true;
                }
            }
        }
        
        if(flag)
        {
            return confirm('Are you sure you want to remove these Questions for this exam?');
        }
    }
    
</script>
<center>
   <%-- <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <table id="tblMain" border="1" bgcolor="aliceblue" cellspacing="0" cellpadding="0" style="margin:none; border:1px solid #296488">
                <tr>
                    <td style="width:20px; border:none; height:20px">
                    
                    </td>
                    <td style="border:none;color:Burlywood; font-style:italic; height:20px; text-align:left" valign="top">
                        Exam Constraint: <asp:Label id="lbl_ShowExamConstraint" runat="server"></asp:Label>
                    </td>
                    <td style="width:20px;border:none; height:20px">
                    
                    </td>
                </tr>
                <tr>
                    <td style="width:20px; border:none; height:20px">
                    
                    </td>
                    <td style="border:none; height:20px; text-align:left" valign="top">
                        <asp:Label ID="lbl_error" runat="server"></asp:Label>
                    </td>
                    <td style="width:20px;border:none; height:20px">
                    
                    </td>
                </tr>
                <tr>
                    <td style="width:20px;border:none">
                    
                    </td>
                    <td style="border:none">
                     <table border="1" cellspacing="0" cellpadding="0" style="margin:none;border:1px solid green" bgcolor="#e0ffff">
                        <tr>
                            <td style=" border-left:none; border-right:none; border-top:none; border-bottom:none" valign="top" align="left">
                                <table  border="0" cellspacing="0" cellpadding="0" style="margin:none;">
                                    <tr>
                                        <td style="width:5px"  bgcolor="#b0e0e6">
                                        </td>
                                        <td colspan="3" bgcolor="#b0e0e6" valign="top" align="left">
                                            <asp:Label ID="Label8" runat="server" Font-Bold="True" Text="Remove Setup Questions For:"></asp:Label>
                                            <asp:Label ID="lbl_ExamName" runat="server" Font-Bold="True" Font-Underline="False" ForeColor="Desktop"></asp:Label>
                                            </td>
                                        <td style="width:10px"  bgcolor="#b0e0e6">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:5px">
                                        
                                        </td>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text="Category"></asp:Label>
                                        </td>
                                        <td style="width:5px">
                                        
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="dr_Category" runat="server" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="dr_Category_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width:10px">
                                        
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:5px">
                                        
                                        </td>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="Type"></asp:Label>
                                        </td>
                                        <td style="width:5px">
                                        
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="dr_Type" runat="server" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="dr_Type_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width:10px">
                                        
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 5px">
                                        </td>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text="Selection Type"></asp:Label>
                                        </td>
                                        <td style="width: 5px">
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="dr_ListOrRandom" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dr_ListOrRandom_SelectedIndexChanged"
                                                Width="300px">
                                            </asp:DropDownList></td>
                                        <td style="width: 10px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 5px">
                                        </td>
                                        <td>
                                            <asp:Label ID="lblQuestionLevel" runat="server" Text="Question Level"></asp:Label></td>
                                        <td style="width: 5px">
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="drdnSelectQuestionLevel" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="drdnSelectQuestionLevel_SelectedIndexChanged" Width="300px">
                                            </asp:DropDownList></td>
                                        <td style="width: 10px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 5px">
                                        </td>
                                        <td colspan="3">
                                            <asp:Label ID="lbl_Questions" runat="server"></asp:Label></td>
                                        <td style="width: 10px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:5px">
                                        
                                        </td>
                                        <td colspan="3">
                                            <asp:Panel ID="pnl_questions" runat="server" Width="300px">
                                                <asp:GridView ID="Grid_Questions" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                    ForeColor="#333333" GridLines="None" OnRowDataBound="Grid_Questions_RowDataBound" OnPageIndexChanging="Grid_Questions_PageIndexChanging" AllowPaging="true" PageSize="5">
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
                                                        <asp:TemplateField HeaderText="Questions">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQuestion" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Setup Mark">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSetupMark" runat="server"></asp:Label>
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
                                        <td style="width:10px">
                                        
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 5px">
                                        </td>
                                        <td colspan="3">
                                            <%--<asp:Button ID="btn_Remove" runat="server" Text="Remove" OnClientClick="return RemoveQuestion(this.form)" OnClick="btn_Remove_Click" />--%>
                                            <asp:Button ID="btn_Remove" runat="server" Text="Remove" OnClick="btn_Remove_Click" /></td>
                                        <td style="width: 10px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 5px">
                                        </td>
                                        <td>
                                        </td>
                                        <td style="width: 5px">
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="HiddenFieldForStoreChkBoxIndex" runat="server" />
                                            <%--ctl00_CPH_Main_ctl00_HiddenFieldForStoreChkBoxIndex--%>
                                        </td>
                                        <td style="width: 10px">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        
                    </table>
                    </td>
                    <td style="width:20px;border:none">
                    
                    </td>
                </tr>
                <tr>
                    <td style="width:20px; border:none; height:20px">
                    
                    </td>
                    <td style="border:none; height:20px">
                    </td>
                    <td style="width:20px;border:none; height:20px">
                    
                    </td>
                </tr>
            </table>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</center>