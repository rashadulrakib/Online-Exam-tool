<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExamEntry.ascx.cs" Inherits="Controls_ExamEntry" %>
<script type="text/javascript" language="javascript">
    function checkHour(sender,args)
    {
        var sHour = new String();
        
        var flag = true;
        
        sHour = args.Value;
        
        if(sHour=="00")
        {
            flag = false;
            args.IsValid=false;
        }
        
        if(flag)
        {
            args.IsValid=true;
        }
    }
    
    function checkMinute(sender,args)
    {
        var sMinute = new String();
        
        var flag = true;
        
        sMinute = args.Value;
        
        if(sMinute=="00")
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
</script>

<center>
    <table id="tblMain" border="1" bgcolor="aliceblue" cellspacing="0" cellpadding="0" style="margin:none; border:1px solid #296488">
        <tr>
            <td style="width:20px; border:none">
            
            </td>
            <td style="border:none; text-align:left">
                <asp:ValidationSummary ID="vsExamEntry" runat="server" HeaderText="Following fields missing or invalid" />
                
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
                                            <asp:Label ID="Label8" runat="server" Text="Exam Entry" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td style="width: 10px" bgcolor="#b0e0e6">
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
                                            <asp:TextBox ID="txt_ExamName" runat="server" MaxLength="100"></asp:TextBox><span style="color:Red; vertical-align:top">*<asp:RequiredFieldValidator
                                                ID="rfvExamName" runat="server" ControlToValidate="txt_ExamName" Display="None"
                                                ErrorMessage="Exam name required." SetFocusOnError="True"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                    ID="revExamName" runat="server" ControlToValidate="txt_ExamName" Display="None"
                                                    ErrorMessage="character  '  is not allowed for exam name" SetFocusOnError="True"
                                                    ValidationExpression="([^']{1,100})"></asp:RegularExpressionValidator></span></td>
                                        <td style="width:10px">
                                        
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 5px">
                                        </td>
                                        <td style="text-align:left" valign="top">
                                            <asp:Label ID="Label2" runat="server" Text="Total Marks"></asp:Label></td>
                                        <td style="width: 5px">
                                        </td>
                                        <td style="text-align:left" valign="top">
                                            <asp:TextBox ID="txt_TotalMarks" runat="server"></asp:TextBox><span style="color:Red; vertical-align:top">*<asp:RequiredFieldValidator
                                                ID="rfvTotalMarks" runat="server" ControlToValidate="txt_TotalMarks" Display="None"
                                                ErrorMessage="Total marks required." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="cvTotalMark" runat="server" ControlToValidate="txt_TotalMarks"
                                                    Display="None" ErrorMessage="Total marks must greater than zero and integer" Operator="GreaterThan"
                                                    SetFocusOnError="True" Type="Integer" ValueToCompare="0"></asp:CompareValidator></span></td>
                                        <td style="width: 10px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 5px;">
                                        </td>
                                        <td style="text-align:left" valign="top">
                                            <asp:Label ID="Label3" runat="server" Text="Exam Date"></asp:Label></td>
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
                                            <asp:Label ID="Label5" runat="server" Text="Start Time"></asp:Label></td>
                                        <td style="width: 5px">
                                        </td>
                                        <td style="text-align: left" valign="top">
                                            HR:MIN:[Am/Pm]<asp:DropDownList ID="dr_Hour" runat="server" Width="50px">
                                            </asp:DropDownList>:<asp:DropDownList ID="dr_Minute" runat="server" Width="50px">
                                            </asp:DropDownList>:<asp:DropDownList ID="dr_AMPM" runat="server" Width="50px">
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="cusvHour" runat="server" ClientValidationFunction="checkHour"
                                                ControlToValidate="dr_Hour" Display="None" ErrorMessage="Start hour cannot be 00" SetFocusOnError="True"></asp:CustomValidator>
                                            <%--<asp:CustomValidator ID="cusvMinute" runat="server" ClientValidationFunction="checkMinute"
                                                ControlToValidate="dr_Minute" Display="None" ErrorMessage="Start minute cannot be 00"
                                                SetFocusOnError="True"></asp:CustomValidator>--%></td>
                                        <td style="width: 10px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 5px">
                                        </td>
                                        <td style="text-align:left" valign="top">
                                            <asp:Label ID="Label4" runat="server" Text="Duration"></asp:Label></td>
                                        <td style="width: 5px">
                                        </td>
                                        <td style="text-align:left" valign="top">
                                            <asp:TextBox ID="txt_Duration" runat="server"></asp:TextBox><span style="color:Red; vertical-align:top">*</span><asp:RequiredFieldValidator
                                                ID="rfvDuration" runat="server" ControlToValidate="txt_Duration"
                                                Display="None" ErrorMessage="Exam Duration required." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <asp:Label ID="Label6" runat="server" Text="Hour"></asp:Label><asp:CompareValidator ID="cvDurationGreaterThan" runat="server" ControlToValidate="txt_Duration"
                                                Display="None" ErrorMessage="Exam duration must greater than zero" Operator="GreaterThan"
                                                Type="Double" ValueToCompare="0"></asp:CompareValidator>
                                            <asp:CompareValidator ID="cvDurationLessthanEqual" runat="server" ControlToValidate="txt_Duration"
                                                Display="None" ErrorMessage="Exam duration must less than or equal five hour"
                                                Operator="LessThanEqual" Type="Double" ValueToCompare="5"></asp:CompareValidator></td>
                                        <td style="width: 10px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 5px">
                                        </td>
                                        <td style="text-align: left" valign="top">
                                            <asp:Label ID="Label7" runat="server" Text="Constraint"></asp:Label></td>
                                        <td style="width: 5px">
                                        </td>
                                        <td style="text-align: left" valign="top">
                                           <%-- <asp:RadioButtonList ID="rdo_SelectConstraint" runat="server" CellPadding="0" CellSpacing="0"
                                                RepeatDirection="Horizontal">
                                            </asp:RadioButtonList>--%>
                                            <asp:CheckBox ID="chk_Partial" runat="server" Text="Allow partial marking" />
                                            <asp:CheckBox ID="chk_Negative" runat="server" Text="Allow negative marking" />
                                            </td>
                                        <td style="width: 10px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 5px">
                                        </td>
                                        <td style="text-align: left" valign="top">
                                        </td>
                                        <td style="width: 5px">
                                        </td>
                                        <td style="text-align: left" valign="top">
                                            <asp:Button ID="btn_ExamEntry" runat="server" Text="Exam Entry" OnClick="btn_ExamEntry_Click" OnClientClick="javascript:document.getElementById('ctl00_CPH_Main_ctl00_HiddenFieldForDate').value=document.getElementById('tester').value;" /></td>
                                        <td style="width: 10px">
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
            <td style="width:20px; height:20px; border:none">
            
            </td>
            <td style="height:20px; border:none">
                <asp:HiddenField ID="HiddenFieldForDate" runat="server" />
            </td>
            <td style="width:20px; height:20px; border:none">
            
            </td>
        </tr>
    </table>
</center>

