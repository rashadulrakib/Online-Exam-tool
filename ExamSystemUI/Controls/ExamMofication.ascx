<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExamMofication.ascx.cs" Inherits="Controls_ExamMofication" %>
<script type="text/javascript" language="javascript">
    function confirmMsg(lblErrorID,drdnID,frm)
    {
        ClearErrorLebel(lblErrorID);
        
        var odrdnID=document.getElementById(drdnID);
        
        var olblErrorID=document.getElementById(lblErrorID);
        
        if(odrdnID.value=="[Select One]")
        {
            //olblErrorID.innerHTML="<font color='red'><ul><li>Must select an exam</li></ul></font>";
            
            return false;
        }
        else
        {
            return confirm('Are you sure you want to delete this Exam?');
        }
    }
    
    function checkSelectExam(sender,args)
    {
        var sSelectExam = new String();
        
        var flag = true;
        
        sSelectExam = args.Value;
        
        if(sSelectExam=="[Select One]")
        {
            flag = false;
            args.IsValid=false;
        }
        
        if(flag)
        {
            args.IsValid=true;
        }
    }
    function ClearErrorLebel(lblErrorID)
    {
        var olblErrorID = document.getElementById(lblErrorID);
        olblErrorID.innerHTML="";     
    }
    
    function clientFunctionForUpdate(lblErrorID,hfID)
    {
        ClearErrorLebel(lblErrorID);
        
        var oHiddenFieldforDate=document.getElementById(hfID);
        var oCelenderField=document.getElementById('tester');
        
        oHiddenFieldforDate.value=oCelenderField.value;
        
    }
