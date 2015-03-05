<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuestionSetup.ascx.cs" Inherits="Controls_QuestionSetup" %>
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
    
    function SetupQuestion(frm,lblErrorID)
    {
        ClearErrorLebel(lblErrorID);
        
        var iCountSelectedRow=0;
        var iIndexOfaRow=-1;
        var iSelectedRowIndex=0;
        var iIndexOfaRowForTextBox=-1;
        var ob=document.getElementById('ctl00_CPH_Main_ctl00_HiddenFieldForStoreChkBoxIndex');
        var obMark=document.getElementById('ctl00_CPH_Main_ctl00_HiddenFieldForStoreTxtBoxMark');
        ob.value="";
        obMark.value="";
        
        for (i = 0; i < frm.length; i++)
        {
            if (frm.elements[i].name.indexOf("deleteRec") !=  - 1)
            {
                iIndexOfaRow=iIndexOfaRow+1;
                if (frm.elements[i].checked)
                {    
                    iCountSelectedRow=iCountSelectedRow+1;
                    ob.value=ob.value+iIndexOfaRow+":";
                    iSelectedRowIndex=iIndexOfaRow;
                }
            }//txtSetupQuestionMark
            if(frm.elements[i].name.indexOf("txtSetupQuestionMark") !=  - 1)
            {
                iIndexOfaRowForTextBox=iIndexOfaRowForTextBox+1;
                var sMark;
                if(frm.elements[i].value.length<=0)
                {
                    sMark="null";
                }
                else
                {
                    sMark=frm.elements[i].value;
                }
                obMark.value=obMark.value+sMark+":";
                //alert(obMark.value);
            }
        }
    }
    function ViewQuestion(frm,lblErrorID)
    {
        ClearErrorLebel(lblErrorID);
        
        var iCountSelectedRow=0;
        var iIndexOfaRow=-1;
        var iSelectedRowIndex=0;
        var ob=document.getElementById('ctl00_CPH_Main_ctl00_HiddenFieldForStoreChkBoxIndex');
        ob.value="";
        
        for (i = 0; i < frm.length; i++)
        {
            if (frm.elements[i].name.indexOf("deleteRec") !=  - 1)
            {
                iIndexOfaRow=iIndexOfaRow+1;
                if (frm.elements[i].checked)
                {    
                    iCountSelectedRow=iCountSelectedRow+1;
                    ob.value=ob.value+iIndexOfaRow+":";
                    iSelectedRowIndex=iIndexOfaRow;
                }
            }
        }
    }

