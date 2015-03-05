<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CandidateSetup.ascx.cs" Inherits="Controls_CandidateSetup" %>
<script type="text/javascript" language="javascript">
    function ClearErrorLebel(lblErrorID)
    {
        var olblErrorID = document.getElementById(lblErrorID);
        olblErrorID.innerHTML="";     
    }
    
    function CheckCVExtension(sender,args)
    {
        var sCVFileName = new String();
        
        var flag = true;
        
        sCVFileName = args.Value;
       
        if (sCVFileName.lastIndexOf(".htm",sCVFileName.length-1) > 0 || sCVFileName.lastIndexOf(".html",sCVFileName.length-1) > 0 || sCVFileName.lastIndexOf(".doc",sCVFileName.length-1) > 0 || sCVFileName.lastIndexOf(".pdf",sCVFileName.length-1) > 0 || sCVFileName.lastIndexOf(".rtf",sCVFileName.length-1) > 0)
        {

        }
        else
        {
            flag = false;
            args.IsValid=false;
        }
        
        if(flag)
        {
            args.IsValid=true;
        }
    
    }
    
    function CheckPhotoExtension(sender,args)
    {
        var sPhotoName = new String();
        
        var flag = true;
        
        sPhotoName = args.Value;
       
        if (sPhotoName.lastIndexOf(".gif",sPhotoName.length-1) > 0 || sPhotoName.lastIndexOf(".png",sPhotoName.length-1) > 0 || sPhotoName.lastIndexOf(".jpg",sPhotoName.length-1) > 0 || sPhotoName.lastIndexOf(".jpeg",sPhotoName.length-1) > 0 || sPhotoName.lastIndexOf(".bmp",sPhotoName.length-1) > 0)
        {

        }
        else
        {
            flag = false;
            args.IsValid=false;
        }
        
        if(flag)
        {
            args.IsValid=true;
        }
    
    }
</script>