</script>
<center>
    <table id="tblMain" border="1" bgcolor="aliceblue" cellspacing="0" cellpadding="0" style="margin:none; border:1px solid #296488">
         <tr>
            <td style="width:20px; border:none">
            
            </td>
            <td style="border:none; text-align:left">
                <asp:ValidationSummary ID="vsExamModification" runat="server" HeaderText="Following fields missing or invalid" ValidationGroup="vgExamUpdate" /><asp:ValidationSummary ID="vsDelete" runat="server" HeaderText="Following fields missing or invalid" ValidationGroup="vgExamDelete" />
            </td>
            <td style="width:20px; border:none">
            
            </td>
        </tr>
        <tr>
            <td style="width:20px; height:20px; border:none">
            
            </td>
            <td style="height:20px; border:none; text-align:left">
                <asp:Label id="lbl_error" runat="server"></asp:Label>
            </td>
            <td style="width:20px; height:20px; border:none">
            
            </td>
        </tr>
        <tr>
            <td style="width:20px; border:none">
            
            </td>
            <td style=" border:none">
                <table id="tblExamEntry" border="1" cellspacing="0" cellpadding="0" bgcolor="#e0ffff" style="margin:0;border:1px solid green">
                    <tr>
                        <td id="tdExamEntry" style="border:none">
                            <table  border="0" cellspacing="0" cellpadding="0" style="margin:0">
                                <tr>
                                    <td style="width: 5px" bgcolor="#b0e0e6">
                                    </td>
                                    <td style="text-align: left" bgcolor="#b0e0e6" valign="top" colspan="3">
                                        <asp:Label ID="Label11" runat="server" Text="Exam Modification" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td style="width: 10px" bgcolor="#b0e0e6">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                        <asp:Label ID="Label9" runat="server" Text="Select Exam"></asp:Label></td>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                        <asp:DropDownList ID="dr_SelectExam" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dr_SelectExam_SelectedIndexChanged"
                                            Width="230px">
                                        </asp:DropDownList><span style="color:Red; vertical-align:top">*</span><asp:CustomValidator
                                            ID="cusvSelectExam" runat="server" ClientValidationFunction="checkSelectExam"
                                            ControlToValidate="dr_SelectExam" Display="None" ErrorMessage="Must select an exam"
                                            SetFocusOnError="True" ValidationGroup="vgExamUpdate"></asp:CustomValidator>
                                        <asp:CustomValidator ID="cusvDeleteExam" runat="server" ClientValidationFunction="checkSelectExam"
                                            ControlToValidate="dr_SelectExam" Display="None" ErrorMessage="Must select an exam"
                                            SetFocusOnError="True" ValidationGroup="vgExamDelete"></asp:CustomValidator></td>
                                    <td style="width: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:5px">
                                    
                                    </td>
                                    <td style="text-align:left" valign="top">
                                        <asp:Label ID="Label1" runat="server" Text="Exam Name"></asp:Label>
                                    </td>
                                    <td style="width:5px">
                                    
                                    </td>
                                    <td style="text-align:left" valign="top">
                                        <asp:TextBox ID="txt_ExamName" runat="server" ReadOnly="True" Width="230px"></asp:TextBox>
                                    </td>
                                    <td style="width:10px">
                                    
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align:left" valign="top">
                                        <asp:Label ID="Label2" runat="server" Text="Current Total"></asp:Label></td>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align:left" valign="top">
                                        <asp:TextBox ID="txt_TotalMarks" runat="server" ReadOnly="True" Width="230px"></asp:TextBox></td>
                                    <td style="width: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align:left" valign="top">
                                        <asp:Label ID="Label8" runat="server" Text="New Total"></asp:Label></td>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align:left" valign="top">
                                        <asp:TextBox ID="txt_CurrentTotal" runat="server" Width="230px"></asp:TextBox><asp:CompareValidator
                                            ID="cvTotalMark" runat="server" ControlToValidate="txt_CurrentTotal" Display="None"
                                            ErrorMessage="Total marks must greater than zero and integer" Operator="GreaterThan" SetFocusOnError="True"
                                            Type="Integer" ValueToCompare="0" ValidationGroup="vgExamUpdate"></asp:CompareValidator></td>
                                    <td style="width: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                        <asp:Label ID="Label10" runat="server" Text="Current Exam Time"></asp:Label></td>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                        <asp:TextBox ID="txt_LastDate" runat="server" ReadOnly="True" Width="230px"></asp:TextBox></td>
                                    <td style="width: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5px;">
                                    </td>
                                    <td style="text-align:left" valign="top">
                                        <asp:Label ID="Label3" runat="server" Text="New Date"></asp:Label></td>
                                    <td style="width: 5px;">
                                    </td>
                                    <td style="text-align:left" valign="top">
	                                    <fieldset style="width:235px;padding:5px;">
		                                    <legend>Calendar</legend>

		                                    <input type="text" name="tester" id="tester" />
		                                    
		                                    <p/>
		                                    <input type="text" name="tester_day" id="tester_day" value="" size="3" maxLength="2" />
		                                    <select name="tester_month" id="tester_month">
			                                    <option value="1">January</option>
			                                    <option value="2">February</option>
			                                    <option value="3">March</option>
			                                    <option value="4">April</option>
			                                    <option value="5">May</option>
			                                    <option value="6">June</option>
			                                    <option value="7">July</option>
			                                    <option value="8">August</option>
			                                    <option value="9">September</option>
			                                    <option value="10">October</option>
			                                    <option value="11">November</option>
			                                    <option value="12">December</option>
		                                    </select>
		                                    <input type="text" name="tester_year" id="tester_year" value="" size="5" maxLength="4"/>
		                                    <p/>
		                                    <div id="cal_tester_display"></div>
		                                    <script type="text/javascript">
			                                    cal1 = new Calendar ("cal1", "tester", new Date());
			                                    renderCalendar (cal1);
		                                    </script>
	                                    </fieldset>
                                    </td>
                                    <td style="width: 10px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                        <asp:Label ID="Label5" runat="server" Text="New Start Time"></asp:Label></td>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                        HR:MIN:[Am/Pm]<asp:DropDownList ID="dr_Hour" runat="server" Width="50px">
                                        </asp:DropDownList>:<asp:DropDownList ID="dr_Minute" runat="server" Width="50px">
                                        </asp:DropDownList>:<asp:DropDownList ID="dr_AMPM" runat="server" Width="50px">
                                        </asp:DropDownList></td>
                                    <td style="width: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                        <asp:Label ID="Label12" runat="server" Text="Current Duration"></asp:Label></td>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                        <asp:TextBox ID="txt_LastDuration" runat="server" ReadOnly="True" Width="230px"></asp:TextBox><asp:Label ID="Label13" runat="server" Text="Hour"></asp:Label></td>
                                    <td style="width: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align:left" valign="top">
                                        <asp:Label ID="Label4" runat="server" Text="New Duration"></asp:Label></td>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align:left" valign="top">
                                        <asp:TextBox ID="txt_Duration" runat="server" Width="230px"></asp:TextBox><asp:Label ID="Label6" runat="server" Text="Hour"></asp:Label><asp:CompareValidator
                                                ID="cvDurationGreaterThan" runat="server" ControlToValidate="txt_Duration" Display="None"
                                                ErrorMessage="Exam duration must greater than zero" Operator="GreaterThan" Type="Double"
                                                ValidationGroup="vgExamUpdate" ValueToCompare="0"></asp:CompareValidator>
                                        <asp:CompareValidator ID="cvDurationLessThanEqual" runat="server" ControlToValidate="txt_Duration"
                                            Display="None" ErrorMessage="Exam duration must less than or equal five hour"
                                            Operator="LessThanEqual" Type="Double" ValidationGroup="vgExamUpdate" ValueToCompare="5"></asp:CompareValidator></td>
                                    <td style="width: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                        <asp:Label ID="Label14" runat="server" Text="Current Constraint"></asp:Label></td>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                        <asp:TextBox ID="txt_LastConstraint" runat="server" ReadOnly="True" Width="230px"></asp:TextBox></td>
                                    <td style="width: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                        <asp:Label ID="Label7" runat="server" Text="New Constraint"></asp:Label></td>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                        <%--<asp:RadioButtonList ID="rdo_SelectConstraint" runat="server" CellPadding="0" CellSpacing="0"
                                            RepeatDirection="Horizontal">
                                        </asp:RadioButtonList></td>--%>
                                        <asp:CheckBox ID="chk_Partial" runat="server" Text="Allow partial marking" />
                                        <asp:CheckBox ID="chk_Negative" runat="server" Text="Allow negative marking" />
                                    <td style="width: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5px;">
                                    </td>
                                    <td style="text-align: left;" valign="top">
                                    </td>
                                    <td style="width: 5px;">
                                    </td>
                                    <td style="text-align: left;" valign="top">
                                        <%--<asp:Button ID="btn_ExamUpdate" runat="server" Text="Exam Update" OnClientClick="javascript:document.getElementById('ctl00_CPH_Main_ctl00_HiddenFieldForDate').value=document.getElementById('tester').value;" OnClick="btn_ExamUpdate_Click" Width="100px" ValidationGroup="vgExamUpdate" />--%>
                                        <asp:Button ID="btn_ExamUpdate" runat="server" Text="Exam Update" OnClick="btn_ExamUpdate_Click" Width="100px" ValidationGroup="vgExamUpdate" />
                                        <asp:Button ID="btn_ExamDelete" runat="server" OnClick="btn_ExamDelete_Click" Text="Exam Delete" Width="100px" ValidationGroup="vgExamDelete" /></td>
                                    <td style="width: 10px;">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width:20px; border:none">
            
            </td>
        </tr>
        <tr>
            <td style="width:20px;height:20px;; border:none">
            
            </td>
            <td style=" border:none;height:20px;">
                <asp:HiddenField ID="HiddenFieldForDate" runat="server" />
            </td>
            <td style="width:20px; border:none;height:20px;">
            
            </td>
        </tr>
    </table>
</center>