</script>
<center>
    <table id="tblMain" border="1" bgcolor="aliceblue" cellspacing="0" cellpadding="0" style="margin:none; border:1px solid #296488">
        <tr>
            <td style="width:20px; height:20px; border:none">
            
            </td>
            <td style="border:none; color:Burlywood; font-style:italic; height:20px" align=center>
                Exam Constraint: <asp:Label id="lbl_ShowExamConstraint" runat="server"></asp:Label>
            </td>
            <td style="width:20px; height:20px; border:none">
            
            </td>
        </tr>
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
                <table border="1" cellspacing="0" cellpadding="0" style="margin:none;border:1px solid green" bgcolor="#e0ffff">
                    <tr>
                        <td colspan="1" style="border:none; width:5px" bgcolor="#b0e0e6">
                        </td>
                        <td style="border:none;" colspan="3">
                            <table border="0" cellspacing="0" cellpadding="0" style="margin:none; width:100%">
                                <tr>
                                    <td style="border:none; text-align:left" bgcolor="#b0e0e6">
                                        <asp:Label ID="Label8" runat="server" Text="Question Setup For Exam: " Font-Bold="True"></asp:Label>
                                        <asp:Label ID="lbl_ExamName" runat="server" Font-Bold="True" Font-Underline="False" ForeColor="Desktop"></asp:Label>
                                        </td>
                                </tr>
                                <tr>
                                    <td style="border:none;text-align:left" bgcolor="#b0e0e6">
                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Exam Total Mark: "></asp:Label>
                                        <asp:Label ID="lbl_ExamTotalMark" runat="server" Font-Bold="True" Font-Underline="False" ForeColor="Desktop"></asp:Label>
                                        <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="Exam Total Duration: "></asp:Label>
                                        <asp:Label ID="lbl_ExamDuration" runat="server" Font-Bold="True" Font-Underline="False" ForeColor="Desktop"></asp:Label>
                                        <asp:Label ID="Label7" runat="server" Font-Bold="True" Text="Minute"></asp:Label></td>
                                </tr>
                            </table>
                        </td>
                        <td colspan="1" style="border:none; width:10px" bgcolor="#b0e0e6">
                        </td>
                    </tr>
                    
                    <tr>
                        <td align="left" style="border:none; width:5px" valign="top">
                        </td>
                        <td style=" border-left:none; border-right:1px solid green; border-top:none; border-bottom:none" valign="top" align="left">
                            <table border="0" cellspacing="0" cellpadding="0" style="margin:none;">
                                <tr>
                                    <td colspan="1">
                                        <asp:Label ID="Label1" runat="server" Text="Category"></asp:Label></td>
                                    <td colspan="1" style="width:5px">
                                    </td>
                                    <td colspan="1">
                                        <asp:DropDownList ID="dr_Category" runat="server" Width="280px" AutoPostBack="True" OnSelectedIndexChanged="dr_Category_SelectedIndexChanged">
                                        </asp:DropDownList></td>
                                    <td style="width:10px">
                                    
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="1">
                                        <asp:Label ID="Label2" runat="server" Text="Type"></asp:Label></td>
                                    <td colspan="1" style="width:5px">
                                    </td>
                                    <td colspan="1">
                                        <asp:DropDownList ID="dr_Type" runat="server" Width="280px" AutoPostBack="True" OnSelectedIndexChanged="dr_Type_SelectedIndexChanged">
                                        </asp:DropDownList></td>
                                    <td style="width: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="1">
                                        <asp:Label ID="Label3" runat="server" Text="Selection Type"></asp:Label></td>
                                    <td colspan="1" style="width:5px">
                                    </td>
                                    <td colspan="1">
                                    <asp:DropDownList ID="dr_ListOrRandom" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dr_ListOrRandom_SelectedIndexChanged"
                                            Width="280px">
                                        </asp:DropDownList></td>
                                    <td style="width: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="1">
                                        <asp:Label ID="lblQuestionLevel" runat="server" Text="Question Level"></asp:Label></td>
                                    <td colspan="1" style="width: 5px">
                                    </td>
                                    <td colspan="1">
                                        <asp:DropDownList ID="drdnSelectQuestionLevel" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="drdnSelectQuestionLevel_SelectedIndexChanged" Width="280px">
                                        </asp:DropDownList></td>
                                    <td style="width: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="1">
                                        <asp:Label ID="lbl_RandomQuestions" runat="server" Text="Random Questions"></asp:Label></td>
                                    <td colspan="1" style="width: 5px">
                                    </td>
                                    <td colspan="1">
                                        <asp:TextBox ID="txt_RandomQuestions" runat="server" Width="200px"></asp:TextBox>
                                        <asp:Label ID="lblRandomOutOf" runat="server" Text="Out of"></asp:Label>
                                        <asp:TextBox ID="txtRandomOutOf" runat="server" ReadOnly="True" Width="30px"></asp:TextBox></td>
                                    <td style="width: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="lbl_Questions" runat="server"></asp:Label></td>
                                    <td style="width: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Panel ID="pnl_questions" runat="server" Width="490px">
                                            <asp:GridView ID="Grid_Questions" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                ForeColor="#333333" GridLines="None" OnRowDataBound="Grid_Questions_RowDataBound" AllowPaging="True" OnPageIndexChanging="Grid_Questions_PageIndexChanging" PageSize="5">
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
                                                            <asp:Label ID="lblQuestion" runat="server" Width="250px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Entry Mark">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOrginalMark" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="New Mark">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtSetupQuestionMark" Width="30px" runat="server" Text=""></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Time">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPossibleTime" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:BoundField DataField='<%# Eval("QuestionSetupQuestion.QuestionText") %>' HeaderText="Questions">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>--%>
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
                                    <td colspan="3">
                                    </td>
                                    <td style="width: 10px; height:5px">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <%--<asp:Button ID="btn_Setup" runat="server" Text="Setup" OnClick="btn_Setup_Click" OnClientClick="return SetupQuestion(this.form)" />--%>
                                        <asp:Button ID="btn_Setup" runat="server" Text="Setup" OnClick="btn_Setup_Click"/>
                                        <%--<asp:Button ID="btn_View" runat="server" OnClientClick="return ViewQuestion(this.form)"
                                            Text="View" OnClick="btn_View_Click" />--%>
                                        <asp:Button ID="btn_View" runat="server" Text="View" OnClick="btn_View_Click" /></td>
                                    <td style="width: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:HiddenField ID="HiddenFieldForStoreChkBoxIndex" runat="server" />
                                        <asp:HiddenField ID="HiddenFieldForStoreTxtBoxMark" runat="server" />
                                        <%--ctl00_CPH_Main_ctl00_HiddenFieldForStoreChkBoxIndex--%>
                                    </td>
                                    <td style="width: 10px">
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="left" style="border-right:none; border-top: medium none; border-left: medium none;
                            border-bottom: medium none" valign="top">
                        </td>
                        <td style="border:none" valign="top" align="left">
                            <table border="0" cellpadding="0" cellspacing="0" style="margin:0">
                                <tr>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align:left; vertical-align:top">
                                        <asp:Label ID="Label5" runat="server" Text="Question"></asp:Label></td>
                                    <td style="vertical-align: top; text-align: left; width:5px">
                                    </td>
                                    <td style="text-align:left; vertical-align:top">
                                        <asp:TextBox ID="txt_SaveQuestion" runat="server" TextMode="MultiLine" ReadOnly="True" Height="75px" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5px;">
                                    </td>
                                    <td>
                                        <asp:Label ID="lblRightQuestionLevelShow" runat="server" Text="Question  Level"></asp:Label></td>
                                    <td style="width: 5px;">
                                    </td>
                                    <td>
                                        <asp:TextBox ID="lblRightQlebelShow" runat="server" ReadOnly="true"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 5px; height:10px">
                                    </td>
                                    <td>
                                    </td>
                                    <td style="width: 5px; height:10px">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5px">
                                    
                                    </td>
                                    <td colspan="1">
                                    </td>
                                    <td colspan="1" style="width: 5px;">
                                    </td>
                                    <td colspan="1" >
                                        <asp:Panel ID="pnl_Choices" runat="server">
                                        </asp:Panel>
                                    </td>
                                </tr>
                             
                            </table>
                        </td>
                        <td align="left" style="border:none; width:10px" valign="top">
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