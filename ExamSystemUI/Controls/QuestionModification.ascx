<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuestionModification.ascx.cs" Inherits="Controls_QuestionModification" %>
<script type="text/javascript" language="javascript">
        
    var x=0;
    var TotalTextBoxes=0;
    var flag=false;
    var flagOnlyOnetimeCalled=true;
    var sID = new String();
    var iTextBoxNumber=0;
    
    function ClearErrorLebel(lblErrorID)
    {
        var olblErrorID = document.getElementById(lblErrorID);
        olblErrorID.innerHTML="";     
    }
    
    function StoreTxtBoxCheckBoxValue(TotalChoices,HiddenFieldID,lblErrorID)
    {
        //alert(HiddenFieldID+":T:"+TotalChoices);
        
        ClearErrorLebel(lblErrorID);
        
        if(flagOnlyOnetimeCalled)
        {
            x=TotalChoices;
            TotalTextBoxes=TotalChoices;
            flagOnlyOnetimeCalled=false;
        }
        
        var i=0;
        var oHiddenFieldID = document.getElementById(HiddenFieldID);
        oHiddenFieldID.value="";
        //alert(oHiddenFieldID);
        //alert("value of x::"+x);
        for(i=0;i<=x;i++)
        {
             var oTextBoxID=document.getElementById(i.toString());
             var oChkBoxID =document.getElementById("CHE"+i.toString());
             
             if(oTextBoxID!=null && oChkBoxID!=null)
             {
                //alert(oTextBoxID+":"+oChkBoxID);
                oHiddenFieldID.value=oHiddenFieldID.value+oTextBoxID.value+":"+oChkBoxID.checked+"@";
             }
        }
        
        //alert(oHiddenFieldID.value);
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
              frm.elements[i].checked = true;
            else
              frm.elements[i].checked = false;
          }
          else if (ValId.indexOf('deleteRec') !=  - 1)
          {
      
            if (frm.elements[i].checked == false)
              frm.elements[1].checked = false;
          }
        }
      } 
    } 
    
    function confirmMsg(frm,lblErrorID)
    {
        ClearErrorLebel(lblErrorID);
        
        var flag=false;
        var iIndexOfaRow=-1;
        var ob=document.getElementById('ctl00_CPH_Main_ctl00_HiddenFieldForStoreChkBoxIndex');
        ob.value="";
        for (i = 0; i < frm.length; i++)
        {
            if (frm.elements[i].name.indexOf("deleteRec") !=  - 1)
            {
                iIndexOfaRow=iIndexOfaRow+1;
                if (frm.elements[i].checked)
                {    
                    flag=true;
                    ob.value=ob.value+iIndexOfaRow+":";
                }
            }
        }
        
        if(flag)
        {
            return confirm('Are you sure you want to delete these Questions?');
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
    
    function AppendTextBox(iTotalTextBoxesinServer,IsfromServer,ServeriTextBoxNumber)
    {
        
        if(flagOnlyOnetimeCalled)
        {
            x=iTotalTextBoxesinServer;
            TotalTextBoxes=iTotalTextBoxesinServer;
            flagOnlyOnetimeCalled=false;
        }
        //alert("APPENd:"+x);
 
        var oTxt = document.createElement('input');
        oTxt.type = "text";
 
        x=x+1;
        TotalTextBoxes=TotalTextBoxes+1;
 
        var id =x.toString();
        oTxt.id = id;  
 
        oTxt.onclick =  function(e) 
        {
 
            sID=oTxt.id.toString();
            
            iTextBoxNumber = parseInt(sID); //clicked TextBox's ID
 
            AppendTextBox(0,0,iTextBoxNumber);
        }
 
        if(IsfromServer==1)
        {
            iTextBoxNumber=ServeriTextBoxNumber;
        }
        var start=iTextBoxNumber+1;
        var end=x;
        var flag=true;
        
        for(var i=start;i<end;i++)
        {
            var oTextBoxID=document.getElementById(i.toString());
            if(oTextBoxID!=null)
            {
                flag=false;
                break;
            }
        }
 
        if(x-iTextBoxNumber==1 || flag)
        {
            var oTxtContainer= document.getElementById("ctl00_CPH_Main_ctl00_pnl_Choices");
            
            oTxtContainer.appendChild(oTxt);
            
            var oCheckBox=document.createElement('input');
            oCheckBox.id="CHE"+x.toString();
            oCheckBox.type="checkbox";
            oTxtContainer.appendChild(oCheckBox);
            
            var oSpanText=document.createElement('span');
            oSpanText.id="ISV"+x.toString();
            oSpanText.innerHTML=" Is Valid ";
            oTxtContainer.appendChild(oSpanText);
            
            var oButton=document.createElement('input');
            oButton.id="BTN"+x.toString();
            oButton.type="button";
            oButton.value="Delete";
            
            oButton.onclick = function(eButton)
            {
                var IDTextBoxContainer=document.getElementById("ctl00_CPH_Main_ctl00_pnl_Choices");
                var sButtonID = new String();
                sButtonID=oButton.id.toString();
                var iButtonID = parseInt(sButtonID.substring(3,sButtonID.length)); //ctl00_CPH_Main_ctl00_btn_Choice.length=31
                
                var IDTextBoxID = document.getElementById(iButtonID.toString()); //  get the ID of the Text Box
                var IDCheckBoxID= document.getElementById("CHE"+iButtonID.toString()); // get the ID of the Check Box
                var IDIsValidID = document.getElementById("ISV"+iButtonID.toString()); //get the ID of IsValid
                var IDButtonID = document.getElementById("BTN"+iButtonID.toString()); // get the ID of the Button
                var IDBrID=document.getElementById("BRI"+iButtonID.toString()); //get the id of BR

                IDTextBoxContainer.removeChild(IDTextBoxID);
                IDTextBoxContainer.removeChild(IDCheckBoxID);
                IDTextBoxContainer.removeChild(IDIsValidID); 
                IDTextBoxContainer.removeChild(IDButtonID);  //remove Button
                IDTextBoxContainer.removeChild(IDBrID);  //remove BR

                TotalTextBoxes=TotalTextBoxes-1;

                if(TotalTextBoxes==0)
                {
                    x=0;
                    iTextBoxNumber=0;
                }
                
            }
            
            oTxtContainer.appendChild(oButton); 
            
            CreateTable(); 
            
        }
        else
        {
            x=x-1;
            TotalTextBoxes=TotalTextBoxes-1;
        }
       
    }
    
    function RemoveRow(obj)
    {
        
        var IDTextBoxContainer=document.getElementById("ctl00_CPH_Main_ctl00_pnl_Choices");
        var sButtonID = new String();
        sButtonID=obj.toString();
        var iButtonID = parseInt(sButtonID.substring(3,sButtonID.length)); //ctl00_CPH_Main_ctl00_btn_Choice.length=31
           
        var IDTextBoxID = document.getElementById(iButtonID.toString()); //  get the ID of the Text Box
        var IDCheckBoxID= document.getElementById("CHE"+iButtonID.toString()); // get the ID of the Check Box
        var IDIsValidID = document.getElementById("ISV"+iButtonID.toString()); //get the ID of IsValid
        var IDButtonID = document.getElementById("BTN"+iButtonID.toString()); // get the ID of the Button
        var IDBrID=document.getElementById("BRI"+iButtonID.toString()); //get the id of BR

        IDTextBoxContainer.removeChild(IDTextBoxID);
        IDTextBoxContainer.removeChild(IDCheckBoxID);
        IDTextBoxContainer.removeChild(IDIsValidID); 
        IDTextBoxContainer.removeChild(IDButtonID);  //remove Button
        IDTextBoxContainer.removeChild(IDBrID);  //remove BR
    }
    
    function CreateTable()
    {
        var oTable=document.createElement('table');
        oTable.setAttribute('cellspacing','0');
        oTable.setAttribute('cellpadding','0');
        oTable.setAttribute('border','0');

        var oRow=document.createElement('tr');
        var otd=document.createElement('td');

        oRow.appendChild(otd);
        oTable.appendChild(oRow);
        
        document.getElementById("ctl00_CPH_Main_ctl00_pnl_Choices").appendChild(oTable);
    } 
    
    function checkAnyEmptyCheckOrObjective(sender,args)
    {
        
        
        var flag = true;
        
//        var olblErrorID = document.getElementById("ctl00_CPH_Main_ctl00_lbl_error");
//        
//        alert(olblErrorID);
//            
//        var bAnyCheckBoxIsChecked=false;
//        var bAnyOptionIsGiven=false;
//        
//        var bAnyCheckboxOrTextBoxIsExisted=false;
//    
//        var sErrorString = new String();
//        
//        var i=0;
//        
//        alert(x);
//    
//        for(i=0;i<=x;i++)
//        {
//             var oTextBoxID=document.getElementById(i.toString());
//             var oChkBoxID =document.getElementById("CHE"+i.toString());
//             
//             if(oTextBoxID!=null && oChkBoxID!=null)
//             {
//                bAnyCheckboxOrTextBoxIsExisted=true;
//                
//                if(oChkBoxID.checked)
//                {
//                    bAnyCheckBoxIsChecked=true;
//                }
//                
//                if(oTextBoxID.value.length>0)
//                {
//                    bAnyOptionIsGiven=true;
//                }
//             }
//        }
//        
//        //alert(oHiddenFieldID.value);
//        
//        olblErrorID.innerHTML="";
//        sErrorString="";
//        
//        //alert(x);
//        
//        if(bAnyCheckboxOrTextBoxIsExisted)
//        {
//            if(bAnyCheckBoxIsChecked==false || bAnyOptionIsGiven==false)
//            {
//                if(bAnyCheckBoxIsChecked==false)
//                {
//                    sErrorString=sErrorString+"<li>At least one check box must be checked.</li>";
//                }
//                
//                if(bAnyOptionIsGiven==false)
//                {
//                    sErrorString=sErrorString+"<li>At least one objective answer must given.</li>";
//                }
//              
//                olblErrorID.innerHTML="<font color='red'><ul>"+sErrorString+"</ul></font>";
//            
//                flag = false;
//                args.IsValid=false;
//            }
//        }
         
         
        if(flag)
        {
            args.IsValid=true;
        }
    }
           	
</script>
<center>
    <table border="1" style="margin:none;border:1px solid #296488" bgcolor="aliceblue" cellspacing="0" cellpadding="0">
         <tr>
            <td style="width:20px; border:none">
            
            </td>
            <td style="border:none; text-align:left; vertical-align:top">
                <asp:ValidationSummary id="vsQuestionUpdate" HeaderText="Following fields missing or invalid" runat="server" ValidationGroup="vgQuestionUpdate"></asp:ValidationSummary>
            </td>
            <td style="width:20px; border:none">
            
            </td>
        </tr>
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
                 <table border="1" style="margin:none;border:1px solid green" cellspacing="0" cellpadding="0" bgcolor="#e0ffff">
                     <tr>
                         <td align="left" colspan="1" style="border:none; width:5px" bgcolor="#b0e0e6" valign="top">
                         </td>
                         <td align="left" colspan="2" style="border:none" bgcolor="#b0e0e6" valign="top">
                            <asp:Label ID="Label8" runat="server" Text="Question Modification" Font-Bold="True"></asp:Label>
                         </td>
                         <td align="left" colspan="1" style="border:none; width:10px" bgcolor="#b0e0e6" valign="top">
                         </td>
                     </tr>
                    <tr>
                        <td align="left" style="border:none; width:5px" valign="top">
                        </td>
                        <td style=" border-left:none; border-right:1px solid green; border-top:none; border-bottom:none" align="left" valign="top">
                             <table border="0" cellpadding="0" cellspacing="0" style="margin:0">
                                <tr>
                                    <td style="text-align:left" valign="top" colspan="3">
                                        <table border="0" cellpadding="0" cellspacing="0" style="margin:0;">
                                            <tr>
                                                <td style="border:none">
                                                    <asp:Label ID="Label1" runat="server" Text="Category"></asp:Label>
                                                </td>
                                                <td style="width:5px">
                                    
                                                </td>
                                                <td style="border:none">
                                                    <asp:DropDownList ID="dr_Category" runat="server" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="dr_Category_SelectedIndexChanged"></asp:DropDownList><span style="color: #ff0000">*</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="border:none">
                                                     <asp:Label ID="Label2" runat="server" Text="Type"></asp:Label>
                                                </td>
                                                <td style="width:5px">
                                    
                                                </td>
                                                <td style="border:none">
                                                    <asp:DropDownList ID="dr_Type" runat="server" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="dr_Type_SelectedIndexChanged"></asp:DropDownList><span style="color: #ff0000">*</span>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width:10px">
                                    
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:left" valign="top" colspan="3">
                                        <asp:Label ID="lbl_Questions" runat="server"></asp:Label>
                                    </td>
                                    <td style="width:10px">
                                    
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="text-align:left" valign="top">
                                        <asp:Panel ID="pnl_questions" runat="server" HorizontalAlign="Left" Width="300px">
                                            
                                            <asp:GridView ID="Grid_Questions" runat="server" CellPadding="4"
                                                GridLines="None" ForeColor="#333333" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="Grid_Questions_PageIndexChanging" PageSize="5">
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="CheckAll" onclick="return check_uncheck (this);" runat="server" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="deleteRec" onclick="return check_uncheck (this);" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Questions">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuestion" runat="server" Text = '<%# Eval("QuestionText") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:BoundField HeaderText="Questions" DataField="QuestionText">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    
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
                                    <td style="width: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="text-align: left" valign="top">
                                        <table style="margin:0;" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="height: 24px">
                                                    <asp:Button ID="btn_Delete" runat="server" Text="Delete" OnClick="btn_Delete_Click"/>
                                                </td>
                                                <td style="width:1px; height: 24px;">
                                                
                                                </td>
                                                <td style="height: 24px">
                                                    <asp:Button ID="btn_View" runat="server" Text="View" OnClick="btn_View_Click"/>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:HiddenField ID="HiddenFieldForStoreChkBoxIndex" runat="server"/>
                                        <asp:HiddenField id="HiddenStoreTxtBoxCheckBoxValue" runat="server"/>
                                        <asp:HiddenField ID="HiddenForStoreSelectedRowIndex" runat="server" />
                                    </td>
                                    <td style="width:10px">
                                    
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="border:none;" align="left" valign="top">
                            <table border="0" cellpadding="0" cellspacing="0" style="margin:0;">
                                <tr>
                                    <td align="left" style="border:none; width:5px" valign="top">
                                    </td>
                                    <td style="border:none;" align="left" valign="top">
                                        <asp:Label ID="Label5" runat="server" Text="Question"></asp:Label>
                                    </td>
                                    <td align="left" style="border:none; width:5px" valign="top">
                                    </td>
                                    <td colspan="5" style="border:none;" align="left" valign="top">
                                        <asp:TextBox ID="txt_SaveQuestion" runat="server" TextMode="MultiLine" ReadOnly="True" Height="75px" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>       
                                <tr>
                                    <td align="left" style="border:none;" valign="top">
                                    </td>
                                    <td align="left" style="border:none;" valign="top">
                                        <asp:Label ID="lblRightQuestionLevelShow" runat="server" Text="Question  Level"></asp:Label></td>
                                    <td align="left" style="border:none;" valign="top">
                                    </td>
                                    <td align="left" colspan="5" style="border:none;" valign="top">
                                        <asp:Label ID="lblRightQlebelShow" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td align="left" style="border:none; width:5px" valign="top">
                                    </td>
                                    <td style="border:none;" align="left" valign="top">
                                        <asp:Label id="Label3" runat="server" Text="Current Mark"></asp:Label>
                                    </td>
                                    <td align="left" style="border:none; width:5px" valign="top">
                                    </td>
                                    <td style="border:none;" align="left" valign="top">
                                        <asp:TextBox ID="txt_SaveDefaultMark" runat="server" Width="40px" ReadOnly="True"></asp:TextBox></td>
                                    <td align="left" style="border:none; width:5px" valign="top">
                                    </td>
                                    <td style="border:none;" align="left" valign="top">
                                        <asp:Label ID="Label4" runat="server" Text="New Mark"></asp:Label></td>
                                    <td align="left" style="border:none; width:5px" valign="top">
                                    </td>
                                    <td style="border:none;" align="left" valign="top">
                                        <asp:TextBox ID="txt_CurrentDefaultMark" runat="server" Width="40px"></asp:TextBox><span style="color: #ff0000">*</span><asp:CompareValidator
                                                ID="cvDefaultMark" runat="server" ControlToValidate="txt_CurrentDefaultMark"
                                                Display="None" ErrorMessage="Default mark must greater than zero" Operator="GreaterThan"
                                                SetFocusOnError="True" Type="Double" ValidationGroup="vgQuestionUpdate" ValueToCompare="0"></asp:CompareValidator></td>
                                </tr>
                                <tr>
                                    <td align="left" style="border:none; width:5px" valign="top">
                                    </td>
                                    <td style="border:none;" align="left" valign="top">
                                        <asp:Label ID="Label6" runat="server" Text="Current Time"></asp:Label></td>
                                    <td align="left" style="border:none; width:5px" valign="top">
                                    </td>
                                    <td style="border:none;" align="left" valign="top">
                                        <asp:TextBox ID="txt_LastPossibleTime" runat="server" Width="40px" ReadOnly="True"></asp:TextBox></td>
                                    <td align="left" style="border:none; width:5px" valign="top">
                                    </td>
                                    <td style="border:none;" align="left" valign="top">
                                        <asp:Label ID="Label7" runat="server" Text="New Possible Time"></asp:Label></td>
                                    <td align="left" style="border:none; width:5px" valign="top">
                                    </td>
                                    <td style="border:none;" align="left" valign="top">
                                        <asp:TextBox ID="txt_CurrentpossibleTime" runat="server" Width="40px"></asp:TextBox><span style="color: #ff0000">*</span>Minute<asp:CompareValidator
                                                ID="cvPossibleTime" runat="server" ControlToValidate="txt_CurrentpossibleTime"
                                                Display="None" ErrorMessage="Possible time must greater than zero" Operator="GreaterThan"
                                                SetFocusOnError="True" Type="Double" ValidationGroup="vgQuestionUpdate" ValueToCompare="0"></asp:CompareValidator></td>
                                </tr>
                                <tr>
                                    <td align="left" style="border:none; width:5px" valign="top">
                                    </td>
                                    <td style="border:none;" align="left" valign="top">
                                        </td>
                                    <td align="left" style="border:none; width:5px" valign="top">
                                    </td>
                                    <td colspan="5" style="border:none;" align="left" valign="top">
                                        <asp:Panel ID="pnl_Choices" runat="server">
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="border:none; width:5px;" valign="top">
                                    </td>
                                    <td align="left" style="border:none;" valign="top">
                                        </td>
                                    <td align="left" style="border:none; width:5px;" valign="top">
                                    </td>
                                    <td align="left" colspan="5" style="border:none;" valign="top">
                                        <asp:Button ID="btn_Update" runat="server" Text="Update" OnClick="btn_Update_Click" ValidationGroup="vgQuestionUpdate" />
                                        <asp:CustomValidator ID="cusvAnyEmptyCheckOrObjective" runat="server" ClientValidationFunction="checkAnyEmptyCheckOrObjective"
                                            ControlToValidate="txt_SaveQuestion" Display="None" SetFocusOnError="True"></asp:CustomValidator></td>
                                </tr>
                            </table>
                        </td>
                        <td align="left" style="border:none;width:10px" valign="top">
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
