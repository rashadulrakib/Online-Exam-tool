<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExamProcess.ascx.cs" Inherits="Controls_ExamProcess" %>
<script type="text/javascript" language="javascript">
    function CheckAttachedFileExtension(sender,args)
    {
        var sAnswerFileName = new String();
        
        var flag = true;
        
        sAnswerFileName = args.Value;
       
        if (sAnswerFileName.lastIndexOf(".doc",sAnswerFileName.length-1) > 0 || sAnswerFileName.lastIndexOf(".vsd",sAnswerFileName.length-1)>0)
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
    
    function checkMaxCharacterLength(sender,args)
    {
        var sAnswerFileText = new String();
        
        var flag = true;
        
        sAnswerFileText = args.Value;
       
        if (sAnswerFileText.length>4000)
        {
            flag = false;
            args.IsValid=false;
        }
        
        if(flag)
        {
            args.IsValid=true;
        }
    }
    
    function getChoiceValue(totalChoices)
    {
        var i=1;
        
        var hiddenChoiceID = document.getElementById("ctl00_CPH_MainCandidate_ctl00_HiddenForGridChoices");
        
        hiddenChoiceID.value="";
        
        for(i=1;i<=totalChoices;i=i+1)
        {
            var chkID = document.getElementById("ctl00_CPH_MainCandidate_ctl00_Grid_Answers_ctl02_chk"+i.toString());
                        
            hiddenChoiceID.value=hiddenChoiceID.value+chkID.checked+"@";
        }
        
        //alert(hiddenChoiceID.value+":"+HiddenTxtForChoicesID.text);
    }
    
    function getTextBoxValue(txtID)
    {
        var oTxt = document.getElementById(txtID);
        
        var hiddenTextBoxID = document.getElementById("ctl00_CPH_MainCandidate_ctl00_HiddenForGridTextBox");
        hiddenTextBoxID.value=oTxt.value;
    }
    
    function ExamFinishClick()
    {
        CommonClick();
        
        return confirm('Are you sure to Finish Exam?');
    }
    
    function CommonClick()
    {
        //alert("CommonClick");
        
        var oTxt = document.getElementById("ctl00_CPH_MainCandidate_ctl00_Grid_Answers_ctl02_txtTempDescriptive")
        
        if(oTxt!=null)
        {
            var hiddenTextBoxID = document.getElementById("ctl00_CPH_MainCandidate_ctl00_HiddenForGridTextBox");
            hiddenTextBoxID.value=oTxt.value;
            
            //alert(hiddenTextBoxID.value);
        }
        else
        {
            var i=1;
            
            var hiddenChoiceID = document.getElementById("ctl00_CPH_MainCandidate_ctl00_HiddenForGridChoices");
            hiddenChoiceID.value="";
            
            while(true)
            {
                var chkID = document.getElementById("ctl00_CPH_MainCandidate_ctl00_Grid_Answers_ctl02_chk"+i.toString());
                
                if(chkID==null)
                {
                    break;
                }
                
                hiddenChoiceID.value=hiddenChoiceID.value+chkID.checked+"@";
                
                i=i+1;
            }
        }
    }
    
</script>

<table cellpadding="0" cellspacing="0" border="0" style="margin:none;">
    <tr>
        <td style="border:none; height:20px" valign="top" align="center">
            <asp:Label ID="lbl_error" runat="server" ></asp:Label>
        </td>
    </tr>
</table>
<table cellpadding="0" cellspacing="0" border="0" style="margin:none; width:100%;">
    <tr align="left" valign="top">
        <td style="border:none" colspan="3">
            <table cellpadding="0" cellspacing="0" border="0" style="margin:none; width:100%;">
                <tr align="left">
                    <td style="border:none; color:Burlywood; font-size:larger; font-style:italic" align="left" valign="top"> Candidate Name: <asp:Label ID="lbl_CandidateName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="border:none; color:Burlywood; font-size:larger; font-style:italic" align="left" valign="top"> Exam Name: <asp:Label ID="lbl_ExamName" runat="server"></asp:Label> Exam Constraint: <asp:Label ID="lbl_ExamConstraint" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="border:none; color:Burlywood; font-size:larger; font-style:italic" align="left" valign="top">
                        Exam Total: <asp:Label ID="lbl_ExamTotal" runat="server"></asp:Label> Exam Duration : <asp:Label ID="lbl_ExamDuration" runat="server"></asp:Label> Hour</td>
                </tr>
            </table>        
        </td>
    </tr>
    <tr>
        <td style="border:none; height:10px" colspan="3">
        </td>
    </tr>
    <tr>
        <td align="left" style="border:none;" valign="top">
        </td>
        <td align="left" style="width: 10px; border:none;" valign="top">
        </td>
        <td align="left" style="border:none;" valign="top">
            <asp:ValidationSummary id="vsAttachAnswerFile" HeaderText="Following fields missing or invalid" runat="server" ValidationGroup="vgAttachFile"></asp:ValidationSummary>
        </td>
    </tr>
    <tr style="height:450px">
        <td style="border:none;"align="left" valign="top">
            <asp:TreeView ID="treeViewQuestions" runat="server" ImageSet="Simple" ShowLines="True" OnSelectedNodeChanged="treeViewQuestions_SelectedNodeChanged" onclick="CommonClick()">
                <ParentNodeStyle Font-Bold="False" />
                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                    VerticalPadding="0px" />
                <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="0px"
                    NodeSpacing="0px" VerticalPadding="0px" />
            </asp:TreeView>
        
        </td>
        <td style="border:none; width:10px" align="left" valign="top">
        
        </td>
        <td style="border:none" align="left" valign="top">
            <asp:Panel ID="pnl_Answers" runat="server">
                <asp:GridView ID="Grid_Answers" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="None" AllowPaging="true" PageSize="1" OnPageIndexChanging="Grid_Answers_PageIndexChanging" OnRowDataBound="Grid_Answers_RowDataBound" Width="100%" OnPageIndexChanged="Grid_Answers_PageIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderText="Questions">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <table style="margin:none; border:none; width:100%" cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td style="border:none; width:85px">Question No:</td>
                                        <td style="border:none; width:80px; color:DarkCyan">
                                            <asp:Label ID="lblQuestionNo" runat="server" ></asp:Label> 
                                        </td>
                                        <td style="border:none;" align="left">
                                            Question: <asp:Label ID="lblQuestion" runat="server" ForeColor="Chocolate" ></asp:Label>                                            
                                        </td>
                                        <td style="border:none;" align="right">
                                           Mark: <asp:Label ID="lblQuestionMark" runat="server" ></asp:Label>    
                                        </td>
                                    </tr>
                                </table>
                                <table style="margin:none; width:100%" cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td style="border:none; height:10px">
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td id="tdLastAttcahedFileContainer" runat="server" style="border:none">
                                            <asp:FileUpload ID="fupAnswerAttach" runat="server" /> Last AttachedFile: <asp:Label ID="lbllastAttachedFile" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="OliveDrab"></asp:Label>
                                            <asp:RegularExpressionValidator id="revCandidateAttachFile" runat="server" SetFocusOnError="True" ErrorMessage="Character  - and  '  is not allowed for Answer file" Display="None" ControlToValidate="fupAnswerAttach" ValidationExpression="([^'-]{1,400})" ValidationGroup="vgAttachFile"></asp:RegularExpressionValidator>
                                            <asp:CustomValidator id="cusvAttachedAnswerFile" runat="server" SetFocusOnError="True" ErrorMessage="Valid Answer file Extension(.doc,.vsd)" Display="None" ControlToValidate="fupAnswerAttach" ClientValidationFunction="CheckAttachedFileExtension" ValidationGroup="vgAttachFile"></asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td id="tdAttcahedFileFormatContainer" runat="server" style="border:none;" align="left">Attach Answer File(- is not allowed in file name, extension will be[.doc,.vsd]):</td>
                                    </tr>
                                    <tr>
                                        <td style="border:none">
                                            <asp:Panel ID="pnl_DescriptiveOrObjective" runat="server">
                                                <asp:TextBox ID="txtTempDescriptive" runat="server" TextMode="MultiLine" Height="200px" Width="800px" EnableViewState="true" MaxLength="4000"></asp:TextBox>
                                                <asp:CustomValidator ID="cusvMaxLength" runat="server" ClientValidationFunction="checkMaxCharacterLength"
                                                    ControlToValidate="txtTempDescriptive" Display="None" ErrorMessage="Maximum character length is 4000 for answer text"
                                                    SetFocusOnError="True" ValidationGroup="vgAttachFile"></asp:CustomValidator>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerTemplate>
                        <table style="margin:none; width:100%" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td style="border:none" align="left" valign="top">
                                    <asp:LinkButton ID="LinkButtonPrev" CommandArgument="Prev" CommandName="Page" runat="server" ForeColor="Snow" ValidationGroup="vgAttachFile">Prev</asp:LinkButton>
                                </td>
                                <td style="border:none; width:520px"> 
                                </td>
                                <td style="border:none" align="left" valign="top">
                                    <asp:LinkButton ID="LinkButtonNext" CommandArgument="Next" CommandName="Page" runat="server" ForeColor="Snow" ValidationGroup="vgAttachFile">Next</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </PagerTemplate>
                    <RowStyle BackColor="#EFF3FB" />
                    <EditRowStyle BackColor="#2461BF" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>    
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td style="height:20px" colspan="3">
        
        </td>
    </tr>
    <tr>
        <td align="right" colspan="3">
            <asp:Button ID="btn_Finish" runat="server" Text="Finish Exam" OnClick="btn_Finish_Click" OnClientClick="return ExamFinishClick()" ValidationGroup="vgAttachFile" />
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:HiddenField ID="HiddenForGridChoices" runat="server" />
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:HiddenField ID="HiddenForGridTextBox" runat="server" />   
        </td>
    </tr>
</table>