<center>
    <table id="tblMain" border="1" bgcolor="aliceblue" cellspacing="0" cellpadding="0" style="margin:none; border:1px solid #296488">
        <tr>
            <td style="width:20px;border:none;">
            </td>
            <td style="border:none; text-align:left" valign="top">
                <asp:ValidationSummary ID="vsCandidateSetup" runat="server" HeaderText="Following fields missing or invalid" />
            </td>
            <td style="border:none;width:20px;">
            </td>
        </tr>
        <tr>
            <td style="width:20px; border:none; height:20px">
            
            </td>
            <td style="border:none; height:20px; text-align:left" valign="top">
                <asp:Label id="lbl_error" runat="server"></asp:Label>
            </td>
            <td style="width:20px;border:none; height:20px">
            
            </td>
        </tr>    
        <tr>
            <td style="width:20px;border:none">
            </td>
            <td style="border:none">
                <table id="tblExamEntry" border="1" cellspacing="0" cellpadding="0" bgcolor="#e0ffff" style="margin:0;border:1px solid green">
                    <tr>
                        <td id="tdExamEntry" style="border:none">
                            <table  border="0" cellspacing="0" cellpadding="0" style="margin:0">
                                <tr>
                                    <td style="width: 5px" bgcolor="#b0e0e6">
                                    </td>
                                    <td colspan="3" style="text-align: left" valign="top" bgcolor="#b0e0e6">
                                        <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="Candidate Setup For Exam:"></asp:Label>
                                        <asp:Label ID="lbl_ExamName" runat="server" Font-Bold="True" ForeColor="Desktop"></asp:Label></td>
                                    <td style="width: 10px" bgcolor="#b0e0e6">
                                    </td>
                                </tr>
                                 <tr>
                                    <td style="width:5px">
                                    
                                    </td>
                                    <td style="text-align:left" valign="top">
                                        <asp:Label ID="Label1" runat="server" Text="Candidate Name"></asp:Label>
                                    </td>
                                    <td style="width:5px">
                                        
                                    </td>
                                    <td style="text-align:left" valign="top" >
                                        <asp:TextBox ID="txt_CandidateName" runat="server" Width="300px" MaxLength="100"></asp:TextBox><span style="color:Red; vertical-align:top">*</span>
                                    </td>
                                    <td style="width:10px">
                                        <asp:RequiredFieldValidator ID="rfvCandidateName" runat="server" ControlToValidate="txt_CandidateName"
                                            Display="None" ErrorMessage="Candidate Name required." SetFocusOnError="True"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                ID="revCandidateName" runat="server" ControlToValidate="txt_CandidateName" Display="None"
                                                ErrorMessage="character  '  and  -   is not allowed for Candidate name" SetFocusOnError="True"
                                                ValidationExpression="([^'-]{2,200})"></asp:RegularExpressionValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                        <asp:Label ID="Label7" runat="server" Text="Email"></asp:Label></td>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                        <asp:TextBox ID="txt_Email" runat="server" MaxLength="100" Width="300px"></asp:TextBox><span style="color:Red; vertical-align:top">*</span></td>
                                    <td style="width: 10px">
                                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txt_Email"
                                            Display="None" ErrorMessage="Email Address required." SetFocusOnError="True"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                ID="revEmail" runat="server" ControlToValidate="txt_Email" Display="None" ErrorMessage="Invalid Email Address"
                                                SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                                </tr>
                                <tr>
                                    <td style="width:5px">
                                    
                                    </td>
                                    <td style="text-align:left" valign="top">
                                        <asp:Label ID="Label2" runat="server" Text="Last Result"></asp:Label>
                                    </td>
                                    <td style="width:5px">
                                    
                                    </td>
                                    <td style="text-align:left" valign="top" colspan="1" >
                                        <asp:DropDownList ID="dr_SelectCGOrMark" runat="server" Width="150px">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txt_MarkOrCG" Width="30px" runat="server"></asp:TextBox><span style="color:Red; vertical-align:top">*</span><asp:Label
                                            ID="lblOutOf" runat="server" Text="Out of "></asp:Label>
                                        <asp:TextBox ID="txtOutOf" runat="server" Width="30px"></asp:TextBox><span style="color:Red; vertical-align:top">*</span></td>
                                    <td style="width:10px">
                                        <asp:RequiredFieldValidator ID="rfvLastResult" runat="server" ControlToValidate="txt_MarkOrCG"
                                            Display="None" ErrorMessage="Last Result required." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="cvLastResult" runat="server" ControlToValidate="txt_MarkOrCG"
                                            Display="None" ErrorMessage="Last Result must greater than zero" Operator="GreaterThan"
                                            SetFocusOnError="True" Type="Double" ValueToCompare="0"></asp:CompareValidator>
                                        <asp:RequiredFieldValidator ID="rfvLastResultRange" runat="server" ControlToValidate="txtOutOf"
                                            Display="None" ErrorMessage="Last Result Range required." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="cvLastResultRange" runat="server" ControlToCompare="txt_MarkOrCG"
                                            ControlToValidate="txtOutOf" Display="None" ErrorMessage="Out of value must greater than or equal last result value"
                                            Operator="GreaterThanEqual" SetFocusOnError="True" Type="Double" ValueToCompare="1"></asp:CompareValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                        <asp:Label ID="Label3" runat="server" Text="Last Institution"></asp:Label></td>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                        <asp:TextBox ID="txt_LastInstitution" runat="server" Width="300px" MaxLength="50"></asp:TextBox><span style="color:Red; vertical-align:top">*</span></td>
                                    <td style="width: 10px">
                                        <asp:RequiredFieldValidator ID="rfvLastInstitution" runat="server" ControlToValidate="txt_LastInstitution"
                                            Display="None" ErrorMessage="Last Institution required." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revLastInstitution" runat="server" ControlToValidate="txt_LastInstitution"
                                            Display="None" ErrorMessage="character  '  is not allowed for Last Institution"
                                            SetFocusOnError="True" ValidationExpression="([^']{1,50})"></asp:RegularExpressionValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                        <asp:Label ID="Label4" runat="server" Text="Last Passing Year"></asp:Label></td>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                        <asp:TextBox ID="txt_LastPassingYear" runat="server" Width="300px"></asp:TextBox><span style="color:Red; vertical-align:top">*</span></td>
                                    <td style="width: 10px">
                                        <asp:RequiredFieldValidator ID="rfvLastPassingYear" runat="server" ControlToValidate="txt_LastPassingYear"
                                            Display="None" ErrorMessage="Last Passing year required." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="rvLastPassingYear" runat="server" ControlToValidate="txt_LastPassingYear"
                                            Display="None" ErrorMessage="Last passing year range is [1000-9999]" MaximumValue="9999"
                                            MinimumValue="1000" SetFocusOnError="True" Type="Integer"></asp:RangeValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                        <asp:Label ID="Label5" runat="server" Text="Uplaod CV"></asp:Label></td>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                        <asp:FileUpload ID="fup_ccandidateCv" runat="server" BorderStyle="None" /><span style="color:Red; vertical-align:top">*</span></td>
                                    <td style="width: 10px">
                                        <asp:RequiredFieldValidator ID="rfvCandidateCV" runat="server" ControlToValidate="fup_ccandidateCv"
                                            Display="None" ErrorMessage="Candidate CV Required." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revCandidateCV" runat="server" ControlToValidate="fup_ccandidateCv"
                                            Display="None" ErrorMessage="Character  - and  '  is not allowed for CV Name"
                                            SetFocusOnError="True" ValidationExpression="([^'-]{1,400})"></asp:RegularExpressionValidator>
                                        <asp:CustomValidator ID="cusvCV" runat="server" ClientValidationFunction="CheckCVExtension"
                                            ControlToValidate="fup_ccandidateCv" Display="None" ErrorMessage="Valid CV Extension(.html,.htm,.doc,.pdf,.rtf)"
                                            SetFocusOnError="True"></asp:CustomValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                        <asp:Label ID="Label8" runat="server" Text="Photo"></asp:Label></td>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top"><asp:FileUpload ID="fup_CandidatePhoto" runat="server" BorderStyle="None" /></td>
                                    <td style="width: 10px">
                                        <asp:RegularExpressionValidator ID="revCandidatePhoto" runat="server" ControlToValidate="fup_CandidatePhoto"
                                            Display="None" ErrorMessage="Character  - and  '  is not allowed for photo" SetFocusOnError="True"
                                            ValidationExpression="([^'-]{1,400})"></asp:RegularExpressionValidator>
                                        <asp:CustomValidator ID="cusvPhoto" runat="server" ClientValidationFunction="CheckPhotoExtension"
                                            ControlToValidate="fup_CandidatePhoto" Display="None" ErrorMessage="Valid Photo Extension(.gif,.png,.jpg,.jpeg,.bmp)"
                                            SetFocusOnError="True"></asp:CustomValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                    </td>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                        <asp:RadioButtonList ID="rdoEmaiSendNotSend" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Selected="True">Dont Send</asp:ListItem>
                                            <asp:ListItem>Send ID &amp; Password</asp:ListItem>
                                        </asp:RadioButtonList></td>
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
                                        <asp:Button ID="btn_Setup" runat="server" Text="Setup" OnClick="btn_Setup_Click" /></td>
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
</center>