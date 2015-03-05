<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LabelEntryAndModification.ascx.cs" Inherits="Controls_LabelEntryAndModification" %>
<script type="text/javascript" language="javascript">
    function ClearErrorLebel(lblErrorID)
    {
        var olblErrorID = document.getElementById(lblErrorID);
        olblErrorID.innerHTML="";     
    }
    
    function PopWindow(id1,lblErrorID)
    {
        ClearErrorLebel(lblErrorID);
        
        var selObj = document.getElementById(id1);
        var i=0;
        var flagSelected = false;
        
        for (i=0; i<selObj.options.length; i++) 
        {
            if (selObj.options[i].selected) 
            {
                flagSelected=true;
                break;
            }
        }
        
        if(flagSelected)
        {
            return confirm('Are you sure you want to delete this Level?');
        }
    }
    
    function checkMaxCharacterLength(sender,args)
    {
        var sDescription = new String();
        
        var flag = true;
        
        sDescription = args.Value;
       
        if (sDescription.length>1000)
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
    <table cellpadding="0" cellspacing="0" style="border:none; margin:none">
        <tr>
            <td style="border:none; text-align:left">
                </td>
        </tr>
        <tr>
            <td style="border:none">
                <table id="tblMain" border="1" bgcolor="aliceblue" cellspacing="0" cellpadding="0" style="margin:none; border:1px solid #296488">
                    <tr>
                        <td style="width:20px; border:none">
                        
                        </td>
                        <td style="border:none; text-align:left" valign="top">
                            <asp:ValidationSummary ID="vsLevelModification" runat="server" HeaderText="Following fields missing or invalid" ValidationGroup="LevelEntryUpdate" />
                            
                        </td>
                        <td style="width:20px;border:none;">
                        
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
                                         <td style="width:5px; border:none" bgcolor="#b0e0e6">
                                         </td>
                                         <td style="border:none" bgcolor="#b0e0e6">
                                             <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Recruitment Level page:"></asp:Label></td>
                                     </tr>
                                    <tr>
                                        <td style="border:none; width:5px">
                                        </td>
                                        <td id="tdExamEntry" style="border:none">
                                            <table  border="0" cellspacing="0" cellpadding="0" style="margin:0">
                                                <tr>
                                                    <td style="text-align: left; vertical-align:top" >
                                                        <asp:ListBox ID="listLevels" runat="server" Width="200px" Height="150px">
                                                        </asp:ListBox></td>
                                                    <td style="width: 5px">
                                                        </td>
                                                    <td style="text-align: left; vertical-align:top">
                                                        <asp:Button ID="btnAdfromList" runat="server" Text="Modify -->" OnClick="btnAdfromList_Click"></asp:Button></td>
                                                    <td style="text-align: left; vertical-align:top;">
                                                        <table border="0" cellspacing="0" cellpadding="0" style="margin:0;">
                                                            <tr>
                                                                <td style="width: 10px">
                                                                    
                                                                </td>
                                                                <td style="text-align: left; vertical-align:top;">Name</td>
                                                                <td style="width: 5px">
                                                                
                                                                </td>
                                                                <td style="text-align: left; vertical-align:top">
                                                                    <asp:TextBox ID="txtLavelName" runat="server" Width="300px" MaxLength="200"></asp:TextBox></td>
                                                                <td style="vertical-align: top; text-align: left">
                                                                    <span style="color:Red; vertical-align:top">*<asp:RequiredFieldValidator ID="rfvLevelName"
                                                                        runat="server" ControlToValidate="txtLavelName" Display="None" ErrorMessage="Level Name required."
                                                                        SetFocusOnError="True" ValidationGroup="LevelEntryUpdate"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                                            ID="revLevelName" runat="server" ControlToValidate="txtLavelName" Display="None"
                                                                            ErrorMessage="character ' and  -  is not allowed for Level name" SetFocusOnError="True"
                                                                            ValidationExpression="([^'-]{1,200})" ValidationGroup="LevelEntryUpdate"></asp:RegularExpressionValidator></span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10px"> 
                                                                
                                                                </td>
                                                                <td style="text-align: left; vertical-align:top;">Description</td>
                                                                <td style="width: 5px">
                                                                
                                                                </td>
                                                                <td style="text-align: left; vertical-align:top">
                                                                    <asp:TextBox ID="txtLevelDescription" runat="server" Height="100px" TextMode="MultiLine" Width="300px" MaxLength="1000"></asp:TextBox></td>
                                                                <td style="vertical-align: top; text-align: left">
                                                                    <asp:CustomValidator ID="cusvMaxLength" runat="server" ClientValidationFunction="checkMaxCharacterLength"
                                                                        ControlToValidate="txtLevelDescription" Display="None" ErrorMessage="Maximum character length is 1000 for level description"
                                                                        SetFocusOnError="True" ValidationGroup="LevelEntryUpdate"></asp:CustomValidator></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 10px">
                                                                </td>
                                                                <td style="vertical-align: top; text-align: left">
                                                                </td>
                                                                <td style="width: 5px">
                                                                </td>
                                                                <td style="vertical-align: top; text-align: left">
                                                        <asp:Button ID="btn_AddLevel" runat="server" Text="Add" Width="50px" OnClick="btn_AddLevel_Click" ValidationGroup="LevelEntryUpdate" />
                                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" ValidationGroup="LevelEntryUpdate" />
                                                                    <asp:Button ID="btn_delete" runat="server" Text="Delete" OnClick="btn_delete_Click"/></td>
                                                                <td style="vertical-align: top; text-align: left">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left" valign="top">
                                                    </td>
                                                    <td style="width: 5px">
                                                    </td>
                                                    <td style="width: 5px">
                                                    </td>
                                                    <td style="text-align: left" valign="top">
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
            </td>
        </tr>
    </table>
  
</center>